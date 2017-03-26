using Bytes2you.Validation;
using FFY.Services.Contracts;
using FFY.Web.Custom.Attributes;
using FFY.Web.Mappings;
using FFY.Web.Models.Furniture;
using FFY.Web.Models.Home;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FFY.Web.Controllers
{
    [Localize]
    public class HomeController : Controller
    {
        private const int DefaultProductsCount = 4;

        private readonly IMapperProvider mapper;
        private readonly IProductsService productsService;

        public HomeController(IMapperProvider mapper,
            IProductsService productsService)
        {
            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IProductsService>(productsService, "Products service cannot be null.")
                .IsNull()
                .Throw();

            this.mapper = mapper;
            this.productsService = productsService;
        }

        public ActionResult Index(HomeViewModel model)
        {
            var latestProducts = this.productsService.GetLatestProducts(DefaultProductsCount);
            var highestRatedProducts = this.productsService.GetHighestRatedProducts(DefaultProductsCount);
            var discountProducts = this.productsService.GetDiscountProducts(DefaultProductsCount);

            model.LatestProducts =
               this.mapper.Map<IEnumerable<SingleProductSelectionViewModel>>(latestProducts);

            model.HighestRatedProducts =
                this.mapper.Map<IEnumerable<SingleProductSelectionViewModel>>(highestRatedProducts);

            model.DiscountProducts =
                this.mapper.Map<IEnumerable<SingleProductSelectionViewModel>>(discountProducts);

            return this.View(model);
        }
    }
}