using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Patcher.Client
{
    class DiffExtender
    {
        String quote = '"'.ToString();
        public void applyOnePatch(string sourceFile, string vcdiffFile, string outputFile,int testFlag)
        {
            Process xdelta = new Process();
            xdelta.StartInfo.CreateNoWindow = true;
            if (testFlag==0) xdelta.StartInfo.FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ @"\xdelta3.x86_64.exe";
            else xdelta.StartInfo.FileName =@"E:\Develop\AutoPatcher2\test\bin\x64\Debug\xdelta3.x86_64.exe";
           // else xdelta.StartInfo.FileName = "xdelta3"; // Works with xdelta3.exe and xdelta3 package, doesn't work with ./xdelta3
            xdelta.StartInfo.UseShellExecute = false;
            xdelta.StartInfo.RedirectStandardOutput = xdelta.StartInfo.RedirectStandardError = false;

            string paramenters =  "-d -f -s %source% %vcdiff% %output%";
            xdelta.StartInfo.Arguments = paramenters.Replace("%source%", quote + sourceFile + quote).Replace("%vcdiff%", quote + vcdiffFile + quote).Replace("%output%", quote + outputFile + quote);

            xdelta.Start();
            xdelta.WaitForExit();
        }
    }
}
