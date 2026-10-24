using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.FW.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly Moq.Mock<Infrastructure.Interfaces.IUnitOfWork> _mockUnitOfWork;
        private readonly Moq.Mock<Infrastructure.Interfaces.IWise> _mockWise;
        private readonly Moq.Mock<Netcenter.Sdk.INetcenterSDK> _mockNetcenter;
        private readonly Api.Controllers.HomeController _controller;


        public HomeControllerTests()
        {
            _mockUnitOfWork = new Moq.Mock<Infrastructure.Interfaces.IUnitOfWork>();
            _mockWise = new Moq.Mock<Infrastructure.Interfaces.IWise>();
            _mockNetcenter = new Moq.Mock<Netcenter.Sdk.INetcenterSDK>();
            _controller = new Api.Controllers.HomeController(_mockUnitOfWork.Object, _mockNetcenter.Object)
            {
                RequestContext = TestBase.FakeHttpRequestContext()
            };
        }


        [TestMethod]
        public void HomeController_About_GET_HasAppInfo()
        {
            // Act

            // Arrange
            var result = _controller.About_GET() as System.Web.Http.IHttpActionResult;

            var version = (string)new PrivateObject(result, "Content").GetFieldOrProperty("version");
            var versionDate = (DateTime)new PrivateObject(result, "Content").GetFieldOrProperty("versionDate");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(version));
            Assert.IsTrue(versionDate.Year == DateTime.Now.Year);
        }

        [TestMethod]
        public void HomeController_Notification_GET_HasNotifications()
        {
            // Act
            int page = 1;
            int pagesize = 25;
            string search = "";

            _mockUnitOfWork.Setup(x => x.Read<BusinessObject.Entities.Notification>(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.Notification, bool>>>()
                )).Returns(() => MockedData.GetNotifications());

            // Arrange
            var result = _controller.Notification_GET(page, pagesize, search);

            var page2 = (int)new PrivateObject(result, "Content").GetFieldOrProperty("page");
            var pagecount = (int)new PrivateObject(result, "Content").GetFieldOrProperty("pagecount");
            var notifications = (List<BusinessObject.Entities.Notification>)new PrivateObject(result, "Content").GetFieldOrProperty("notifications");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(page == page2);
            Assert.IsTrue(pagecount > 0);
            Assert.IsTrue(notifications.Count > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void HomeController_Notification_POST_CreateSuccessful()
        {
            // Act
            BusinessObject.Entities.Notification notification = new BusinessObject.Entities.Notification() { Id = 0 };

            // Arrange
            var result = _controller.Notification_POST(notification);

            var model = (BusinessObject.Entities.Notification)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Id == 0);

            _mockUnitOfWork.Verify(x => x.Create(Moq.It.IsAny<BusinessObject.Entities.Notification>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void HomeController_Notification_POST_UpdateSuccessful()
        {
            // Act
            BusinessObject.Entities.Notification notification = new BusinessObject.Entities.Notification() { Id = 1 };

            // Arrange
            var result = _controller.Notification_POST(notification);

            var model = (BusinessObject.Entities.Notification)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Id == 1);

            _mockUnitOfWork.Verify(x => x.Update(Moq.It.IsAny<BusinessObject.Entities.Notification>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void HomeController_Notification_POST_HasErrors()
        {
            // Act
            BusinessObject.Entities.Notification notification = new BusinessObject.Entities.Notification() { Id = 0, Message = null };
            _controller.ModelState.AddModelError("Message", "Message is required");

            // Arrange
            var result = _controller.Notification_POST(notification);

            var statusCode = (System.Net.HttpStatusCode)new PrivateObject(result, "StatusCode").Target;
            var httpResults = (IEnumerable<Api.Models.HttpResult>)new PrivateObject(result, "Content").Target;

            int cnt = 0;
            foreach (var r in httpResults)
                cnt++;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(statusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(cnt > 0);
        }

        [TestMethod]
        public void HomeController_NotificationCheck_GET_HasListOf()
        {
            // Act
            _mockUnitOfWork.Setup(x => x.Read<BusinessObject.Entities.Notification>(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.Notification, bool>>>()
                )).Returns(() => MockedData.GetNotifications());

            // Arrange
            var result = _controller.NotificationCheck_GET();

            var total = (int)new PrivateObject(result, "Content").GetFieldOrProperty("total");
            var notifications = (List<BusinessObject.Entities.Notification>)new PrivateObject(result, "Content").GetFieldOrProperty("notifications");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(total > 0);
            Assert.IsTrue(notifications.Count < 6);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void HomeController_WhosOnline_GET_HasListOf()
        {
            // Act
            //_mockNetcenter.Setup(x => x.User(Moq.It.IsAny<string>(), Moq.It.IsAny<bool>()))
            //    .Returns(() => MockedData.GetUsers().First());

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.OnlineUsers, bool>>>()
                )).Returns(() => MockedData.GetOnlineUsers());

            // Arrange
            var result = _controller.WhosOnline_GET();

            var onlineUsers = (List<Api.Models.OnlineUsers>)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(onlineUsers.Count == 0);
            Assert.IsTrue(onlineUsers[0].NtUsername == "domain\\user");
            Assert.IsTrue(onlineUsers[0].ActiveTime == "00:32:49");

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void HomeController_UserHeartbeat_POST_CreateSuccessful()
        {
            // Act
            /* commented out to force Moq to return a NULL object */
            //_mockUnitOfWork.Setup(x => x.Read<BusinessObject.Entities.OnlineUsers>(
            //    Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.OnlineUsers, bool>>>()
            //    )).Returns(() => null);

            // Arrange
            var result = _controller.UserHeartbeat_POST();

            // Assert
            Assert.IsNotNull(result);

            _mockUnitOfWork.Verify(x => x.Create(Moq.It.IsAny<BusinessObject.Entities.OnlineUsers>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void HomeController_UserHeartbeat_POST_UpdateSuccessful()
        {
            // Act
            _mockUnitOfWork.Setup(x => x.Read<BusinessObject.Entities.OnlineUsers>(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.OnlineUsers, bool>>>()
                )).Returns(() => MockedData.GetOnlineUsers());

            // Arrange
            var result = _controller.UserHeartbeat_POST();

            // Assert
            Assert.IsNotNull(result);

            _mockUnitOfWork.Verify(x => x.Update(Moq.It.IsAny<BusinessObject.Entities.OnlineUsers>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

    }
}
