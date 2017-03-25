using FFY.Providers.Assembly;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

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