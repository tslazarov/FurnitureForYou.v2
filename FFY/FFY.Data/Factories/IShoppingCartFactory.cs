using FFY.Models;

namespace FFY.Data.Factories
{
    public interface IShoppingCartFactory
    {
        ShoppingCart CreateShoppingCart(string userId, User user, decimal total = 0);
    }
}
