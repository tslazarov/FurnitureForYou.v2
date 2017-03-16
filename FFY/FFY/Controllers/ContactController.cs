using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Services.Contracts;
using FFY.Web.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactFactory contactFactory;
        private readonly IContactsService contactsService;

        public ContactController(IContactFactory contactFactory,
            IContactsService contactsService)
        {
            Guard.WhenArgument<IContactFactory>(contactFactory, "Contact factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IContactsService>(contactsService, "Contacts service cannot be null.")
                .IsNull()
                .Throw();

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
            model.SendOn = DateTime.Now;
            model.StatusType = ContactStatusType.NotProcessed;

            if (this.ModelState.IsValid)
            {
                var contact = this.contactFactory.CreateContact(model.Title,
                    model.Email,
                    model.Content,
                    model.SendOn,
                    model.StatusType);

                this.contactsService.AddContact(contact);

                // TODO: possible better handling later
                return RedirectToAction("Index", "Home");
            }

            return this.View();
        }
    }
}