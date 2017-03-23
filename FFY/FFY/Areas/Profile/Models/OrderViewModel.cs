using FFY.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Profile.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "StreetRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "StreetValidation", MinimumLength = 2)]
        [Display(Name = "Street", ResourceType = typeof(Language))]
        public string Street { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "CityRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "CityValidation", MinimumLength = 2)]
        [Display(Name = "City", ResourceType = typeof(Language))]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "CountryRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "CountryValidation", MinimumLength = 2)]
        [Display(Name = "Country", ResourceType = typeof(Language))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PhoneNumberRequired")]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PhoneNumberValidation")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Language))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "SelectedPaymentTypeRequired")]
        [Display(Name = "SelectedPaymentType", ResourceType = typeof(Language))]
        public string SelectedPaymentType { get; set; }
    }
}