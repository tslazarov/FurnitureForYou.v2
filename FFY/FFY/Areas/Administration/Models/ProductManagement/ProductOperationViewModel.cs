using FFY.Models;
using FFY.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class ProductOperationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ProductNameRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ProductNameValidation", MinimumLength = 2)]
        [Display(Name = "ProductName", ResourceType = typeof(Language))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PriceRequired")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PriceValidation")]
        [Range(0, 100000, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PriceRangeValidation")]
        [Display(Name = "Price", ResourceType = typeof(Language))]
        public decimal Price { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "DiscountPercentageRequired")]
        [Range(0, 100, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "DiscountPercentageValidation")]
        [Display(Name = "DiscountPercentage", ResourceType = typeof(Language))]
        public int DiscountPercentage { get; set; }

        public decimal DiscountedPrice {
            get
            {
                return this.Price - (this.Price * (this.DiscountPercentage / 100.0M));
            }
        }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "QuantityRequired")]
        [Range(0, 10000, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "QuantityValidation")]
        [Display(Name = "Quantity", ResourceType = typeof(Language))]
        public int Quantity { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "DescriptionRequired")]
        [StringLength(500, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "DescriptionValidation", MinimumLength = 10)]
        [Display(Name = "Description", ResourceType = typeof(Language))]
        [AllowHtml]
        public string Description { get; set; }

        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Room", ResourceType = typeof(Language))]
        public Room Room { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category", ResourceType = typeof(Language))]
        public Category Category { get; set; }

        [Display(Name = "ProductImage", ResourceType = typeof(Language))]
        public string ImagePath { get; set; }
    }
}