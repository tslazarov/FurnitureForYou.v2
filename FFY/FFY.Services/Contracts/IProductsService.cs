using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Contracts
{
    public interface IProductsService
    {
        void AddProduct(Product product);

        Product GetProductById(int id);

        IEnumerable<Product> SearchProducts(string searchWord, string sortBy, int page = 1, int productsPerPage = 10);

        int GetProductsCount(string searchWord);
    }
}
