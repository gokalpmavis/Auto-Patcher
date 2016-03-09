using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.IO.Compression;
using Patcher.Client.Properties;

namespace Patcher.Client
{

    public partial class PatcherForm : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        ///
        /// Handling the window messages
        /////
        /// 
        public int TestFlag=0;
        private static readonly Object locker = new Object();
        public static Semaphore semaphore = new Semaphore(0, 1);
        public static string version;
        private RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
         
       public PatcherForm()
        {
            //version=Settings.Default.Version;
            Form1_Load(new object(), new EventArgs()); 
            InitializeComponent();
            Console.WriteLine(version);
           
        }
       public void SemamphoreWaiter()
       {
           Patcher.Client.RepositoryManager.semaphorePatcher.WaitOne();
           semaphore.Release();
           /*Settings.Default.Version = Patcher.RepositoryManager.LastVersion;
           Settings.Default.Save();*/
       }


       public void button1_Click(object sender, EventArgs e)
       {
           try
           {
               progressBar2.Minimum = 0;
               progressBar2.Maximum = 10000000;
               Thread runner = new Thread(ProcessFolders);
               runner.Start();
               Thread runnerWait = new Thread(SemamphoreWaiter);
               runnerWait.Start();
               
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message + " : " + ex.StackTrace);
           }

       }


        private void ProcessFolders()
        {
            if (progressBar2.InvokeRequired)
            {
                if (label2.InvokeRequired)
                {
                    label2.Invoke(new MethodInvoker(delegate { label2.Text = "checking current version..."; }));
                    progressBar2.Invoke(new MethodInvoker(delegate { progressBar2.Style = ProgressBarStyle.Marquee; }));
                }
                progressBar2.Invoke(new MethodInvoker(delegate { progressBar2.Visible = true; }));
            }
            Console.WriteLine(Application.StartupPath);
            if (TestFlag == 0)
            {
                if (System.IO.File.Exists(Path.Combine(Application.StartupPath, "Version.txt")))
                {
                    FileInfo fileInfo = new FileInfo(Path.Combine(Application.StartupPath, "Version.txt"));
                    if (fileInfo.Length == 0)
                        version = "unknown";
                    else
                    {
                        StreamReader file = new StreamReader(Path.Combine(Application.StartupPath, "Version.txt"));
                        version = file.ReadLine();
                    }
                }
                else
                {
                    version = "unknown";
                }
            }

            _repositoryManager.ProcessFolders(this.textBox1.Text, version, TestFlag);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        RepositoryManager _repositoryManager;

        public void Form1_Load(object sender, EventArgs e)
        {
            _repositoryManager = new RepositoryManager();

            _repositoryManager.ProgressChanged += _repositoryManager_ProgressChanged;
        }

        void _repositoryManager_ProgressChanged(int value)
        {
            if (progressBar2.InvokeRequired)
            {
                if (progressBar2.Style == ProgressBarStyle.Marquee)
                {
                    
                    progressBar2.Invoke(new MethodInvoker(delegate { progressBar2.Style = ProgressBarStyle.Blocks; }));
                    if (label2.InvokeRequired)
                   
                        label2.Invoke(new MethodInvoker(delegate { label2.Text = "updating version..."; }));
                    
                }
                
            }
           if (progressBar2.InvokeRequired)
           {

               if (value == 10000000)
                    progressBar2.Invoke(new MethodInvoker(delegate { progressBar2.Value = value; }));
               else
                    progressBar2.Invoke(new MethodInvoker(delegate { progressBar2.Value += value; }));
               if (label3.InvokeRequired)
               {
                   int perc = (int)((100 * progressBar2.Value) / 10000000);
                   label3.Invoke(new MethodInvoker(delegate { label3.Text = "%" + (perc.ToString()); }));
               }
           }
           
        }
    }
}
