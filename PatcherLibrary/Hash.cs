using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;

namespace PatchLibrary
{
    
    class Hash
    {
        public string HashFile(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return HashFile(fileStream);
            }
        }

        public string HashFile(FileStream stream)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (stream != null)
            {
                stream.Seek(0, SeekOrigin.Begin);

                MD5 md5 = MD5CryptoServiceProvider.Create();
                byte[] hash = md5.ComputeHash(stream);
                foreach (byte size in hash)
                    stringBuilder.Append(size.ToString("x2"));

                stream.Seek(0, SeekOrigin.Begin);
            }

            return stringBuilder.ToString();
        }
        
    }
}
