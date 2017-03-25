using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Custom.Attributes;
using FFY.Web.Models.Contact;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    [Localize]
    public class ContactController : Controller
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IContactFactory contactFactory;
        private readonly IContactsService contactsService;

        public ContactController(IDateTimeProvider dateTimeProvider,
            IContactFactory contactFactory,
            IContactsService contactsService)
        {
            Guard.WhenArgument<IDateTimeProvider>(dateTimeProvider, "Date time provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IContactFactory>(contactFactory, "Contact factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IContactsService>(contactsService, "Contacts service cannot be null.")
                .IsNull()
                .Throw();

            this.dateTimeProvider = dateTimeProvider;
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
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactViewModel model)
        {
            model.SendOn = this.dateTimeProvider.GetCurrentTime();
            model.StatusType = ContactStatusType.NotProcessed;

            var contact = this.contactFactory.CreateContact(model.Title,
                model.Email,
                model.Content,
                model.SendOn,
                model.StatusType);

            this.contactsService.AddContact(contact);

            return this.View();
        }
    }
}