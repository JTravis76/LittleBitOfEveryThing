using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Core.Tests.Services
{
    [TestClass]
    public class GraphQLSvcTests
    {
        private readonly Moq.Mock<Netcenter.Sdk.INetcenterSDK> _netcenter;
        private readonly Moq.Mock<Microsoft.Extensions.Configuration.IConfiguration> _configuration;
        private readonly Moq.Mock<Infrastructure.Services.Repositories.DataContext> _context;
        
        private readonly WebApp.Infrastructure.Services.GraphQL.GqlSvc _service;

        public GraphQLSvcTests()
        {
            _configuration = new Moq.Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _netcenter = new Moq.Mock<Netcenter.Sdk.INetcenterSDK>();
            _context = new Moq.Mock<Infrastructure.Services.Repositories.DataContext>();

            _service = new Infrastructure.Services.GraphQL.GqlSvc(_configuration.Object);
        }


        [TestMethod]
        public void GraphQLSvc_MakeSchema_HasAboutServer()
        {
            // Arrange

            // Act
            var schema = _service.MakeSchema();

            var schemaType = schema
                .GetNonContextTypes()
                .Where(x => x.Name == "AboutServer")
                .FirstOrDefault();

            var fields = schemaType.GetFields();

            // Assert          
            Assert.IsNotNull(schema);
            Assert.IsTrue(fields.Count() > 0);
        }

        [TestMethod]
        public void GraphQLSvc_AboutServer_HasDetails()
        {
            // Arrange

            // Act
            BusinessObjects.Dtos.AboutServer aboutServer = _service.AboutServer();

            // Assert
            Assert.IsNotNull(aboutServer);
            Assert.IsTrue(aboutServer.Version != null);
            Assert.IsNotNull(aboutServer.VersionDate);
        }




    }
}
