using FFY.IdentityConfig;
using FFY.IdentityConfig.Contracts;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.App_Start.NinjectModules
{
    public class IdentityNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthenticationProvider>().To<HttpContextAuthenticationProvider>();
        }
    }
}