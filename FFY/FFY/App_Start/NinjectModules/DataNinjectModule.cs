using Ninject.Modules;
using Ninject.Extensions.Conventions;

using FFY.Data.Assembly;
using FFY.Data.Contracts;
using FFY.Data;
using Ninject.Web.Common;
using FFY.Data.Factories;
using Ninject.Extensions.Factory;

namespace FFY.Web.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind(x =>
                    x.FromAssemblyContaining<IDataAssembly>()
                    .SelectAllClasses()
                    .BindDefaultInterface()
                );

            this.Rebind<IFFYDbContext>().To<FFYDbContext>().InRequestScope();
            this.Bind<IUserFactory>().ToFactory();
            this.Bind<IShoppingCartFactory>().ToFactory();
            this.Bind<IContactFactory>().ToFactory();
            this.Bind<IRoomFactory>().ToFactory();
        }
    }
}