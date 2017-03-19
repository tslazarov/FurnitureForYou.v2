using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace FFY.Providers.Contracts
{
    public interface IRouteDataProvider
    {
        RouteData GetRouteData(Controller controller);
    }
}
