using FFY.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
