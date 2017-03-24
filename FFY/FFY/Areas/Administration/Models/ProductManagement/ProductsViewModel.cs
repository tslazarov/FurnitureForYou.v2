using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class ProductsViewModel
    {
            public IEnumerable<SingleProductViewModel> Products { get; set; }

            public int ProductsCount { get; set; }

            public int Pages { get; set; }

            public int Page { get; set; }

            public SearchModel SearchModel { get; set; }
    }
}