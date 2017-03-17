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

        public Product GetProductById(int id)
        {
            return this.data.ProductsRepository.GetById(id);
        }
    }
}
