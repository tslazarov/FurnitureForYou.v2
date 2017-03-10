using System.Web;
using FFY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FFY.IdentityConfig.Contracts;

namespace FFY.IdentityConfig
{
    public class HttpContextAuthenticationProvider : IAuthenticationProvider
    {
        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public IdentityResult CreateUser(string email, string password)
        {
            var user = new User { Email = email, UserName = email };
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var result = manager.Create(user, password);

            return result;
        }

        public IdentityResult CreateUser(User user, string password)
        {
            var manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //TODO: Remove when view form is fixed
            user.FirstName = "Test";
            user.LastName = "Test";

            var result = manager.Create(user, password);

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
