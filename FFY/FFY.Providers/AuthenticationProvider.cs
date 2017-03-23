using System.Web;
using FFY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FFY.Providers.Contracts;
using FFY.IdentityConfig;
using Bytes2you.Validation;

namespace FFY.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IHttpContextProvider httpContextProvider;

        public AuthenticationProvider(IHttpContextProvider httpContextProvider)
        {
            Guard.WhenArgument<IHttpContextProvider>(httpContextProvider, "Http context provider cannot be null.")
                .IsNull()
                .Throw();

            this.httpContextProvider = httpContextProvider;
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.IsAuthenticated;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserId();
            }
        }

        public IdentityResult CreateUser(User user, string password)
        {
            var manager = this.httpContextProvider.CurrentOwinContext.GetUserManager<ApplicationUserManager>();

            var result = manager.Create(user, password);

            if(result.Succeeded)
            {
                manager.AddToRole(user.Id, "User");
            }

            return result;
        }

        public void SignIn(User user, bool isPersistent, bool rememberBrowser)
        {
            var manager = this.httpContextProvider.CurrentOwinContext.GetUserManager<ApplicationSignInManager>();

            manager.SignIn(user, isPersistent, rememberBrowser);
        }

        public SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout)
        {
            var manager = this.httpContextProvider.CurrentOwinContext.GetUserManager<ApplicationSignInManager>();

            return manager.PasswordSignIn(email, password, rememberMe, shouldLockout);
        }

        public void SignOut()
        {
            this.httpContextProvider.CurrentOwinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
