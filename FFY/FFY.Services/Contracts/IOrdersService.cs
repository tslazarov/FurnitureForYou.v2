using FFY.Models;
using System.Collections.Generic;

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

        void DeleteOrder(Order order);
    }
}
