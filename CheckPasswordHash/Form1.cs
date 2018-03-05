using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CheckPasswordHash
{
    public partial class Form1 : Form
    {
        Dictionary<string, hashCount> userHashes = new Dictionary<string, hashCount>();
        List<string> filePaths = new List<string>();       
        SHA1 hashFunction = new SHA1Managed();
        bool searchWeb = false;
        int TimeoutAPI = 1500;  //API calls are limited to 1.5s
        const int HASHLENGTH = 40;  //SHA1 hash length

        /// <summary>
        /// Information on each hash the user wants to check
        /// </summary>
        struct hashCount
        {
            public bool hashFound;
            public int count;
            public string hint;
        }

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load files to check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFile_Btn_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = openFileDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                if (!filePaths.Contains(openFileDialog1.FileName))
                {
                    filePaths.Add(openFileDialog1.FileName);
                }
            }
        }

        /// <summary>
        /// Will check the users list of hashes againsts a file or using the Web API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkHash_Btn_Click(object sender, EventArgs e)
        {
            if(searchWeb)
            {
                MessageBox.Show("Due to API rate limits (1.5s) checking a large number of hashes may take a while.");
            }

            Tuple<bool, int> returnTup = new Tuple<bool, int>(false, 0);
            Dictionary<string, hashCount> updatedHashes = new Dictionary<string, hashCount>();

            foreach (string hash in userHashes.Keys)
            {                
                if(searchWeb)
                {                    
                    returnTup = searchWebAPI(hash);
                    Thread.Sleep(TimeoutAPI);
                }
                else
                {
                    if (filePaths.Count > 0)
                    {
                        foreach (string path in filePaths)
                        {
                            try
                            {
                                returnTup = Check(hash, path);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error reading file, does it contain non SHA1 hash values? \n" + ex.Message);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select some files");
                    }
                }

                hashCount hc = userHashes[hash];
                hc.hashFound = returnTup.Item1;
                hc.count = returnTup.Item2;
                updatedHashes.Add(hash, hc);
            }
            userHashes.Clear();
            userHashes = updatedHashes;
            updatePasswordAndHint();
        }

        /// <summary>
        /// Checks if hash exists in data breaches and the number of times it was used by checking the Web API
        /// </summary>
        /// <param name="hashToFind"></param>
        /// <returns></returns>
        private Tuple<bool, int> searchWebAPI(string hashToFind)
        {
            bool hashFound = false;
            string searchString = @"https://api.pwnedpasswords.com/pwnedpassword/" + hashToFind.ToLower();
            int hashCount = 0;
            
            using (var webClient = new WebClient())
            {
                try
                {
                    WebRequest request = WebRequest.Create(searchString);
                    WebResponse response = request.GetResponse();

                    Stream dataStream = response.GetResponseStream();

                    StreamReader reader = new StreamReader(dataStream);

                    string s  = reader.ReadToEnd();

                    Int32.TryParse(s, out hashCount);
                                        
                    hashFound = true;
                }
                catch(Exception ex)
                {
                    hashFound = false;                    
                }
            }

            return Tuple.Create(hashFound, hashCount);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Adds a hash of the currently entered password and a hint to the list of passwords to check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addHash_Btn_Click(object sender, EventArgs e)
        {
            if (password_TB.Text != "")
            {
                if (passwordRetype_TB.Text == password_TB.Text)
                {
                    string passwd = password_TB.Text;
                    byte[] passwordArray = Encoding.UTF8.GetBytes(passwd);
                    byte[] hashArray = hashFunction.ComputeHash(passwordArray);

                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (byte b in hashArray)
                    {
                        stringBuilder.AppendFormat("{0:X2}", b);
                    }
                    if (!userHashes.ContainsKey(stringBuilder.ToString()))
                    {
                        hashCount hc = new hashCount();
                        hc.count = 0;
                        hc.hashFound = false;
                        hc.hint = hint_TB.Text;
                        userHashes.Add(stringBuilder.ToString(), hc);
                    }
                    password_TB.Text = "";
                    passwordRetype_TB.Text = "";
                    hint_TB.Text = "";
                }
                else
                {
                    MessageBox.Show("Passwords do not match");
                }
            }
            else
            {
                MessageBox.Show("Please enter password");
            }
            updatePasswordAndHint();
        }

        /// <summary>
        /// Updates the display with users hash list and if they have been found
        /// </summary>
        private void updatePasswordAndHint()
        {
            passwordAndHints_LB.Items.Clear();
            foreach (KeyValuePair<string, hashCount> kvp in userHashes)
            {
                hashCount hc = kvp.Value;
                string s = "";
                if(hc.hashFound)
                {
                    s = "found " + hc.count.ToString() + " times";
                }
                else
                {
                    s = "not found";
                }
                passwordAndHints_LB.Items.Add(kvp.Key + " - " + hc.hint + " - " + s);
            }
        }        

        /// Checks if hash exists in data breaches and the number of times it was used by check a file of hashes
        static Tuple<bool, int> Check(string asHex, string filename)
        {
            bool hashFound = false;
            int hasCount = 0;

            using (var fs = File.OpenRead(filename))
            {
                var low = 0L;
                // We don't need to start at the very end
                var high = fs.Length - (HASHLENGTH - 1); // EOF - 1 HASHLENGTH

                StreamReader sr = new StreamReader(fs);

                while (low <= high)
                {
                    var middle = (low + high + 1) / 2;
                    fs.Seek(middle, SeekOrigin.Begin);

                    // Resync with base stream after seek
                    sr.DiscardBufferedData();

                    var readLine = sr.ReadLine();

                    // 1) If we are NOT at the beginning of the file, we may have only read a partial line so
                    //    Read again to make sure we get a full line.
                    // 2) No sense reading again if we are at the EOF
                    if ((middle > 0) && (!sr.EndOfStream)) readLine = sr.ReadLine() ?? "";

                    string[] parts = readLine.Split(':');
                    string hash = parts[0];

                    // By default string compare does a culture-sensitive comparison we may not be what we want?
                    // Do an ordinal compare (0-9 < A-Z < a-z)
                    int compare = String.Compare(asHex, hash, StringComparison.Ordinal);

                    if (compare < 0)
                    {
                        high = middle - 1;
                    }
                    else if (compare > 0)
                    {
                        low = middle + 1;
                    }
                    else
                    {
                        if(parts.Length > 1)
                        {
                            if(Int32.TryParse(parts[1], out hasCount))
                            {

                            }
                        }
                        hashFound = true;
                        break;
                    }
                }
            }
            
            return Tuple.Create(hashFound, hasCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchWeb_CB_CheckedChanged(object sender, EventArgs e)
        {
            searchWeb = searchWeb_CB.Checked;
        }
        /// <summary>
        /// Loads issues page on GitHub
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bugReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/MikeS159/CheckPasswordHash/issues");
        }
        /// <summary>
        /// Loads readme page on GitHub
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void readMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/MikeS159/CheckPasswordHash/blob/master/README.md");
        }

        /// <summary>
        /// Displays program information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Assembly assem = Assembly.GetEntryAssembly();
            string programName = FileVersionInfo.GetVersionInfo(assem.Location).ProductName;
            string companyName = FileVersionInfo.GetVersionInfo(assem.Location).CompanyName;
            string copyright = FileVersionInfo.GetVersionInfo(assem.Location).LegalCopyright;            
            string assemVer = FileVersionInfo.GetVersionInfo(assem.Location).FileVersion;
            string infoVer = FileVersionInfo.GetVersionInfo(assem.Location).ProductVersion;
            string s =
            programName + " by " +
            companyName + "\n" +
            copyright + "\n\n" +
            "Version: " + infoVer + " (" + assemVer + ")";
            MessageBox.Show(s);
        }

        /// <summary>
        /// Loads the HaveIBeenPwned passwords webpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iNeedHashFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://haveibeenpwned.com/Passwords");
        }
    }
}
