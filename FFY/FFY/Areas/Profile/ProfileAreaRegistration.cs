using System.Web.Mvc;

namespace FFY.Web.Areas.Profile
{
    public class ProfileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Profile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProfileDefault",
                "profile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, language = "en" }
            );

            context.MapRoute(
                "ProfileDefaultWithLanguage",
                "{language}/profile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, language = "en" }
            );
        }
    }
}