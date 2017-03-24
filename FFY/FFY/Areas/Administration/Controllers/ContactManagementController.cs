using Bytes2you.Validation;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.ContactManagement;
using FFY.Web.Custom.Attributes;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Localize]
    [Security(Roles = "Administrator, Moderator", RedirectUrl = "~/en/error/unauthorized")]
    public class ContactManagementController : Controller
    {
        private const int ContactsPerPage = 10;

        private readonly IMapperProvider mapper;
        private readonly IContactsService contactsService;

        public ContactManagementController(IMapperProvider mapper,
            IContactsService contactsService)
        {
            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
               .IsNull()
               .Throw();

            Guard.WhenArgument<IContactsService>(contactsService, "Contacts service cannot be null.")
                .IsNull()
                .Throw();

            this.mapper = mapper;
            this.contactsService = contactsService;
        }

        // GET: Administration/ContactManagement
        public ViewResult Index(ContactsViewModel model)
        {
            return this.View(model);
        }


        // GET: Administration/SearchContacts
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