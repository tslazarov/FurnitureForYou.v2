using FFY.Models;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.ContactManagement
{
    public class SingleContactViewModel : IMapFrom<Contact>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public DateTime SendOn { get; set; }

        public ContactStatusType ContactStatusType { get; set; }
    }
}