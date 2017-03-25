using FFY.Models;
using FFY.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Models.Contact
{
    public class ContactViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Language))]
        [EmailAddress(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "EmailValidation")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "TitleRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "TitleValidation", MinimumLength = 2)]
        [Display(Name = "Title", ResourceType = typeof(Language))]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ContentRequired")]
        [StringLength(500, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ContentValidation", MinimumLength = 10)]
        [Display(Name = "Content", ResourceType = typeof(Language))]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SendOn { get; set; }

        [Required]
        public ContactStatusType StatusType { get; set; }
    }
}