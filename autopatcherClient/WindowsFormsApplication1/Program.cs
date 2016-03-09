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
using System.Net;
using System.Security.Cryptography;

namespace Patcher.Client
{
    static class Program
    {
      

        [STAThread]
        static void Main(string[] args)
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PatcherForm());
        }

        
    }
}
