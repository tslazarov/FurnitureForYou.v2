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

        void UpdateOrderStatuses(Order order, OrderStatusType orderStatus, OrderPaymentStatusType orderPaymentStatus);

        Order GetOrderById(int id);

        void TransferProducts(Order order, ShoppingCart shoppingCart);

        IEnumerable<Order> SearchOrders(string searchWord, string sortBy, string filterBy, int page = 1, int ordersPerPage = 10);

        int GetOrdersCount(string searchWord, string filterBy);
    }
}
