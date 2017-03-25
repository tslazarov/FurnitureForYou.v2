using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Services.Contracts;
using System.Linq;

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

            var cartProduct = shoppingCart.CartProducts.FirstOrDefault(p => 
                p.ProductId == product.Id && p.IsInCart);

            if(cartProduct == null)
            {
                cartProduct = this.cartProductFactory.CreateCartProduct(quantity, product, true, false);
                shoppingCart.CartProducts.Add(cartProduct);
            }
            else
            {
                cartProduct.Quantity += quantity;
            }

            cartProduct.Total = cartProduct.Quantity * cartProduct.Product.DiscountedPrice;

            shoppingCart.Total = shoppingCart.CartProducts
                .Where(p => p.IsInCart)
                .Sum(p => (p.Product.DiscountedPrice * p.Quantity));

            this.data.ShoppingCartsRepository.Update(shoppingCart);
            this.data.SaveChanges();
        }

        public void Remove(ShoppingCart shoppingCart, CartProduct cartProduct)
        {
            Guard.WhenArgument<ShoppingCart>(shoppingCart, "Shopping cart cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<CartProduct>(cartProduct, "Cart product cannot be null.")
                .IsNull()
                .Throw();

            var foundCartProduct = shoppingCart.CartProducts.FirstOrDefault(p => 
                p.Id == cartProduct.Id && p.IsInCart);

            if (foundCartProduct != null)
            {
                shoppingCart.CartProducts.Remove(foundCartProduct);
            }

            shoppingCart.Total = shoppingCart.CartProducts
                .Where(p => p.IsInCart)
                .Sum(p =>
            (p.Product.DiscountedPrice * p.Quantity));

            this.data.ShoppingCartsRepository.Update(shoppingCart);
            this.data.SaveChanges();
        }

        public void Clear(ShoppingCart shoppingCart)
        {
            Guard.WhenArgument<ShoppingCart>(shoppingCart, "Shopping cart cannot be null.")
                .IsNull()
                .Throw();

            foreach (var cartItem in shoppingCart.CartProducts)
            {
                cartItem.IsInCart = false;
                this.data.CartProductsRepository.Update(cartItem);
            }

            shoppingCart.Total = 0;

            this.data.ShoppingCartsRepository.Update(shoppingCart);
            this.data.SaveChanges();
        }

        public int CartProductsCount(string cartId)
        {
            Guard.WhenArgument<string>(cartId, "Shopping cart id cannot be null or empty.")
                .IsNullOrEmpty()
                .Throw();

            return this.data.ShoppingCartsRepository.GetById(cartId)
                .CartProducts.Where(cp => cp.IsInCart).Count();
        }

        public ShoppingCart GetShoppingCartById(string cartId)
        {
            Guard.WhenArgument<string>(cartId, "Shopping cart id cannot be null or empty.")
                .IsNullOrEmpty()
                .Throw();

            return this.data.ShoppingCartsRepository.GetById(cartId);
        }
    }
}
