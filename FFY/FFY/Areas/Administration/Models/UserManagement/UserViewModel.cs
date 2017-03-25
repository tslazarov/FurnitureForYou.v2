using FFY.Web.Areas.Profile.Models;

namespace FFY.Web.Areas.Administration.Models.UserManagement
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string Role { get; set; }

        public ProfileViewModel ProfileModel { get; set; }
    }
}