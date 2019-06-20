using FFY.Models;
using System.Collections.Generic;

namespace FFY.Services.Contracts
{
    public interface IProductsService
    {
        void AddProduct(Product product);

        void UpdateProduct(Product product);

        Product GetProductById(int id);

        IEnumerable<Product> GetProductsSelection(string filterBy,
            string searchWord,
            int? from,
            int? to,
            int page,
            int productsPerPage = 16);

        int GetProductsSelectionCount(string filterBy, string searchWord, int? from, int? to);

        IEnumerable<Product> SearchProducts(string searchWord, string sortBy, int page = 1, int productsPerPage = 10);

        int GetProductsCount(string searchWord);

        IEnumerable<Product> GetLatestProducts(int count);

        IEnumerable<Product> GetHighestRatedProducts(int count);

        IEnumerable<Product> GetDiscountProducts(int count);

        void DetachProduct(Product product);
    }
}
