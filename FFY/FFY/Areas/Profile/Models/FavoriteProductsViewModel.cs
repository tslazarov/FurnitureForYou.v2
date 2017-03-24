using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Profile.Models
{
    public class FavoriteProductsViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<SingleFavoriteProductViewModel> FavoriteProducts { get; set; }

        public int FavoriteProductsCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }
    }
}