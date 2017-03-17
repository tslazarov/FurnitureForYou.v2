﻿using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Models;
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
        private const string DefaultRoomImageFileName = "default-room-image";
        private const string DefaultCategoryImageFileName = "default-category-image";
        private const string DefaultRoomFolderName = "rooms";
        private const string DefaultCategoryFolderName = "categories";
        private const string ExistingRoomErrorMessage = "Room addition was unsuccessful. The room may already exist";
        private const string ExistingCategoryErrorMessage = "Room addition was unsuccessful. The room may already exist";

        private readonly IImageUploader imageUploader;
        private readonly IProductFactory productFactory;
        private readonly IProductsService productsService;
        private readonly IRoomFactory roomFactory;
        private readonly IRoomsService roomsService;
        private readonly ICategoryFactory categoryFactory;
        private readonly ICategoriesService categoriesService;

        public ProductManagementController(IImageUploader imageUploader,
            IProductFactory productFactory,
            IProductsService productsService,
            IRoomFactory roomFactory,
            IRoomsService roomsService,
            ICategoryFactory categoryFactory,
            ICategoriesService categoriesService)
        {
            Guard.WhenArgument<IImageUploader>(imageUploader, "Image uploader cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IProductFactory>(productFactory, "Product factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IProductsService>(productsService, "Products service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IRoomFactory>(roomFactory, "Room factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IRoomsService>(roomsService, "Rooms service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICategoryFactory>(categoryFactory, "Room factory cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<ICategoriesService>(categoriesService, "Rooms service cannot be null.")
                .IsNull()
                .Throw();

            this.imageUploader = imageUploader;
            this.productFactory = productFactory;
            this.productsService = productsService;
            this.roomFactory = roomFactory;
            this.roomsService = roomsService;
            this.categoryFactory = categoryFactory;
            this.categoriesService = categoriesService;
        }

        // GET: Administration/ProductManagement
        public ViewResult Index()
        {
            return this.View();
        }

        // GET: Administration/AddProduct
        public ViewResult ProductAddition()
        {
            this.ViewBag.Rooms = this.roomsService.GetRooms();
            this.ViewBag.Categories = this.categoriesService.GetCategories();

            return this.View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductAdditionViewModel model)
        {
            var file = Request.Files[0];

            string imageFileName = DefaultRoomImageFileName;
            string folderName = DefaultRoomFolderName;

            model.ImagePath = this.imageUploader.Upload(file, Server, imageFileName, folderName);

            model.Room = this.roomsService.GetRoomById(model.RoomId);
            model.Category = this.categoriesService.GetCategoryById(model.CategoryId);

            var product = this.productFactory.CreateProduct(model.Name,
                model.Quantity,
                model.Price,
                model.DiscountedPrice,
                model.DiscountPercentage,
                model.DiscountPercentage > 0 ? true : false,
                model.Description,
                model.Category.Id,
                model.Category,
                model.Room.Id,
                model.Room,
                model.ImagePath,
                false);

            try
            {
                this.productsService.AddProduct(product);

                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception)
            {
                // this.ErrorMessage.Text = ExistingCategoryErrorMessage;
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public ActionResult AddRoom(RoomPartialViewModel model)
        {
            var file = Request.Files[0];

            string imageFileName = DefaultRoomImageFileName;
            string folderName = DefaultRoomFolderName;

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

        [HttpPost]
        public ActionResult AddCategory(CategoryPartialViewModel model)
        {
            var file = Request.Files[0];

            string imageFileName = DefaultCategoryImageFileName;
            string folderName = DefaultCategoryFolderName;

            model.ImagePath = this.imageUploader.Upload(file, Server, imageFileName, folderName);

            var category = this.categoryFactory.CreateCategory(model.Name, model.ImagePath);

            try
            {
                this.categoriesService.AddCategory(category);

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