using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FFY.UnitTests.Web.ProductManagementControllerTests.Mocks
{
    public class MockedHttpFileCollectionBase : HttpFileCollectionBase
    {
        public override HttpPostedFileBase this[int index]
        {
            get
            {
                return new MockedHttpPostedFileBase();
            }
        }
    }
}
