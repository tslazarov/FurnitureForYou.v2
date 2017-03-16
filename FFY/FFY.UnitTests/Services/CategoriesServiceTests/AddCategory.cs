using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Services.CategoriesServiceTests
{
    [TestFixture]
    public class AddCategory
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCategoryIsPassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => categoriesService.AddCategory(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCategoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Category cannot be null.";

            var mockedData = new Mock<IFFYData>();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                categoriesService.AddCategory(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldCallAddMethodOfDataCategoryRepository()
        {
            // Arrange
            var mockedCategory = new Mock<Category>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CategoriesRepository.Add(It.IsAny<Category>()))
                .Verifiable();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act
            categoriesService.AddCategory(mockedCategory.Object);

            // Assert
            mockedData.Verify(d =>
                d.CategoriesRepository.Add(mockedCategory.Object), Times.Once);
        }

        [Test]
        public void ShouldCallSaveChangesMethodOfData()
        {
            // Arrange
            var mockedCategory = new Mock<Category>();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CategoriesRepository.Add(It.IsAny<Category>()))
                .Verifiable();
            mockedData.Setup(d => d.SaveChanges()).Verifiable();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act
            categoriesService.AddCategory(mockedCategory.Object);

            // Assert
            mockedData.Verify(d =>
                d.SaveChanges(), Times.Once);
        }
    }
}
