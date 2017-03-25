using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace FFY.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IFFYData data;

        public ProductsService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }

        public void AddProduct(Product product)
        {
            Guard.WhenArgument<Product>(product, "Product cannot be null.")
                .IsNull()
                .Throw();

            this.data.ProductsRepository.Add(product);
            this.data.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Guard.WhenArgument<Product>(product, "Product cannot be null.")
                .IsNull()
                .Throw();

            this.data.ProductsRepository.Update(product);
            this.data.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return this.data.ProductsRepository.GetById(id);
        }

        public IEnumerable<Product> SearchProducts(string searchWord, string sortBy, int page = 1, int productsPerPage = 10)
        {
            var skip = (page - 1) * productsPerPage;

            var products = this.BuildSearchQuery(searchWord);

            switch (sortBy)
            {
                case "name":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "price":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "room":
                    products = products.OrderBy(p => p.Room.Name);
                    break;
                case "category":
                    products = products.OrderBy(p => p.Category.Name);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            var resultProducts = products
                .Skip(skip)
                .Take(productsPerPage)
                .ToList();

            return resultProducts;
        }

        public int GetProductsCount(string searchWord)
        {
            var products = this.BuildSearchQuery(searchWord);
            return products.Count();
        }

        private IQueryable<Product> BuildSearchQuery(string searchWord)
        {
            var products = this.data.ProductsRepository.All();

            if (!string.IsNullOrEmpty(searchWord))
            {
                products = products.Where(p => p.Name.ToLower().Contains(searchWord.ToLower()));
            }

            return products;
        }
    }
}
