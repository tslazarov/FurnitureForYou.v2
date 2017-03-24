﻿using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services.Contracts
{
    public interface IContactsService
    {
        void AddContact(Contact contact);

        IEnumerable<Contact> SearchContacts(string searchWord, string sortBy, string filterBy, int page = 1, int contactsPerPage = 10);

        int GetContactsCount(string searchWord, string filterBy);
    }
}
