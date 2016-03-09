using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Patcher.Diff;
using System.Diagnostics;
using Microsoft.Win32;
using System.Deployment.Application;
using System.Net;
using System.Threading.Tasks;
using System.Data.Entity;
using PatchLibrary;
namespace Patcher.Diff
{

    public partial class DiffForm : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        ///
        /// Handling the window messages
        ///
        private static readonly Object locker = new Object();
        private RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static Semaphore semaphore = new Semaphore(0, 1);
        public DiffForm()
        {

            Form1_Load(new object(), new EventArgs()); 
            InitializeComponent();
            /*
             this.args = args;
             this.Load += new EventHandler(OnPageLoad);
             this.Shown += new EventHandler(OnShown);
             logStamp("Patcher started");
             autoUpdateTimer = new System.Windows.Forms.Timer();
             autoUpdateTimer.Tick += new EventHandler(AutoUpdateTick);
             autoUpdateTimer.Interval = 2 * 60 * 60 * 1000; // 2 hours
             autoUpdateTimer.Start();*/
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        public void button1_Click(object sender, EventArgs e)
        {
            /*
            this.textBox1.Text = @"C:\Users\gokalp.mavis\Desktop\dota 2 beta";
            this.textBox2.Text = @"C:\Users\gokalp.mavis\Desktop\dota2beta_old";
            this.textBox3.Text = @"C:\Users\gokalp.mavis\Desktop\share";*/
            try
            {
                Thread runner = new Thread(ProcessFolders);
                runner.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + ex.StackTrace);
            }





        }

        private void ProcessFolders()
        {

            Console.WriteLine(versionNew + versionOld);
            _repositoryManager.ProcessFolders(this.newVersionPath.Text, this.oldVersionPath.Text, this.destinyPath.Text,this.oldVersion.Text, this.newVersion.Text);
            PatchLibrary.RepositoryManager.TempSemaphore.WaitOne();
            semaphore.Release();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.newVersionPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        
        

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.oldVersionPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        

        RepositoryManager _repositoryManager;

        public void Form1_Load(object sender, EventArgs e)
        {

            _repositoryManager = new RepositoryManager();
                   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.destinyPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        
    }
}
