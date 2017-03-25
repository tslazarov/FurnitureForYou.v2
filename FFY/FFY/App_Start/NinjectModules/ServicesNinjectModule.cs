using FFY.Services.Assembly;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace FFY.Web.App_Start.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind(x =>
                x.FromAssemblyContaining<IServicesAssembly>()
                .SelectAllClasses()
                .BindDefaultInterface()
            );
        }
    }
}