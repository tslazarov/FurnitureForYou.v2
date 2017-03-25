using FFY.Models;
using FFY.Web.Mappings;

namespace FFY.Web.Areas.Profile.Models
{
    public class SingleFavoriteProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}