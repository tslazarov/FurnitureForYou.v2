using Ninject.Modules;
using Ninject.Extensions.Conventions;
using FFY.Services.Assembly;


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