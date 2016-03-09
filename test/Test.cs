using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using Patcher.Client;
using Patcher.Diff;
using Patcher.Zipper;
using System.Threading;
using System.Windows.Forms;
namespace test
{

    [SetUpFixture]
    public class SetupClass
    {
        public string newVersion = @"C:\Users\gokalp.mavis\Desktop\Deneme";
        public string oldVersion1 = @"C:\Users\gokalp.mavis\Desktop\Deneme1";
        public string oldVersion2 = @"C:\Users\gokalp.mavis\Desktop\Deneme2";
        public string sharedDirectory = @"C:\inetpub\wwwroot\twpatcher\share";

        int temp = 0;

        public void repoCreator(string path, int size, int fileCount)
        {
            for (int count = 0; count < fileCount; count++)
            {
                Random random = new Random();
                FileStream fs = new FileStream(path + count.ToString(), FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();
                using (StreamWriter sw = new StreamWriter(path + count.ToString()))
                {

                    for (int i = 0; i < size * 10 + temp * 1000; i++)
                    {
                        sw.Write(i + " ");
                    }
                    
                    sw.Close();
                }
                temp++;

            }
        }
        
        public void fileEraser(string path, int offset, int fileCount)
        {
            for (int count = 0; count < fileCount; count++)
            {
                if (System.IO.File.Exists(path + (offset + count).ToString()))
                {
                    System.IO.File.Delete(path + (offset + count).ToString());
                }
            }
        }

        public void fileChanger(string path, int offset, int fileCount)
        {
            for (int count = 0; count < fileCount; count++)
            {
                if (System.IO.File.Exists(path + (offset + count).ToString()))
                {
                    using (FileStream stream = System.IO.File.OpenRead(path + (offset + count).ToString()))
                    using (FileStream writeStream = System.IO.File.OpenWrite(path + (offset + count).ToString() + ".temp"))
                    {
                        FileInfo info = new FileInfo(path + (offset + count));
                        int size = (int)info.Length;

                        BinaryReader reader = new BinaryReader(stream);
                        BinaryWriter writer = new BinaryWriter(writeStream);

                        // create a buffer to hold the bytes 
                        byte[] buffer = new Byte[size / 2];
                        // while the read method returns bytes
                        // keep writing them to the output stream

                        stream.Read(buffer, 0, size / 2);
                        writeStream.Write(buffer, 0, size / 2);

                    }
                }
                System.IO.File.Delete(path + (offset + count).ToString());
                System.IO.File.Move(path + (offset + count).ToString() + ".temp", path + (offset + count).ToString());
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        [SetUp]
        public void Setup()
        {
            
            if (System.IO.Directory.Exists(oldVersion2))
                Directory.Delete(oldVersion2, true);

            if (System.IO.Directory.Exists(oldVersion1))
                Directory.Delete(oldVersion1, true);

            if (System.IO.Directory.Exists(newVersion))
                Directory.Delete(newVersion, true);

            foreach (string file in System.IO.Directory.GetFiles(sharedDirectory))
            {
                System.IO.File.Delete(file);
            }
            foreach (string dir in System.IO.Directory.GetDirectories(sharedDirectory))
            {
                System.IO.Directory.Delete(dir, true);
            }

            System.IO.File.Create(sharedDirectory + @"\lastVersion.txt").Dispose();
            TextWriter tw = new StreamWriter(sharedDirectory + @"\lastVersion.txt", true);
            tw.WriteLine("3");
            tw.Close();

            Directory.CreateDirectory(newVersion);
            repoCreator(newVersion + @"\", 256, 100);

            Directory.CreateDirectory(newVersion + @"\Inner1");
            repoCreator(newVersion + @"\Inner1\", 256, 25);

            Directory.CreateDirectory(newVersion + @"\Inner2");
            repoCreator(newVersion + @"\Inner2\", 256, 25);

            Directory.CreateDirectory(newVersion + @"\Inner1\Inner1.1");
            repoCreator(newVersion + @"\Inner1\Inner1.1\", 256, 25);

            Directory.CreateDirectory(newVersion + @"\Inner1\Inner1.2");
            repoCreator(newVersion + @"\Inner1\Inner1.2\", 256, 25);

            Directory.CreateDirectory(newVersion + @"\Inner3");
            repoCreator(newVersion + @"\Inner3\", 256, 25);

            DirectoryCopy(newVersion, oldVersion1, true);

            fileEraser(oldVersion1 + @"\", 0, 30);
            fileChanger(oldVersion1 + @"\", 30, 30);

            fileEraser(oldVersion1 + @"\Inner1\", 0, 10);
            fileChanger(oldVersion1 + @"\Inner1\", 10, 10);

            fileEraser(oldVersion1 + @"\Inner2\", 0, 10);
            fileChanger(oldVersion1 + @"\Inner2\", 10, 10);

            fileEraser(oldVersion1 + @"\Inner1\Inner1.1\", 0, 10);
            fileChanger(oldVersion1 + @"\Inner1\Inner1.1\", 10, 10);

            fileEraser(oldVersion1 + @"\Inner1\Inner1.2\", 0, 10);
            fileChanger(oldVersion1 + @"\Inner1\Inner1.2\", 10, 10);

            Directory.Delete(oldVersion1 + @"\Inner3", true);

            DirectoryCopy(oldVersion1, oldVersion2, true);

            fileEraser(oldVersion2 + @"\", 60, 10);
            fileChanger(oldVersion2 + @"\", 30, 30);

            fileEraser(oldVersion2 + @"\Inner1\", 10, 4);
            fileChanger(oldVersion2 + @"\Inner1\", 20, 3);

            fileEraser(oldVersion2 + @"\Inner1\Inner1.1\", 10, 4);
            fileChanger(oldVersion2 + @"\Inner1\Inner1.1\", 20, 3);

            Directory.Delete(oldVersion1 + @"\Inner2", true);
            
        }
    }
    [TestFixture]
    public class Test
    {
        public string newVersion = @"C:\Users\gokalp.mavis\Desktop\Deneme";
        public string oldVersion1 = @"C:\Users\gokalp.mavis\Desktop\Deneme1";
        public string oldVersion2 = @"C:\Users\gokalp.mavis\Desktop\Deneme2";
        public string sharedDirectory = @"C:\inetpub\wwwroot\twpatcher\share";
        
        public static Semaphore semaphoreDiff1 = new Semaphore(0, 1);

        public static Semaphore semaphoreDiff2 = new Semaphore(0, 1);

        public static Semaphore semaphoreRepair1 = new Semaphore(0, 1);

        public static Semaphore semaphoreRepair2 = new Semaphore(0, 1);
        public void fileEraser(string path, int offset, int fileCount)
        {
            for (int count = 0; count < fileCount; count++)
            {
                if (System.IO.File.Exists(path + (offset + count).ToString()))
                {
                    System.IO.File.Delete(path + (offset + count).ToString());
                }
            }
        }

        public void fileChanger(string path, int offset, int fileCount)
        {
            for (int count = 0; count < fileCount; count++)
            {
                if (System.IO.File.Exists(path + (offset + count).ToString()))
                {
                    using (FileStream stream = System.IO.File.OpenRead(path + (offset + count).ToString()))
                    using (FileStream writeStream = System.IO.File.OpenWrite(path + (offset + count).ToString() + ".temp"))
                    {
                        FileInfo info = new FileInfo(path + (offset + count));
                        int size = (int)info.Length;

                        BinaryReader reader = new BinaryReader(stream);
                        BinaryWriter writer = new BinaryWriter(writeStream);

                        // create a buffer to hold the bytes 
                        byte[] buffer = new Byte[size / 2];
                        // while the read method returns bytes
                        // keep writing them to the output stream

                        stream.Read(buffer, 0, size / 2);
                        writeStream.Write(buffer, 0, size / 2);

                    }
                }

                System.IO.File.Delete(path + (offset + count).ToString());
                System.IO.File.Move(path + (offset + count).ToString() + ".temp", path + (offset + count).ToString());
            }
        }

        [Test]
        public void a()//testRepair1()
        {

            Patcher.Zipper.RepairForm formRepair = new Patcher.Zipper.RepairForm();
            formRepair.testFlag = 1;
            formRepair.sourceFolderText.Text = oldVersion2;
            formRepair.textBox4.Text = "1";
            formRepair.targetFolderText.Text = sharedDirectory;
            Thread thread = new Thread(() => formRepair.publishBtn_Click(new object(), new EventArgs()));
            thread.Start();
            Patcher.Zipper.RepairForm.semaphore.WaitOne();
            Hash hash = new Hash();
            string realContent = @"E:\Develop\AutoPatcher2\test\1content.txt";
            Assert.AreEqual(hash.HashFile(realContent), hash.HashFile(sharedDirectory + @"\1_content.txt"));

        }

        [Test]
        public void b()//testRepair2()
        {

            Patcher.Zipper.RepairForm formRepair = new Patcher.Zipper.RepairForm();
            formRepair.testFlag = 1;
            formRepair.sourceFolderText.Text = oldVersion1;
            formRepair.textBox4.Text = "2";
            formRepair.targetFolderText.Text = sharedDirectory;
            //formRepair.textBox1.Text = oldVersion2;
            Thread thread = new Thread(() => formRepair.publishBtn_Click(new object(), new EventArgs()));
            thread.Start();
            Patcher.Zipper.RepairForm.semaphore.WaitOne();
            Hash hash = new Hash();
            string realContent = @"E:\Develop\AutoPatcher2\test\2content.txt";
            Assert.AreEqual(hash.HashFile(realContent), hash.HashFile(sharedDirectory + @"\2_content.txt"));

        }

        [Test]
        public void c()//testRepair2()
        {

            Patcher.Zipper.RepairForm formRepair = new Patcher.Zipper.RepairForm();
            formRepair.testFlag = 1;
            formRepair.sourceFolderText.Text = newVersion;
            formRepair.textBox4.Text = "3";
            formRepair.targetFolderText.Text = sharedDirectory;
            //formRepair.textBox1.Text = oldVersion2;
            Thread thread = new Thread(() => formRepair.publishBtn_Click(new object(), new EventArgs()));
            thread.Start();
            Patcher.Zipper.RepairForm.semaphore.WaitOne();
            Hash hash = new Hash();
            string realContent = @"E:\Develop\AutoPatcher2\test\3content.txt";
            Assert.AreEqual(hash.HashFile(realContent), hash.HashFile(sharedDirectory + @"\3_content.txt"));

        }

        [Test]
        public void d()// testDiff1()
        {

            Patcher.Diff.DiffForm formDiff = new Patcher.Diff.DiffForm();
            formDiff.testFlag = 1;
            formDiff.versionNew = "3";
            formDiff.versionOld = "2";
            formDiff.newVersionPath.Text = newVersion;
            formDiff.oldVersionPath.Text = oldVersion1;
            formDiff.destinyPath.Text = sharedDirectory;
            formDiff.Form1_Load(new object(), new EventArgs());
            formDiff.button1_Click(new object(), new EventArgs());
            Patcher.Diff.DiffForm.semaphore.WaitOne();
            FileInfo fileInfo = new FileInfo(sharedDirectory + @"\2-3_insert.cmpr");
            Assert.AreEqual(87083820, fileInfo.Length);
            Assert.AreEqual(60, System.IO.File.ReadLines(sharedDirectory + @"\2-3_updatecontent.txt").Count());

        }

        [Test]
        public void e()//testDiff2()
        {

            Patcher.Diff.DiffForm formDiff = new Patcher.Diff.DiffForm();
            formDiff.testFlag = 1;
            formDiff.versionNew = "3";
            formDiff.versionOld = "1";
            formDiff.newVersionPath.Text = newVersion;
            formDiff.oldVersionPath.Text = oldVersion2;
            formDiff.destinyPath.Text = sharedDirectory;
            formDiff.Form1_Load(new object(), new EventArgs());
            formDiff.button1_Click(new object(), new EventArgs());
            Patcher.Diff.DiffForm.semaphore.WaitOne();
            FileInfo fileInfo = new FileInfo(sharedDirectory + @"\1-3_insert.cmpr");
            Assert.AreEqual(84382650, fileInfo.Length);
            Assert.AreEqual(68, System.IO.File.ReadLines(sharedDirectory + @"\1-3_updatecontent.txt").Count());

        }


        [Test]
        public void f()//testClientPatch1()
        {
            
            fileEraser(oldVersion1 + @"\", 30, 15);
            fileChanger(oldVersion1 + @"\", 60, 10);

            fileEraser(oldVersion1 + @"\Inner1\", 10, 4);
            fileChanger(oldVersion1 + @"\Inner1\", 15, 5);
            
            Patcher.Client.PatcherForm formClient = new Patcher.Client.PatcherForm();
            formClient.InitializeComponent();
            formClient.TestFlag = 1;
            Patcher.Client.PatcherForm.version = "2";
            formClient.textBox1.Text = oldVersion1;
            formClient.Form1_Load(new object(), new EventArgs());
            formClient.button1_Click(new object(), new EventArgs());
            Patcher.Client.PatcherForm.semaphore.WaitOne();

            test.RepositoryManager repo = new test.RepositoryManager();
            int[] result = repo.ProcessFolders(newVersion, oldVersion1, "2");
            int insertNeed = result[0];
            int updateNeed = result[1];
            int deleteNeed = result[2];
            Assert.AreEqual(insertNeed, 0);
            Assert.AreEqual(updateNeed, 0);
            Assert.AreEqual(deleteNeed, 0);

            return;

        }

        [Test]
        public void g()//testClientPatch2()
        {

            fileEraser(oldVersion2 + @"\", 70, 10);
            fileChanger(oldVersion2 + @"\", 80, 10);

            fileEraser(oldVersion2 + @"\Inner1\", 15, 4);
            fileChanger(oldVersion2 + @"\Inner1\", 20, 3);

            Patcher.Client.PatcherForm formClient = new Patcher.Client.PatcherForm();
            formClient.InitializeComponent();
            formClient.TestFlag = 1;
            Patcher.Client.PatcherForm.version = "1";
            formClient.textBox1.Text = oldVersion2;
            formClient.Form1_Load(new object(), new EventArgs());
            formClient.button1_Click(new object(), new EventArgs());
            Patcher.Client.PatcherForm.semaphore.WaitOne();

            test.RepositoryManager repo = new test.RepositoryManager();
            int[] result = repo.ProcessFolders(newVersion, oldVersion2, "1");
            int insertNeed = result[0];
            int updateNeed = result[1];
            int deleteNeed = result[2];
            Assert.AreEqual(insertNeed, 0);
            Assert.AreEqual(updateNeed, 0);
            Assert.AreEqual(deleteNeed, 0);

            return;

        }
    }
}