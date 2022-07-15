using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiPayout.Framework.Utilities
{
    public class FileUtility
    {
        public static string GetFileContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public static string GetFullFilePath(string file)
        {
            return Directory.GetCurrentDirectory() + "/" + file;
        }
    }
}
