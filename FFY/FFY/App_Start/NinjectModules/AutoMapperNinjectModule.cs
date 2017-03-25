using FFY.Web.Mappings;
using Ninject.Modules;

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