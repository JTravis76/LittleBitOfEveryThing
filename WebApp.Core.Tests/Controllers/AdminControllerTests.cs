using Microsoft.VisualStudio.TestTools.UnitTesting;
using UMAS.Infrastructure.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UMAS.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        private readonly Moq.Mock<UMASContext> _context;
        private readonly Api.Controllers.AdminController _controller;

        public AdminControllerTests()
        {
            _context = new Moq.Mock<UMASContext>();
            _controller = new Api.Controllers.AdminController(_context.Object)
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


        #region Group Types

        [TestMethod]
        public void AdminController_GroupType_GET_HasListOf()
        {
            // Act
            int page = 1;
            int pagesize = 25;
            string search = "";

            var groupTypes = MockedData.GetGroupTypes();
            _context.Setup(x => x.GroupTypes).Returns(MockedData.MockDbSet(groupTypes).Object);

            // Arrange
            var result = (OkObjectResult)_controller.GroupTypes_GET(page, pagesize, search);

            var model = result.Value as Api.Models.GroupTypePagination;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Total == 1);
            Assert.IsTrue(model.PageCount > 0);
            Assert.IsTrue(model.GroupTypes.Count > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_GroupType_POST_Add()
        {
            // Act
            Core.Entities.GroupType groupType = new Core.Entities.GroupType();

            // Arrange
            var result = (OkObjectResult)_controller.GroupType_POST(groupType);

            var model = (Core.Entities.GroupType)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.GroupTypeId == 0);

            _context.Verify(x => x.Add(Moq.It.IsAny<Core.Entities.GroupType>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_GroupType_POST_Update()
        {
            // Act
            Core.Entities.GroupType groupType = new Core.Entities.GroupType() { GroupTypeId = 1 };

            // Arrange
            var result = (OkObjectResult)_controller.GroupType_POST(groupType);

            var model = (Core.Entities.GroupType)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.GroupTypeId == 1);

            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.GroupType>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_GroupType_POST_HasErrors()
        {
            // Act
            Core.Entities.GroupType groupType = new Core.Entities.GroupType() { GroupTypeName = "" };

            _controller.ModelState.Clear();
            _controller.ModelState.AddModelError("Name", "Group Type Name is required");

            // Arrange
            var result = (BadRequestObjectResult)_controller.GroupType_POST(groupType);

            var httpResults = (IEnumerable<Api.Models.HttpResult>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(httpResults.Count() > 0);
        }

        [TestMethod]
        public void AdminController_GroupType_DELETE_Update()
        {
            // Act
            int groupTypeId = 1;

            var groupTypes = MockedData.GetGroupTypes();
            _context.Setup(x => x.GroupTypes).Returns(MockedData.MockDbSet(groupTypes).Object);

            // Arrange
            var result = (OkObjectResult)_controller.GroupType_DELETE(groupTypeId);

            var model = (Core.Entities.GroupType)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.GroupTypeId == 1);

            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.GroupType>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_GroupType_DELETE_HasErrors()
        {
            // Act
            int groupTypeId = 3;

            var groupTypes = MockedData.GetGroupTypes();
            _context.Setup(x => x.GroupTypes).Returns(MockedData.MockDbSet(groupTypes).Object);

            // Arrange
            var result = (BadRequestObjectResult)_controller.GroupType_DELETE(groupTypeId);

            var httpResults = (IEnumerable<Api.Models.HttpResult>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(httpResults.Count() > 0);
        }

        #endregion

        #region Groups

        [TestMethod]
        public void AdminController_Group_GET_HasListOf()
        {
            // Act
            int page = 1;
            int pagesize = 25;
            string search = "";

            var groups = MockedData.GetGroups();
            _context.Setup(x => x.Groups).Returns(MockedData.MockDbSet(groups).Object);

            // Arrange
            var result = (OkObjectResult)_controller.Group_GET(page, pagesize, search);

            var model = result.Value as Api.Models.GroupPagination;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Total == 1);
            Assert.IsTrue(model.PageCount > 0);
            Assert.IsTrue(model.Groups.Count > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_Group_POST_Add()
        {
            // Act
            Core.Entities.Group group = new Core.Entities.Group() { GroupId = 0, GroupTypeId = 1 };

            _context.Setup(x => x.Database).Returns(MockedData.MockDatabaseFacade(_context.Object));

            // Arrange
            var result = (OkObjectResult)_controller.Group_POST(group);

            var model = (Core.Entities.Group)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.GroupId == 0);

            _context.Verify(x => x.Database.BeginTransaction(), Moq.Times.Once);
            _context.Verify(x => x.Add(Moq.It.IsAny<Core.Entities.Group>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_Group_POST_Update()
        {
            // Act
            Core.Entities.Group group = new Core.Entities.Group()
            {
                GroupId = 1,
                GroupTypeId = 1,
                GroupMember = MockedData.GetGroupMembers().ToList()
            };

            _context.Setup(x => x.Database).Returns(MockedData.MockDatabaseFacade(_context.Object));

            // Arrange
            var result = (OkObjectResult)_controller.Group_POST(group);

            var model = (Core.Entities.Group)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.GroupId == 1);

            _context.Verify(x => x.Database.BeginTransaction(), Moq.Times.Once);
            //No-way to verify the "ExecuteSqlCommand()" was triggered, just counted the invocation on the Database method
            _context.Verify(x => x.Database, Moq.Times.Exactly(3));
            _context.Verify(x => x.Add(Moq.It.IsAny<Core.Entities.GroupMember>()), Moq.Times.Once);
            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.Group>()), Moq.Times.Once);
            _context.Verify(x => x.Database.CommitTransaction(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_Group_POST_HasErrors()
        {
            // Act
            Core.Entities.Group group = new Core.Entities.Group()
            {
                GroupId = 0,
                GroupTypeId = 0
            };

            // Arrange
            var result = (BadRequestObjectResult)_controller.Group_POST(group);

            var httpResults = (IEnumerable<Api.Models.HttpResult>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(httpResults.Count() > 0);
        }

        [TestMethod]
        public void AdminController_Group_DELETE_Update()
        {
            // Act
            int groupId = 1;

            _context.Setup(x => x.Database).Returns(MockedData.MockDatabaseFacade(_context.Object));

            var groups = MockedData.GetGroups();
            _context.Setup(x => x.Groups).Returns(MockedData.MockDbSet(groups).Object);

            // Arrange
            var result = (OkObjectResult)_controller.Group_DELETE(groupId);

            var model = (Core.Entities.Group)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.GroupId == 1);

            //No-way to verify the "ExecuteSqlCommand()" was triggered, just counted the invocation on the Database method
            _context.Verify(x => x.Database, Moq.Times.Exactly(3));
            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.Group>()), Moq.Times.Once);
            _context.Verify(x => x.Database.CommitTransaction(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_Group_DELETE_HasErrors()
        {
            // Act
            int groupId = 3;

            _context.Setup(x => x.Database).Returns(MockedData.MockDatabaseFacade(_context.Object));

            var groups = MockedData.GetGroups();
            _context.Setup(x => x.Groups).Returns(MockedData.MockDbSet(groups).Object);

            // Arrange
            var result = (BadRequestObjectResult)_controller.Group_DELETE(groupId);

            var httpResults = (IEnumerable<Api.Models.HttpResult>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(httpResults.Count() > 0);

            _context.Verify(x => x.Database.BeginTransaction(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        #endregion

        #region Config

        [TestMethod]
        public void AdminController_Config_GET_HasListOf()
        {
            // Act
            int page = 1;
            int pagesize = 25;
            string search = "";

            var configs = MockedData.GetConfigs();
            _context.Setup(x => x.Configs).Returns(MockedData.MockDbSet(configs).Object);

            // Arrange
            var result = (OkObjectResult)_controller.Config_GET(page, pagesize, search);

            var model = result.Value as Api.Models.ConfigPagination;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Total == 4);
            Assert.IsTrue(model.PageCount > 0);
            Assert.IsTrue(model.Configs.Count > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void AdminController_Config_POST_Update()
        {
            // Act
            Core.Entities.Config config = new Core.Entities.Config
            {
                ConfigName = "TEST-CONFIG",
                ConfigType = "Array",
                ConfigStatus = false
            };

            // Arrange
            var result = (OkObjectResult)_controller.Config_POST(config);

            var model = (Core.Entities.Config)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.ConfigName == "TEST-CONFIG");
            Assert.IsNull(model.ConfigStatus);

            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.Config>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        #endregion

    }
}
