using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFY.Models;
using FFY.Data.Contracts;
using Bytes2you.Validation;
using FFY.Services.Contracts;

namespace FFY.Services
{
    public class CartProductsService : ICartProductsService
    {
        private readonly IFFYData data;
        public CartProductsService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }

        public CartProduct GetCartProductById(int id)
        {
            return this.data.CartProductsRepository.GetById(id);
        }
    }
}
