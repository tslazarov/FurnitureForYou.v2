using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class CategoryPartialViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ImagePath")]
        public string ImagePath { get; set; }
    }
}