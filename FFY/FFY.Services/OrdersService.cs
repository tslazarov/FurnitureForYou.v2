using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

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

        public void UpdateOrderStatuses(Order order, OrderStatusType orderStatus, OrderPaymentStatusType orderPaymentStatus)
        {
            Guard.WhenArgument<Order>(order, "Order cannot be null.")
                .IsNull()
                .Throw();

            order.OrderStatusType = orderStatus;
            order.OrderPaymentStatusType = orderPaymentStatus;

            this.data.OrdersRepository.Update(order);
            this.data.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            return this.data.OrdersRepository.GetById(id);
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

        public IEnumerable<Order> SearchOrders(string searchWord, string sortBy, string filterBy, int page = 1, int ordersPerPage = 10)
        {
            var skip = (page - 1) * ordersPerPage;

            var orders = this.BuildSearchAndFilterQuery(searchWord, filterBy);

            switch (sortBy)
            {
                case "address":
                    orders = orders.OrderBy(o => o.Address.Street);
                    break;
                case "date":
                    orders = orders.OrderByDescending(o => o.SendOn);
                    break;
                case "sender":
                    orders = orders.OrderBy(o => o.User.FirstName);
                    break;
                default:
                    orders = orders.OrderByDescending(o => o.SendOn);
                    break;
            }

            var resultOrders = orders
                .Skip(skip)
                .Take(ordersPerPage)
                .ToList();

            return resultOrders;
        }

        public int GetOrdersCount(string searchWord, string filterBy)
        {
            var orders = this.BuildSearchAndFilterQuery(searchWord, filterBy);
            return orders.Count();
        }

        private IQueryable<Order> BuildSearchAndFilterQuery(string searchWord, string filterBy)
        {
            var orders = this.data.OrdersRepository.All();

            if (!string.IsNullOrEmpty(filterBy))
            {
                var status = int.Parse(filterBy);

                if (status > 0)
                {
                    orders = orders.Where(o => (int)o.OrderStatusType == status);
                }
            }

            if (!string.IsNullOrEmpty(searchWord))
            {
                orders = orders.Where(o => o.User.FirstName.ToLower().Contains(searchWord.ToLower())
                    || o.Address.Street.ToLower().Contains(searchWord.ToLower()));
            }

            return orders;
        }
    }
}
