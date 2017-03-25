using FFY.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFY.Web.Models.Account
{
    public class RegisterViewModel
    {

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Language))]
        [EmailAddress(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "EmailValidation")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "FirstNameRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "FirstNameValidation", MinimumLength = 2 )]
        [Display(Name = "FirstName", ResourceType = typeof(Language))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "LastNameRequired")]
        [StringLength(30, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "LastNameValidation", MinimumLength = 2)]
        [Display(Name = "LastName", ResourceType = typeof(Language))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PasswordLengthValidation", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PasswordValidation")]
        [Display(Name = "Password", ResourceType = typeof(Language))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [DataType(DataType.Password, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ConfirmPasswordValidation")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Language))]
        [Compare("Password", ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "ConfirmPasswordCompare")]
        public string ConfirmPassword { get; set; }
    }
}