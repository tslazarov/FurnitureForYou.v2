using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public IEnumerable<Product> GetProductsSelection(string filterBy,
            string searchWord,
            int? from,
            int? to,
            int page,
            int productsPerPage = 16)
        {
            var skip = (page - 1) * productsPerPage;

            var products = this.BuildSearchAndFilterQuery(searchWord, from, to);

            switch (filterBy)
            {
                case "all":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "latest":
                    products = products.OrderByDescending(p => p.Id);
                    break;
                case "rating":
                    products = products.OrderByDescending(p => p.Rating);
                    break;
                case "discount":
                    products = products.Where(p => p.HasDiscount)
                        .OrderByDescending(p => p.DiscountPercentage);
                    break;
                default:
                    products = products.Where(p => p.Room.Name == filterBy)
                        .OrderByDescending(p => p.Id);
                    break;
            }

            var resultProducts = products
                .Skip(skip)
                .Take(productsPerPage)
                .ToList();

            return resultProducts;
        }

        public int GetProductsSelectionCount(string filterBy, string searchWord, int? from, int? to)
        {
            var products = this.BuildSearchAndFilterQuery(searchWord, from, to);

            switch (filterBy)
            {
                case "all":
                case "latest":
                case "rating":
                    break;
                case "discount":
                    products = products.Where(p => p.HasDiscount);
                    break;
                default:
                    products = products.Where(p => p.Room.Name.ToLower() == filterBy.ToLower());
                    break;
            }

            return products.Count();
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

        private IQueryable<Product> BuildSearchAndFilterQuery(string searchWord,
            int? from,
            int? to)
        {
            var products = this.data.ProductsRepository.All();

            if(from != null)
            {
                products = products.Where(p => p.DiscountedPrice >= from);
            }

            if(to != null)
            {
                products = products.Where(p => p.DiscountedPrice <= to);
            }

            if (!string.IsNullOrEmpty(searchWord))
            {
                products = products.Where(p => p.Name.ToLower().Contains(searchWord.ToLower()));
            }

            return products;
        }

        public IEnumerable<Product> GetLatestProducts(int count)
        {
            return this.data.ProductsRepository.All().OrderByDescending(p => p.Id).Take(count);
        }

        public IEnumerable<Product> GetHighestRatedProducts(int count)
        {
            return this.data.ProductsRepository.All().OrderByDescending(p => p.Rating).Take(count);
        }

        public IEnumerable<Product> GetDiscountProducts(int count)
        {
            return this.data.ProductsRepository.All().OrderByDescending(p => p.DiscountPercentage).Take(count);
        }
    }
}
