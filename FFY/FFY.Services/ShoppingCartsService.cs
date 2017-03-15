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
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IFFYData data;

        public ShoppingCartsService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }

        public void AssignShoppingCart(ShoppingCart shoppingCart)
        {
            Guard.WhenArgument<ShoppingCart>(shoppingCart, "Shopping cart cannot be null.")
                .IsNull()
                .Throw();

            this.data.ShoppingCartsRepository.Add(shoppingCart);
            this.data.SaveChanges();
        }
    }
}
