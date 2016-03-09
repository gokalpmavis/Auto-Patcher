using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
namespace MvcApplication1.Controllers
{
    public class RepositoryController : Controller
    {
        string _sharedDirectory = @"C:\inetpub\wwwroot\twpatcher\share\";

        public ActionResult GetLastVersion()
        {
            using (StreamReader reader = new StreamReader(_sharedDirectory+"lastVersion.txt"))
            {
                return Content(reader.ReadLine());
            }
        }

        public ActionResult GetNextVersion()
        {
            return GetLastVersion();
        }

        public FileStreamResult GetFile(string fileName)
        {
            FileStream fs=null;

            if (fileName == "")
                return null;

            while (fileName[0] == '\u005c')
                fileName = fileName.Substring(1, fileName.Length - 1);

            string truePath=fileName;

            if (System.IO.File.Exists( _sharedDirectory+ truePath))
            {
                fs = new FileStream(_sharedDirectory + truePath,FileMode.Open, FileAccess.Read);

                return new FileStreamResult(fs, "application/octet-stream");
            }

            else         
                return null;
         }
     }    
}
