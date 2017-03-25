using FFY.Models;
using System.Collections.Generic;

namespace FFY.Services.Contracts
{
    public interface IContactsService
    {
        void AddContact(Contact contact);

        void UpdateContactStatus(Contact contact, User user, ContactStatusType status, string userId);

        Contact GetContactById(int id);

        IEnumerable<Contact> SearchContacts(string searchWord, string sortBy, string filterBy, int page = 1, int contactsPerPage = 10);

        int GetContactsCount(string searchWord, string filterBy);
    }
}
