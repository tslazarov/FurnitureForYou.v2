using System.Collections.Generic;

namespace FFY.Web.Areas.Administration.Models.OrderManagement
{
    public class OrdersViewModel
    {
        public IEnumerable<SingleOrderViewModel> Orders { get; set; }

        public int OrdersCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public SearchModel SearchModel { get; set; }
    }
}