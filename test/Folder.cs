using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    public class Folder
    {
        public int IndentFolder;
        public int Flag;
        public List<File> FileList { get; set; }
        public string Name { get; set; }
        public Folder()
        {
            Flag = 0;
        }

    }
}
