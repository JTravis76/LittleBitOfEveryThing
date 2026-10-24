using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace UMAS.Tests
{
    public class TestBase
    {
        //public static ControllerContext FakeControllerContext()
        //{
        //    //var queryString = new System.Collections.Specialized.NameValueCollection { { "id", "1" } };

        //    //var mockRequest = new Moq.Mock<System.Web.HttpRequestBase>();
        //    //mockRequest.Setup(r => r.QueryString).Returns(queryString);
        //    //mockRequest.Setup(x => x.UserHostAddress).Returns("127.0.0.1");
        //    //mockRequest.Setup(x => x.UrlReferrer).Returns(new System.Uri("http://tempuri.org/?ReturnUrl=%2f"));

        //    //var mockResponse = new Moq.Mock<System.Web.HttpResponseBase>();
        //    //mockResponse.Setup(x => x.Cookies).Returns(new System.Web.HttpCookieCollection());
        //    //mockResponse.Setup(x => x.StatusCode).Returns(200);

        //    //var mockSession = new Moq.Mock<System.Web.HttpSessionStateBase>();
        //    //var mockServer = new Moq.Mock<System.Web.HttpServerUtilityBase>();

        //    //var fakeIdentity = new System.Security.Principal.GenericIdentity("domain\\user");
        //    //var fakePrincipal = new System.Security.Principal.GenericPrincipal(fakeIdentity, null);


        //    var claims = new System.Security.Claims.Claim[]
        //    {
        //        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "domain\\user")
        //    };
        //    var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims);
        //    var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(claimsIdentity);

        //    var httpContext = new Moq.Mock<HttpContext>();
        //    httpContext.Setup(x => x.User).Returns(claimsPrincipal);
        //    //httpContext.Setup(c => c.Request).Returns(mockRequest.Object);
        //    //httpContext.Setup(x => x.Response).Returns(mockResponse.Object);
        //    //httpContext.Setup(x => x.Session).Returns(mockSession.Object);
        //    //httpContext.Setup(x => x.Server).Returns(mockServer.Object);

        //    var defaultHttpContext = new Moq.Mock<DefaultHttpContext>();
        //    defaultHttpContext.Setup(x => x.User).Returns(claimsPrincipal);

        //    var controllerContext = new Moq.Mock<ControllerContext>();
        //    //controllerContext.Setup(x => x.HttpContext).Returns(defaultHttpContext.Object);
        //    controllerContext.Setup(x => x.HttpContext).Returns(httpContext.Object);

        //    return controllerContext.Object;
        //}

        //public static System.Security.Principal.IIdentity FakeIdentity()
        //{
        //    var genericIdentity = new System.Security.Principal.GenericIdentity("domain\\user");
        //    return genericIdentity;
        //}
    }
}
