using Bytes2you.Validation;
using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

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

        public void UpdateContactStatus(Contact contact, User user, ContactStatusType status, string userId)
        {
            Guard.WhenArgument<Contact>(contact, "Contact cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<User>(user, "User cannot be null.")
                .IsNull()
                .Throw();

            contact.ContactStatusType = status;
            contact.UserProcessedBy = status == ContactStatusType.NotProcessed ? null : user;
            contact.UserProccessedById = status == ContactStatusType.NotProcessed ? null : userId;

            this.data.ContactsRepository.Update(contact);
            this.data.SaveChanges();
        }

        public Contact GetContactById(int id)
        {
            return this.data.ContactsRepository.GetById(id);
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
