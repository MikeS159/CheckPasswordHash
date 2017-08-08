﻿using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CheckPasswordHash
{
    public partial class Form1 : Form
    {
        List<string> hashesFromFile1 = new List<string>();
        List<string> hashesFromFile2 = new List<string>();
        Dictionary<string,string> hashesToSearch = new Dictionary<string, string>();
        StreamReader sr;
        SHA1 hashFunction = new SHA1Managed();
        string passwd;
        byte[] passwordArray;
        byte[] hashArray;
        //string hashToSearch;
        string filePath;
        bool threadSwitch = false;
        bool doneReading = false;
        bool stringFound = false;
        ulong lineRead = 0;
        ulong linesAtATime = 1000000;

        public Form1()
        {
            InitializeComponent();
        }

        private void readChunk()
        {
            if (!doneReading)
            {
                if (threadSwitch)
                {
                    hashesFromFile1.Clear();
                    for (ulong i = 0; i < linesAtATime; i++)
                    {
                        if (sr.EndOfStream)
                        {
                            doneReading = true;
                            break;
                        }
                        else
                        {
                            string s = sr.ReadLine();
                            hashesFromFile1.Add(s);
                            lineRead++;
                        }
                    }
                }
                else
                {
                    hashesFromFile2.Clear();
                    for (ulong i = 0; i < linesAtATime; i++)
                    {
                        if (sr.EndOfStream)
                        {
                            doneReading = true;
                            break;
                        }
                        else
                        {
                            string s = sr.ReadLine();
                            hashesFromFile2.Add(s);
                            lineRead++;
                        }
                    }
                }
            }
        }

        private void searchChunk()
        {
            if (!threadSwitch)
            {
                for (int i = 0; i < hashesToSearch.Keys.Count; i++)
                {
                    if (hashesFromFile1.Contains(hashesToSearch.Keys.ElementAt(i)))
                    {
                        hashesToSearch[hashesToSearch.Keys.ElementAt(i)] = hashesToSearch[hashesToSearch.Keys.ElementAt(i)] + " - Found";
                        //stringFound = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < hashesToSearch.Keys.Count; i++)
                {
                    if (hashesFromFile2.Contains(hashesToSearch.Keys.ElementAt(i)))
                    {
                        hashesToSearch[hashesToSearch.Keys.ElementAt(i)] = hashesToSearch[hashesToSearch.Keys.ElementAt(i)] + " - Found";
                        //stringFound = true;
                    }
                }
            }
        }

        private void masterSpawn()
        {
            doneReading = false;
            stringFound = false;
            sr = new StreamReader(filePath);
            label2.Text = lineRead.ToString();

            Thread t = new Thread(readChunk);
            t.Start();
            t.Join();

            while ((!doneReading) && (!stringFound))
            {
                label2.Text = lineRead.ToString("N0");
                Application.DoEvents();
                threadSwitch = !threadSwitch;
                Thread t1 = new Thread(readChunk);
                Thread t2 = new Thread(searchChunk);
                t1.Start();
                t2.Start();
                t1.Join();
                t2.Join();
            }
            threadSwitch = !threadSwitch;
            t = new Thread(searchChunk);
            sr.Close();
            lineRead = 0;
            hashesFromFile1.Clear();
            hashesFromFile2.Clear();
            passwordAndHints_LB.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in hashesToSearch)
            {
                //passwordAndHints_RTB.Text = passwordAndHints_RTB.Text + kvp.Key + " - " + kvp.Value + "\n";
                passwordAndHints_LB.Items.Add(kvp.Key + " - " + kvp.Value + "\n");
            }
            if (stringFound)
            {
                MessageBox.Show("Password found - change it where ever you used it and never use it again");
            }
            else
            {
                MessageBox.Show("Password not found- you are ok");
            }
        }

        private void openFile_Btn_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = openFileDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                sr = new StreamReader(filePath);
            }
        }

        private void checkHash_Btn_Click(object sender, EventArgs e)
        {
            checkFreeMemory();
            masterSpawn();
        }

        private void checkFreeMemory()
        {
            ComputerInfo CI = new ComputerInfo();
            ulong mem = ulong.Parse(CI.AvailablePhysicalMemory.ToString());
            linesAtATime = mem / (40 * 4 * 2 * 2);
            //40 characters per hash * 4 bytes per char (worst case for UTF8 * 2 arrays at once * 2 so only 50% memory is used
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string url = @"https://haveibeenpwned.com/api/v2/pwnedpassword/ce0b2b771f7d468c0141918daea704e0e5ad45db";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();            
        }

        private void addHash_Btn_Click(object sender, EventArgs e)
        {
            if (password_TB.Text != "")
            {
                if (passwordRetype_TB.Text == password_TB.Text)
                {
                    passwd = password_TB.Text;
                    passwordArray = Encoding.UTF8.GetBytes(passwd);
                    hashArray = hashFunction.ComputeHash(passwordArray);

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
            passwordAndHints_LB.Items.Clear();
            foreach (KeyValuePair<string,string> kvp in hashesToSearch)
            {
                passwordAndHints_LB.Items.Add(kvp.Key + " - " + kvp.Value + "\n");
            }
        }
    }
}