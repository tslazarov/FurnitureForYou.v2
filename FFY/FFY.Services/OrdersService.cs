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
    public class OrdersService : IOrdersService
    {
        private readonly IFFYData data;

        public OrdersService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }

        public void AddOrder(Order order)
        {
            Guard.WhenArgument<Order>(order, "Order cannot be null.")
                .IsNull()
                .Throw();

            this.data.OrdersRepository.Add(order);
            this.data.SaveChanges();
        }

        public void TransferProducts(Order order, ShoppingCart shoppingCart)
        {
            Guard.WhenArgument<Order>(order, "Order cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ShoppingCart>(shoppingCart, "Shopping cart cannot be null.")
                .IsNull()
                .Throw();

            order.Products = shoppingCart.CartProducts
                .Where(cp => cp.IsInCart)
                .ToList();

            foreach (var cartProduct in order.Products)
            {
                cartProduct.Product.Quantity -= cartProduct.Quantity;
                if (cartProduct.Product.Quantity < 0)
                {
                    cartProduct.IsOutOfStock = true;
                }
            }
        }
    }
}
