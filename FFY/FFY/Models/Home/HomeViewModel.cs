using FFY.Web.Areas.Administration.Models.ProductManagement;
using FFY.Web.Models.Furniture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Models.Home
{
    public class HomeViewModel
    {
        public IEnumerable<SingleProductSelectionViewModel> LatestProducts { get; set; }

        public IEnumerable<SingleProductSelectionViewModel> HighestRatedProducts { get; set; }

        public IEnumerable<SingleProductSelectionViewModel> DiscountProducts { get; set; }
    }
}