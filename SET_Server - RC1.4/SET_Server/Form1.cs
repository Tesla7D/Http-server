/*
 *
 *	FILE		:	Form1.cs
 *	PROJECT		:	PROG2120 - Assignment #6
 *	PROGRAMMERS	:	Denys Solomonov     Ali Rohaili     Grigoriy Kozyrev
 *	STUDENT #s	:	6849806             6300321         6850549
 *	FIRST VER.	:	14 November 2014
 *	DESCRIPTION	:	SET Server Pages - Form creation and logic
 * 	
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SET_Server
{
    public partial class MainForm : Form
    {
        Server server;
        public MainForm()
        {
            InitializeComponent();

            DirectoryTextbox.Text = "C:\\";
            UpdateFileList();
            DefaultFileBox.SelectedIndex = DefaultFileBox.FindStringExact("index.html");
            PortBox.Value = 1337;
            NumOfClients.Value = 10;

            loadConfig();
            btnStop.Enabled = false;
            server = null;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.Stop() == true)
            {
                btnStop.Enabled = false;
                btnStart.Enabled = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = IPAddress.Parse("0.0.0.0");

            if (DirectoryTextbox.Text != "" && DefaultFileBox.SelectedIndex != -1)
            {
                server = new Server(DirectoryTextbox.Text, DefaultFileBox.SelectedItem.ToString());
                if (server.Start(ipAddress, Convert.ToInt32(PortBox.Value), Convert.ToInt32(NumOfClients.Value)) == true)
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
            }
        }

        private void loadConfig()
        {
            string[] dataLines = { "" };
            int counter = 0;
            //try case
            try
            {
                //reads all data and loads to a string array
                dataLines = System.IO.File.ReadAllLines("config.ini");
            }
            //if not then a error is logged
            catch (Exception)
            {
                MessageBox.Show("Error Openning config file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            for (int i = 0; i < dataLines.Length; i++)
            {
                if (dataLines[i].Contains("=") == true)
                {
                    string[] parse = dataLines[i].Split('=');
                    if (counter == 0)
                    {
                        if (Directory.Exists(parse[1]) == true)
                        {
                            this.DirectoryTextbox.Text = parse[1];
                            if (DirectoryTextbox.Text[DirectoryTextbox.Text.Length - 1] != '\\')
                            {
                                DirectoryTextbox.Text += '\\';
                            }
                            UpdateFileList();
                        }
                    }
                    else if (counter == 1)
                    {
                        if (File.Exists(DirectoryTextbox.Text + parse[1]) == true)
                        {         
                            DefaultFileBox.SelectedIndex = DefaultFileBox.FindStringExact(parse[1]);
                        }
                    }
                    else if (counter == 2)
                    {
                        decimal number = 0;
                        if (Decimal.TryParse(parse[1], out number) == true)
                        {
                            this.PortBox.Value = number;
                        }
                    }
                    else if (counter == 3)
                    {
                        decimal number = 0;
                        if (Decimal.TryParse(parse[1], out number) == true)
                        {
                            this.NumOfClients.Value = number;
                        }
                    }
                    counter++;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }
    
        private void UpdateFileList()
        {
            if (Directory.Exists(DirectoryTextbox.Text))
            {
                DefaultFileBox.Items.Clear();

                DirectoryInfo di = new DirectoryInfo(DirectoryTextbox.Text);
                FileInfo[] files = di.GetFiles("*.*");

                foreach (FileInfo file in files)
                {
                    if (Server.extensions.ContainsKey(file.Extension))
                    {
                        DefaultFileBox.Items.Add(file.Name);
                    }
                }
            }
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog directoryPicker = new FolderBrowserDialog();
            directoryPicker.ShowDialog();

            if (Directory.Exists(directoryPicker.SelectedPath))
            {
                DirectoryTextbox.Text = directoryPicker.SelectedPath;
                UpdateFileList();
            }
        }
    }
}
