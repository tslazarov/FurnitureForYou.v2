using FFY.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFY.Models;
using FFY.Data.Contracts;
using Bytes2you.Validation;

namespace FFY.Services
{
    public class UsersService : IUsersService
    {
        private readonly IFFYData data;

        public UsersService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }


        public void AddProductToFavorites(User user, Product product)
        {
            Guard.WhenArgument<User>(user, "User cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<Product>(product, "Product cannot be null.")
                .IsNull()
                .Throw();

            user.FavoritedProducts.Add(product);
            product.Favoriters.Add(user);

            this.data.UsersRepository.Update(user);
            this.data.ProductsRepository.Update(product);
            this.data.SaveChanges();
        }

        public void RemoveProductFromFavorites(User user, Product product)
        {
            Guard.WhenArgument<User>(user, "User cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<Product>(product, "Product cannot be null.")
                .IsNull()
                .Throw();

            user.FavoritedProducts.Remove(product);
            product.Favoriters.Remove(user);

            this.data.UsersRepository.Update(user);
            this.data.ProductsRepository.Update(product);
            this.data.SaveChanges();
        }

        public User GetUserById(string id)
        {
            return this.data.UsersRepository.GetById(id);
        }
    }
}
