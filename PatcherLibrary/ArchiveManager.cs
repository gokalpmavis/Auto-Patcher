using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;
using System.Threading;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading.Tasks;
using PatchLibrary;

namespace PatchLibrary
{
    class ArchiveManager
    {
        public static int CompressLevel = 1;  // 0: Just archive 9:maximum compression.
        public Dictionary<string, Dictionary<string, List<FileIdentifier>>> CompressContent;
        public List<string> NoChangeList;
        string _workingDirectory ;
        int _beenFlag = 0;
        public ArchiveManager(int compressionLevel)
        {
            CompressLevel = compressionLevel;
            CompressContent = new Dictionary<string, Dictionary<string, List<FileIdentifier>>>();
            NoChangeList = new List<string>();
        }

        internal void AddToCompressContent(string folder, Dictionary<string, List<FileIdentifier>> fileListForZip)
        {
            if (_beenFlag == 0)
            {
                _beenFlag = 1;
                _workingDirectory = folder;
            }
            CompressContent[folder] = new Dictionary<string, List<FileIdentifier>>();
            foreach (string archive in fileListForZip.Keys)
            {
                CompressContent[folder][archive] = new List<FileIdentifier>();
                CompressContent[folder][archive].AddRange(fileListForZip[archive]);
            }
        }

        public void Compress()
        {
            try{
                Parallel.ForEach
                (CompressContent.Keys.ToList(),
                    new ParallelOptions { MaxDegreeOfParallelism = 4 },
                    folder =>
                    {
                        SharpZipCompressJob(folder, CompressContent[folder]);
                    }
                );
            }

            catch (AggregateException exception)
            {
                foreach (Exception ex in exception.InnerExceptions)
                    Console.WriteLine(ex.Message);
            }
        }

        internal string GetFolderContentStr()
        {
            string str = "";
            foreach (string folder in CompressContent.Keys)
            {
                str += "folder\t" + GetSafeName(folder) + "\n";
                foreach (string archive in CompressContent[folder].Keys)
                {
                    if (archive == "*subfolders*") // not an archive file. Just a trick for representation of subfolders.
                    {
                        foreach (FileIdentifier fid in CompressContent[folder][archive])
                        {
                            str += "\tsubfolder\t" + GetSafeName(Path.GetFileName(fid.Filename)) + "\n";
                        }
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(Path.Combine(folder, archive));
                        string lastModified = Convert.ToString(fi.LastWriteTime.Ticks);
                        str += "\tarchive\t" + GetSafeName(archive) + "\t" + fi.Length/*Form1.getSizeStr(fi.Length, out marker) + " " + marker*/ + "\t" + lastModified + "\n";
                        foreach (FileIdentifier fid in CompressContent[folder][archive])
                        {
                            lastModified = Convert.ToString(fid.ModifiedTime.Ticks);// TODO_Omer change it to tick count
                            str += "\t\tfile\t" + GetSafeName(Path.GetFileName(fid.Filename)) + "\t" + fid.Size /*Form1.getSizeStr(fid.Size, out marker) + " " + marker*/ + "\t" + lastModified + "\n";
                        }
                    }
                }
            }
            return str;
        }

        internal void ClearFolderContent()
        {
            foreach (string folder in CompressContent.Keys)
            {

                foreach (string archive in CompressContent[folder].Keys)
                {
                    if (archive == "*subfolders*") // not an archive file. Just a trick for representation of subfolders.
                    {
                        
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(Path.Combine(folder, archive));
                        foreach (FileIdentifier fid in CompressContent[folder][archive])
                        {
                        
                        }
                    }
                }
            }
        }

        private string GetSafeName(string name) // returns a path with quotes
        {
            return "\"" + name + "\"";
        }

        internal void PrintToFile(string path)
        {
            string content = GetFolderContentStr();
            System.IO.File.WriteAllText(path, content);
        }


        public void SharpZipCompressJob(string workingDirectory, Dictionary<string, List<FileIdentifier>> fileListForZip)
        {
            Parallel.ForEach(fileListForZip.Keys,
                    new ParallelOptions { MaxDegreeOfParallelism = 10 },
                    key =>
                    {
                        SingleArchiveCompressJob( key, fileListForZip[key]);
                    }
            );
        }

        public void SingleArchiveCompressJob(string key, List<FileIdentifier> fileList)
        {
            
            if (fileList.Count > 0 && key.EndsWith(".zip"))
            {

                string archiveFile = Path.Combine(_workingDirectory, key);
                if (System.IO.File.Exists(archiveFile))
                {
                    Console.WriteLine("Archive already exists {0}", archiveFile);
                    return;
                }
               /* string folder = Path.GetDirectoryName(archiveFile);
                if (noChangeList.Contains(archiveFile))
                {
                    Console.WriteLine("Archive already exists {0}",archiveFile);
                    return;
                }
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }*/
                using (ZipOutputStream zipStream = new ZipOutputStream(System.IO.File.Create(archiveFile)))
                {
                    zipStream.SetLevel(CompressLevel);
                    byte[] buffer = new byte[32 * 1024];
                    foreach (FileIdentifier fi in fileList)
                    {
                        string fileName = "";
                        try
                        {
                            fileName = ZipEntry.CleanName(Path.GetFileName(fi.Filename));
                            ZipEntry entry = new ZipEntry(fileName);
                            entry.ExtraData = GetBytes(Convert.ToString(fi.ModifiedTime.Ticks));
                            entry.Size = fi.Size;
                            zipStream.PutNextEntry(entry);
                            using (FileStream fs = System.IO.File.OpenRead(fi.Filename))
                            {
                                Console.WriteLine("File {0} is being compressed in to archive {1}...", Path.GetFileName(fi.Filename), archiveFile);
                                StreamUtils.Copy(fs, zipStream, buffer);
                            }
                            zipStream.CloseEntry();
                        }
                        catch (System.Exception )
                        {
                            Console.WriteLine("File {0} couldn't added to archive {1}...", fileName, archiveFile);
                        }
                    }
                    zipStream.Finish();
                    zipStream.Close();
                }
            }
        }
        internal string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        internal byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        internal void Clear()
        {
            CompressContent.Clear();
            NoChangeList.Clear();
        }
    }
}
