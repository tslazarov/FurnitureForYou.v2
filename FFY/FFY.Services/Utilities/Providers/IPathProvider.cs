using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Utilities.Providers
{
    public interface IPathProvider
    {
        string GetFileName(string fileName);
    }
}
