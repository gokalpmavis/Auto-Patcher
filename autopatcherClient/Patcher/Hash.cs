using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;

namespace Patcher.Client
{
    
    public class Hash
    {
        public string HashFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return HashFile(fs);
            }
        }

        public string HashFile(FileStream stream)
        {
            StringBuilder sb = new StringBuilder();

            if (stream != null)
            {
                stream.Seek(0, SeekOrigin.Begin);

                MD5 md5 = MD5CryptoServiceProvider.Create();
                byte[] hash = md5.ComputeHash(stream);
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));

                stream.Seek(0, SeekOrigin.Begin);
            }

            return sb.ToString();
        }
        
    }
}
