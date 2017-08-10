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
        Dictionary<string,string> hashesToSearch = new Dictionary<string, string>();
        List<string> filePaths = new List<string>();       
        SHA1 hashFunction = new SHA1Managed();
        bool searchWeb = false;
        int TimeoutAPI = 1500;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFile_Btn_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = openFileDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                filePaths.Add(openFileDialog1.FileName);                
            }
        }

        private void checkHash_Btn_Click(object sender, EventArgs e)
        {
            List<string> foundHashes = new List<string>();

            if (searchWeb)
            {
                MessageBox.Show("Due to API rate limits (1.5s) checking a large number of hashes may take a while.");
                foreach (string hash in hashesToSearch.Keys)
                {
                    if (searchWebAPI(hash))
                    {
                        foundHashes.Add(hash);
                    }
                    Thread.Sleep(TimeoutAPI);
                }
            }
            else
            {                
                if (filePaths.Count > 0)
                {
                    foreach (string path in filePaths)
                    {
                        foreach (string hash in hashesToSearch.Keys)
                        {
                            try
                            {
                                if (Check(hash, path))
                                {
                                    foundHashes.Add(hash);
                                }
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Error reading file, does it contain non SHA1 hash values? \n" + ex.Message);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select some files");
                }
            }

            foreach(string s in foundHashes)
            {
                hashesToSearch[s] = hashesToSearch[s] + " - Found!";
            }
            updatePasswordAndHint();
        }

        private bool searchWebAPI(string hashToFind)
        {
            bool hashFound = false;
            string searchString = @"https://haveibeenpwned.com/api/v2/pwnedpassword/" + hashToFind.ToLower();

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.Headers.Add("user-agent", ".NET 4.5.2");
                    var response = webClient.DownloadString(searchString);
                    hashFound = true;
                }
                catch(Exception ex)
                {
                    hashFound = false;
                    if(ex.Message.Contains("429"))  //This isn't the best way to do it but I couldn't get HttpWebResponse working (403 errors)
                    {
                        Thread.Sleep(TimeoutAPI); //Wait for rate limit
                        hashFound = searchWebAPI(hashToFind);
                    }
                }
            }

            return hashFound;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

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
                    hashesToSearch.Add(stringBuilder.ToString(), hint_TB.Text);
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

        private void updatePasswordAndHint()
        {
            passwordAndHints_LB.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in hashesToSearch)
            {
                passwordAndHints_LB.Items.Add(kvp.Key + " - " + kvp.Value + "\n");
            }
        }

        /// <summary>
        /// From https://github.com/DavidBetteridge/CheckPwnedPasswords
        /// </summary>
        /// <param name="asHex"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        static bool Check(string asHex, string filename)
        {
            const int LINELENGTH = 40;  //SHA1 hash length

            var buffer = new byte[LINELENGTH];
            using (var sr = File.OpenRead(filename))
            {
                //Number of lines
                var high = (sr.Length / (LINELENGTH + 2)) - 1;
                var low = 0L;

                while (low <= high)
                {
                    var middle = (low + high + 1) / 2;
                    sr.Seek((LINELENGTH + 2) * ((long)middle), SeekOrigin.Begin);
                    sr.Read(buffer, 0, LINELENGTH);
                    var readLine = Encoding.ASCII.GetString(buffer);

                    switch (readLine.CompareTo(asHex))
                    {
                        case 0:
                            return true;

                        case 1:
                            high = middle - 1;
                            break;

                        case -1:
                            low = middle + 1;
                            break;

                        default:
                            break;
                    }
                }

            }
            return false;
        }

        private void searchWeb_CB_CheckedChanged(object sender, EventArgs e)
        {
            searchWeb = searchWeb_CB.Checked;
        }

        private void bugReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MikeS159/CheckPasswordHash/issues");
        }

        private void readMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/MikeS159/CheckPasswordHash/blob/master/README.md");
        }

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

        private void iNeedHashFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://haveibeenpwned.com/Passwords");
        }
    }
}
