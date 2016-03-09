using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatcherLibrary
{
    class DiffMaker
    {

        #region Global variables & type defs

        // runtime environment & debug mode
        Boolean debugMode = false;
        Boolean forceUnicodeMode = false;
        Boolean forceLongPath = false;
        String settingsFile = "";
        String programPath = "";
        String programDir = "";
        Boolean runningInWindows = false;
        Boolean useLongPath;

        Database settings;
        String quote = '"'.ToString();
        Int32 outputPlace = 0;
        Int32 currentlySelectedTab = 0;
        Int32 currentlyBeingEditedJob = 0;
        Boolean tryDetectingEpisodeNumber = false;
        Boolean useCustomParamenter = false;
        String customParamenter = "";
        Boolean run64bitxdelta = true;
        Boolean dist64bitxdelta = false;
        Boolean funnyMode = false;
        Boolean batchProcessingMode = false;

        // shared variables for batch processing
        Int32 jobsCount = 0;
        Boolean thisJobDone = false;
        String sourceFile_currentJob = "";
        String targetFile_currentJob = "";
        String outputDir_currentJob = "";

        public delegate void Int32_Delegate(Int32 index);
        public delegate void String_Delegate(String log);
        public delegate void Void_Delegate();

        struct EpNum
        {
            public String text;// = "";
            public Int32 number;// = -1;
            public Int32 length;// = 0;
            public Int32 priority;// = 0; 
        }

        #endregion
        private Boolean containNonASCIIChar(string inputString)
        {
            if (forceUnicodeMode) return true;
            string asAscii = "";

            // Strip non-ASCII characters from the string using Regex
            asAscii = Regex.Replace(inputString, @"[^\u0000-\u007F]", string.Empty);

            if (inputString != asAscii) return true;
            else return false;
        }
        private string getDirectoryName(string fullFilename)
        {
            string[] path = fullFilename.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.None);
            string currentPath = "";
            if (path.Length > 0)
            {
                if (runningInWindows && path[0].EndsWith(":") && path.Length > 2) currentPath = path[0] + "\\";
                else currentPath = path[0];
                for (int i = 1; i < path.Length - 1; i++)
                {
                    currentPath = Path.Combine(currentPath, path[i]);
                    //MessageBox.Show(currentPath);
                }
            }
            if (fullFilename.StartsWith("/") && !currentPath.StartsWith("/")) currentPath = "/" + currentPath; // use this dirty hack until a proper solution is make, that is, soon™
            return currentPath;
        }
        private Boolean isLongPath(string fullPath, Boolean isDir)
        {
            if (forceLongPath) return true;
            Boolean pathIsLong = false;
            if ((fullPath.Length > 255) || getDirectoryName(fullPath).Length > 247 || (fullPath.Length > 247 && isDir)) pathIsLong = true;
            else
            {
                string[] path = fullPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
                foreach (string tmp in path) if (tmp.Length > 247) pathIsLong = true;
                for (int i = 0; i < path.Length; i++)
                {
                    if ((path[i].Length > 247) && ((i == path.Length - 1) || isDir || path[i].Length > 255)) pathIsLong = true;
                }
            }
            return pathIsLong;
        }
        private string killAOption(string options)
        {
            string result = options;
            if (options.Contains("-A="))
            {
                int i = options.IndexOf("-A=");
                result = options.Substring(0, i);
                string tmp = options.Substring(i + 3);
                if (tmp.Length > 0)
                {
                    if (tmp[0] == '"')
                    {
                        // kill string in quotes
                        if (tmp.Length > 1)
                        {
                            int quote = tmp.IndexOf('"', 1);
                            if (quote >= 1 && quote + 1 < tmp.Length) result += " " + tmp.Substring(quote + 1);
                        }
                    }
                    else
                    {
                        // kill string before the first white space. If no space found, kill all
                        tmp = tmp.TrimEnd();
                        int space = tmp.IndexOf(' ');
                        if (space >= 0 && space + 1 < tmp.Length)
                        {
                            result += " " + tmp.Substring(space + 1);
                        }
                    }
                }
            }
            return result;
        }

        public void createPatch(string _sourceFile, string _targetFile, string _outDir)
        {
            string sourceFile = _sourceFile;
            string targetFile = _targetFile;
            string outputDir = _outDir;
            string target = System.IO.Path.Combine(outputDir, "temp.vcdiff");

            Process xdelta = new Process();
            xdelta.StartInfo.CreateNoWindow = true;
            if ( run64bitxdelta) xdelta.StartInfo.FileName = @"E:\Develop\AutoPatcher2\PatcherLibrary\xdelta3.x86_64.exe";
            else xdelta.StartInfo.FileName = "xdelta3"; // Works with xdelta3.exe and xdelta3 package, doesn't work with ./xdelta3
            xdelta.StartInfo.UseShellExecute = false;
            xdelta.StartInfo.RedirectStandardOutput = false;
            xdelta.StartInfo.RedirectStandardError = false;

            xdelta.StartInfo.Arguments = "-D -R -e -s %source% %patched% %vcdiff%";
            

            if (true) xdelta.StartInfo.Arguments = "-D -R " + xdelta.StartInfo.Arguments; // to avoid decompression and recompression on certain format like gz. Can be overriden


            // Needs to kill -A option first 'cause xdelta3 always takes the last one
            xdelta.StartInfo.Arguments = killAOption(xdelta.StartInfo.Arguments);
            if (true) xdelta.StartInfo.Arguments = "-A=\"" + Path.GetFileName(targetFile) + "//" + Path.GetFileName(sourceFile) + "/\" " + xdelta.StartInfo.Arguments;
           
            xdelta.StartInfo.Arguments = xdelta.StartInfo.Arguments.Replace("%source%", quote + sourceFile + quote).Replace("%patched%", quote + targetFile + quote).Replace("%vcdiff%", quote + target + quote);

            xdelta.Start();
         
            xdelta.WaitForExit();

        }
    }
}
