using FFY.Models;
using FFY.Web.Mappings;
using System;

namespace FFY.Web.Areas.Administration.Models.OrderManagement
{
    public class SingleOrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public User User { get; set; }

        public Address Address { get; set; }

        public DateTime SendOn { get; set; }

        public OrderStatusType OrderStatusType { get; set; }
    }
}