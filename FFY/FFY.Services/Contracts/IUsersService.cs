using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Contracts
{
    public interface IUsersService
    {
        User GetUserById(string id);

        void AddProductToFavorites(User user, Product product);

        void RemoveProductFromFavorites(User user, Product product);

        void RateProduct(User user, Product product, int rating);
    }
}
