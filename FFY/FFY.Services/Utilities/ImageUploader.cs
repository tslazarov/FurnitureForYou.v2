using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFY.Services.Utilities
{
    public class ImageUploader : IImageUploader
    {
        public string Upload(HttpPostedFileBase image, HttpServerUtilityBase server, string imageFileName, string folderName)
        {
            if (image.ContentLength > 0)
            {
                if (image.ContentType == "image/png" || image.ContentType == "image/jpeg")
                {
                    string subPath = @"~\Images\" + folderName;

                    if (Directory.Exists(server.MapPath(subPath)))
                    {
                        Directory.CreateDirectory(server.MapPath(subPath));
                    }

                    // Not testable, but if we want to assure uniqueness of a file name we have to use some random factor
                    imageFileName = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMinutes.ToString() + Path.GetFileName(image.FileName);

                    image.SaveAs(server.MapPath(subPath + @"\" + imageFileName));

                    return imageFileName;
                }
            }

            return imageFileName;
        }
    }
}
