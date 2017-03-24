using FFY.Models;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Profile.Models
{
    public class SingleFavoriteProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}