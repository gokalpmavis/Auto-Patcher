using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PatchLibrary
{
    public enum FileComparisionOption
    {
        None,
        IgnorePath
    }

    public class FileIdentifier:IComparable<FileIdentifier>
    {
        private string _fileName;
        private DateTime modifiedTime;
        private long _size;
        private bool _isFolder;

        public FileIdentifier() { }

        public FileIdentifier(FileIdentifier original)
        {
            this.Filename = original.Filename;
            this.ModifiedTime = original.ModifiedTime;
            this.Size = original.Size;
            this.IsFolder = original.IsFolder;
        }

        public bool IsFolder
        {
            get { return _isFolder; }
            set { _isFolder = value; }
        }

        public string Filename
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public System.DateTime ModifiedTime
        {
            get { return modifiedTime; }
            set { modifiedTime = value; }
        }
        public long Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public bool Equals(FileIdentifier fi)
        {
            if(this.IsFolder  && fi.IsFolder)
            {
                return this.Filename.Equals(fi.Filename,StringComparison.CurrentCultureIgnoreCase);
            }
            if (this.IsFolder == fi.IsFolder == true)
            {
                return (this.Filename.Equals(fi.Filename, StringComparison.CurrentCultureIgnoreCase))
                    && (this.Size == fi.Size)
                    && (this.ModifiedTime == fi.ModifiedTime);
            }
            return false;
        }

        public bool Equals(FileIdentifier fi,FileComparisionOption option)
        {
            if (option == FileComparisionOption.None)
            {
                return this.Equals(fi);
            }
            else if (option == FileComparisionOption.IgnorePath)
            {
                if (this.IsFolder && fi.IsFolder)
                {
                    return Path.GetFileName(this.Filename).Equals(Path.GetFileName(fi.Filename), StringComparison.CurrentCultureIgnoreCase);
                }
                if (this.IsFolder == fi.IsFolder == true)
                {
                    return (Path.GetFileName(this.Filename).Equals(Path.GetFileName(fi.Filename), StringComparison.CurrentCultureIgnoreCase))
                        && (this.Size == fi.Size)
                        && (this.ModifiedTime == fi.ModifiedTime);
                }
            }
            
            return false;
        }

        public int CompareTo(FileIdentifier f2)
        {
            return (this.Size - f2.Size < 0) ? 1 : ((this.Size == f2.Size) ? 0 : -1);
        }
    }
    
}


