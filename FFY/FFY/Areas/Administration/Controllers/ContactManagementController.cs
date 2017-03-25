using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.ContactManagement;
using FFY.Web.Custom.Attributes;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Localize]
    [Security(Roles = "Administrator, Moderator", RedirectUrl = "~/en/error/unauthorized")]
    public class ContactManagementController : Controller
    {
        private const int ContactsPerPage = 10;

        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IMapperProvider mapper;
        private readonly IContactsService contactsService;
        private readonly IUsersService usersService;

        public ContactManagementController(IAuthenticationProvider authenticationProvider, 
            IMapperProvider mapper,
            IContactsService contactsService,
            IUsersService usersService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
               .IsNull()
               .Throw();

            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
               .IsNull()
               .Throw();

            Guard.WhenArgument<IContactsService>(contactsService, "Contacts service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IUsersService>(usersService, "Users service cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = authenticationProvider;
            this.mapper = mapper;
            this.contactsService = contactsService;
            this.usersService = usersService;
        }

        // GET: Administration/ContactManagement
        public ViewResult Index(ContactsViewModel model)
        {
            return this.View(model);
        }

        // GET: Administration/Contacts/Id
        public ViewResult ContactDetailed(ContactViewModel model, int id)
        {
            model.Contact = this.contactsService.GetContactById(id);
            
            if(model.Contact == null)
            {
                return this.View("PageNotFound");
            }

            return this.View(model);
        }

        // POST: Administration/ContactManagement/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(ContactViewModel model)
        {
            var id = this.authenticationProvider.CurrentUserId;

            var contact = this.contactsService.GetContactById(model.Contact.Id);
            var user = this.usersService.GetUserById(id);

            this.contactsService.UpdateContactStatus(contact, 
                user, 
                model.Contact.ContactStatusType, 
                id);

            return this.RedirectToAction("ContactDetailed", new { id = model.Contact.Id });
        }

        // GET: Administration/ContactManagement/SearchContacts
        public PartialViewResult SearchContacts(SearchModel searchModel, ContactsViewModel contactsModel, int? page)
        {
            int actualPage = page ?? 1;

            var result = this.contactsService.SearchContacts(searchModel.SearchWord, searchModel.SortBy, searchModel.FilterBy, actualPage, ContactsPerPage);
            var count = this.contactsService.GetContactsCount(searchModel.SearchWord, searchModel.FilterBy);

            contactsModel.SearchModel = searchModel;
            contactsModel.ContactsCount = count;
            contactsModel.Pages = (int)Math.Ceiling((double)count / ContactsPerPage);
            contactsModel.Page = actualPage;
            contactsModel.Contacts = mapper.Map<IEnumerable<SingleContactViewModel>>(result);

            return this.PartialView("ContactsPartial", contactsModel);
        }
    }
}