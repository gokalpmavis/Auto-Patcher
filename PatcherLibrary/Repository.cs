using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace PatchLibrary
{
    class Repository
    {
        public String PathSource,PathDestiny;
        private Hash _hash = new Hash();
      
        
        public void RepositoryContent(String path, int level,string version)
        {
            string[] files = Directory.GetFiles(path);
            string[] folders = Directory.GetDirectories(path);
            string tab = "";
            int indent = 0;
            for (int pointer = 0; pointer < level; pointer++)
            {
                tab = tab + "\t";
                indent++;
            }
           

            Task t = Task.Factory.StartNew(() =>
            {
                files = Directory.GetFiles(path);
                foreach (string filek in files)
                {

                    System.IO.File.AppendAllText(Path.Combine(PathDestiny, version + "_content.txt"),
                       filek.Replace(PathSource, ""));
                    string hash1;
                    hash1 = _hash.HashFile(filek);

                    hash1 = "\t" + hash1;
                    System.IO.File.AppendAllText(Path.Combine(PathDestiny, version + "_content.txt"),
                        hash1 + Environment.NewLine);


                }
                 foreach (string folder in folders)
                {

                    System.IO.File.AppendAllText(Path.Combine(PathDestiny, version + "_content.txt"),
                      "folder" + "\t" + folder.Replace(PathSource, "") + Environment.NewLine);

                    RepositoryContent(folder, level + 1,version);                  
                }                
                
            });
            t.Wait();
        }
    }
}
