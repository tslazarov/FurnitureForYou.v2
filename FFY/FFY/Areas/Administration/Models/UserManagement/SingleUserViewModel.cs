using FFY.Models;
using FFY.Web.Mappings;

namespace FFY.Web.Areas.Administration.Models.UserManagement
{
    public class SingleUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}