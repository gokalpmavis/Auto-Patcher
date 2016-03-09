using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.Windows.Forms;
namespace Patcher.Client
{
    public class RepositoryManager
    {
        public int TestFlag;
        public static Semaphore semaphorePatcher = new Semaphore(0, 1);
        string _versionNumber;
        private Repository _repositoryNew = new Repository();
        private Repository _repositoryOld = new Repository();
        private int _index;
        long _totalSize = 0;
        string _lastVersion;
        //public static string LastVersion;
        string _versionToGo;
        string _downloadURL = "http://192.168.1.160/repository/getfile?filename=";
        private List<File> FileArrayMaker(string textBoxContent,string folderName,int depth)
        {
            List<File> fileArray = new List<Patcher.Client.File>();
            int pointer;
            
            string[] contentLines = System.IO.File.ReadAllLines(textBoxContent + @"\content.txt");

            for (pointer = depth; pointer < contentLines.Length; pointer++)
            {
                if (contentLines[pointer].StartsWith("folder"))
                    break;
                File tempFile = new File();
                string name;
                StringBuilder builderToName = new StringBuilder();
                int count;
                for (count = 1; contentLines[pointer][count] != '\t'; count++)
                {
                    //Console.WriteLine("aha" + contentLines[pointer][count]);
                    builderToName.Append(contentLines[pointer][count]);
                }
                name = builderToName.ToString();
                if(folderName != null)
                    name.Replace(folderName, "");
                tempFile.Name = name;
                StringBuilder builderToHash = new StringBuilder();
                count++;
                for (int countTemp = 0; count < contentLines[pointer].Length; count++, countTemp++)
                    builderToHash.Append(contentLines[pointer][count]);
                string hash;
                hash = builderToHash.ToString();
                tempFile.Hash = hash;

                fileArray.Add(tempFile);
            }
            _index = pointer;
            return fileArray;

        }
        private void FileSeperate(string path, string type, int flag)
        {
            GC.Collect();
            if (!System.IO.File.Exists(path + @"\" + type + "content.txt") || !System.IO.File.Exists(path + @"\" + type))
            {
                return;
            }
            FileStream fileStreamSource = null;
            fileStreamSource = System.IO.File.Open(path + @"\" + type, FileMode.Open);



            string[] contentLines = System.IO.File.ReadAllLines(path + @"\" + type + "content.txt");
            foreach (string temp in contentLines)
            {
                string pathTemp = temp;
                FileStream fileStreamDestiny = null;
                if (temp.Contains("folder "))
                {
                    pathTemp = temp.Replace("folder ", "");

                    Directory.CreateDirectory(path + pathTemp);

                }

                else
                {
                    try
                    {

                        int counter = 0;
                        string name;
                        long offset, size;
                        string stringOffset, stringSize;

                        StringBuilder builder = new StringBuilder();
                        for (; temp[counter] != '\t'; counter++)
                            builder.Append(temp[counter]);

                        name = builder.ToString();

                        counter++;
                        builder = new StringBuilder();
                        for (; temp[counter] != '\t'; counter++)
                            builder.Append(temp[counter]);

                        stringOffset = builder.ToString();
                        offset = long.Parse(stringOffset);

                        counter++;
                        builder = new StringBuilder();
                        for (; counter < temp.Length; counter++)
                            builder.Append(temp[counter]);

                        stringSize = builder.ToString();
                        size = long.Parse(stringSize);

                        if (ProgressChanged != null) ProgressChanged((int)((size * 10000000) / _totalSize));


                        if (flag == 1)
                        {
                            fileStreamDestiny = null;
                            fileStreamDestiny = System.IO.File.Open(path + @"\diff", FileMode.Append);

                            int bytesRead = 0;
                            while (bytesRead != size)
                            {
                                const int chunkSize = 102400000;


                                fileStreamSource.Seek(0, SeekOrigin.Current);
                                if (size < bytesRead + chunkSize)
                                {
                                    var buffer = new byte[((int)size - bytesRead)];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);

                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                    bytesRead += ((int)size - bytesRead);
                                }
                                else
                                {
                                    var buffer = new byte[chunkSize];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);
                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                    bytesRead += buffer.Length;
                                }
                            }
                            
                            fileStreamDestiny.Close();

                            DiffExtender diffExtender = new DiffExtender();
                            diffExtender.applyOnePatch(path + @"\" + name, path + @"\diff", path + @"\" + name + "Temp",TestFlag);

                            System.IO.File.Delete(path + @"\" + name);

                            System.IO.File.Delete(path + @"\diff");

                            System.IO.File.Move(path + @"\" + name + "Temp", path + @"\" + name);
                            
                        }
                        else
                        {
                            fileStreamDestiny = null;
                            fileStreamDestiny = System.IO.File.Open(path + @"\" + name, FileMode.Append);

                            int bytesRead = 0;
                            while (bytesRead != size)
                            {
                                const int chunkSize = 102400000;


                                fileStreamSource.Seek(0, SeekOrigin.Current);
                                if (size < bytesRead + chunkSize)
                                {
                                    var buffer = new byte[((int)size - bytesRead)];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);

                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                    bytesRead += ((int)size - bytesRead);
                                }
                                else
                                {
                                    var buffer = new byte[chunkSize];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);
                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                    bytesRead += buffer.Length;
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " : " + ex.StackTrace);
                    }

                    finally
                    {
                        if (flag == 0)
                            fileStreamDestiny.Close();

                    }
                }

            }

            fileStreamSource.Close();
            System.IO.File.Delete(path + @"\" + type);

            System.IO.File.Delete(path + @"\" + type + "content.txt");



        }


        private bool IsServerAccessible(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                request.Abort();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (WebException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    

        private long Downloader(string destiny, string url, string type,int flag,string downloadName)
        {
            long size = 0;
            if (flag == 0) {

                if (IsServerAccessible(url))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(new Uri(url + @"\"  + type), destiny + @"\" + downloadName);


                    }
                    FileInfo fileInfo = new FileInfo(destiny + @"\" + downloadName);

                    size=fileInfo.Length;
                    if (size == 0)
                    {
                        System.IO.File.Delete(destiny + @"\" + downloadName);

                    }
                }
            }
            else{
                if (IsServerAccessible(url))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(new Uri(url + @"\" + _versionNumber + "-" + _versionToGo + "_" + type + @"\"+downloadName), destiny + @"\" + downloadName);

                    }
                    FileInfo fileInfo = new FileInfo(destiny + @"\" + downloadName);

                    size = fileInfo.Length;
                    if (size == 0)
                    {
                        System.IO.File.Delete(destiny + @"\" + downloadName);

                    }
                }
            }
            return size;
                
        }
        public void LastVersionLearner(string url)
        {
            if (IsServerAccessible(url))
            {
                using (WebClient webClient = new WebClient())
                {
                    _lastVersion = webClient.DownloadString(new Uri(url));
                }
            }
            
        }

        public void GetNextVersion(string url)
        {
            if (IsServerAccessible(url))
            {
                using (WebClient webClient = new WebClient())
                {
                    _versionToGo = webClient.DownloadString(new Uri(url));
                }
            }

        }
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }
        public static void UnZip(string zipFile, string folderPath)
        {
            if (!System.IO.File.Exists(zipFile))
                throw new FileNotFoundException();

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            Shell32.Shell objShell = new Shell32.Shell();
            Shell32.Folder destinationFolder = objShell.NameSpace(folderPath);
            Shell32.Folder sourceFile = objShell.NameSpace(zipFile);

            foreach (var file in sourceFile.Items())
            {
                destinationFolder.CopyHere(file, 4 | 16);
            }
        }
        public delegate void ProgressChangedDelegate(int value);
        public event ProgressChangedDelegate ProgressChanged;
        public int Counter(char character, string word)
        {
            int count = 0;
            foreach (char c in word)
            {
                if (c == character) count++;
            }
            return count;

        }
        public void ProcessFolders(string textBoxContent,string version,int testFlag)
        {
            TestFlag = testFlag;
            LastVersionLearner("http://192.168.1.160/repository/getlastversion");
            if (version == "unknown")
            {
                _versionNumber = _lastVersion;
            }
            else
                _versionNumber = version;
            GetNextVersion("http://192.168.1.160/repository/getnextversion");
            Console.WriteLine(version);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<string> insert = new List<string>();
            List<string> delete = new List<string>();
            List<string> update = new List<string>();

            List<Folder> insertFolder = new List<Folder>();
            List<Folder> deleteFolder = new List<Folder>();

            _repositoryOld.BasePath = textBoxContent;
            if (System.IO.File.Exists(textBoxContent + @"\content.txt"))
            {
                System.IO.File.Delete(textBoxContent + @"\content.txt");
            }

            Downloader(textBoxContent, _downloadURL,_versionNumber+ "_content.txt", 0, "content.txt");
            _repositoryOld.Create(textBoxContent, 0);
            List<Folder> folderarray = new List<Folder>();
            string[] lines = System.IO.File.ReadAllLines(textBoxContent + @"\content.txt");
            Folder tempfolder = new Folder();
            tempfolder.Name = "current";

            List<File> fileArray = FileArrayMaker(textBoxContent,null,0);
            tempfolder.FileList = fileArray;
            folderarray.Add(tempfolder);
            while (_index != lines.Length)
            {
                Folder tempFolderNew = new Folder();
                StringBuilder builder = new StringBuilder();
                int counter;
                string name;
                for (counter = lines[_index].IndexOf("\t")+1; counter < lines[_index].Length; counter++)
                    builder.Append(lines[_index][counter]);
                name = builder.ToString();
                tempFolderNew.Name = name;
                tempFolderNew.FileList = FileArrayMaker(textBoxContent,name,_index + 1);
                folderarray.Add(tempFolderNew);
            }

            foreach (Folder temp in folderarray)
            {
                int counter = 0, pointer = 0;
                int flag = 0;
                for (counter = 0; String.Compare((_repositoryOld.FolderArray[counter].Name), (temp.Name)) != 0; counter++)
                {

                    if (counter == (_repositoryOld.FolderArray.Count) - 1)
                    {
                        insertFolder.Add(temp);
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    foreach (File tempFileList in temp.FileList)
                    {
                        flag = 0;
                        string folderName = _repositoryOld.FolderArray[counter].Name;
                        if (_repositoryOld.FolderArray[counter].FileList.Count == 0)
                        {
                            insert.Add( @"\" + tempFileList.Name);
                            flag = 1;

                        }
                        else
                        {
                            for (pointer = 0; Path.GetFileName((((_repositoryOld.FolderArray[counter]).FileList)[pointer]).Name) != Path.GetFileName(Path.Combine(temp.Name, tempFileList.Name)); pointer++)
                            {

                                //Console.WriteLine(Path.GetFileName((((_repositoryOld.FolderArray[counter]).FileList)[pointer]).Name));
                                //Console.WriteLine( Path.GetFileName(Path.Combine(temp.Name, tempFileList.Name)));
                               if (pointer == ((_repositoryOld.FolderArray[counter].FileList.Count) - 1))
                                {
                                    insert.Add( @"\" + tempFileList.Name);
                                    flag = 1;
                                    break;
                                }
                            }
                            if (flag == 0)
                            {

                                if (String.Compare(((_repositoryOld.FolderArray[counter].FileList)[pointer]).Hash, (tempFileList.Hash)) != 0)
                                {
                                    update.Add( @"\" + tempFileList.Name);
                                }

                            }
                        }
                    }

                }

            }
            
            for (int counter = 0; counter < insert.Count; counter++)
            {
                if (insert[counter].Contains(@"current\"))
                {
                    insert[counter] = insert[counter].Replace(@"current\", "");
                }
            }
            for (int counter = 0; counter < update.Count; counter++)
            {
                if (update[counter].Contains(@"current\"))
                {
                    update[counter] = update[counter].Replace(@"current\", "");
                }
            }
            foreach (Folder tempFolder in insertFolder)
            {
                Directory.CreateDirectory(textBoxContent + tempFolder.Name);
                foreach (File tempFile in tempFolder.FileList)
                {
                    insert.Add( @"\" + tempFile.Name);

                }

            }

            if (System.IO.File.Exists(textBoxContent + @"\patchResult.txt"))
            {
                System.IO.File.Delete(textBoxContent + @"\patchResult.txt");
            }
            System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "CORRECTING CURRENT VERSION" + Environment.NewLine);

            System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "insertFiles" + Environment.NewLine);
            string[] contentLines = System.IO.File.ReadAllLines(textBoxContent + @"\content.txt");
            foreach (string eachInsertFile in insert)
            {
                string toDownlaod=null;
                foreach (string contentEachLine in contentLines)
                {
                    if (contentEachLine.StartsWith (eachInsertFile))
                    {
                        toDownlaod = contentEachLine.Substring(contentEachLine.IndexOf("\t")+1);
                        break;
                    }

                }
            Download:
                long sizeOfZip = Downloader(textBoxContent, _downloadURL, toDownlaod + ".zip", 0, eachInsertFile + ".zip"); ;
                    
                if (Counter('\u005c',eachInsertFile) == 1)
                {
                    System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), eachInsertFile + "\t" +" size of zip=" + sizeOfZip.ToString());
                    UnZip(textBoxContent + eachInsertFile + ".zip", textBoxContent + Path.GetDirectoryName(eachInsertFile));
                    Hash _hash=new Hash();
                    if (toDownlaod != _hash.HashFile(textBoxContent + eachInsertFile))
                    {
                        System.IO.File.Delete(textBoxContent + eachInsertFile);
                        goto Download;
                    }
                    FileInfo fileInfo = new FileInfo(textBoxContent + eachInsertFile);
                    System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "\t" + " decompressed size =" + fileInfo.Length.ToString() + Environment.NewLine);
                }
                else
                {
                    System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), eachInsertFile + "\t"+ "size of zip=" + sizeOfZip.ToString());
                    UnZip(textBoxContent + eachInsertFile + ".zip", textBoxContent + eachInsertFile.Substring(0, eachInsertFile.LastIndexOf(@"\")));
                    Hash _hash = new Hash();
                    if (toDownlaod != _hash.HashFile(textBoxContent + eachInsertFile))
                    {
                        System.IO.File.Delete(textBoxContent + eachInsertFile);
                        goto Download;
                    }
                    FileInfo fileInfo = new FileInfo(textBoxContent + eachInsertFile);
                    System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "\t" + " decompressed size =" + fileInfo.Length.ToString() + Environment.NewLine);
                }
                System.IO.File.Delete(textBoxContent + eachInsertFile + ".zip");
            }
            if(update.Count!=0)System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "updateFiles" + Environment.NewLine);
            foreach (string eachUpdateFile in update)
            {

                string toDownlaod=null;
                foreach (string contentEachLine in contentLines)
                {
                    if (contentEachLine.StartsWith(eachUpdateFile))
                    {
                        toDownlaod = contentEachLine.Substring(contentEachLine.IndexOf("\t")+1);
                        break;
                    }

                }
            Download:
                long sizeOfZip = Downloader(textBoxContent, _downloadURL, toDownlaod + ".zip", 0, eachUpdateFile + ".zip"); 
                
               
                if (System.IO.File.Exists(textBoxContent + eachUpdateFile))
                {
                    System.IO.File.Delete(textBoxContent + eachUpdateFile);
                }
                System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), eachUpdateFile + "\t" + " size of zip=" + sizeOfZip.ToString());

                UnZip(textBoxContent + eachUpdateFile + ".zip", textBoxContent+ Path.GetDirectoryName(eachUpdateFile));
                Hash _hash = new Hash();
                if (toDownlaod != _hash.HashFile(textBoxContent + eachUpdateFile))
                {
                    System.IO.File.Delete(textBoxContent + eachUpdateFile);
                    goto Download;
                }
                FileInfo fileInfo = new FileInfo(textBoxContent + eachUpdateFile);
                System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "\t" + "decompressed size =" + fileInfo.Length.ToString() + Environment.NewLine);
               
                System.IO.File.Delete(textBoxContent + eachUpdateFile + ".zip");
            }

            Downloader(textBoxContent, _downloadURL, "update", 1, "updateChunks.txt");
            Downloader(textBoxContent, _downloadURL, "insert", 1, "insertChunks.txt");
            long updateSize = 0;
            long insertSize = 0;
            if (System.IO.File.Exists(textBoxContent + @"\insertChunks.txt"))
                insertSize=DownloadAndMerge(textBoxContent + @"\insertChunks.txt", "insert", textBoxContent);
            
            if(System.IO.File.Exists(textBoxContent+@"\updateChunks.txt"))
                updateSize=DownloadAndMerge(textBoxContent + @"\updateChunks.txt", "update", textBoxContent);
            
            
            Downloader(textBoxContent, _downloadURL, _versionNumber + "-" + _versionToGo + "_updatecontent.txt", 0, "updatecontent.txt");
            Downloader(textBoxContent, _downloadURL, _versionNumber + "-" + _versionToGo + "_insertcontent.txt", 0, "insertcontent.txt");
            Downloader(textBoxContent, _downloadURL, _versionNumber + "-" + _versionToGo + "_deletecontent.txt", 0, "deletecontent.txt");
            System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "UPDATE FILE SIZE FOR NEXT VERSION = " + updateSize.ToString() + Environment.NewLine);
            System.IO.File.AppendAllText(Path.Combine(textBoxContent, @"patchResult.txt"), "INSERT FILE SIZE FOR NEXT VERSION = " + insertSize.ToString() + Environment.NewLine);

            
            if (System.IO.File.Exists(textBoxContent + @"\insert"))
            {
                FileStream fileStream = null; ;
                fileStream = System.IO.File.Open(textBoxContent + @"\insert", FileMode.Open);
                _totalSize += fileStream.Length;
                fileStream.Close();
            }
            if (System.IO.File.Exists(textBoxContent + @"\update"))
            {
                FileStream fileStream = null; ;
                fileStream = System.IO.File.Open(textBoxContent + @"\update", FileMode.Open);
                _totalSize += fileStream.Length;
                fileStream.Close();
            }
            Console.WriteLine(_totalSize);
            FileSeperate(textBoxContent, "insert", 0);
            FileSeperate(textBoxContent, "update", 1);
            if (System.IO.File.Exists(textBoxContent + @"\" + "deletecontent.txt"))
            {

                string[] deletelines = System.IO.File.ReadAllLines(textBoxContent + @"\" + "deletecontent.txt");
                foreach (string temp in deletelines)
                {
                    string pathtemp = temp;
                    if (temp.Contains("folder "))
                    {
                        pathtemp = temp.Replace("folder ", "");
                        try
                        {
                            Directory.Delete(textBoxContent + pathtemp, true);
                        }
                        catch (IOException ex)
                        {
                           MessageBox.Show(ex.Message);
                        }


                    }
                    else
                    {

                        pathtemp = temp.Replace("current", "");
                        pathtemp = textBoxContent + pathtemp;
                        try
                        {
                            System.IO.File.Delete(pathtemp);
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }


                    }
                }
                System.IO.File.Delete(textBoxContent + @"\" + "deletecontent.txt");
            }

            sw.Stop();

            if (ProgressChanged != null) ProgressChanged(10000000);

            MessageBox.Show("Done!", "COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(Path.Combine(textBoxContent, "patchResult.txt"));
            if (_lastVersion != _versionToGo)
            {
                ProcessFolders(textBoxContent,version,testFlag);
            }
            else
            {
                _versionNumber = _versionToGo;
                semaphorePatcher.Release();
            }
        }

        private long DownloadAndMerge(string content, string type,string destiny)
        {
            string[] lines = System.IO.File.ReadAllLines(content);
            long totalSize=0;
            FileStream fileStreamDestiny = System.IO.File.Open(destiny + @"\"+type, FileMode.Append);

            foreach (var line in lines) {

                long tempSize = Downloader(destiny, _downloadURL, type, 1, line);
                FileStream fileStreamSource;
            Label:
                try
                {
                    fileStreamSource = System.IO.File.Open(destiny + @"\" + line, FileMode.Open);

                }
                catch
                {
                    goto Label;
                }

                totalSize += tempSize;
                var buffer = new byte[tempSize];

                fileStreamSource.Read(buffer, 0, buffer.Length);

                fileStreamSource.Close();

                System.IO.File.Delete(destiny + @"\" + line);

                fileStreamDestiny.Seek(0, SeekOrigin.Current);
                fileStreamDestiny.Write(buffer, 0, buffer.Length);
            }
            
            fileStreamDestiny.Close();

            System.IO.File.Delete(content);

            return totalSize;
        }
    }
}
