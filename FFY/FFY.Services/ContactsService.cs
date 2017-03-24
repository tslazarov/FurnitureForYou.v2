using FFY.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFY.Models;
using FFY.Data.Contracts;
using Bytes2you.Validation;

namespace FFY.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IFFYData data;

        public ContactsService(IFFYData data)
        {
            Guard.WhenArgument<IFFYData>(data, "Data cannot be null.")
                .IsNull()
                .Throw();

            this.data = data;
        }

        public void AddContact(Contact contact)
        {
            Guard.WhenArgument<Contact>(contact, "Contact cannot be null.")
                .IsNull()
                .Throw();

            this.data.ContactsRepository.Add(contact);
            this.data.SaveChanges();
        }

        public IEnumerable<Contact> SearchContacts(string searchWord, string sortBy, string filterBy, int page = 1, int contactsPerPage = 10)
        {
            var skip = (page - 1) * contactsPerPage;

            var contacts = this.BuildSearchAndFilterQuery(searchWord, filterBy);

            switch (sortBy)
            {
                case "title":
                    contacts = contacts.OrderBy(p => p.Title);
                    break;
                case "date":
                    contacts = contacts.OrderByDescending(p => p.SendOn);
                    break;
                case "email":
                    contacts = contacts.OrderBy(p => p.Email);
                    break;
                default:
                    contacts = contacts.OrderByDescending(p => p.SendOn);
                    break;
            }

            var resultContacts = contacts
                .Skip(skip)
                .Take(contactsPerPage)
                .ToList();

            return resultContacts;
        }

        public int GetContactsCount(string searchWord, string filterBy)
        {
            var contacts = this.BuildSearchAndFilterQuery(searchWord, filterBy);
            return contacts.Count();
        }

        private IQueryable<Contact> BuildSearchAndFilterQuery(string searchWord, string filterBy)
        {
            var contacts = this.data.ContactsRepository.All();

            if (!string.IsNullOrEmpty(filterBy))
            {
                var status = int.Parse(filterBy);

                if (status > 0)
                {
                    contacts = contacts.Where(p => (int)p.ContactStatusType == status);
                }
            }

            if (!string.IsNullOrEmpty(searchWord))
            {
                contacts = contacts.Where(p => p.Email.ToLower().Contains(searchWord.ToLower())
                    || p.Title.ToLower().Contains(searchWord.ToLower()));
            }

            return contacts;
        }
    }
}
