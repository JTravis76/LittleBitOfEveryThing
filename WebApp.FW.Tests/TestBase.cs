namespace WebApp.FW.Tests
{
    public class TestBase
    {
        public static System.Web.Http.Controllers.HttpRequestContext FakeHttpRequestContext()
        {
            /*== Principal */
            var principal = new System.Security.Principal.GenericPrincipal(FakeIdentity(), new string[] { "Admin", "Approver" });

            var mockPrincipal = new Moq.Mock<System.Security.Principal.IPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(FakeIdentity());
            mockPrincipal.Setup(x => x.IsInRole(Moq.It.IsAny<string>())).Returns(true);

            //Build the HttpRequestContext
            var requestContext = new Moq.Mock<System.Web.Http.Controllers.HttpRequestContext>();
            requestContext.Setup(x => x.Principal).Returns(mockPrincipal.Object);
            requestContext.Setup(x => x.VirtualPathRoot).Returns("/");

            return requestContext.Object;
        }

        public static System.Security.Principal.IIdentity FakeIdentity()
        {
            var genericIdentity = new System.Security.Principal.GenericIdentity("domain\\user");
            return genericIdentity;
        }

        public static System.Web.Http.Controllers.HttpControllerContext FakeControllerContext()
        {
            string IP = "127.0.0.1";

            var queryString = new System.Collections.Specialized.NameValueCollection { { "id", "1" } };

            /*== Principal */
            var principal = new System.Security.Principal.GenericPrincipal(FakeIdentity(), new string[] { "Admin", "Approver" });

            var mockPrincipal = new Moq.Mock<System.Security.Principal.IPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(FakeIdentity());

            /*== Http Request */
            var mockRequest = new Moq.Mock<System.Web.HttpRequestBase>();
            mockRequest.Setup(r => r.QueryString).Returns(queryString);
            mockRequest.Setup(x => x.UserHostAddress).Returns(IP);
            //mockRequest.Setup(x => x.MapPath(Moq.It.IsAny<string>())).Returns(System.AppDomain.CurrentDomain.BaseDirectory);

            /*== Http Context */
            var mockHttpContext = new Moq.Mock<System.Web.HttpContextBase>();
            mockHttpContext.Setup(t => t.User).Returns(mockPrincipal.Object);
            mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);

            /*== Controller Context */
            var controllerContext = new Moq.Mock<System.Web.Http.Controllers.HttpControllerContext>();

            return controllerContext.Object;
        }


        //public static System.Web.Mvc.ControllerContext FakeControllerContext()
        //{
        //    var queryString = new System.Collections.Specialized.NameValueCollection { { "id", "1" } };

        //    var mockRequest = new Moq.Mock<System.Web.HttpRequestBase>();
        //    mockRequest.Setup(r => r.QueryString).Returns(queryString);
        //    mockRequest.Setup(x => x.UserHostAddress).Returns("127.0.0.1");
        //    mockRequest.Setup(x => x.UrlReferrer).Returns(new System.Uri("http://tempuri.org/?ReturnUrl=%2f"));

        //    var mockResponse = new Moq.Mock<System.Web.HttpResponseBase>();
        //    mockResponse.Setup(x => x.Cookies).Returns(new System.Web.HttpCookieCollection());
        //    mockResponse.Setup(x => x.StatusCode).Returns(200);

        //    var mockSession = new Moq.Mock<System.Web.HttpSessionStateBase>();
        //    var mockServer = new Moq.Mock<System.Web.HttpServerUtilityBase>();

        //    var fakeIdentity = new System.Security.Principal.GenericIdentity("User");
        //    var fakePrincipal = new System.Security.Principal.GenericPrincipal(fakeIdentity, null);

        //    var mockHttpContext = new Moq.Mock<System.Web.HttpContextBase>();
        //    mockHttpContext.Setup(t => t.User).Returns(fakePrincipal);
        //    mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
        //    mockHttpContext.Setup(x => x.Response).Returns(mockResponse.Object);
        //    mockHttpContext.Setup(x => x.Session).Returns(mockSession.Object);
        //    mockHttpContext.Setup(x => x.Server).Returns(mockServer.Object);

        //    var controllerContext = new Moq.Mock<System.Web.Mvc.ControllerContext>();
        //    controllerContext.Setup(t => t.HttpContext).Returns(mockHttpContext.Object);

        //    return controllerContext.Object;
        //}


    }
}
