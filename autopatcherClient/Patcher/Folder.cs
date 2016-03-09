using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patcher.Client
{
    public class Folder
    {
        public int Indent;
        public List<File> FileList { get; set; }
        public string Name { get; set; }
        public Folder()
        {
        }

    }
}
