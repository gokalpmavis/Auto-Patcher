using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using PatchLibrary;
using System.Globalization;

namespace PatchLibrary
{
    class FileManager
    {
        int _beenFlag = 0;
        private ArchiveManager _rar;
        public int MinArchiveSize=0;
        Dictionary<string, Dictionary<FileIdentifier, List<FileIdentifier>>> olderContent;
        List <string> _includedExtentions=new List<string>();
        List <string> _ignoredFolders=new List<string>();
        List <string> _ignoredExtentions=new List<string>();
        List<string> _ignoredFiles = new List<string>();
        string _sourceFolder;
        public FileManager(List<string> includedExtentions, List<string> ignoredFolders, List<string> ignoredExtentions, List<string> ignoredFiles,string source,int compressionLevel)
        {
            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            this._ignoredFiles = ignoredFiles;
            _sourceFolder = source;
            _rar = new ArchiveManager(compressionLevel);
            olderContent = new Dictionary<string, Dictionary<FileIdentifier, List<FileIdentifier>>>();
        }
        
        internal bool IsIncludedExtention(string ext)
        {
            foreach (string extention in _includedExtentions)
            {
                if (extention.Equals(ext, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }


        internal bool IsIgnoredFolder(string path)
        {
            foreach (string ignoredPath in _ignoredFolders)
            {
                string temp = Path.Combine(_sourceFolder, ignoredPath);
                if (temp.Equals(path, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }


        internal bool IsIgnoredExtention(string ext)
        {
            foreach (string extention in _ignoredExtentions)
            {
                if (extention.Equals(ext, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }


        internal bool IsIncludedFile(string path)
        {
            string extention = Path.GetExtension(path);
            if (IsIncludedExtention(extention))
            {
                return true;
            }
            return false;
        }
        internal bool IsIgnoredFile(string file)
        {
            string extention = Path.GetExtension(file);
            if (IsIgnoredExtention(extention))
            {
                return true;
            }

            foreach (string item in _ignoredFiles)
            {
                string ignoredFile;
                if (item.Contains("*"))
                {
                    int first = item.IndexOf("*");
                    int last = item.LastIndexOf("*");
                    if (first == 0 && last == item.Length - 1) // ignored file regex: *asd* ignore asda, asd, dasda etc
                    {
                        ignoredFile = item.TrimStart("*".ToCharArray());
                        ignoredFile = ignoredFile.TrimEnd("*".ToCharArray());
                        CultureInfo culture = CultureInfo.CurrentCulture;
                        if (culture.CompareInfo.IndexOf(file, ignoredFile, CompareOptions.IgnoreCase) >= 0)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (first == 0) // ends with
                        {
                            ignoredFile = item.TrimStart("*".ToCharArray());
                            if (file.EndsWith(ignoredFile, StringComparison.CurrentCultureIgnoreCase))
                            {
                                return true;
                            }
                        }
                        if (last == item.Length - 1) // startswith
                        {
                            ignoredFile = item.TrimEnd("*".ToCharArray());
                            if (file.StartsWith(ignoredFile, StringComparison.CurrentCultureIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }

                }
                else
                {
                    if (file.Equals(item, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        internal void GenerateCompressContentRecursively(string source, string target,bool sourceIsIgnored,int flag)
        {
            if (_beenFlag == 0)
            {
                _beenFlag = 1;
                if ( !Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }
            }
            string[] files=null;
            files = Directory.GetFiles(source);
            Dictionary<string, List<FileIdentifier>> fileListByType = new Dictionary<string, List<FileIdentifier>>();
            Dictionary<string, List<FileIdentifier>> fileListForZip;
            foreach(string path in files)
            {
                    if (IsIgnoredFile(Path.GetFileName(path)) || (sourceIsIgnored && !IsIncludedFile(path)))
                    {
                        continue;
                    }
                    string extention = Path.GetExtension(path);
                    if (!fileListByType.ContainsKey(extention))
                    {
                        fileListByType[extention] = new List<FileIdentifier>();
                    }
                    FileInfo fi = new FileInfo(path);
                    FileIdentifier fid = new FileIdentifier();
                    fid.Filename = fi.FullName;
                    fid.IsFolder = false;
                    fid.Size = fi.Length;
                    fid.ModifiedTime = fi.LastWriteTime;
                    fileListByType[extention].Add(fid);
               
            }
            GetZipFileList(target,fileListByType, out fileListForZip);

         /*   foreach (string asd in directories)
                Console.WriteLine(asd);*/

            string[] directories = Directory.GetDirectories(source);
            foreach (string path in directories)
            {
                FileInfo fi = new FileInfo(path);
                FileIdentifier fid = new FileIdentifier();
                fid.Filename = fi.FullName;
                fid.IsFolder = true;
                if (!fileListForZip.ContainsKey("*subfolders*"))
                {
                    fileListForZip["*subfolders*"] = new List<FileIdentifier>();
                }
                fileListForZip["*subfolders*"].Add(fid);
            }

            _rar.AddToCompressContent(target, fileListForZip);
            foreach (string path in directories)
            {

                bool isIgnored = sourceIsIgnored || IsIgnoredFolder(path);
                string subFolder = Path.GetFileName(path);
                string targetFolder = Path.Combine(target, subFolder);
                /*if (!isIgnored && !Directory.Exists(targetFolder)) 
                { 
                    Directory.CreateDirectory(targetFolder); 
                }*/
                
               /* if(flag==0)
                {

                    string part = path.Replace(source, "");
                    string previousSubDirectory = prev + part;
                    if (Directory.Exists(previousSubDirectory))
                    {
                        generateCompressContentRecursively(path, targetFolder, isIgnored, flag, previousSubDirectory);
                    }
                    else
                    {
                        generateCompressContentRecursively(path, targetFolder, isIgnored, 1, null);
                    }
                }
                else
                {
                    generateCompressContentRecursively(path, targetFolder, isIgnored, flag, null);
                }*/
                GenerateCompressContentRecursively(path, targetFolder, isIgnored, flag);
            }
        }

        private void GetZipFileList(string target,Dictionary<string, List<FileIdentifier>> fileListByType,out Dictionary<string, List<FileIdentifier>> fileListForZip)
        {
            fileListForZip = new Dictionary<string, List<FileIdentifier>>();
            List<FileIdentifier> scrap = new List<FileIdentifier>();
            long scrapSize = 0;
            foreach (string extention in fileListByType.Keys)
            {
                List<FileIdentifier> temp = new List<FileIdentifier>();
                fileListByType[extention].Sort();
                long currentSize = 0;
                foreach (FileIdentifier fid in fileListByType[extention])
                {
                    if (currentSize > MinArchiveSize)
                    {
                        AddToList(target, ref fileListForZip, temp);
                        temp.Clear();
                        currentSize = 0;
                    }
                    temp.Add(fid);
                    currentSize += fid.Size;
                }
                if (scrapSize > MinArchiveSize)
                {
                    AddToList(target, ref fileListForZip, scrap);
                    scrap.Clear();
                    scrapSize = 0;
                }
                scrap.AddRange(temp);
                scrapSize += currentSize;
            }
            AddToList(target, ref fileListForZip, scrap);
        }

        private void AddToList(string folderName, ref Dictionary<string, List<FileIdentifier>> fileListForZip, List<FileIdentifier> temp)
        {
            if (temp.Count > 0)
            {
                string key = CheckForOlderContent(folderName, temp);
                if(key == "") // if archive is a new archive, get a random name
                {
                    key = Path.GetFileName(temp[0].Filename) + ".zip";
                    string fullName = Path.Combine(Path.GetDirectoryName(folderName), key);
                    while (System.IO.File.Exists(fullName))
                    {
                        Console.WriteLine(" Congratulations. Your computer managed to create same random name with {0} which is a probability of 1 / 36^10 . You should buy a lottery ticket", fullName);
                        key = Path.GetFileName(temp[0].Filename) + ".zip";
                        fullName = Path.Combine(Path.GetDirectoryName(folderName), key);
                    }
                }
                PatchLibrary.Hash hash = new PatchLibrary.Hash();
                key = hash.HashFile(Directory.GetParent(temp[0].Filename) + @"\" + key.Replace(".zip", "")) + ".zip";
                
                fileListForZip[key] = new List<FileIdentifier>();
                fileListForZip[key].AddRange(temp);
            }
        }

        private string CheckForOlderContent(string folderName, List<FileIdentifier> temp)
        {
            if (olderContent.Keys.Contains(folderName))
            {
                foreach (FileIdentifier archive in olderContent[folderName].Keys)
                {
                    List<FileIdentifier> older = olderContent[folderName][archive];
                    bool exists = CheckArchiveContent(older, temp);
                    if (exists)
                    {
                        _rar.NoChangeList.Add(Path.Combine(folderName,archive.Filename));
                        return archive.Filename;
                    }
                }
            }
            return "";
        }

        private bool CheckArchiveContent(List<FileIdentifier> older, List<FileIdentifier> newer)
        {
            if (newer.Count != older.Count)
            {
                return false;
            }
            bool found = false;
            foreach (FileIdentifier newfile in newer)
            {
                found = false;
                foreach (FileIdentifier oldfile in older)
                {
                    if (newfile.Equals(oldfile, FileComparisionOption.IgnorePath))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    break;
                }
            }
            return found;
        }

        internal void CompressFolder(string source, string target,int flag)
        {
            if (System.IO.File.Exists(Path.Combine(target, "zipContent.txt")))
            {
                GetOlderContent(Path.Combine(target, "zipContent.txt"));
            }
            GenerateCompressContentRecursively(source, target, false,flag);
            _rar.Compress();
            //Console.WriteLine(rar.getFolderContentStr());
           // rar.printToFile(Path.Combine(target, "zipContent.txt"));
        }

        private void GetOlderContent(string contentFile)
        {
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(contentFile);
                string currentFolder = "";
                FileIdentifier currentArchive = new FileIdentifier();
                FileIdentifier currentFile = new FileIdentifier();
                FileIdentifier nullFi = new FileIdentifier();
                nullFi.Filename = "*subfolders*";
                foreach (string str in lines)
                {
                    string line = str.TrimStart("\t".ToCharArray());
                    if (line.StartsWith("folder", StringComparison.CurrentCultureIgnoreCase))
                    {
                        currentFolder = GetFolder(line);
                        olderContent[currentFolder] = new Dictionary<FileIdentifier, List<FileIdentifier>>();
                    }
                    else if (line.StartsWith("archive", StringComparison.CurrentCultureIgnoreCase))
                    {
                        currentArchive = GetFile(line);
                        olderContent[currentFolder][currentArchive] = new List<FileIdentifier>();
                    }
                    else if (line.StartsWith("file", StringComparison.CurrentCultureIgnoreCase))
                    {
                        currentFile = GetFile(line);
                        olderContent[currentFolder][currentArchive].Add(currentFile);
                    }
                    else if (line.StartsWith("subfolder", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!olderContent[currentFolder].Keys.Contains(nullFi))
                        {
                            olderContent[currentFolder][nullFi] = new List<FileIdentifier>();
                        }
                        string subfolder = GetFolder(line);
                        FileIdentifier fi = new FileIdentifier();
                        fi.Filename = subfolder;
                        fi.IsFolder = true;
                        olderContent[currentFolder][nullFi].Add(fi);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private FileIdentifier GetFile(string line)
        {
            FileIdentifier fi = new FileIdentifier();
            fi.IsFolder = false;
            int start = line.IndexOf("\"") + 1;
            int end = line.LastIndexOf("\"");
            fi.Filename = line.Substring(start, end - start);
            line = line.Substring(line.IndexOf(fi.Filename) + fi.Filename.Length + 1);

            string[] tokens = line.Split(" \t\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            long size = Convert.ToInt64(tokens[0]);
            long timestamp = (long)(Convert.ToInt64(tokens[1]));
            DateTime lastModified = new DateTime(timestamp);
            fi.Size = size;
            fi.ModifiedTime = lastModified;
            return fi;
        }

        private string GetFolder(string line)
        {
            string result = null;
            int start = line.IndexOf("\"") + 1;
            int end = line.LastIndexOf("\"");
            result = line.Substring(start, end - start);
            return result;
        }

        internal void Clear()
        {
            olderContent.Clear();
            _rar.Clear();
        }
    }
}
