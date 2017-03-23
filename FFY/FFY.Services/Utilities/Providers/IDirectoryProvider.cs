using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Utilities.Providers
{
    public interface IDirectoryProvider
    {
        void CreateDirectory(string path);

        bool IsDirectoryExisting(string path);
    }
}
