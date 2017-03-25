using FFY.Models;
using FFY.Resources;
using System.ComponentModel.DataAnnotations;

namespace FFY.Web.Models.Furniture
{
    public class DetailedProductViewModel
    {
        public DetailedProductViewModel()
        {
            this.Product = new Product();
        }

        public Product Product { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ProductQuantityRequired")]
        [Range(1, 100, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ProductQuantity")]
        public int Quantity { get; set; }

        [Range(1, 5, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ProductRating")]
        public int GivenRating { get; set; }
    }
}