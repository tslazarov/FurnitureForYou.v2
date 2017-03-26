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
    public class GetProductsSelection
    {
        [Test]
        public void ShouldReturnAllProducts_WhenNoSearchWordAndFromToParametersAreProvided()
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
            var result = productsService.GetProductsSelection("all", "", null, null, 1, 16);

            // Assert
            CollectionAssert.AreEquivalent(products, result);
        }

        [Test]
        public void ShouldReturnProductsMatchingTheSearchingWord_WhenSearchWordIsProvided()
        {
            var search = "bed";

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
            var result = productsService.GetProductsSelection("all", search, null, null, 1, 16);

            // Assert
            Assert.AreSame(products[0], result.First());
        }
        
        [Test]
        public void ShouldReturnCorrectProductsCollection_WhenSearchWordIsProvided()
        {
            // Arrange
            var search = "d";

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
            var result = productsService.GetProductsSelection("all", search, null, null, 1, 16);

            // Assert
            Assert.AreSame(products[0], result.First());
            Assert.AreSame(products[2], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollection_WhenSearchWordAndFromParameterAReProvided()
        {
            // Arrange
            var search = "a";
            var from = 100;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", DiscountedPrice = 50 },
                new Product() { Name = "Chair", DiscountedPrice = 60 },
                new Product() { Name = "Wardrobe", DiscountedPrice = 110 },
                new Product() { Name = "Table", DiscountedPrice = 80 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection("all", search, from, null, 1, 16);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreSame(products[2], result.First());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollection_WhenSearchWordAndToParameterAReProvided()
        {
            // Arrange
            var search = "a";
            var to = 100;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", DiscountedPrice = 50 },
                new Product() { Name = "Chair", DiscountedPrice = 60 },
                new Product() { Name = "Wardrobe", DiscountedPrice = 110 },
                new Product() { Name = "Table", DiscountedPrice = 80 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection("all", search, null, to, 1, 16);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreSame(products[1], result.First());
            Assert.AreSame(products[3], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollection_WhenSearchWordFromAndToParameterAReProvided()
        {
            // Arrange
            var search = "a";
            var from = 70;
            var to = 100;

            var products = new List<Product>()
            {
                new Product() { Name = "Bed", DiscountedPrice = 50 },
                new Product() { Name = "Chair", DiscountedPrice = 60 },
                new Product() { Name = "Wardrobe", DiscountedPrice = 110 },
                new Product() { Name = "Table", DiscountedPrice = 80 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection("all", search, from, to, 1, 16);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreSame(products[3], result.First());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollectionSortedByName_WhenSearchWordAndFilterAreProvided()
        {
            // Arrange
            var search = "a";
            var filterBy = "all";

            var products = new List<Product>()
            {
                new Product() { Name = "Wardrobe" },
                new Product() { Name = "Bed" },
                new Product() { Name = "Table" },
                new Product() { Name = "Chair" }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection(filterBy, search, null, null, 1, 16);

            // Assert
            Assert.AreSame(products[3], result.First());
            Assert.AreSame(products[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollectionSortedLatestAddition_WhenSearchWordAndFilterAreProvided()
        {
            // Arrange
            var search = "a";
            var filterBy = "latest";

            var products = new List<Product>()
            {
                new Product() { Id = 3, Name = "Wardrobe" },
                new Product() { Id = 1, Name = "Bed", DiscountedPrice = 50 },
                new Product() { Id = 2, Name = "Table", DiscountedPrice = 80 },
                new Product() { Id = 4, Name = "Chair", DiscountedPrice = 60 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection(filterBy, search, null, null, 1, 16);

            // Assert
            Assert.AreSame(products[3], result.First());
            Assert.AreSame(products[2], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollectionSortedByRating_WhenSearchWordAndFilterAreProvided()
        {
            // Arrange
            var search = "a";
            var filterBy = "rating";

            var products = new List<Product>()
            {
                new Product() { Rating = 3, Name = "Wardrobe" },
                new Product() { Rating = 5, Name = "Bed", DiscountedPrice = 50 },
                new Product() { Rating = 5, Name = "Table", DiscountedPrice = 80 },
                new Product() { Rating = 4, Name = "Chair", DiscountedPrice = 60 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection(filterBy, search, null, null, 1, 16);

            // Assert
            Assert.AreSame(products[2], result.First());
            Assert.AreSame(products[0], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollectionSortedBysDiscount_WhenSearchWordAndFilterAreProvided()
        {
            // Arrange
            var filterBy = "discount";

            var products = new List<Product>()
            {
                new Product() { HasDiscount = true, DiscountPercentage = 10 },
                new Product() { HasDiscount = false, DiscountPercentage = 0 },
                new Product() { HasDiscount = true, DiscountPercentage = 15 },
                new Product() { HasDiscount = true, DiscountPercentage = 2 }
            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection(filterBy, null, null, null, 1, 16);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.AreSame(products[2], result.First());
            Assert.AreSame(products[3], result.Last());
        }

        [Test]
        public void ShouldReturnCorrectProductsCollectionFromRoomSortedById_WhenSearchWordAndFilterAreProvided()
        {
            // Arrange
            var filterBy = "Bedroom";

            var products = new List<Product>()
            {
                new Product() { Room = new Room() { Name = "Bedroom" }, Id = 5 },
                new Product() { Room = new Room() { Name = "Bathroom" }, Id = 4 },
                new Product() { Room = new Room() { Name = "Bedroom" }, Id = 3 },
                new Product() { Room = new Room() { Name = "Kitchen" }, Id = 2 },

            };

            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.ProductsRepository.All())
                .Returns(products.AsQueryable);

            var productsService = new ProductsService(mockedData.Object);

            // Act
            var result = productsService.GetProductsSelection(filterBy, null, null, null, 1, 16);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreSame(products[0], result.First());
            Assert.AreSame(products[2], result.Last());
        }
        
        [Test]
        public void ShouldReturnCorrectProductsPerPage_WhenSearchWordAndPageAreProvided()
        {
            // Arrange
            var search = "";

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
            var result = productsService.GetProductsSelection("all", search, null, null, 2, 2);

            // Assert
            Assert.AreSame(products[2], result.First());
            Assert.AreSame(products[3], result.Last());
        }

        [Test]
        public void ShouldReturnNoProducts_WhenNoProductsAreMatchingTheCriterias()
        {
            // Arrange
            var search = "qwerty";

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
            var result = productsService.GetProductsSelection("all", search, null, null, 1, 10);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
