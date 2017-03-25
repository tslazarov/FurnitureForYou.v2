using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.Areas.Administration.Models.ContactManagement
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }

        public ContactStatusType Status { get; set; }
    }
}