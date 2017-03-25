using FFY.Services.Utilities.Providers;
using System.IO;

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
