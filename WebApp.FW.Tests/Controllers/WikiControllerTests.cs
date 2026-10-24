using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WebApp.FW.Tests.Controllers
{
    [TestClass]
    public class WikiControllerTests
    {
        private readonly Moq.Mock<Infrastructure.Interfaces.IUnitOfWork> _mockUnitOfWork;
        private readonly Moq.Mock<Netcenter.Sdk.INetcenterSDK> _mockNetcenter;
        private readonly Api.Controllers.WikiController _controller;

        public WikiControllerTests()
        {
            _mockUnitOfWork = new Moq.Mock<Infrastructure.Interfaces.IUnitOfWork>();
            _mockNetcenter = new Moq.Mock<Netcenter.Sdk.INetcenterSDK>();
            _controller = new Api.Controllers.WikiController(_mockUnitOfWork.Object, _mockNetcenter.Object)
            {
                RequestContext = TestBase.FakeHttpRequestContext()
            };
        }




        [TestMethod]
        public void WikiController_Navigation_GET_HasListOf()
        {
            // Arrange

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiMenu, bool>>>()
                )).Returns(() => MockedData.GetWikiMenus());

            // Act
            var result = _controller.Navigation_GET();

            var model = (List<BusinessObject.Entities.WikiMenu>)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_WikiContent_GET_HasArticle()
        {
            // Arrange
            int id = 1;

            _mockNetcenter.Setup(x => x.UserAppRoleNames(Moq.It.IsAny<string>()))
                .Returns(() => MockedData.GetUserRoles());

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => MockedData.GetWikiContents());

            // Act
            var result = _controller.WikiContent_GET(id);

            var model = (BusinessObject.Entities.WikiContent)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Id == id);
            Assert.IsTrue(model.Hits > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_WikiContent_GET_Null()
        {
            // Arrange
            int id = 1;

            _mockNetcenter.Setup(x => x.UserAppRoleNames(Moq.It.IsAny<string>()))
                .Returns(() => MockedData.GetUserRoles());

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => Enumerable.Empty<BusinessObject.Entities.WikiContent>());

            // Act
            var result = _controller.WikiContent_GET(id);

            //var model = new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            //Assert.IsNull(model);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_WikiContent_POST_CreateSuccessful()
        {
            // Arrange
            bool isHome = false;
            BusinessObject.Entities.WikiContent content = new BusinessObject.Entities.WikiContent
            {
                Id = 0,
                IsHome = true
            };

            _mockUnitOfWork.Setup(x => x.ExecuteSqlCommand(Moq.It.IsAny<string>(), Moq.It.IsAny<object>()))
                .Callback(() => isHome = true);

            _mockUnitOfWork.Setup(x => x.ReadAsNoTracking(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => Enumerable.Empty<BusinessObject.Entities.WikiContent>());

            // Act
            var result = _controller.WikiContent_POST(content);

            var model = (BusinessObject.Entities.WikiContent)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(isHome);
            Assert.IsTrue(model.Version == 1);

            _mockUnitOfWork.Verify(x => x.BeginTransaction(System.Data.IsolationLevel.ReadCommitted), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Create(Moq.It.IsAny<BusinessObject.Entities.WikiContent>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_WikiContent_POST_UpdateSuccessful()
        {
            // Arrange
            bool isHome = false;
            BusinessObject.Entities.WikiContent content = new BusinessObject.Entities.WikiContent
            {
                Id = 1,
                IsHome = true
            };

            _mockUnitOfWork.Setup(x => x.ExecuteSqlCommand(Moq.It.IsAny<string>(), Moq.It.IsAny<object>()))
                .Callback(() => isHome = true);

            _mockUnitOfWork.Setup(x => x.ReadAsNoTracking(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => MockedData.GetWikiContents());

            // Act
            var result = _controller.WikiContent_POST(content);

            var model = (BusinessObject.Entities.WikiContent)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(isHome);
            Assert.IsTrue(model.Version == 3);

            _mockUnitOfWork.Verify(x => x.BeginTransaction(System.Data.IsolationLevel.ReadCommitted), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Update(Moq.It.IsAny<BusinessObject.Entities.WikiContent>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_WikiContent_POST_HasErrors()
        {
            // Arrange

            BusinessObject.Entities.WikiContent content = new BusinessObject.Entities.WikiContent
            {
                Id = 1,
                IsHome = false,
                Title = null
            };

            _controller.ModelState.Clear();
            _controller.ModelState.AddModelError("", "Title is required");

            // Act
            var result = _controller.WikiContent_POST(content);

            var statusCode = (System.Net.HttpStatusCode)new PrivateObject(result, "StatusCode").Target;
            var httpResults = (IEnumerable<Api.Models.HttpResult>)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(statusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(httpResults.ToList().Count > 0);
        }

        [TestMethod]
        public void WikiController_Articles_HasListOf()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = null;

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => MockedData.GetWikiContents());

            // Act
            var result = _controller.Articles(page, pagesize, search);

            var page1 = (int)new PrivateObject(result, "Content").GetFieldOrProperty("page");
            var pagecount = (int)new PrivateObject(result, "Content").GetFieldOrProperty("pagecount");
            var articles = (List<Api.Models.WikiContentDto>)new PrivateObject(result, "Content").GetFieldOrProperty("articles");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(articles.Count > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_Articles_HasListOf_BySearch()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = "Home";

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => MockedData.GetWikiContents());

            // Act
            var result = _controller.Articles(page, pagesize, search);

            var page1 = (int)new PrivateObject(result, "Content").GetFieldOrProperty("page");
            var pagecount = (int)new PrivateObject(result, "Content").GetFieldOrProperty("pagecount");
            var articles = (List<Api.Models.WikiContentDto>)new PrivateObject(result, "Content").GetFieldOrProperty("articles");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(articles.Count > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_WikiSearch_HasListOf()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = null;

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => MockedData.GetWikiContents());

            // Act
            var result = _controller.WikiSearch(page, pagesize, search);

            var page1 = (int)new PrivateObject(result, "Content").GetFieldOrProperty("page");
            var pagecount = (int)new PrivateObject(result, "Content").GetFieldOrProperty("pagecount");
            var articles = (List<Api.Models.WikiSearchDto>)new PrivateObject(result, "Content").GetFieldOrProperty("articles");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(articles.Count > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_WikiSearch_HasListOf_BySearch()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = "Home";

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiContent, bool>>>()
                )).Returns(() => MockedData.GetWikiContents());

            // Act
            var result = _controller.WikiSearch(page, pagesize, search);

            var page1 = (int)new PrivateObject(result, "Content").GetFieldOrProperty("page");
            var pagecount = (int)new PrivateObject(result, "Content").GetFieldOrProperty("pagecount");
            var articles = (List<Api.Models.WikiSearchDto>)new PrivateObject(result, "Content").GetFieldOrProperty("articles");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(articles.Count > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_Menus_HasListOf()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiMenu, bool>>>(),
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiMenu, object>>>()
                )).Returns(() => MockedData.GetWikiMenus());

            _mockUnitOfWork.Setup(x => x.ReadById<BusinessObject.Entities.WikiMenu>(Moq.It.IsAny<object>()))
                .Returns(() => MockedData.GetWikiMenus().First());

            // Act
            var result = _controller.Menus();

            var model = (List<Api.Models.WikiMenuDto>)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count > 0);

            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_Menu_POST_CreateSuccessful()
        {
            // Arrange
            BusinessObject.Entities.WikiMenu menu = new BusinessObject.Entities.WikiMenu
            {
                Id = 0,
                ContentId = 1
            };

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiMenu, bool>>>()
                )).Returns(() => MockedData.GetWikiMenus());

            _mockUnitOfWork.Setup(x => x.ReadById<BusinessObject.Entities.WikiContent>(Moq.It.IsAny<object>()))
                .Returns(() => MockedData.GetWikiContents().Find(x => x.Id == menu.ContentId));

            // Act
            var result = _controller.Menu_POST(menu);

            var model = (BusinessObject.Entities.WikiMenu)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.ContentId == 1);
            Assert.IsNotNull(model.WikiContent);

            _mockUnitOfWork.Verify(x => x.Create(Moq.It.IsAny<BusinessObject.Entities.WikiMenu>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Update(Moq.It.IsAny<List<BusinessObject.Entities.WikiMenu>>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_Menu_POST_UpdateSuccessful()
        {
            // Arrange
            BusinessObject.Entities.WikiMenu menu = new BusinessObject.Entities.WikiMenu
            {
                Id = 1,
                ContentId = 1
            };

            _mockUnitOfWork.Setup(x => x.Read(
                Moq.It.IsAny<System.Linq.Expressions.Expression<Func<BusinessObject.Entities.WikiMenu, bool>>>()
                )).Returns(() => MockedData.GetWikiMenus());

            _mockUnitOfWork.Setup(x => x.ReadById<BusinessObject.Entities.WikiContent>(Moq.It.IsAny<object>()))
                .Returns(() => MockedData.GetWikiContents().Find(x => x.Id == menu.ContentId));

            // Act
            var result = _controller.Menu_POST(menu);

            var model = (BusinessObject.Entities.WikiMenu)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.ContentId == 1);
            Assert.IsNotNull(model.WikiContent);

            _mockUnitOfWork.Verify(x => x.Update(Moq.It.IsAny<BusinessObject.Entities.WikiMenu>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Update(Moq.It.IsAny<List<BusinessObject.Entities.WikiMenu>>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_Menu_POST_HasErrors()
        {
            // Arrange
            BusinessObject.Entities.WikiMenu menu = new BusinessObject.Entities.WikiMenu
            {
                Id = 1,
                ContentId = 0
            };

            // Act
            var result = _controller.Menu_POST(menu);

            var statusCode = (System.Net.HttpStatusCode)new PrivateObject(result, "StatusCode").Target;
            var httpResults = (IEnumerable<Api.Models.HttpResult>)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(statusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(httpResults.ToList().Count > 0);
        }

        [TestMethod]
        public void WikiController_Menu_DELETE_DeleteSuccessful()
        {
            // Arrange
            int id = 1;

            _mockUnitOfWork.Setup(x => x.ReadById<BusinessObject.Entities.WikiMenu>(Moq.It.IsAny<object>()))
                .Returns(() => MockedData.GetWikiMenus().Find(x => x.Id == id));

            // Act
            var result = _controller.Menu_DELETE(id);

            // Assert
            Assert.IsNotNull(result);

            _mockUnitOfWork.Verify(x => x.Delete(Moq.It.IsAny<BusinessObject.Entities.WikiMenu>()), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Moq.Times.Once);
            _mockUnitOfWork.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void WikiController_Menu_DELETE_HasErrors()
        {
            // Arrange
            int id = 1;

            _mockUnitOfWork.Setup(x => x.ReadById<BusinessObject.Entities.WikiMenu>(Moq.It.IsAny<object>()))
                .Returns(() => null);

            // Act
            var result = _controller.Menu_DELETE(id);

            var statusCode = (System.Net.HttpStatusCode)new PrivateObject(result, "StatusCode").Target;
            var httpResults = (IEnumerable<Api.Models.HttpResult>)new PrivateObject(result, "Content").Target;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(statusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(httpResults.ToList().Count > 0);
        }




    }
}
