using FFY.Models;
using FFY.Providers.Contracts;
using FFY.Services.Contracts;
using FFY.Web.Areas.Administration.Controllers;
using FFY.Web.Areas.Administration.Models;
using FFY.Web.Areas.Administration.Models.UserManagement;
using FFY.Web.Mappings;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace FFY.UnitTests.Web.UserManagementControllerTests
{
    [TestFixture]
    public class SearchUsers
    {
        [Test]
        public void ShouldCallSearchUsersMethodOfUsersService()
        {
            // Arrange
            var page = 1;
            var usersPerPage = 10;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users)
                .Verifiable();
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            mockedUserService.Verify(cs =>
                cs.SearchUsers(searchModel.SearchWord,
                    searchModel.SortBy,
                    page,
                    usersPerPage), Times.Once);
        }

        [Test]
        public void ShouldCallGetUsersCountMethodOfUsersService()
        {
            // Arrange
            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count)
                .Verifiable();

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                    mockedMapperProvider.Object,
                    mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, null);

            // Assert
            mockedUserService.Verify(cs =>
                cs.GetUsersCount(searchModel.SearchWord), Times.Once);
        }

        [Test]
        public void ShouldSetSearchModelOfUsersViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreSame(searchModel, usersViewModel.SearchModel);
        }

        [Test]
        public void ShouldSetUsersCountOfUsersViewModel()
        {
            // Arrange
            var page = 1;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreEqual(users.Count, usersViewModel.UsersCount);
        }

        [Test]
        public void ShouldSetPagesOfUsersViewModel()
        {
            // Arrange
            var usersPerPage = 10;
            var page = 1;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>();

            var expectedPages = (int)Math.Ceiling((double)users.Count / usersPerPage);

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreEqual(expectedPages, usersViewModel.Pages);
        }

        [Test]
        public void ShouldSetPageOfUsersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            Assert.AreEqual(page, usersViewModel.Page);
        }

        [Test]
        public void ShouldMapAndSetUsersOfUsersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>() { user };

            var singleUsers = new List<SingleUserViewModel>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp =>
                mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()))
                .Returns(singleUsers);
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act 
            userManagementController.SearchUsers(searchModel, usersViewModel, page);

            // Assert
            CollectionAssert.AreEquivalent(singleUsers, usersViewModel.Users);
        }

        [Test]
        public void ShouldRenderUsersPartialViewWithUsersViewModel()
        {
            // Arrange
            var page = 3;

            var searchModel = new SearchModel();
            var usersViewModel = new UsersViewModel();

            var user = new User();
            var users = new List<User>();

            var mockedAuthenticationProvider = new Mock<IAuthenticationProvider>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            mockedMapperProvider.Setup(mp => mp.Map<IEnumerable<SingleUserViewModel>>(It.IsAny<object>()));
            var mockedUserService = new Mock<IUsersService>();
            mockedUserService.Setup(us => us.SearchUsers(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(users);
            mockedUserService.Setup(us => us.GetUsersCount(It.IsAny<string>()))
                .Returns(users.Count);

            var userManagementController = new UserManagementController(mockedAuthenticationProvider.Object,
                   mockedMapperProvider.Object,
                   mockedUserService.Object);

            // Act and Assert
            userManagementController.WithCallTo(umc => umc.SearchUsers(searchModel,
                usersViewModel,
                page))
                .ShouldRenderPartialView("UsersPartial")
                .WithModel<UsersViewModel>(model => Assert.AreEqual(usersViewModel, model));
        }
    }
}
