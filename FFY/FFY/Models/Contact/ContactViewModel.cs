using FFY.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFY.Web.Models.Contact
{
    public class ContactViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} should be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Send on")]
        public DateTime SendOn { get; set; }

        [Required]
        [Display(Name = "Status type")]
        public ContactStatusType StatusType { get; set; }
    }
}