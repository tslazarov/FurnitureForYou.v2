﻿using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Services.Contracts;
using FFY.Services.Utilities;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.ProductManagement;
using FFY.Web.Custom.Attributes;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FFY.Web.Areas.Administration.Controllers
{
    [Localize]
    [Security(Roles = "Administrator, Moderator", RedirectUrl = "~/en/error/unauthorized")]
    public class ProductManagementController : Controller
    {
        private const int ProductsPerPage = 10;

        private const string DefaultRoomImageFileName = "default-room-image.jpg";
        private const string DefaultCategoryImageFileName = "default-category-image.jpg";
        private const string DefaultProductImageFileName = "default-product-image.jpg";
        private const string DefaultRoomFolderName = "rooms";
        private const string DefaultCategoryFolderName = "categories";
        private const string DefaultProductFolderName = "products";

        private readonly IMapperProvider mapper;
        private readonly IImageUploader imageUploader;
        private readonly IProductFactory productFactory;
        private readonly IProductsService productsService;
        private readonly IRoomFactory roomFactory;
        private readonly IRoomsService roomsService;
        private readonly ICategoryFactory categoryFactory;
        private readonly ICategoriesService categoriesService;

        public ProductManagementController(IMapperProvider mapper,
            IImageUploader imageUploader,
            IProductFactory productFactory,
            IProductsService productsService,
            IRoomFactory roomFactory,
            IRoomsService roomsService,
            ICategoryFactory categoryFactory,
            ICategoriesService categoriesService)
        {

            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
                .IsNull()
                .Throw();

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

            this.mapper = mapper;
            this.imageUploader = imageUploader;
            this.productFactory = productFactory;
            this.productsService = productsService;
            this.roomFactory = roomFactory;
            this.roomsService = roomsService;
            this.categoryFactory = categoryFactory;
            this.categoriesService = categoriesService;
        }

        // GET: Administration/ProductManagement
        public ViewResult Index(ProductsViewModel model)
        {
            return this.View(model);
        }

        // GET: Administration/SearchProducts
        public PartialViewResult SearchProducts(SearchModel searchModel, ProductsViewModel favoriteProductsViewModel, int? page)
        {
            int actualPage = page ?? 1;

            var result = this.productsService.SearchProducts(searchModel.SearchWord, searchModel.SortBy, actualPage, ProductsPerPage);
            var count = this.productsService.GetProductsCount(searchModel.SearchWord);

            favoriteProductsViewModel.SearchModel = searchModel;
            favoriteProductsViewModel.ProductsCount = count;
            favoriteProductsViewModel.Pages = (int)Math.Ceiling((double)count / ProductsPerPage);
            favoriteProductsViewModel.Page = actualPage;
            favoriteProductsViewModel.Products = mapper.Map<IEnumerable<SingleProductViewModel>>(result);

            return this.PartialView("ProductsPartial", favoriteProductsViewModel);
        }

        // GET: Administration/ProductAddition
        public ViewResult ProductAddition()
        {
            this.ViewBag.Rooms = this.roomsService.GetRooms();
            this.ViewBag.Categories = this.categoriesService.GetCategories();
            this.ViewBag.Operation = "AddProduct";

            return this.View("ProductOperation");
        }

        // POST: Administration/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductOperationViewModel model)
        {
            var file = Request.Files[0];

            string imageFileName = DefaultProductImageFileName;
            string folderName = DefaultProductFolderName;

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

                return this.RedirectToAction("index", "productManagement", new { area = "administration" });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("", "Problem occured during product addition.");
                return this.View("productOperation", model);
            }
        }

        // GET: Administration/ProductAddition
        public ViewResult ProductEditing(int? id, ProductOperationViewModel model)
        {
            if(id == null)
            {
                // 404
            }

            this.ViewBag.Rooms = this.roomsService.GetRooms();
            this.ViewBag.Categories = this.categoriesService.GetCategories();
            this.ViewBag.Operation = "EditProduct";

            var product = this.productsService.GetProductById(id.Value);

            if(product == null)
            {
                // 404
            }

            model.Id = product.Id;
            model.Name = product.Name;
            model.Price = product.Price;
            model.DiscountPercentage = product.DiscountPercentage;
            model.Quantity = product.Quantity;
            model.Description = product.Description;
            model.RoomId = product.RoomId.Value;
            model.CategoryId = product.CategoryId.Value;
            model.ImagePath = product.ImagePath;

            ModelState.Clear();

            return this.View("ProductOperation", model);
        }

        // POST: Administration/UpdateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductOperationViewModel model)
        {
            var product = this.productsService.GetProductById(model.Id);
            var file = Request.Files[0];

            string imageFileName = model.ImagePath;
            string folderName = DefaultProductFolderName;

            product.Name = model.Name;
            product.Price = model.Price;
            product.DiscountPercentage = model.DiscountPercentage;
            product.DiscountedPrice = model.DiscountedPrice;
            product.Quantity = model.Quantity;
            product.Description = model.Description;

            product.ImagePath = this.imageUploader.Upload(file, Server, imageFileName, folderName);

            product.Room = this.roomsService.GetRoomById(model.RoomId);
            product.Category = this.categoriesService.GetCategoryById(model.CategoryId);

            product.HasDiscount = model.DiscountPercentage > 0 ? true : false;

            try
            {
                this.productsService.UpdateProduct(product);

                return this.RedirectToAction("index", "productManagement", new { area = "administration" });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("", "Problem occured during product editing.");
                return this.View("productOperation", model);
            }
        }

        // POST: Administration/AddRoom
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                return this.RedirectToAction("productOperation", "productManagement", new { area = "administration" });
            }
            catch (Exception)
            {
            }

            return this.RedirectToAction("productOperation", "productManagement", new { area = "administration" });
        }

        // POST: Administration/AddCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                this.RedirectToAction("productOperation", "productManagement", new { area = "administration" });
            }
            catch (Exception)
            {
            }

            return this.RedirectToAction("productOperation", "productManagement", new { area = "administration" });
        }
    }
}