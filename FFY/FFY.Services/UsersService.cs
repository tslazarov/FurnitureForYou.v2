using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Product> GetFavoriteProducts(string id, int page = 1, int productsPerPage = 16)
        {
            Guard.WhenArgument<string>(id, "User id cannot be null or empty.")
                .IsNullOrEmpty()
                .Throw();

            var skip = (page - 1) * productsPerPage;

            var products = this.data.UsersRepository.GetById(id).FavoritedProducts;

            var resultProducts = products.OrderBy(p => p.Name)
                .Skip(skip)
                .Take(productsPerPage)
                .ToList();

            return resultProducts;
        }

        public IEnumerable<User> SearchUsers(string searchWord, string sortBy, int page = 1, int usersPerPage = 10)
        {
            var skip = (page - 1) * usersPerPage;

            var users = this.BuildSearchQuery(searchWord);

            switch (sortBy)
            {
                case "name":
                    users = users.OrderBy(u => u.FirstName);
                    break;
                case "email":
                    users = users.OrderBy(x => x.UserName);
                    break;
                default:
                    users = users.OrderBy(x => x.FirstName);
                    break;
            }

            var resultUsers = users
                .Skip(skip)
                .Take(usersPerPage)
                .ToList();

            return resultUsers;
        }

        public int GetFavoriteProductsCount(string id)
        {
            Guard.WhenArgument<string>(id, "User id cannot be null or empty.")
                .IsNullOrEmpty()
                .Throw();

            var products = this.data.UsersRepository.GetById(id).FavoritedProducts;
            return products.Count();
        }

        public int GetUsersCount(string searchWord)
        {
            var users = this.BuildSearchQuery(searchWord);
            return users.Count();
        }

        private IQueryable<User> BuildSearchQuery(string searchWord)
        {
            var users = this.data.UsersRepository.All();

            if (!string.IsNullOrEmpty(searchWord))
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(searchWord.ToLower())
                    || u.LastName.ToLower().Contains(searchWord.ToLower())
                    || u.UserName.ToLower().Contains(searchWord.ToLower()));
            }

            return users;
        }
    }
}
