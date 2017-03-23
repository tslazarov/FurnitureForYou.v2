using FFY.Services.Utilities.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Utilities
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool IsDirectoryExisting(string path)
        {
            return Directory.Exists(path);
        } 
    }
}
