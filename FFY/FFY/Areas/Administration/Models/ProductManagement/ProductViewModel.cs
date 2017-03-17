using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class ProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Room> Rooms { get; set; }

    }
}