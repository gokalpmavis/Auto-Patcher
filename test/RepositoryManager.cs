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
using test;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Windows.Forms;
using BsDiff;
namespace test
{
    public class RepositoryManager
    {

       
        public int[] ProcessFolders(string textBox1Content, string textBox2Content,string version)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int[] result=new int[3];
            int insertFlag=0, deleteFlag=0, updateFlag = 0;
            List<string> insert = new List<string>();
            List<string> delete = new List<string>();
            List<string> update = new List<string>();
            List<Folder> insertFolder = new List<Folder>();
            List<Folder> deleteFolder = new List<Folder>();
            Repository repositoryNewVersion = new Repository(textBox1Content);
            Repository repositoryOldVersion = new Repository(textBox2Content);
            repositoryNewVersion.RepositoryContent(textBox1Content, 0);
            repositoryOldVersion.RepositoryContent(textBox2Content, 0);


            foreach (Folder temp in repositoryNewVersion.FolderArray)
            {
                int pointer1 = 0, pointer2 = 0;
                int flag = 0;
                for (pointer1 = 0; String.Compare((repositoryOldVersion.FolderArray[pointer1].Name), (temp.Name)) != 0 && insertFlag == 0; pointer1++)
                {
                    if (pointer1 == (repositoryOldVersion.FolderArray.Count) - 1)
                    {
                        insertFlag = 1;
                        insertFolder.Add(temp);
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0 && !(insertFlag==1 && updateFlag==1))
                {
                    foreach (File temp1 in temp.FileList)
                    {
                        flag = 0;
                        string at = repositoryOldVersion.FolderArray[pointer1].Name;
                        for (pointer2 = 0; repositoryOldVersion.FolderArray[pointer1].Flag == 1 && String.Compare((((repositoryOldVersion.FolderArray[pointer1]).FileList)[pointer2]).Name, (temp1.Name)) != 0 && insertFlag == 0; pointer2++)
                        {
                            if (pointer2 == ((repositoryOldVersion.FolderArray[pointer1].FileList.Count) - 1))
                            {

                                insertFlag = 1;
                                insert.Add(temp.Name + @"\" + temp1.Name);
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {

                            if (repositoryOldVersion.FolderArray[pointer1].Flag == 1 && String.Compare(((repositoryOldVersion.FolderArray[pointer1].FileList)[pointer2]).Hash, (temp1.Hash)) != 0)
                            {
                                Console.WriteLine(temp1.Name+ " geldim");
                                update.Add(temp.Name + @"\" + temp1.Name);
                                updateFlag = 1;
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
                        deleteFlag = 1;
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0 && deleteFlag==0)
                {
                    foreach (File temp1 in temp.FileList)
                    {
                        string at = repositoryNewVersion.FolderArray[pointer1].Name;
                        if (!(temp1.Name ==  "content.txt" || temp1.Name ==  "patchResult.txt"))
                        {
                            for (pointer2 = 0; repositoryNewVersion.FolderArray[pointer1].Flag == 1 && deleteFlag == 0 && String.Compare((((repositoryNewVersion.FolderArray[pointer1]).FileList)[pointer2]).Name, (temp1.Name)) != 0; pointer2++)
                            {
                                if (pointer2 == ((repositoryNewVersion.FolderArray[pointer1].FileList.Count) - 1))
                                {
                                    deleteFlag = 1;
                                    delete.Add(temp.Name + @"\" + temp1.Name);
                                    break;
                                }
                            }
                        }
                    }

                }

            }
            result[0] = insertFlag;
            result[1] = updateFlag;
            result[2] = deleteFlag;
            return result;
        }

    }

}
