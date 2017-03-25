using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.CategoriesServiceTests
{
    [TestFixture]
    public class GetCategories
    {
        [Test]
        public void ShouldCallAllMethodOfDataCategoriesRepository()
        {
            // Arrange
            var categories = new List<Category>() {
                new Category() { Name = "Tables" },
                new Category() { Name = "Beds" }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CategoriesRepository.All())
                .Returns(categories.AsQueryable())
                .Verifiable();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act
            categoriesService.GetCategories();

            // Assert
            mockedData.Verify(d => d.CategoriesRepository.All(), Times.Once);
        }

        [Test]
        public void ShouldReturnListWithAllRooms()
        {
            // Arrange
            var categories = new List<Category>() {
                new Category() { Name = "Tables" },
                new Category() { Name = "Beds" }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CategoriesRepository.All())
                .Returns(categories.AsQueryable())
                .Verifiable();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act
            var result = categoriesService.GetCategories();

            // Assert
            CollectionAssert.AreEqual(categories, result);
        }
    }
}
