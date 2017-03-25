using Bytes2you.Validation;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Profile.Models;
using FFY.Web.Custom.Attributes;
using FFY.Web.Mappings;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FFY.Web.Areas.Profile.Controllers
{
    [Localize]
    [Authorize]
    public class FavoritesController : Controller
    {
        private const int ProductsPerPage = 16;

        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IMapperProvider mapper;
        private readonly IUsersService usersService;

        public FavoritesController(IAuthenticationProvider authenticationProvider,
            IMapperProvider mapper,
            IUsersService usersService)
        {
            Guard.WhenArgument<IAuthenticationProvider>(authenticationProvider, "Authentication provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IUsersService>(usersService, "Users service cannot be null.")
                .IsNull()
                .Throw();

            this.authenticationProvider = authenticationProvider;
            this.mapper = mapper;
            this.usersService = usersService;
        }


        // GET: Profile/Favorites
        public ActionResult Index(FavoriteProductsViewModel model)
        {
            return this.View(model);
        }

        // GET: Administration/SearchProducts
        public PartialViewResult PagingProducts(FavoriteProductsViewModel productsModel, int? page)
        {
            int actualPage = page ?? 1;

            var userId = this.authenticationProvider.CurrentUserId;
            var result = this.usersService.GetFavoriteProducts(userId, actualPage, ProductsPerPage);
            var count = this.usersService.GetFavoriteProductsCount(userId);

            productsModel.FavoriteProductsCount = count;
            productsModel.Pages = (int)Math.Ceiling((double)count / ProductsPerPage);
            productsModel.Page = actualPage;
            productsModel.FavoriteProducts = mapper.Map<IEnumerable<SingleFavoriteProductViewModel>>(result);

            return this.PartialView("FavoriteProductsPartial", productsModel);
        }
    }
}