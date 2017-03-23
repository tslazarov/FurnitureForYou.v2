using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Contracts
{
    public interface IOrdersService
    {
        void AddOrder(Order order);

        void TransferProducts(Order order, ShoppingCart shoppingCart);
    }
}
