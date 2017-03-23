using FFY.Services.Utilities.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Utilities
{
    public class PathProvider : IPathProvider
    {
        public string GetFileName(string fileName)
        {
            return Path.GetFileName(fileName);
        }
    }
}
