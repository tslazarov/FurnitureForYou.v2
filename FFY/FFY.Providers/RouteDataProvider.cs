using FFY.Providers.Contracts;
using System.Web.Mvc;
using System.Web.Routing;

namespace FFY.Providers
{
    public class RouteDataProvider : IRouteDataProvider
    {
        public RouteData GetRouteData(Controller controller)
        {
            return controller.RouteData;
        }
    }
}
