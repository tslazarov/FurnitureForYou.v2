using System.Collections.Generic;

namespace FFY.Web.Areas.Administration.Models.ContactManagement
{
    public class ContactsViewModel
    {
        public IEnumerable<SingleContactViewModel> Contacts { get; set; }

        public int ContactsCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public SearchModel SearchModel { get; set; }
    }
}