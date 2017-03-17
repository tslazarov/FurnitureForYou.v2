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
    public class GetCategoryById
    {
        [TestCase(1, "Tables")]
        [TestCase(1, "Beds")]
        public void ShouldCallGetByIdMethodOfDataCategoriesRepository(int id, string name)
        {
            // Arrange
            var category = new Category() { Id = id, Name = name };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CategoriesRepository.GetById(It.IsAny<int>()))
                .Returns(category)
                .Verifiable();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act
            categoriesService.GetCategoryById(id);

            // Assert
            mockedData.Verify(d => d.CategoriesRepository.GetById(id), Times.Once);
        }

        [TestCase(1, "Desks")]
        [TestCase(1, "Chairs")]
        public void ShouldReturnCorrectCategory(int id, string name)
        {
            // Arrange
            var category = new Category() { Id = id, Name = name };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.CategoriesRepository.GetById(It.IsAny<int>()))
                .Returns(category)
                .Verifiable();

            var categoriesService = new CategoriesService(mockedData.Object);

            // Act
            var result = categoriesService.GetCategoryById(id);

            // Assert
            Assert.AreEqual(category, result);
        }
    }
}
