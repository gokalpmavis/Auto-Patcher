using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using Patcher.Zipper;
using System.Data.SqlClient;
using PatchLibrary;
namespace Patcher.Zipper
{
    public partial class RepairForm : Form
    {
        public static Semaphore semaphore = new Semaphore(0, 1);
        string _version;
        public int testFlag = 0;
        readonly string _ignoreListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Craft Wars", "ignorelist.txt");
        int _compressionLevel;
        public RepairForm()
        {
            Form1_Load(new object(), new EventArgs()); 
            InitializeComponent();
            levelComboBox.SelectedIndex = 1;
        }
        private void sourceChooserBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                sourceFolderText.Text = folderBrowserDialog.SelectedPath;
            }
        }


       

        private void targetChooserBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                targetFolderText.Text = folderBrowserDialog.SelectedPath;
            }
        }

        public void publishBtn_Click(object sender, EventArgs e)
        {
            SaveIgnoreList();

            Thread runner = new Thread(ProcessFolders);

            runner.Start();
            PatchLibrary.RepairProcess.SemaphoreTemp.WaitOne();
            semaphore.Release();

        }
        public void LoadIgnoreLists()
        {
            try
            {
                if (System.IO.File.Exists(_ignoreListFile))
                {
                    string[] lines = System.IO.File.ReadAllLines(_ignoreListFile);
                    for (int pointer = 0; pointer < lines.Length; pointer++)
                    {
                        string[] tokens = lines[pointer].Split(">".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (tokens.Length < 2)
                        {
                            continue;
                        }
                        if (tokens[0].Equals("ignored extentions", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] extentions = tokens[1].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (string extention in extentions)
                            {
                                this.ignoredExtentions.Items.Add((string)extention);
                            }
                        }
                        else if (tokens[0].Equals("ignored files", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] files = tokens[1].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (string file in files)
                            {
                                this.ignoredFiles.Items.Add((string)file);
                            }
                        }
                        else if (tokens[0].Equals("ignored folders", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] folders = tokens[1].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (string folder in folders)
                            {
                                this.ignoredFolders.Items.Add((string)folder);
                            }
                        }
                        else if (tokens[0].Equals("included extentions", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] folders = tokens[1].Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (string folder in folders)
                            {
                                this.includedExtentions.Items.Add((string)folder);
                            }
                        }
                        else if (tokens[0].Equals("source folder", StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.sourceFolderText.Text = tokens[1];
                        }
                        else if (tokens[0].Equals("target folder", StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.targetFolderText.Text = tokens[1];
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ProcessFolders()
        {
            LoadIgnoreLists();
            _version = textBox4.Text;
            List<String> ignoredextentions = new List<string>();
            foreach (string extention in ignoredExtentions.Items)
            {
                ignoredextentions.Add(extention);
            }

            List<String> includedextentions = new List<string>();
            foreach (string extention in includedExtentions.Items )
            {
                includedextentions.Add(extention);
            }

            List<String> ignoredfiles = new List<string>();
            foreach (string file in ignoredFiles.Items)
            {
                ignoredfiles.Add(file);
            }

            List<String> ignoredfolders  = new List<string>();
            foreach (string folder in ignoredFolders.Items)
            {
                ignoredfolders.Add(folder);
            }
            _repairProcess.ProcessFolders( includedextentions, ignoredfolders,  ignoredextentions,  ignoredfiles, sourceFolderText.Text,targetFolderText.Text,_version,_compressionLevel);
        }
        PatchLibrary.RepairProcess _repairProcess;

        public void Form1_Load(object sender, EventArgs e)
        {
            _repairProcess = new RepairProcess();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //unzipFolders();
        }
        public void SaveIgnoreList()
        {
            try
            {
                using (StreamWriter writer = System.IO.File.CreateText(_ignoreListFile))
                {
                    writer.Write("ignored extentions>");
                    foreach (string extention in ignoredExtentions.Items)
                    {
                        writer.Write(extention + ";");
                    }
                    writer.WriteLine("");
                    writer.Write("ignored files>");
                    foreach (string file in ignoredFiles.Items)
                    {
                        writer.Write(file + ";");
                    }
                    writer.WriteLine("");
                    writer.Write("ignored folders>");
                    foreach (string file in ignoredFolders.Items)
                    {
                        writer.Write(file + ";");
                    }
                    writer.WriteLine("");
                    writer.Write("included extentions>");
                    foreach (string extention in includedExtentions.Items)
                    {
                        writer.Write(extention + ";");
                    }
                    writer.WriteLine("");
                    writer.WriteLine("source folder>" + sourceFolderText.Text);
                    writer.WriteLine("target folder>" + targetFolderText.Text);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void levelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _compressionLevel = Convert.ToInt32(levelComboBox.SelectedItem.ToString());
        }

        private void addNewIgnoredExtention_Click(object sender, EventArgs e)
        {
            if (newIgnoredExtention.Text != "")
            {
                string newExtention = "";
                if (!newIgnoredExtention.Text.StartsWith("."))
                {
                    newExtention += ".";
                }
                newExtention += newIgnoredExtention.Text;
                if (!ignoredExtentions.Items.Contains(newExtention))
                {
                    ignoredExtentions.Items.Add((string)newExtention);
                    SaveIgnoreList();
                }
                newIgnoredExtention.Text = "";
            }
        }

        private void addNewIgnoredFile_Click(object sender, EventArgs e)
        {
            if (newIgnoredFile.Text != "" && !ignoredFiles.Items.Contains(newIgnoredFile.Text))
            {
                ignoredFiles.Items.Add((string)newIgnoredFile.Text);
                SaveIgnoreList();
            }
            newIgnoredFile.Text = "";
        }

        private void addNewIgnoredFolderbtn_Click(object sender, EventArgs e)
        {
            
            string folder = newIgnoredFolder.Text;
            if (folder != "")
            {
                if (!ignoredFolders.Items.Contains(folder))
                {
                    ignoredFolders.Items.Add((string)folder);
                    SaveIgnoreList();
                }
            }
            newIgnoredFolder.Text = "";
        }

        private void addNewIncludedExtention_Click(object sender, EventArgs e)
        {
            if (newIncludedExtention.Text != "" && !includedExtentions.Items.Contains(newIncludedExtention.Text))
            {
                string newExtention = "";
                if (!newIncludedExtention.Text.StartsWith("."))
                {
                    newExtention += ".";
                }
                newExtention += newIncludedExtention.Text;
                if (!includedExtentions.Items.Contains(newExtention))
                {
                    includedExtentions.Items.Add((string)newExtention);
                    SaveIgnoreList();
                }
            }
        }

        private void deleteExtentionbtn_Click(object sender, EventArgs e)
        {
            if (ignoredExtentions.Items.Count > 0)
            {
                int selectedIndex = ignoredExtentions.SelectedIndex;
                ignoredExtentions.Items.Remove(ignoredExtentions.SelectedItem);
                SaveIgnoreList();
                if (ignoredExtentions.Items.Count > selectedIndex)
                {
                    ignoredExtentions.SelectedIndex = selectedIndex;
                }
                else
                {
                    ignoredExtentions.SelectedIndex = selectedIndex-1;
                }
            }
        }

        private void deleteIgnoredFilebtn_Click(object sender, EventArgs e)
        {
            if (ignoredFiles.Items.Count > 0)
            {
                int selectedIndex = ignoredFiles.SelectedIndex;
                ignoredFiles.Items.Remove(ignoredFiles.SelectedItem);
                SaveIgnoreList();
                if (ignoredFiles.Items.Count > selectedIndex)
                {
                    ignoredFiles.SelectedIndex = selectedIndex;
                }
                else
                {
                    ignoredFiles.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        private void deleteIgnoredFolderbtn_Click(object sender, EventArgs e)
        {
            if (ignoredFolders.Items.Count > 0)
            {
                int selectedIndex = ignoredFolders.SelectedIndex;
                ignoredFolders.Items.Remove(ignoredFolders.SelectedItem);
                SaveIgnoreList();
                if (ignoredFolders.Items.Count > selectedIndex)
                {
                    ignoredFolders.SelectedIndex = selectedIndex;
                }
                else
                {
                    ignoredFolders.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        private void deleteIncludedExtention_Click(object sender, EventArgs e)
        {
            if (includedExtentions.Items.Count > 0)
            {
                int selectedIndex = includedExtentions.SelectedIndex;
                includedExtentions.Items.Remove(includedExtentions.SelectedItem);
                SaveIgnoreList();
                if (includedExtentions.Items.Count > selectedIndex)
                {
                    includedExtentions.SelectedIndex = selectedIndex;
                }
                else
                {
                    includedExtentions.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        
        

        private void sourceFolderText_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void targetFolderText_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
