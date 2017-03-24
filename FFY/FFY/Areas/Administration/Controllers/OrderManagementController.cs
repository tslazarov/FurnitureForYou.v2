using Bytes2you.Validation;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.OrderManagement;
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
    public class OrderManagementController : Controller
    {
        private const int OrdersPerPage = 10;

        private readonly IMapperProvider mapper;
        private readonly IOrdersService ordersService;

        public OrderManagementController(IMapperProvider mapper,
            IOrdersService ordersService)
        {
            Guard.WhenArgument<IMapperProvider>(mapper, "Mapper provider cannot be null.")
               .IsNull()
               .Throw();

            Guard.WhenArgument<IOrdersService>(ordersService, "Orders service cannot be null.")
                .IsNull()
                .Throw();

            this.mapper = mapper;
            this.ordersService = ordersService;
        }


        // GET: Administration/OrderManagement
        public ViewResult Index(OrdersViewModel model)
        {
            return this.View(model);
        }

        // GET: Administration/SearchOrders
        public PartialViewResult SearchOrders(SearchModel searchModel, OrdersViewModel ordersModel, int? page)
        {
            int actualPage = page ?? 1;

            var result = this.ordersService.SearchOrders(searchModel.SearchWord, searchModel.SortBy, searchModel.FilterBy, actualPage, OrdersPerPage);
            var count = this.ordersService.GetOrdersCount(searchModel.SearchWord, searchModel.FilterBy);

            ordersModel.SearchModel = searchModel;
            ordersModel.OrdersCount = count;
            ordersModel.Pages = (int)Math.Ceiling((double)count / OrdersPerPage);
            ordersModel.Page = actualPage;
            ordersModel.Orders = mapper.Map<IEnumerable<SingleOrderViewModel>>(result);

            return this.PartialView("OrdersPartial", ordersModel);
        }
    }
}