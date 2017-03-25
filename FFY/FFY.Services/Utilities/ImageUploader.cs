using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Utilities.Providers;
using System.Web;

namespace FFY.Services.Utilities
{
    public class ImageUploader : IImageUploader
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IDirectoryProvider directoryProvider;
        private readonly IPathProvider pathProvider;

        public ImageUploader(IDateTimeProvider dateTimeProvider,
            IDirectoryProvider directoryProvider,
            IPathProvider pathProvider)
        {
            Guard.WhenArgument<IDateTimeProvider>(dateTimeProvider, "Date time provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IDirectoryProvider>(directoryProvider, "Directory provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IPathProvider>(pathProvider, "Path provider cannot be null.")
                .IsNull()
                .Throw();

            this.dateTimeProvider = dateTimeProvider;
            this.directoryProvider = directoryProvider;
            this.pathProvider = pathProvider;
        }

        public string Upload(HttpPostedFileBase image, HttpServerUtilityBase server, string imageFileName, string folderName)
        {
            if (image.ContentLength > 0 && (image.ContentType == "image/png" || image.ContentType == "image/jpeg"))
            {
                string subPath = @"~\Images\" + folderName;

                if (!this.directoryProvider.IsDirectoryExisting(server.MapPath(subPath)))
                {
                    this.directoryProvider.CreateDirectory(server.MapPath(subPath));
                }

                imageFileName = (this.dateTimeProvider.GetCurrentTime() - this.dateTimeProvider.GetCurrentTime().AddYears(-50))
                    .TotalMinutes.ToString() + this.pathProvider.GetFileName(image.FileName);

                image.SaveAs(server.MapPath(subPath + @"\" + imageFileName));

                return imageFileName;
            }

            return imageFileName;
        }
    }
}
