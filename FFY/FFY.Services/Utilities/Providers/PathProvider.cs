using FFY.Services.Utilities.Providers;
using System.IO;

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
