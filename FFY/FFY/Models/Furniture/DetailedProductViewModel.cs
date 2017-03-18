using FFY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFY.Web.Models.Furniture
{
    public class DetailedProductViewModel
    {
        public DetailedProductViewModel()
        {
            this.Product = new Product();
        }

        public Product Product { get; set; }

        [Range(1, 100, ErrorMessage = "{0} should be a number between 0 and 100")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Range(1, 100, ErrorMessage = "{0} should be a number between 0 and 100")]
        [Display(Name = "Quantity")]
        public int GivenRating { get; set; }
    }
}