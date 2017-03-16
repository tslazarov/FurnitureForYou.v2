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
    }
}
