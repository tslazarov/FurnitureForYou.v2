using FFY.Data;
using FFY.Data.Contracts;
using FFY.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Data.FFYDataTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContextIsPassed()
        {
            // Arrange
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => 
                new FFYData(null, 
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContextIsPassed()
        {
            // Arrange
            var expectedExMessage = "Database context cannot be null.";

            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(null,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullAddressesRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    null,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullAddressesRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Addresses repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        null,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCategoriesRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    null,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCategoriesRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Categories repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        null,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContactsRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    null,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContactsRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Contacts repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        null,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullOrdersRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    null,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullOrdersRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Orders repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        null,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullRoomsRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    null,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullRoomsRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Rooms repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        null,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullShoppingCartsRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    null,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullShoppingCartsRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Shopping carts repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        null,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullCartProductsRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    null,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullCartProductsRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Cart products repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        null,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullUsersRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    null,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullUsersRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Users repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        null,
                        mockedChatUsersRepository.Object,
                        mockedProductsRepository.Object
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullProductsRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    null
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullProductsRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Products repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                        mockedAddressesRepository.Object,
                        mockedCategoriesRepository.Object,
                        mockedContactsRepository.Object,
                        mockedOrdersRepository.Object,
                        mockedRoomsRepository.Object,
                        mockedShoppingCartsRepository.Object,
                        mockedCartProductsRepository.Object,
                        mockedUsersRepository.Object,
                        mockedChatUsersRepository.Object,
                        null
                    ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldThrowArgumentNullException_WhenNullChatUsersRepositoryIsPassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    null,
                    mockedProductsRepository.Object
                ));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullChatUsersRepositoryIsPassed()
        {
            // Arrange
            var expectedExMessage = "Chat users repository cannot be null.";

            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    null,
                    mockedProductsRepository.Object
                ));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidArgumentsArePassed()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedChatUsersRepository = new Mock<IEfRepository<ChatUser>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new FFYData(mockedDbContext.Object,
                    mockedAddressesRepository.Object,
                    mockedCategoriesRepository.Object,
                    mockedContactsRepository.Object,
                    mockedOrdersRepository.Object,
                    mockedRoomsRepository.Object,
                    mockedShoppingCartsRepository.Object,
                    mockedCartProductsRepository.Object,
                    mockedUsersRepository.Object,
                    mockedChatUsersRepository.Object,
                    mockedProductsRepository.Object
                ));
        }
    }
}
