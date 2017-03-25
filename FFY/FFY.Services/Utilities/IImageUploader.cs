using System.Web;

namespace FFY.Services.Utilities
{
    public interface IImageUploader
    {
        string Upload(HttpPostedFileBase image, HttpServerUtilityBase server, string imageFileName, string folderName);
    }
}
