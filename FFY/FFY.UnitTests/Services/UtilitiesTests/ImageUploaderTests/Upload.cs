using FFY.Providers.Contracts;
using FFY.Services.Utilities;
using FFY.Services.Utilities.Providers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFY.UnitTests.Services.UtilitiesTests.ImageUploaderTests
{
    [TestFixture]
    public class Upload
    {
        [TestCase("image-123", "folder")]
        [TestCase("image-426", "newfolder")]
        public void ShouldReturnDefaultImageFileName_WhenImageContentIsNotPositive(string imageDefaultName, string folder)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            var mockedPathProvider = new Mock<IPathProvider>();

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(0);
            var mockedServer = new Mock<HttpServerUtilityBase>();

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                mockedDirectoryProvider.Object,
                mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            Assert.AreEqual(imageDefaultName, imageName);
        }

        [TestCase("image-123", "folder")]
        [TestCase("image-426", "newfolder")]
        public void ShouldReturnDefaultImageFileName_WhenImageContentTypeIsNotJPEGOrPNG(string imageDefaultName, string folder)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            var mockedPathProvider = new Mock<IPathProvider>();

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(10);
            mockedImage.SetupGet(i => i.ContentType).Returns("invalid-format");
            var mockedServer = new Mock<HttpServerUtilityBase>();

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                 mockedDirectoryProvider.Object,
                 mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            Assert.AreEqual(imageDefaultName, imageName);
        }

        [TestCase("image-123", "folder")]
        public void ShouldCallIsDirectoryExistingMethodOfDirectoryProvider(string imageDefaultName, string folder)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(new DateTime(2017, 3, 23));
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            mockedDirectoryProvider.Setup(dp => dp.IsDirectoryExisting(It.IsAny<string>()))
                .Returns(true)
                .Verifiable();
            var mockedPathProvider = new Mock<IPathProvider>();
            mockedPathProvider.Setup(pp => pp.GetFileName(It.IsAny<string>()))
                .Returns<string>(s => s);

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(10);
            mockedImage.SetupGet(i => i.ContentType).Returns("image/jpeg");
            mockedImage.Setup(i => i.SaveAs(It.IsAny<string>()));

            var mockedServer = new Mock<HttpServerUtilityBase>();
            mockedServer.Setup(s => s.MapPath(It.IsAny<string>()))
                .Returns<string>(s => s);

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                 mockedDirectoryProvider.Object,
                 mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            mockedDirectoryProvider.Verify(dp => dp.IsDirectoryExisting(@"~\Images\" + folder), Times.Once);
        }

        [TestCase("image-123", "folder")]
        public void ShouldCallCreateDirectoryMethodOfDirectoryProvider_WhenDirectoryIsNotExisting(string imageDefaultName, string folder)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(new DateTime(2017, 3, 23));
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            mockedDirectoryProvider.Setup(dp => dp.IsDirectoryExisting(It.IsAny<string>()))
                .Returns(false)
                .Verifiable();
            var mockedPathProvider = new Mock<IPathProvider>();
            mockedPathProvider.Setup(pp => pp.GetFileName(It.IsAny<string>()))
                .Returns<string>(s => s);

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(10);
            mockedImage.SetupGet(i => i.ContentType).Returns("image/jpeg");
            mockedImage.Setup(i => i.SaveAs(It.IsAny<string>()));

            var mockedServer = new Mock<HttpServerUtilityBase>();
            mockedServer.Setup(s => s.MapPath(It.IsAny<string>()))
                .Returns<string>(s => s);

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                 mockedDirectoryProvider.Object,
                 mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            mockedDirectoryProvider.Verify(dp => dp.IsDirectoryExisting(@"~\Images\" + folder), Times.Once);
        }

        [TestCase("image-123", "folder")]
        public void ShouldCallMapPathMethodOfServerTwice_WhenDirectoryIsExisting(string imageDefaultName, string folder)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(new DateTime(2017, 3, 23));
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            mockedDirectoryProvider.Setup(dp => dp.IsDirectoryExisting(It.IsAny<string>()))
                .Returns(true);
            mockedDirectoryProvider.Setup(dp => dp.CreateDirectory(It.IsAny<string>())).Verifiable();
            var mockedPathProvider = new Mock<IPathProvider>();
            mockedPathProvider.Setup(pp => pp.GetFileName(It.IsAny <string>()))
                .Returns<string>(s => s);

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(10);
            mockedImage.SetupGet(i => i.ContentType).Returns("image/jpeg");
            mockedImage.Setup(i => i.SaveAs(It.IsAny<string>()));

            var mockedServer = new Mock<HttpServerUtilityBase>();
            mockedServer.Setup(s => s.MapPath(It.IsAny<string>()))
                .Returns<string>(s => s)
                .Verifiable();

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                 mockedDirectoryProvider.Object,
                 mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            mockedServer.Verify(s => s.MapPath(It.IsAny<string>()), Times.Exactly(2));
        }

        [TestCase("image-123", "folder")]
        public void ShouldCallMapPathMethodOfServerThreeTimes_WhenDirectoryIsNotExisting(string imageDefaultName, string folder)
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(new DateTime(2017, 3, 23));
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            mockedDirectoryProvider.Setup(dp => dp.IsDirectoryExisting(It.IsAny<string>()))
                .Returns(false);
            mockedDirectoryProvider.Setup(dp => dp.CreateDirectory(It.IsAny<string>()));
            var mockedPathProvider = new Mock<IPathProvider>();
            mockedPathProvider.Setup(pp => pp.GetFileName(It.IsAny<string>()))
                .Returns<string>(s => s);

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(10);
            mockedImage.SetupGet(i => i.ContentType).Returns("image/jpeg");
            mockedImage.Setup(i => i.SaveAs(It.IsAny<string>()));

            var mockedServer = new Mock<HttpServerUtilityBase>();
            mockedServer.Setup(s => s.MapPath(It.IsAny<string>()))
                .Returns<string>(s => s)
                .Verifiable();

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                 mockedDirectoryProvider.Object,
                 mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            mockedServer.Verify(s => s.MapPath(It.IsAny<string>()), Times.Exactly(3));
        }

        [TestCase("image-123", "folder", "cool-image.jpg")]
        public void ShouldCallSaveAsMethodOfOfImage(string imageDefaultName, string folder, string imageFileName)
        {
            // Arrange
            var currentDate = new DateTime(2017, 3, 23);
            var expectedRandomDate = (currentDate - currentDate.AddYears(-50))
                    .TotalMinutes.ToString();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(currentDate);
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            mockedDirectoryProvider.Setup(dp => dp.IsDirectoryExisting(It.IsAny<string>()))
                .Returns(false);
            mockedDirectoryProvider.Setup(dp => dp.CreateDirectory(It.IsAny<string>()));
            var mockedPathProvider = new Mock<IPathProvider>();
            mockedPathProvider.Setup(pp => pp.GetFileName(It.IsAny<string>()))
                .Returns<string>(s => s);

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(10);
            mockedImage.SetupGet(i => i.ContentType).Returns("image/jpeg");
            mockedImage.SetupGet(i => i.FileName).Returns(imageFileName);
            mockedImage.Setup(i => i.SaveAs(It.IsAny<string>())).Verifiable();

            var mockedServer = new Mock<HttpServerUtilityBase>();
            mockedServer.Setup(s => s.MapPath(It.IsAny<string>()))
                .Returns<string>(s => s);

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                 mockedDirectoryProvider.Object,
                 mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            mockedImage.Verify(i => i.SaveAs(@"~\Images\" + folder + @"\" + expectedRandomDate + imageFileName), Times.Once);
        }

        [TestCase("image-123", "folder", "cool-image.jpg")]
        public void ShouldReturnNewlyFormedImageName(string imageDefaultName, string folder, string imageFileName)
        {
            // Arrange
            var currentDate = new DateTime(2017, 3, 23);
            var expectedRandomDate = (currentDate - currentDate.AddYears(-50))
                    .TotalMinutes.ToString();

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            mockedDateTimeProvider.Setup(dtp => dtp.GetCurrentTime())
                .Returns(currentDate);
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            mockedDirectoryProvider.Setup(dp => dp.IsDirectoryExisting(It.IsAny<string>()))
                .Returns(false);
            mockedDirectoryProvider.Setup(dp => dp.CreateDirectory(It.IsAny<string>()));
            var mockedPathProvider = new Mock<IPathProvider>();
            mockedPathProvider.Setup(pp => pp.GetFileName(It.IsAny<string>()))
                .Returns<string>(s => s);

            var mockedImage = new Mock<HttpPostedFileBase>();
            mockedImage.SetupGet(i => i.ContentLength).Returns(10);
            mockedImage.SetupGet(i => i.ContentType).Returns("image/jpeg");
            mockedImage.SetupGet(i => i.FileName).Returns(imageFileName);
            mockedImage.Setup(i => i.SaveAs(It.IsAny<string>()));

            var mockedServer = new Mock<HttpServerUtilityBase>();
            mockedServer.Setup(s => s.MapPath(It.IsAny<string>()))
                .Returns<string>(s => s);

            var imageUploader = new ImageUploader(mockedDateTimeProvider.Object,
                 mockedDirectoryProvider.Object,
                 mockedPathProvider.Object);

            // Act
            var imageName = imageUploader.Upload(mockedImage.Object, mockedServer.Object, imageDefaultName, folder);

            // Assert
            Assert.AreEqual(expectedRandomDate + imageFileName, imageName);
        }
    }
}
