using FFY.Models;

namespace FFY.Data.Factories
{
    public interface ICartProductFactory
    {
        CartProduct CreateCartProduct(int quantity,
            Product product,
            bool isInCart = true,
            bool isOutOfStock = false);
    }
}
