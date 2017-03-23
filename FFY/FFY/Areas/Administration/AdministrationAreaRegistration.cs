using System.Web.Mvc;

namespace FFY.Web.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdministrationUsers",
                "{language}/administration/users/{id}",
                new { controller = "UserManagement", action = "UserProfile", language = "en" }
            );


            context.MapRoute(
                "AdministrationDefault",
                "{language}/administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, language = "en" }
            );
        }
    }
}