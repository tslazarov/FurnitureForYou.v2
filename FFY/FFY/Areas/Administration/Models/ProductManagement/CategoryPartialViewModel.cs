using FFY.Resources;
using System.ComponentModel.DataAnnotations;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class CategoryPartialViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "CategoryNameRequired")]
        [Display(Name = "CategoryName", ResourceType = typeof(Language))]
        public string Name { get; set; }

        [Display(Name = "CategoryImage", ResourceType = typeof(Language))]
        public string ImagePath { get; set; }
    }
}