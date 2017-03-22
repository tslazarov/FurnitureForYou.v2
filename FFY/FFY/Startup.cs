using FFY.Services;
using FFY.Web.App_Start;
using FFY.Web.SupportChat;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Ninject;
using Owin;

[assembly: OwinStartupAttribute(typeof(FFY.Web.Startup))]
namespace FFY.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
