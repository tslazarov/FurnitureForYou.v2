using FFY.Web.Areas.Profile.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.SupportChatClientControllerTests
{
    [TestFixture]
    public class Index
    {
        [Test]
        public void ShouldReturnDefaultView()
        {
            // Arrange
            var supportChatClientManagementController = new SupportChatClientController();

            // Act and Assert
            supportChatClientManagementController.WithCallTo(scmc => scmc.Index())
                .ShouldRenderDefaultView();
        }
    }
}
