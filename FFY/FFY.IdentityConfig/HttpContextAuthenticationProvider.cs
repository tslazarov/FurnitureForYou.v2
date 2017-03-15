using System.Web;
using FFY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FFY.IdentityConfig.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FFY.IdentityConfig
{
    public class HttpContextAuthenticationProvider : IAuthenticationProvider
    {

        public HttpContextAuthenticationProvider()
        {

        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public IdentityResult CreateUser(User user, string password)
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var result = manager.Create(user, password);

            if(result.Succeeded)
            {
                manager.AddToRole(user.Id, "User");
            }

            return result;
        }

        public void SignIn(User user, bool isPersistent, bool rememberBrowser)
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            manager.SignIn(user, isPersistent, rememberBrowser);
        }

        public SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout)
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            return manager.PasswordSignIn(email, password, rememberMe, shouldLockout);
        }

        public void SignOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
