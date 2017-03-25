using FFY.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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