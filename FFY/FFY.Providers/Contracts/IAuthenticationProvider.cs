using FFY.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Providers.Contracts
{
    public interface IAuthenticationProvider
    {
        bool IsAuthenticated { get; }

        string CurrentUserId { get; }

        IdentityResult CreateUser(User user, string password);

        void SignIn(User user, bool isPersistent, bool rememberBrowser);

        SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout);

        void SignOut();
    }
}
