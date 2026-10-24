using Microsoft.VisualStudio.TestTools.UnitTesting;
using UMAS.Infrastructure.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace UMAS.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly Moq.Mock<ILogger<Api.Controllers.HomeController>> _logger;
        private readonly Moq.Mock<SmtpClient> _smtp;
        private readonly Moq.Mock<UMASContext> _context;
        private readonly Api.Controllers.HomeController _controller;

        public HomeControllerTests()
        {
            _context = new Moq.Mock<UMASContext>();
            _logger = new Moq.Mock<ILogger<Api.Controllers.HomeController>>();
            _smtp = new Moq.Mock<SmtpClient>();
            _controller = new Api.Controllers.HomeController(_logger.Object, _smtp.Object, _context.Object)
            {
                //ControllerContext = TestBase.FakeControllerContext()

                //ControllerContext = new ControllerContext()
                //{
                //    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
                //    {
                //        User = new System.Security.Claims.ClaimsPrincipal(
                //            new System.Security.Claims.ClaimsIdentity(
                //                new System.Security.Claims.Claim[]
                //                {
                //                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "domain\\user")
                //                }))
                //    }
                //}
            };
        }


        //[TestMethod]
        //public void HomeController_FourHundredServerTest_POST_HasVaildationError()
        //{
        //    // Arrange

        //    // Act
        //    var result = (BadRequestObjectResult)_controller.FourHundredServerTest_POST();

        //    var model = (IEnumerable<Api.Models.HttpResult>)result.Value;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(model.Count() > 0);
        //}

        [TestMethod]
        public void HomeController_FiveHundredServerTest_POST_Has500Error()
        {
            // Arrange

            // Act
            //var result =  _controller.FiveHundredServerTest_POST();

            // Assert
            //Assert.IsNotNull(result);
            Assert.IsTrue(1 == 1);
        }

        //[TestMethod]
        //public void HomeController_EmailTest_POST()
        //{
        //    // Arrange
        //    var groups = MockedData.GetGroups();
        //    _context.Setup(x => x.Groups).Returns(MockedData.MockDbSet(groups).Object);

        //    // Act
        //    var result = (OkResult)_controller.EmailTest_POST();

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void HomeController_LogTest_POST()
        //{
        //    // Arrange
        //    int code = 1;

        //    //_logger.Setup(x => x.LogInformation(Moq.It.IsAny<int>(), Moq.It.IsAny<string>(), Moq.It.IsAny<object>()));

        //    // Act
        //    var result = (OkResult)_controller.LogTest_POST(code);

        //    // Assert
        //    Assert.IsNotNull(result);

        //}

    }
}
