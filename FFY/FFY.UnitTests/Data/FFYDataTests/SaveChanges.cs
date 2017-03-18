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
    public class SaveChanges
    {
        [Test]
        public void ShouldCallSaveChangesMethodOnDbContext()
        {
            // Arrange
            var mockedDbContext = new Mock<IFFYDbContext>();
            mockedDbContext.Setup(x => x.SaveChanges()).Verifiable();
            var mockedAddressesRepository = new Mock<IEfRepository<Address>>();
            var mockedCategoriesRepository = new Mock<IEfRepository<Category>>();
            var mockedContactsRepository = new Mock<IEfRepository<Contact>>();
            var mockedOrdersRepository = new Mock<IEfRepository<Order>>();
            var mockedRoomsRepository = new Mock<IEfRepository<Room>>();
            var mockedShoppingCartsRepository = new Mock<IEfRepository<ShoppingCart>>();
            var mockedCartProductsRepository = new Mock<IEfRepository<CartProduct>>();
            var mockedUsersRepository = new Mock<IEfRepository<User>>();
            var mockedProductsRepository = new Mock<IDeletableEfRepository<Product>>();

            var data = new FFYData(mockedDbContext.Object,
                mockedAddressesRepository.Object,
                mockedCategoriesRepository.Object,
                mockedContactsRepository.Object,
                mockedOrdersRepository.Object,
                mockedRoomsRepository.Object,
                mockedShoppingCartsRepository.Object,
                mockedCartProductsRepository.Object,
                mockedUsersRepository.Object,
                mockedProductsRepository.Object
            );

            // Act
            data.SaveChanges();

            // Assert
            mockedDbContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
