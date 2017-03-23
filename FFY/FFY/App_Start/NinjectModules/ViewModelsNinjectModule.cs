using FFY.Web.Areas.Administration.Models.UserManagement;
using FFY.Web.Areas.Profile.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.App_Start.NinjectModules
{
    public class ViewModelsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ProfileViewModel>().To<ProfileViewModel>();
        }
    }
}