using Bytes2you.Validation;
using FFY.Services.Contracts;
using FFY.Web.Models.Furniture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    public class FurnitureController : Controller
    {
        private readonly IProductsService productsService;

        public FurnitureController(IProductsService productsService)
        {
            Guard.WhenArgument<IProductsService>(productsService, "Products service cannot be null.")
                .IsNull()
                .Throw();

            this.productsService = productsService;
        }

        // GET: Furniture/Product
        public ActionResult Product(int? id, DetailedProductViewModel model)
        {
            model.Product = this.productsService.GetProductById(id.Value);

            if(model.Product == null)
            {
                // 404
            }

            return this.View(model);
        }
    }
}