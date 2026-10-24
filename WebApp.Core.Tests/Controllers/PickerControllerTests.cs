using Microsoft.VisualStudio.TestTools.UnitTesting;
using UMAS.Infrastructure.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace UMAS.Tests.Controllers
{
    [TestClass]
    public class PickerControllerTests
    {
        private readonly Moq.Mock<UMASContext> _context;
        private readonly Api.Controllers.PickerController _controller;

        public PickerControllerTests()
        {
            _context = new Moq.Mock<UMASContext>();
            _controller = new Api.Controllers.PickerController(_context.Object);
        }

        [TestMethod]
        public void PickerController_Config_GET_HasEmptyList()
        {
            // Act
            string config = "config1";

            var configs = MockedData.GetConfigs();
            _context.Setup(x => x.Configs).Returns(MockedData.MockDbSet(configs).Object);

            // Arrange
            var result = (OkObjectResult)_controller.Config_GET(config);

            var pickers = (List<Api.Models.Picker>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(pickers.Count == 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void PickerController_Config_GET_HasListOf_Array()
        {
            // Act
            string config = "Array type";

            var configs = MockedData.GetConfigs();
            _context.Setup(x => x.Configs).Returns(MockedData.MockDbSet(configs).Object);

            // Arrange
            var result = (OkObjectResult)_controller.Config_GET(config);

            var pickers = (List<Api.Models.Picker>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(pickers.Count > 0);
            Assert.IsTrue(pickers[0].text == "Value1");

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void PickerController_Config_GET_HasListOf_Object()
        {
            // Act
            string config = "Object type";

            var configs = MockedData.GetConfigs();
            _context.Setup(x => x.Configs).Returns(MockedData.MockDbSet(configs).Object);

            // Arrange
            var result = (OkObjectResult)_controller.Config_GET(config);

            var pickers = (List<Api.Models.Picker>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(pickers.Count > 0);
            Assert.IsTrue(pickers[0].text == "key1");

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void PickerController_GroupTypes_GET_HasListOf()
        {
            // Act
            var groupTypes = MockedData.GetGroupTypes();
            _context.Setup(x => x.GroupTypes).Returns(MockedData.MockDbSet(groupTypes).Object);

            // Arrange
            var result = (OkObjectResult)_controller.GroupTypes_GET();

            var pickers = (List<Api.Models.Picker>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(pickers.Count > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

        [TestMethod]
        public void PickerController_Rooms_GET_HasListOf()
        {
            // Act
            var rooms = MockedData.GetRooms();
            _context.Setup(x => x.Rooms).Returns(MockedData.MockDbSet(rooms).Object);

            // Arrange
            var result = (OkObjectResult)_controller.Rooms_GET();

            var pickers = (List<Api.Models.Picker>)result.Value;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(pickers.Count > 0);

            _context.Verify(x => x.Dispose(), Moq.Times.Once);
        }

    }
}
