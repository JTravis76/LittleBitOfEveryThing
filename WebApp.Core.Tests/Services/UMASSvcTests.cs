using Microsoft.VisualStudio.TestTools.UnitTesting;
using UMAS.Infrastructure.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UMAS.Tests.Services
{
    [TestClass]
    public class UMASSvcTests
    {
        private readonly Moq.Mock<UMASContext> _context;
        private readonly Infrastructure.Services.Application.UMASSvc _svc;

        public UMASSvcTests()
        {
            _context = new Moq.Mock<UMASContext>();
            _svc = new Infrastructure.Services.Application.UMASSvc(_context.Object);
        }


        [TestMethod]
        public void UMASSvc_CheckContainer_False()
        {
            // Arrange
            string container = "hijk4567";

            var configs = MockedData.GetConfigs();
            _context.Setup(x => x.Configs).Returns(MockedData.MockDbSet(configs).Object);

            // Act
            var result = _svc.CheckContainer(container);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UMASSvc_CheckContainer_True()
        {
            // Arrange
            string container = "abcxyz";

            var configs = MockedData.GetConfigs();
            _context.Setup(x => x.Configs).Returns(MockedData.MockDbSet(configs).Object);

            // Act
            var result = _svc.CheckContainer(container);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }


    }
}
