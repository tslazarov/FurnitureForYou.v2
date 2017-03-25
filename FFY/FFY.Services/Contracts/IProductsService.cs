using FFY.Models;
using System.Collections.Generic;

namespace FFY.Services.Contracts
{
    public interface IProductsService
    {
        void AddProduct(Product product);

        void UpdateProduct(Product product);

        Product GetProductById(int id);

        IEnumerable<Product> SearchProducts(string searchWord, string sortBy, int page = 1, int productsPerPage = 10);

        int GetProductsCount(string searchWord);
    }
}
