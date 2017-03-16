using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.Web.Areas.Administration.Models.ProductManagement;
using FFY.Web.Custom.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Security(Roles = "Administrator, Moderator", RedirectUrl = "~/error/unauthorized")]
    public class ProductManagementController : Controller
    {
        private const string DefaultProductImageFileName = "default-room-image";
        private const string DefaultProductFolderName = "rooms";
        private const string ExistingCategoryErrorMessage = "Room addition was unsuccessful. The room may already exist";

        private readonly IImageUploader imageUploader;
        private readonly IRoomFactory roomFactory;
        private readonly IRoomsService roomsService;

        public ProductManagementController(IImageUploader imageUploader,
            IRoomFactory roomFactory,
            IRoomsService roomsService)
        {
            Guard.WhenArgument<IImageUploader>(imageUploader, "Image uploader cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IRoomFactory>(roomFactory, "Room factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IRoomsService>(roomsService, "Rooms service cannot be null.")
                .IsNull()
                .Throw();

            this.imageUploader = imageUploader;
            this.roomFactory = roomFactory;
            this.roomsService = roomsService;
        }

        // GET: Administration/ProductManagement
        public ViewResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult AddRoom(RoomPartialViewModel model)
        {
            var file = Request.Files[0];

            string imageFileName = DefaultProductImageFileName;
            string folderName = DefaultProductFolderName;

            model.ImagePath = this.imageUploader.Upload(file, Server, imageFileName, folderName);

            var room = this.roomFactory.CreateRoom(model.Name, model.ImagePath);

            try
            {
                this.roomsService.AddRoom(room);

                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception)
            {
                // this.ErrorMessage.Text = ExistingCategoryErrorMessage;
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}