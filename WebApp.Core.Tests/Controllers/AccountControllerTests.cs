using Microsoft.VisualStudio.TestTools.UnitTesting;
using UMAS.Infrastructure.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UMAS.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private readonly Moq.Mock<UMASContext> _context;
        private readonly Moq.Mock<Netcenter.Sdk.INetcenterSDK> _netcenter;
        private readonly Api.Controllers.AccountController _controller;

        public AccountControllerTests()
        {
            _context = new Moq.Mock<UMASContext>();
            _netcenter = new Moq.Mock<Netcenter.Sdk.INetcenterSDK>();
            _controller = new Api.Controllers.AccountController(_netcenter.Object, _context.Object)
            {
                //ControllerContext = TestBase.FakeControllerContext()

                ControllerContext = new ControllerContext()
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
                    {
                        User = new System.Security.Claims.ClaimsPrincipal(
                            new System.Security.Claims.ClaimsIdentity(
                                new System.Security.Claims.Claim[]
                                {
                                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "domain\\user")
                                }))
                    }
                }
            };
        }

        [TestMethod]
        public void AccountController_AboutMe_GET_HasUserInformation()
        {
            // Arrange
            _netcenter.Setup(x => x.User(Moq.It.IsAny<string>(), Moq.It.IsAny<bool>() ))
                .Returns(() => MockedData.GetUsers().First());

            _netcenter.Setup(x => x.UserAppRoleNames(Moq.It.IsAny<string>()))
                .Returns(() => MockedData.GetRoles());

            var userInfos = MockedData.GetUserInfos();
            _context.Setup(x => x.UserInfos).Returns(MockedData.MockDbSet(userInfos).Object);

            // Act
            var result = (OkObjectResult)_controller.AboutMe_GET();

            var model = (Api.Models.AboutDto)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.User.NtUsername == "domain\\user");
            Assert.IsTrue(model.UserInfo.Title == "Software Developer I");
            Assert.IsTrue(model.Roles.Any(x => x == "Admin"));

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AccountController_AboutMe_POST_Add()
        {
            // Arrange
            Core.Entities.UserInfo info = new Core.Entities.UserInfo { };

            var userInfos = new List<Core.Entities.UserInfo> { };
            _context.Setup(x => x.UserInfos).Returns(MockedData.MockDbSet(userInfos).Object);

            // Act
            var result = (OkResult)_controller.AboutMe_POST(info);

            // Assert
            Assert.IsNotNull(result);

            _context.Verify(x => x.Add(Moq.It.IsAny<Core.Entities.UserInfo>()), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AccountController_AboutMe_POST_Update()
        {
            // Arrange
            Core.Entities.UserInfo info = new Core.Entities.UserInfo { Title = "My Title", Bio = "My Bio" };

            var userInfos = MockedData.GetUserInfos();
            _context.Setup(x => x.UserInfos).Returns(MockedData.MockDbSet(userInfos).Object);

            // Act
            var result = (OkResult)_controller.AboutMe_POST(info);

            // Assert
            Assert.IsNotNull(result);

            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.UserInfo>()), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }



    }
}
