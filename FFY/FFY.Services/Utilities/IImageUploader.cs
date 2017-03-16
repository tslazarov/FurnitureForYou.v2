using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFY.Services.Utilities
{
    public interface IImageUploader
    {
        string Upload(HttpPostedFileBase image, HttpServerUtilityBase server, string imageFileName, string folderName);
    }
}
