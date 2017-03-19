using Ninject.Modules;
using Ninject.Extensions.Conventions;
using FFY.Providers.Assembly;

namespace FFY.Web.App_Start.NinjectModules
{
    public class ProvidersNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind(x =>
                 x.FromAssemblyContaining<IProvidersAssembly>()
                 .SelectAllClasses()
                 .BindDefaultInterface()
             );
        }
    }
}