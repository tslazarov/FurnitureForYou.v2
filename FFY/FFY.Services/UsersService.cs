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

        public void RateProduct(User user, Product product, int rating)
        {
            Guard.WhenArgument<User>(user, "User cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<Product>(product, "Product cannot be null.")
                .IsNull()
                .Throw();

            product.Rating = (product.Rating * product.RatingCount + rating) / (product.RatingCount + 1);
            product.RatingCount += 1;

            user.RatedProducts.Add(product);
            product.Raters.Add(user);

            this.data.UsersRepository.Update(user);
            this.data.ProductsRepository.Update(product);
            this.data.SaveChanges();
        }

        public User GetUserById(string id)
        {
            Guard.WhenArgument<string>(id, "User id cannot be null or empty.")
                .IsNullOrEmpty()
                .Throw();

            return this.data.UsersRepository.GetById(id);
        }

        public User GetUserByEmail(string email)
        {
            Guard.WhenArgument<string>(email, "User email cannot be null or empty.")
                .IsNullOrEmpty()
                .Throw();

            return this.data.UsersRepository.All().FirstOrDefault(u => u.UserName == email);
        }
    }
}
