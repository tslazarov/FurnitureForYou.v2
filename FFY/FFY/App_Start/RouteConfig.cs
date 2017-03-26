using System.Web.Mvc;
using System.Web.Routing;

namespace FFY.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FurnitureAll",
                url: "{language}/furniture/all",
                defaults: new {
                    language = "en",
                    controller = "Furniture",
                    action = "Products",
                    filterBy = "all"
                }
            );

            routes.MapRoute(
                name: "FurnitureLatest",
                url: "{language}/furniture/latest",
                defaults: new
                {
                    language = "en",
                    controller = "Furniture",
                    action = "Products",
                    filterBy = "latest"
                }
            );

            routes.MapRoute(
                name: "FurnitureDiscount",
                url: "{language}/furniture/discount",
                defaults: new
                {
                    language = "en",
                    controller = "Furniture",
                    action = "Products",
                    filterBy = "discount"
                }
            );

            routes.MapRoute(
                name: "FurnitureRating",
                url: "{language}/furniture/rating",
                defaults: new
                {
                    language = "en",
                    controller = "Furniture",
                    action = "Products",
                    filterBy = "rating"
                }
            );

            routes.MapRoute(
                name: "FurnitureRooms",
                url: "{language}/furniture/room/{filterBy}",
                defaults: new
                {
                    language = "en",
                    controller = "Furniture",
                    action = "Products"
                }
            );

            routes.MapRoute(
                name: "LanguageHomePage",
                url: "{language}",
                defaults: new
                {
                    language = "en",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    language = "en",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                }
            );

            routes.MapRoute(
                name: "DefaultWithLanguage",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new
                {
                    language = "en",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                }
            );
        }
    }
}
