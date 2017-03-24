using FFY.Models;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class SingleProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public Room Room { get; set; }

        public Category Category { get; set; }
    }
}