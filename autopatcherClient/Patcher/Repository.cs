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

namespace Patcher.Client
{
    public class Repository
    {
        public List<Folder> FolderArray = new List<Folder>();
        public String BasePath;
        private Hash _hash = new Hash();
        
        public void Create(String path, int level)
        {

            string[] files = Directory.GetFiles(path);
            string[] folders = Directory.GetDirectories(path);
            
            if (level == 0)
            {

                Folder tempOld = new Folder();
                tempOld.Name="current";
                tempOld.Indent = 0;
                List<File> fileArray = new List<File>();

                foreach (string eachFile in files)
                {
                    File tempFile = new File();
                    tempFile.Name=Path.GetFileName(eachFile);
                    string hashValue;
                    hashValue = _hash.HashFile(eachFile);
                    tempFile.Hash=hashValue;
                    fileArray.Add(tempFile);
                }

                tempOld.FileList=fileArray;
                FolderArray.Add(tempOld);
            }

           
                Folder tempFolder = new Folder();

                List<File> fileArrayToAdd = new List<File>();
                foreach (string folder in folders)
                {
                    tempFolder = new Folder();
                    files = Directory.GetFiles(folder);

                    tempFolder.Indent = level+1;
                    tempFolder.Name=folder.Replace(BasePath, "");

                    fileArrayToAdd = new List<File>();
                        
                    foreach (string file in files)
                    {
                            File tempFile = new File();
                            tempFile.Name=Path.GetFileName(file);
                            string hash1;
                            hash1 = _hash.HashFile(file);
                            tempFile.Hash=hash1;
                            fileArrayToAdd.Add(tempFile);

                    }

                    tempFolder.FileList=fileArrayToAdd;
                    FolderArray.Add(tempFolder);
                    Create(folder, level + 1);
                   
                    
                }

           
        }
    }
}
