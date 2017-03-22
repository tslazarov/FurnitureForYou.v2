using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Custom.Attributes;
using FFY.Web.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    [Localize]
    public class ContactController : Controller
    {
        private readonly IDateTimeProvider dateProvider;
        private readonly IContactFactory contactFactory;
        private readonly IContactsService contactsService;

        public ContactController(IDateTimeProvider dateProvider,
            IContactFactory contactFactory,
            IContactsService contactsService)
        {
            Guard.WhenArgument<IDateTimeProvider>(dateProvider, "Date provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IContactFactory>(contactFactory, "Contact factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IContactsService>(contactsService, "Contacts service cannot be null.")
                .IsNull()
                .Throw();

            this.dateProvider = dateProvider;
            this.contactFactory = contactFactory;
            this.contactsService = contactsService;
        }

        // GET: Contact
        public ViewResult Index()
        {
            return this.View();
        }

        // POST: Contact
        [HttpPost]
        public ActionResult Index(ContactViewModel model)
        {
            model.SendOn = this.dateProvider.GetCurrentTime();
            model.StatusType = ContactStatusType.NotProcessed;

            if (this.ModelState.IsValid)
            {
                var contact = this.contactFactory.CreateContact(model.Title,
                    model.Email,
                    model.Content,
                    model.SendOn,
                    model.StatusType);

                this.contactsService.AddContact(contact);

                return this.View();
            }

            return this.View();
        }
    }
}