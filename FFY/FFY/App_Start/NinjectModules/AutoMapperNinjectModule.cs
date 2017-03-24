using FFY.Web.Mappings;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFY.Web.App_Start.NinjectModules
{
    public class AutoMapperNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapperProvider>().To<MapperProvider>();
        }
    }
}