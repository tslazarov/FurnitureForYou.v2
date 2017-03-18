using FFY.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFY.Models;
using FFY.Data.Contracts;
using Bytes2you.Validation;
using FFY.Data.Factories;

namespace FFY.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IFFYData data;
        private readonly ICartProductFactory cartProductFactory;

        public ShoppingCartsService(IFFYData data,
            ICartProductFactory cartProductFactory)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICartProductFactory>(cartProductFactory, "Cart product factory cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
            this.cartProductFactory = cartProductFactory;
        }

        public void AssignShoppingCart(ShoppingCart shoppingCart)
        {
            Guard.WhenArgument<ShoppingCart>(shoppingCart, "Shopping cart cannot be null.")
                .IsNull()
                .Throw();

            this.data.ShoppingCartsRepository.Add(shoppingCart);
            this.data.SaveChanges();
        }

        public void Add(ShoppingCart shoppingCart, Product product, int quantity)
        {
            Guard.WhenArgument<ShoppingCart>(shoppingCart, "Shopping cart cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<Product>(product, "Product cannot be null.")
                .IsNull()
                .Throw();

            var cartProduct = shoppingCart.CartProducts.FirstOrDefault(p => p.ProductId == product.Id);

            if(cartProduct == null)
            {
                cartProduct = this.cartProductFactory.CreateCartProduct(quantity, product, true);
                shoppingCart.CartProducts.Add(cartProduct);
            }
            else
            {
                cartProduct.Quantity += quantity;
            }

            cartProduct.Total = cartProduct.Quantity * cartProduct.Product.DiscountedPrice;

            shoppingCart.Total = shoppingCart.CartProducts.Sum(p =>
            (p.Product.DiscountedPrice * p.Quantity));

            this.data.ShoppingCartsRepository.Update(shoppingCart);
            this.data.SaveChanges();
        }

        public int CartProductsCount(string cartId)
        {
            Guard.WhenArgument<string>(cartId, "Shopping cart id cannot be null.")
                .IsNullOrEmpty()
                .Throw();

            return this.data.ShoppingCartsRepository.GetById(cartId)
                .CartProducts.Count();
        }
    }
}
