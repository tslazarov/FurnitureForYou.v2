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
    class SearchProducts
    {
        [Test]
        public void ShouldReturnAllProducts_WhenNoSearchWordIsProvided()
        {
            // Arrange
            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Table" },
                new Product() { Name = "Wardrobe" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(null, "", 1, 10);

            // Assert
            CollectionAssert.AreEquivalent(products, result);
        }

        [Test]
        public void ShouldReturnCorrectProduct_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "chair";

            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Table" },
                new Product() { Name = "Wardrobe" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(searchWord, "", 1, 10);

            // Assert
            Assert.AreSame(products[1], result.First());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollection_WhenSearchWordIsProvided()
        {
            // Arrange
            var searchWord = "d";

            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Wardrobe" },
                new Product() { Name = "Table" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(searchWord, "", 1, 10);

            // Assert
            Assert.AreSame(products[0], result.First());
            Assert.AreSame(products[2], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsSortedByName_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "a";

            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Table" },
                new Product() { Name = "Wardrobe" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);
            
            // Act
            var result = productsService.SearchProducts(searchWord, "name", 1, 10);

            // Assert
            Assert.AreSame(products[1], result.First());
            Assert.AreSame(products[3], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsSortedByPrice_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Price = 50 },
                new Product() { Name = "Chair", Price = 40 },
                new Product() { Name = "Table", Price = 70 },
                new Product() { Name = "Wardrobe", Price = 90 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(searchWord, "price", 1, 10);

            // Assert
            Assert.AreSame(products[3], result.First());
            Assert.AreSame(products[1], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsSortedByRoom_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Room = new Room() { Name = "Z"} },
                new Product() { Name = "Chair", Room = new Room() { Name = "A"} },
                new Product() { Name = "Table", Room = new Room() { Name = "C"} },
                new Product() { Name = "Wardrobe", Room = new Room() { Name = "E"} }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(searchWord, "room", 1, 10);

            // Assert
            Assert.AreSame(products[1], result.First());
            Assert.AreSame(products[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsSortedByCategory_WhenSearchWordAndSortParameterAreProvided()
        {
            // Arrange
            var searchWord = "";
            var products = new List<Product>()
            {
                new Product() { Name = "Bed", Category = new Category() { Name = "D"} },
                new Product() { Name = "Chair", Category = new Category() { Name = "A"} },
                new Product() { Name = "Table", Category = new Category() { Name = "Z"} },
                new Product() { Name = "Wardrobe", Category = new Category() { Name = "E"} }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(searchWord, "category", 1, 10);

            // Assert
            Assert.AreSame(products[1], result.First());
            Assert.AreSame(products[2], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsPerPage_WhenSearchWordAndPageAreProvided()
        {
            // Arrange
            var searchWord = "";

            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Table" },
                new Product() { Name = "Wardrobe" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(searchWord, "", 2, 2);

            // Assert
            Assert.AreSame(products[2], result.First());
            Assert.AreSame(products[3], result.Last());
        }

        [Test]
        public void ShouldReturnNoProducts_WhenNoProductsAreMatchingTheCriterias()
        {
            // Arrange
            var searchWord = "qwerty";

            var products = new List<Product>()
            {
                new Product() { Name = "Bed" },
                new Product() { Name = "Chair" },
                new Product() { Name = "Table" },
                new Product() { Name = "Wardrobe" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.SearchProducts(searchWord, "", 1, 10);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
