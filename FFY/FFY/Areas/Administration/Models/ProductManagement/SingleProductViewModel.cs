using FFY.Models;
using FFY.Web.Mappings;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class SingleProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public Room Room { get; set; }

        public Category Category { get; set; }
    }
}