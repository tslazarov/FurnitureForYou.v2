using System.Web.Mvc;
using System.Web.Routing;

namespace FFY.Providers.Contracts
{
    public interface IRouteDataProvider
    {
        RouteData GetRouteData(Controller controller);
    }
}
