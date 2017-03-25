using FFY.Models;

namespace FFY.Services.Contracts
{
    public interface IShoppingCartsService
    {
        void AssignShoppingCart(ShoppingCart shoppingCart);

        void Add(ShoppingCart shoppingCart, Product product, int quantity);

        void Remove(ShoppingCart shoppingCart, CartProduct cProduct);

        void Clear(ShoppingCart shoppingCart);

        int CartProductsCount(string cartId);

        ShoppingCart GetShoppingCartById(string cartId);
    }
}
