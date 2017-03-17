using FFY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class ProductAdditionViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 100000, ErrorMessage = "{0} should be a number between 0 and 100000")]
        [Display(Name = "Price")]
        public int Price { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "{0} should be a number between 0 and 100.")]
        [Display(Name = "Discount percentage")]
        public int DiscountPercentage { get; set; }

        public decimal DiscountedPrice {
            get
            {
                return this.Price - (this.Price * (this.DiscountPercentage / 100.0M));
            }
        }

        [Required]
        [Range(0, 10000, ErrorMessage = "{0} should be a number between 0 and 10000")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Room")]
        public Room Room { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "ImagePath")]
        public string ImagePath { get; set; }
    }
}