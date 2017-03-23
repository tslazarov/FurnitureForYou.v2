using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Profile.Models
{
    public class FavoritesViewModel
    {
        public IEnumerable<Product> FavoritedProducts { get; set; }
    }
}