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
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullDateTimeProviderIsPassed()
        {
            // Arrange
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            var mockedPathProvider = new Mock<IPathProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ImageUploader(null,
                    mockedDirectoryProvider.Object,
                    mockedPathProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDateTimeProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Date time provider cannot be null.";

            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            var mockedPathProvider = new Mock<IPathProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ImageUploader(null,
                mockedDirectoryProvider.Object,
                mockedPathProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullDirectoryProviderIsPassed()
        {
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedPathProvider = new Mock<IPathProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ImageUploader(mockedDateTimeProvider.Object,
                    null,
                    mockedPathProvider.Object));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDirectoryProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Directory provider cannot be null.";

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedPathProvider = new Mock<IPathProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ImageUploader(mockedDateTimeProvider.Object,
                null,
                mockedPathProvider.Object));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullPathProviderIsPassed()
        {
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new ImageUploader(mockedDateTimeProvider.Object,
                    mockedDirectoryProvider.Object,
                    null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullPathProviderIsPassed()
        {
            // Arrange
            var expectedExMessage = "Path provider cannot be null.";

            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ImageUploader(mockedDateTimeProvider.Object,
                mockedDirectoryProvider.Object,
                null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }
        [Test]
        public void ShouldNotThrow_WhenValidDependenciesArePassed()
        {
            // Arrange
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedDirectoryProvider = new Mock<IDirectoryProvider>();
            var mockedPathProvider = new Mock<IPathProvider>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new ImageUploader(mockedDateTimeProvider.Object,
                mockedDirectoryProvider.Object,
                mockedPathProvider.Object));
        }
    }
}
