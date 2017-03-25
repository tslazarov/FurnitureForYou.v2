using FFY.Models;
using System;

namespace FFY.Data.Factories
{
    public interface IContactFactory
    {
        Contact CreateContact(string title,
            string email,
            string emailContent,
            DateTime sendOn,
            ContactStatusType statusType);
    }
}
