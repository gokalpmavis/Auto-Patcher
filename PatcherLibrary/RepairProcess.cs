using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlClient;

namespace PatchLibrary
{
    public class RepairProcess
    {
        FileManager _fm;

        public static Semaphore SemaphoreTemp = new Semaphore(0, 1);
        private readonly string _ignoreListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Craft Wars", "ignorelist.txt");
        List<string> _includedExtentions = new List<string>();
        List<string> _ignoredFolders = new List<string>();
        List<string> _ignoredExtentions = new List<string>();
        List<string> _ignoredFiles = new List<string>();
        string _sourceFolder;
        string _targetFolder;
        string _version;
        private Repository repositor1 = new Repository();
        
        internal string GetZipSourceFolder()
        {
            return _sourceFolder;
        }

        internal string GetZipTargetFolder()
        {
            return _targetFolder;
        }

        internal string GetUnzipTargetFolder()
        {
            return "";
            //return unzipFolder.Text;
        }


        public void ProcessFolders(List<string> includedExtentions, List<string> ignoredFolders, List<string> ignoredExtentions, List<string> ignoredFiles, string source,string target,string version,int compressionLevel)
        {

            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            _sourceFolder = source;
            _targetFolder = target;
            this._version = version;
            _fm = new FileManager(includedExtentions,ignoredFolders, ignoredExtentions, ignoredFiles, _sourceFolder,compressionLevel);
            if (System.IO.File.Exists(_targetFolder +@"\"+ version + "_content.txt"))
            {
                System.IO.File.Delete(_targetFolder +@"\"+ version + "_content.txt");
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ClearAll();
            int flag = 0;
            Console.WriteLine(flag);
            _fm.CompressFolder(_sourceFolder, _targetFolder,flag);
            repositor1.PathSource = _sourceFolder;
            repositor1.PathDestiny = _targetFolder;
            repositor1.RepositoryContent(_sourceFolder, 0,version);
            sw.Stop();
           /* if (File.Exists(targetFolderText.Text + @"\content.txt"))
            {
                File.Delete(targetFolderText.Text + @"\content.txt");
            }*/
           
            Console.WriteLine("Elapsed : {0}", sw.Elapsed);
            SemaphoreTemp.Release();
        }

        private void ClearAll()
        {
            _fm.Clear();
        }
         public static string GetSizeStr(long originalSize, out string marker)
        {
            string size;
            if (originalSize < 1024) // smaller than 1 kb
            {
                size = ((float)originalSize).ToString(); marker = "bytes";
            }
            else if (originalSize < 1024 * 1024) // smaller than 1 mb
            {
                size = ((float)originalSize / 1024).ToString("0.00"); marker = "kb";
            }
            else if (originalSize < 1024 * 1024 * 1024) // // smaller than 1 gb
            {
                size = ((float)originalSize / (1024 * 1024)).ToString("0.00"); marker = "mb";
            }
            else
            {
                size = ((float)originalSize / (1024 * 1024 * 1024)).ToString("0.00"); marker = "gb";
            }
            return size;
        }

    }
}
