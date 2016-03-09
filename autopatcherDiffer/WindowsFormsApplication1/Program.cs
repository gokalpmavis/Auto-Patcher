using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;

namespace Patcher.Diff
{
    static class Program
    {
      

        [STAThread]
        static void Main(string[] args)
        {
            /*using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Instance already running");
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(args));
            }*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DiffForm());
        }

        
    }
}
