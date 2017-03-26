using FFY.Models;
using FFY.Web.Mappings;

namespace FFY.Web.Models.Furniture
{
    public class SingleProductSelectionViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }

        public int DiscountPercentage { get; set; }

        public string ImagePath { get; set; }
    }
}