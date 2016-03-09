using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using PatchLibrary;
using System.Threading.Tasks;

namespace PatchLibrary
{
    public class RepositoryDiff
    {
        public List<Folder> FolderArray = new List<Folder>(); 
        public string PathRoot;
        private Hash _hash = new Hash();
        public RepositoryDiff(string path)
        {
            this.PathRoot = path;
        }


        public void RepositoryContent(String path, int indent)
        {

            string[] files = Directory.GetFiles(path);
            string[] folders = Directory.GetDirectories(path);

            if (indent == 0)
            {

                Folder tempFolder = new Folder();
                tempFolder.Name="current";
                tempFolder.IndentFolder = 0;
                List<File> fileArray = new List<File>();

                foreach (string eachFile in files)
                {
                    File tempFile = new File();
                    tempFile.Name = Path.GetFileName(eachFile);
                    string hashValue;
                    hashValue = _hash.HashFile(eachFile);
                    tempFile.Hash = hashValue;
                    fileArray.Add(tempFile);
                    tempFolder.Flag = 1;
                }

                tempFolder.FileList=fileArray;
                FolderArray.Add(tempFolder);
            }

            Task t = Task.Factory.StartNew(() =>
            {
                Folder temp = new Folder();

                List<File> filearray = new List<File>();
                foreach (string folder in folders)
                {
                    temp = new Folder();
                    files = Directory.GetFiles(folder);

                    temp.IndentFolder = indent + 1;
                    temp.Name=folder.Replace(PathRoot, "");

                    filearray = new List<File>();

                    foreach (string filek in files)
                    {
                        File tempFile = new File();
                        tempFile.Name=Path.GetFileName(filek);
                        string hashValue;
                        hashValue = _hash.HashFile(filek);
                        tempFile.Hash=(hashValue);
                        filearray.Add(tempFile);
                        temp.Flag = 1;

                    }

                    temp.FileList=filearray;
                    FolderArray.Add(temp);
                    RepositoryContent(folder, indent + 1);


                }

            });
            t.Wait();
        }
    }
}
