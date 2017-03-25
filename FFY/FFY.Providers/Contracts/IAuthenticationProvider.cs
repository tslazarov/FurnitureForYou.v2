using FFY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;

namespace FFY.Providers.Contracts
{
    public interface IAuthenticationProvider
    {
        bool IsAuthenticated { get; }

        string CurrentUserId { get; }

        IdentityResult CreateUser(User user, string password);

        IList<string> GetUserRoles(string userId);

        void ChangeUserRole(string userId, string role);

        void UpdateSecurityStamp(string userId);

        void SignIn(User user, bool isPersistent, bool rememberBrowser);

        SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout);

        void SignOut();
    }
}
