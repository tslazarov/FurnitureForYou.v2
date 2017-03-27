using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Controllers;
using FFY.Web.Areas.Profile.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.ShoppingCartControllerTests
{
    [TestFixture]
    public class RemoveCartItem
    {
        [Test]
        public void ShouldCallGetShoppingCartByIdMethodOfShoppingCartsService()
        {
            // Arrange
            var cartId = "13";
            var cartProductId = 42;
            var shoppingCart = new ShoppingCart() { CartProducts = new List<CartProduct>() };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.GetShoppingCartById(It.IsAny<string>()))
                .Returns(shoppingCart)
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.RemoveCartItem(cartId, cartProductId);

            // Assert
            mockedShoppingCartsService.Verify(scs => scs.GetShoppingCartById(cartId), Times.Once);
        }

        [Test]
        public void ShouldCallGetCartProductByIdMethodOfCartProductsService()
        {
            // Arrange
            var cartId = "13";
            var cartProductId = 42;
            var shoppingCart = new ShoppingCart() { CartProducts = new List<CartProduct>() };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.GetShoppingCartById(It.IsAny<string>()))
                .Returns(shoppingCart);
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            mockedCartProductsService.Setup(cps => cps.GetCartProductById(It.IsAny<int>()))
                .Verifiable();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.RemoveCartItem(cartId, cartProductId);

            // Assert
            mockedCartProductsService.Verify(cps => cps.GetCartProductById(cartProductId), Times.Once);
        }

        [Test]
        public void ShouldCallRemoveMethodOfShoppingCartsService()
        {
            // Arrange
            var cartId = "13";
            var cartProductId = 42;
            var shoppingCart = new ShoppingCart() { CartProducts = new List<CartProduct>() };
            var cartProduct = new CartProduct();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.GetShoppingCartById(It.IsAny<string>()))
                .Returns(shoppingCart);
            mockedShoppingCartsService.Setup(scs => scs.Remove(It.IsAny<ShoppingCart>(),
                It.IsAny<CartProduct>()))
                .Verifiable();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            mockedCartProductsService.Setup(cps => cps.GetCartProductById(It.IsAny<int>()))
                .Returns(cartProduct);
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.RemoveCartItem(cartId, cartProductId);

            // Assert
            mockedShoppingCartsService.Verify(scs => 
                scs.Remove(shoppingCart, cartProduct), Times.Once);
        }

        [Test]
        public void ShouldCallInsertItemMethodOfCachingProvider()
        {
            // Arrange
            var cartId = "13";
            var cartProductId = 42;
            var shoppingCart = new ShoppingCart() { CartProducts = new List<CartProduct>() };
            var cartProduct = new CartProduct();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            mockedCachingProvider.Setup(cp => cp.InsertItem(It.IsAny<string>(),
                It.IsAny<int>()))
                .Verifiable();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.GetShoppingCartById(It.IsAny<string>()))
                .Returns(shoppingCart);
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act
            shoppingCartController.RemoveCartItem(cartId, cartProductId);

            // Assert
            mockedCachingProvider.Verify(cp => cp.InsertItem(It.IsAny<string>(), 0), Times.Once);
        }

        [Test]
        public void ShouldRedirectToIndexActionOfShoppingCartController()
        {
            // Arrange
            var cartId = "13";
            var cartProductId = 42;
            var shoppingCart = new ShoppingCart() { CartProducts = new List<CartProduct>() };

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedCachingProvider = new Mock<ICachingProvider>();
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var mockedShoppingCartsService = new Mock<IShoppingCartsService>();
            mockedShoppingCartsService.Setup(scs => scs.GetShoppingCartById(It.IsAny<string>()))
                .Returns(shoppingCart);
            var mockedUsersService = new Mock<IUsersService>();
            var mockedCartProductsService = new Mock<ICartProductsService>();
            var mockedAddressesService = new Mock<IAddressesService>();
            var mockedOrdersService = new Mock<IOrdersService>();
            var mockedAddressFactory = new Mock<IAddressFactory>();
            var mockedOrderFactory = new Mock<IOrderFactory>();

            var shoppingCartController = new ShoppingCartController(mockedAuthenticationProvider.Object,
                        mockedCachingProvider.Object,
                        mockedDateTimeProvider.Object,
                        mockedShoppingCartsService.Object,
                        mockedUsersService.Object,
                        mockedCartProductsService.Object,
                        mockedAddressesService.Object,
                        mockedOrdersService.Object,
                        mockedAddressFactory.Object,
                        mockedOrderFactory.Object);

            // Act and Assert
            shoppingCartController.WithCallTo(scs => scs.RemoveCartItem(cartId, cartProductId))
                .ShouldRedirectTo((ShoppingCartController scs) => 
                    scs.Index(It.IsAny<ShoppingCartViewModel>()));
        }
    }
}
