using FFY.Resources;
using System.ComponentModel.DataAnnotations;

namespace FFY.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Language))]
        [EmailAddress(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "EmailValidation")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password, ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PasswordValidation")]
        [Display(Name = "Password", ResourceType = typeof(Language))]
        public string Password { get; set; }
    }
}