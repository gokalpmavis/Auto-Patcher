using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using PatchLibrary;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatcherLibrary;
namespace PatchLibrary
{
    public class RepositoryManager
    {

        public static Semaphore TempSemaphore = new Semaphore(0, 1);
        private long FileMerge(string destiny, List<string> files, string source, string name, int flag, long preSize, int insertFolder, FileStream insertFolderStream,string compareSource)
        {
            GC.Collect();
            FileStream fileStreamDestiny = null;
            FileStream fileStreamSource = null;
            destiny += name;
            string contentDestiny = destiny + "content.txt";
            destiny += ".cmpr";
            long totalSize = preSize;

            if (flag == 1)
            {
                if (System.IO.File.Exists(destiny))
                {
                    System.IO.File.Delete(destiny);
                }
                if (System.IO.File.Exists(contentDestiny))
                {
                    System.IO.File.Delete(contentDestiny);
                }
            }
            if (insertFolder == 0)
            {
                    fileStreamDestiny = System.IO.File.Open(destiny, FileMode.Append);                   
            }
            else
                fileStreamDestiny = insertFolderStream;
            foreach (string temp in files)
            {
                long updateLength=0;
                string sourceName=null;
                string compare=null;
                if (temp.Contains(@"current\"))
                {
                    sourceName = temp;
                    sourceName = sourceName.Replace(@"current\", (source + @"\"));

                }
                else
                {
                    sourceName = source + temp;
                }
                try
                {
                    Task t = Task.Factory.StartNew(() =>
                    {
                        if (name.Contains("update"))
                        {

                            if (temp.Contains(@"current\"))
                            {

                                compare = temp;
                                compare = compare.Replace(@"current\", (compareSource + @"\"));

                            }
                            else
                            {
                                compare = compareSource + temp;
                            }
                            DiffMaker diffmaker = new DiffMaker();
                            diffmaker.createPatch(compare, sourceName, System.IO.Path.GetDirectoryName(destiny));
                            fileStreamSource = System.IO.File.Open(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(destiny), "temp.vcdiff"), FileMode.Open);
                            int bytesRead = 0;
                            while (bytesRead != fileStreamSource.Length)
                            {
                                const int chunkSize = 102400000;

                                fileStreamSource.Seek(0, SeekOrigin.Current);
                                if (fileStreamSource.Length < bytesRead + chunkSize)
                                {
                                    var buffer = new byte[((int)fileStreamSource.Length - bytesRead)];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);
                                    bytesRead += ((int)fileStreamSource.Length - bytesRead);
                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                }
                                else
                                {
                                    var buffer = new byte[chunkSize];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);
                                    bytesRead += chunkSize;
                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                }

                            }
                            updateLength = bytesRead;
                            fileStreamSource.Close();
                            System.IO.File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(destiny), "temp.vcdiff"));
                        }
                        
                        else
                        {
                            fileStreamSource = System.IO.File.Open(sourceName, FileMode.Open);
                            int bytesRead = 0;
                            while (bytesRead != fileStreamSource.Length)
                            {
                                const int chunkSize = 102400000;

                                fileStreamSource.Seek(0, SeekOrigin.Current);
                                if (fileStreamSource.Length < bytesRead + chunkSize)
                                {
                                    var buffer = new byte[((int)fileStreamSource.Length - bytesRead)];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);
                                    bytesRead += ((int)fileStreamSource.Length - bytesRead);
                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                }
                                else
                                {
                                    var buffer = new byte[chunkSize];
                                    fileStreamSource.Read(buffer, 0, buffer.Length);
                                    bytesRead += chunkSize;
                                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                                }

                            }
                        }


                    });
                    t.Wait();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " : " + ex.StackTrace);
                }
                finally
                {
                    string tempString = temp;
                    string append;
                    if (temp.Contains(@"current\"))
                    {

                        tempString = tempString.Replace(@"current\", @"\");

                    }
                    if (name.Contains("update"))
                    {
                        append = tempString + "\t" + totalSize.ToString() + "\t" + (updateLength).ToString();
                        totalSize += updateLength;
                    }
                    else
                    {
                        append = tempString + "\t" + totalSize.ToString() + "\t" + (fileStreamSource.Length).ToString();
                        totalSize += fileStreamSource.Length;

                        fileStreamSource.Close();
                    }
                    

                    System.IO.File.AppendAllText(contentDestiny, append + Environment.NewLine);

                }
            }
            if (insertFolder == 0)
            {
                fileStreamDestiny.Close();
                    
            }
            return totalSize;
        }
        public void ProcessFolders(string textBox1Content, string textBox2Content, string textBox3Content,string oldVersion,string newVersion)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<string> insert = new List<string>();
            List<string> delete = new List<string>();
            List<string> update = new List<string>();
            List<Folder> insertFolder = new List<Folder>();
            List<Folder> deleteFolder = new List<Folder>();
            RepositoryDiff repositoryNewVersion = new RepositoryDiff(textBox1Content);
            RepositoryDiff repositoryOldVersion = new RepositoryDiff(textBox2Content);
            repositoryNewVersion.RepositoryContent(textBox1Content, 0);
            repositoryOldVersion.RepositoryContent(textBox2Content, 0);


            foreach (Folder temp in repositoryNewVersion.FolderArray)
            {
                int pointer1 = 0, pointer2 = 0;
                int flag = 0;
                for (pointer1 = 0; String.Compare((repositoryOldVersion.FolderArray[pointer1].Name), (temp.Name)) != 0; pointer1++)
                {
                    if (pointer1 == (repositoryOldVersion.FolderArray.Count) - 1)
                    {
                        insertFolder.Add(temp);
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    foreach (File temp1 in temp.FileList)
                    {
                        flag = 0;
                        string at = repositoryOldVersion.FolderArray[pointer1].Name;
                        for (pointer2 = 0; repositoryOldVersion.FolderArray[pointer1].Flag == 1 && String.Compare((((repositoryOldVersion.FolderArray[pointer1]).FileList)[pointer2]).Name, (temp1.Name)) != 0; pointer2++)
                        {
                            if (pointer2 == ((repositoryOldVersion.FolderArray[pointer1].FileList.Count) - 1))
                            {
                                insert.Add(temp.Name + @"\" + temp1.Name);
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {

                            if (repositoryOldVersion.FolderArray[pointer1].Flag == 1 && String.Compare(((repositoryOldVersion.FolderArray[pointer1].FileList)[pointer2]).Hash, (temp1.Hash)) != 0)
                            {
                                update.Add(temp.Name + @"\" + temp1.Name);
                            }

                        }
                    }

                }

            }

            foreach (Folder temp in repositoryOldVersion.FolderArray)
            {
                int pointer1 = 0, pointer2 = 0;
                int flag = 0;
                for (pointer1 = 0; String.Compare((repositoryNewVersion.FolderArray[pointer1].Name), (temp.Name)) != 0; pointer1++)
                {
                    if (pointer1 == (repositoryNewVersion.FolderArray.Count) - 1)
                    {
                        deleteFolder.Add(temp);
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    foreach (File temp1 in temp.FileList)
                    {
                        for (pointer2 = 0; repositoryNewVersion.FolderArray[pointer1].Flag == 1 && String.Compare((((repositoryNewVersion.FolderArray[pointer1]).FileList)[pointer2]).Name, (temp1.Name)) != 0; pointer2++)
                        {
                            if (pointer2 == ((repositoryNewVersion.FolderArray[pointer1].FileList.Count) - 1))
                            {
                                delete.Add(temp.Name + @"\" + temp1.Name);
                                break;
                            }
                        }
                    }

                }

            }

            FileMerge(textBox3Content, update, textBox1Content, @"\"+oldVersion+"-"+newVersion+"_update", 1, 0, 0, null, textBox2Content);
            long size = FileMerge(textBox3Content, insert, textBox1Content, @"\" + oldVersion + "-" + newVersion + "_insert", 1, 0, 0, null, null);
            FileStream fileStreamInsert = null;
            if (insertFolder.Count != 0)
            {
                tryagain:

                try
                {
                    fileStreamInsert = System.IO.File.Open(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_insert.cmpr", FileMode.Append);
                }
                catch{
                    goto tryagain;
                }

            }

            foreach (Folder temp in insertFolder)
            {

                System.IO.File.AppendAllText(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_insertcontent.txt", "folder " + temp.Name + Environment.NewLine);
                List<string> insertfiles = new List<string>();
                foreach (File temp1 in temp.FileList)
                {
                    insertfiles.Add(temp.Name + @"\" + temp1.Name);
                }
                long templength;
                templength = FileMerge(textBox3Content, insertfiles, textBox1Content, @"\" + oldVersion + "-" + newVersion + "_insert", 0, size, 1, fileStreamInsert, null);
                size = templength;
            }
            if (insertFolder.Count != 0)
            {
                fileStreamInsert.Close();
            }

            if (!System.IO.Directory.Exists(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_update"))
                System.IO.Directory.CreateDirectory(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_update");
            Split(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_update.cmpr", textBox3Content + @"\" + oldVersion + "-" + newVersion + "_update","update");

            if (!System.IO.Directory.Exists(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_insert"))
                System.IO.Directory.CreateDirectory(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_insert");
            Split(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_insert.cmpr", textBox3Content + @"\" + oldVersion + "-" + newVersion + "_insert","insert");

            if (System.IO.File.Exists(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_deletecontent.txt"))
            {
                System.IO.File.Delete(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_deletecontent.txt");

            }
            foreach (string temp in delete)
            {
                System.IO.File.AppendAllText(textBox3Content + @"\" + oldVersion + "-" + newVersion + "_deletecontent.txt", temp + Environment.NewLine);
            }
            foreach (Folder temp in deleteFolder)
            {

                System.IO.File.AppendAllText(textBox3Content + @"\" + oldVersion+"-"+newVersion+"_deletecontent.txt", "folder " + temp.Name + Environment.NewLine);
                
            }
            
            MessageBox.Show("Done!", "COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            TempSemaphore.Release();
        }

        private void Split(string bigFile, string destiny,string type)
        {
            int chunkSize = 10240000;
            
            FileInfo fileInfo = new FileInfo(bigFile);
            long fileSize=fileInfo.Length;

            FileStream fileStreamSource = System.IO.File.Open(bigFile, FileMode.Open);

            string contentDestiny = destiny + @"\"+type+"Chunks.txt";

            long bytesRead = 0;
            while (bytesRead != fileSize)
            {
                FileStream fileStreamDestiny = System.IO.File.Open(destiny + @"\temp", FileMode.Append);
                fileStreamSource.Seek(0, SeekOrigin.Current);
                if (fileSize < bytesRead + chunkSize)
                {
                    var buffer = new byte[fileSize - bytesRead];
                    fileStreamSource.Read(buffer, 0, buffer.Length);

                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                    bytesRead += (fileSize - bytesRead);
                }
                else
                {
                    var buffer = new byte[chunkSize];
                    fileStreamSource.Read(buffer, 0, buffer.Length);
                    fileStreamDestiny.Seek(0, SeekOrigin.Current);
                    fileStreamDestiny.Write(buffer, 0, buffer.Length);
                    bytesRead += buffer.Length;
                }

                fileStreamDestiny.Close();
                Hash hasher = new Hash();
                string hash=hasher.HashFile(destiny + @"\temp");
                System.IO.File.AppendAllText(contentDestiny, hash + Environment.NewLine);
                if (System.IO.File.Exists(destiny + @"\" + hash))
                    System.IO.File.Delete(destiny + @"\temp");
                else
                    System.IO.File.Move(destiny + @"\temp", destiny + @"\" + hash);

            }

            fileStreamSource.Close();

        }

    }

}
