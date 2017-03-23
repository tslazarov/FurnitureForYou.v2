using Bytes2you.Validation;
using FFY.Web.Areas.Profile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.UserManagement
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string Role { get; set; }

        public ProfileViewModel ProfileModel { get; set; }
    }
}