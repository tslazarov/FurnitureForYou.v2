using FFY.Models;

namespace FFY.Services.Contracts
{
    public interface ICartProductsService
    {
        CartProduct GetCartProductById(int id);
    }
}
