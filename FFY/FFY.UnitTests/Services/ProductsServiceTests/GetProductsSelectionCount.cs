using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.ProductsServiceTests
{
    [TestFixture]
    public class GetProductsSelectionCount
    {
        [Test]
        public void ShouldReturnAllProductsCount_WhenNoSearchWordFromAndToAreProvided()
        {
            // Arrange
            var filterBy = "all";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, null, null, null);

            // Assert
            Assert.AreEqual(products.Count, result);
        }

        [Test]
        public void ShouldReturnAllProductsCount_WhenSearchWordIsProvided()
        {
            // Arrange
            var filterBy = "all";
            var search = "bed";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Cupboard" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, search, null, null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ShouldReturnAllProductsCount_WhenSearchWordAndFromParameterAreProvided()
        {
            // Arrange
            var filterBy = "all";
            var search = "a";
            var from = 80;
            var products = new List<Product>()
            {
                new Product() { Name = "Bed", DiscountedPrice = 50 },
                new Product() { Name = "Chair", DiscountedPrice = 100 },
                new Product() { Name = "Cupboard", DiscountedPrice = 40 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, search, from, null);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ShouldReturnAllProductsCount_WhenSearchWordAndToParameterAreProvided()
        {
            // Arrange
            var filterBy = "all";
            var search = "a";
            var to = 60;
            var products = new List<Product>()
            {
                new Product() { Name = "Bed", DiscountedPrice = 50 },
                new Product() { Name = "Chair", DiscountedPrice = 100 },
                new Product() { Name = "Cupboard", DiscountedPrice = 40 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, search, null, to);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ShouldReturnAllProductsCount_WhenSearchWordFromAndToParameterAreProvided()
        {
            // Arrange
            var filterBy = "all";
            var search = "a";
            var from = 20;
            var to = 120;
            var products = new List<Product>()
            {
                new Product() { Name = "Bed", DiscountedPrice = 50 },
                new Product() { Name = "Chair", DiscountedPrice = 100 },
                new Product() { Name = "Cupboard", DiscountedPrice = 40 },
                new Product() { Name = "Wardrobe", DiscountedPrice = 200 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, search, from, to);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void ShouldReturnLatestProductsCount_WhenNoSearchWordFromAndToAreProvided()
        {
            // Arrange
            var filterBy = "latest";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, null, null, null);

            // Assert
            Assert.AreEqual(products.Count, result);
        }

        [Test]
        public void ShouldReturnRatingProductsCount_WhenNoSearchWordFromAndToAreProvided()
        {
            // Arrange
            var filterBy = "rating";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, null, null, null);

            // Assert
            Assert.AreEqual(products.Count, result);
        }

        [Test]
        public void ShouldReturnDiscountProductsCount_WhenNoSearchWordFromAndToAreProvided()
        {
            // Arrange
            var filterBy = "discount";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed", HasDiscount = true },
                new Product() { Name = "Chair", HasDiscount = true },
                new Product() { Name = "Desk", HasDiscount = false }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, null, null, null);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void ShouldReturnProductsByRoomCount_WhenNoSearchWordFromAndToAreProvided()
        {
            // Arrange
            var filterBy = "kitchen";
            var products = new List<Product>()
            {
                new Product() { Room = new Room() { Name = "Kitchen" } },
                new Product() { Room = new Room() { Name = "Bedroom" } },
                new Product() { Room = new Room() { Name = "Bathroom" } },
                new Product() { Room = new Room() { Name = "Kitchen" } }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelectionCount(filterBy, null, null, null);

            // Assert
            Assert.AreEqual(2, result);
        }
    }
}
