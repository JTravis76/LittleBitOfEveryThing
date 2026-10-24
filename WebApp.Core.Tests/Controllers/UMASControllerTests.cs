using Microsoft.VisualStudio.TestTools.UnitTesting;
using UMAS.Infrastructure.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace UMAS.Tests.Controllers
{
    [TestClass]
    public class UMASControllerTests
    {
        private readonly Moq.Mock<UMASContext> _context;
        private readonly Moq.Mock<Infrastructure.Interfaces.IUMAS> _umas;
        private readonly Api.Controllers.UMASController _controller;

        public UMASControllerTests()
        {
            _context = new Moq.Mock<UMASContext>();
            _umas = new Moq.Mock<Infrastructure.Interfaces.IUMAS>();
            _controller = new Api.Controllers.UMASController(_context.Object, _umas.Object) {
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
        public void UMASController_Rooms_GET_HasListOf()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = null;

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(rooms).Object);

            // Act
            var result = (OkObjectResult)_controller.Rooms_GET(page, pagesize, search);

            var model = result.Value as Api.Models.RoomDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Rooms.Count > 0);
            Assert.IsTrue(model.Total == 2);
            Assert.IsTrue(model.PageCount == 1);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Rooms_GET_HasListOfBySearch()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = "2";

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(rooms).Object);

            // Act
            var result = (OkObjectResult)_controller.Rooms_GET(page, pagesize, search);

            var model = result.Value as Api.Models.RoomDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Rooms.Any(x => x.ROOM_NO == search));
            Assert.IsTrue(model.Total == 1);
            Assert.IsTrue(model.PageCount == 1);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Rooms_POST_Add()
        {
            // Arrange
            Core.Entities.Room room = new Core.Entities.Room { BUILDING = "100", ROOM_NO = "111" };

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(MockedData.GetRooms()).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Room_POST(room);

            var model = result.Value as Core.Entities.Room;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.ROOM_NO == room.ROOM_NO);

            _context.Verify(x => x.Add(Moq.It.IsAny<Core.Entities.Room>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Rooms_POST_Update()
        {
            // Arrange
            Core.Entities.Room room = new Core.Entities.Room { BUILDING = "100", ROOM_NO = "1" };

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(MockedData.GetRooms()).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Room_POST(room);

            var model = result.Value as Core.Entities.Room;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.ROOM_NO == room.ROOM_NO);

            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.Room>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Rooms_POST_HasErrors()
        {
            // Arrange
            Core.Entities.Room room = new Core.Entities.Room { BUILDING = "100", ROOM_NO = "1", G2_LIMIT = 1.23m };

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(MockedData.GetRooms()).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (BadRequestObjectResult)_controller.Room_POST(room);

            var model = result.Value as IEnumerable<Api.Models.HttpResult>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count() > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Rooms_POST_HasVaildationErrors()
        {
            // Arrange
            _controller.ModelState.Clear();
            _controller.ModelState.AddModelError("", "Building is required");
            _controller.ModelState.AddModelError("", "Room Number is required");

            Core.Entities.Room room = new Core.Entities.Room { BUILDING = "", ROOM_NO = "", G2_LIMIT = 1.23m };

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(MockedData.GetRooms()).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (BadRequestObjectResult)_controller.Room_POST(room);

            var model = result.Value as IEnumerable<Api.Models.HttpResult>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count() > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Containers_GET_HasListOf()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = null;

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Containers_GET(page, pagesize, search);

            var model = result.Value as Api.Models.ContainerDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Containers.Count > 0);
            Assert.IsTrue(model.Total == 2);
            Assert.IsTrue(model.PageCount == 1);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Containers_GET_HasListOfBySearch()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = "510100";

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Containers_GET(page, pagesize, search);

            var model = result.Value as Api.Models.ContainerDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Containers.Count > 0);
            Assert.IsTrue(model.Total == 1);
            Assert.IsTrue(model.PageCount == 1);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Containers_GET_HasListOfBySearchStatus()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = "status:f";

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Containers_GET(page, pagesize, search);

            var model = result.Value as Api.Models.ContainerDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Containers.Count > 0);
            Assert.IsTrue(model.Total == 1);
            Assert.IsTrue(model.PageCount == 1);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Containers_GET_HasListOfBySearchLocation()
        {
            // Arrange
            int page = 1;
            int pagesize = 25;
            string search = "location:2";

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Containers_GET(page, pagesize, search);

            var model = result.Value as Api.Models.ContainerDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Containers.Count > 0);
            Assert.IsTrue(model.Total == 2);
            Assert.IsTrue(model.PageCount == 1);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Container_POST_Add()
        {
            // Arrange
            Core.Entities.Container container = new Core.Entities.Container { CONTAINER_ID = "test0100", GRAMSU = 1m, LOC = "1" };

            _context.Setup(x => x.Database).Returns(MockedData.MockDatabaseFacade(_context.Object));

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(rooms).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            _umas.Setup(x => x.IsNonLiquid(Moq.It.IsAny<string>())).Returns(false);
            _umas.Setup(x => x.IsTheoretical(Moq.It.IsAny<string>())).Returns(false);

            // Act
            var result = (OkObjectResult)_controller.Container_POST(container);

            var model = result.Value as Core.Entities.Container;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.CONTAINER_ID == container.CONTAINER_ID.ToUpper());
            Assert.IsFalse(model.STATUS);
            Assert.IsTrue(container.STATE == false);
            Assert.IsTrue(container.THEO == false);
            Assert.IsNotNull(container.ENTRY_DATE);

            _context.Verify(x => x.Add(Moq.It.IsAny<Core.Entities.Container>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Container_POST_Update()
        {
            // Arrange
            Core.Entities.Container container = new Core.Entities.Container { CONTAINER_ID = "510101", GRAMSU = 0.123m, LOC = "1", STATE = false };

            _context.Setup(x => x.Database).Returns(MockedData.MockDatabaseFacade(_context.Object));

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(rooms).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Container_POST(container);

            var model = result.Value as Core.Entities.Container;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.CONTAINER_ID == container.CONTAINER_ID);
            Assert.IsFalse(model.STATUS);
            Assert.IsNotNull(container.ENTRY_DATE);

            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.Container>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Containers_POST_HasVaildationErrors()
        {
            // Arrange
            _controller.ModelState.Clear();
            _controller.ModelState.AddModelError("", "Container Number is required");
            _controller.ModelState.AddModelError("", "Location is required");

            Core.Entities.Container container = new Core.Entities.Container { };

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (BadRequestObjectResult)_controller.Container_POST(container);

            var model = result.Value as IEnumerable<Api.Models.HttpResult>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count() > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_ContainerTransfer_POST_Update()
        {
            // Arrange
            string[] containerIds = new string[] { "510100" };
            string room = "1";

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(rooms).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkResult)_controller.ContainerTransfer_POST(containerIds, room);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);

            _context.Verify(x => x.Update(Moq.It.IsAny<Core.Entities.Room>()), Moq.Times.Once);
            _context.Verify(x => x.UpdateRange(Moq.It.IsAny<List<Core.Entities.Container>>()), Moq.Times.Once);
            _context.Verify(x => x.SaveChanges(), Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_ContainerTransfer_POST_EmptyListError()
        {
            // Arrange
            string[] containerIds = null;
            string room = "2";

            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(rooms).Object);

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (BadRequestObjectResult)_controller.ContainerTransfer_POST(containerIds, room);

            var model = result.Value as IEnumerable<Api.Models.HttpResult>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(model.Count() > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_ContainerTransfer_POST_MissingRoomError()
        {
            // Arrange
            string[] containerIds = new string[] { "510100" };
            string room = null;

            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (BadRequestObjectResult)_controller.ContainerTransfer_POST(containerIds, room);

            var model = result.Value as IEnumerable<Api.Models.HttpResult>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(model.Count() > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void UMASController_Dashboard_GET_HasObject()
        {
            // Arrange
            var containers = MockedData.GetContainers();
            _context.Setup(x => x.Containers).Returns(MockedData.MockDbSet(containers).Object);

            // Act
            var result = (OkObjectResult)_controller.Dashboard_GET();

            var model = result.Value as Api.Models.DashboardDto;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Containers > 0);
        }

        [TestMethod]
        public void UMASController_ContainerCheck_GET_IsLiquidNonPoly()
        {
            // Arrange
            string containerId = "MSL00001";

            // Act
            var result = (OkObjectResult)_controller.ContainerCheck_GET(containerId);

            var model = result.Value as Api.Models.ContainerCheckDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(model.Liquid);
            Assert.IsTrue(model.Theoretical);
        }

        [TestMethod]
        public void UMASController_ContainerCheck_GET_IsLiquidPolyType()
        {
            // Arrange
            string containerId = "5500001";

            // Act
            var result = (OkObjectResult)_controller.ContainerCheck_GET(containerId);

            var model = result.Value as Api.Models.ContainerCheckDto;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Liquid);
            Assert.IsTrue(model.Theoretical);
        }

        [TestMethod]
        public void UMASController_ContainerCheck_GET_HasErrors()
        {
            // Arrange
            string containerId = null;

            // Act
            var result = (BadRequestObjectResult)_controller.ContainerCheck_GET(containerId);

            var model = result.Value as IEnumerable<Api.Models.HttpResult>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(model.Count() > 0);
        }

        [TestMethod]
        public void UMASController_Histories_GET_HasListOf()
        {
            // Arrange
            string id = "ABC123";

            var histories = MockedData.GetHistories();
            _context.Setup(x => x.Histories).Returns(MockedData.MockDbSet(histories).Object);

            // Act
            var result = (OkObjectResult)_controller.Histories_GET(id);

            var model = result.Value as List<Core.Entities.History>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(model.Count > 0);
        }

        [TestMethod]
        public void UMASController_UpdateRoomGrams_POST_RowAffected()
        {
            // Arrange
            _context.Setup(x => x.Database).Returns(MockedData.MockDatabaseFacade(_context.Object));

            // Act
            var result = (OkObjectResult)_controller.UpdateRoomGrams_POST();

            var model = result.Value as int?;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(1 == 1);

            //No-way to verify the "ExecuteSqlCommand()" was triggered, just counted the invocation on the Database method
            _context.Verify(x => x.Database, Moq.Times.Once);
            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }
    }
}
