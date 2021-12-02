using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectZavod.ViewModels
{
    public static class FileSystemExtensions
    {
        public static string[] GetFoldersFromDirectory(this string directory)
        {
            return Directory.GetDirectories(directory);
        }

        public static string[] GetFilesFromDirectory(this string directory)
        {
            return Directory.GetFiles(directory);
        }
    }
}
