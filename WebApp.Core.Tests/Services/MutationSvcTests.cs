using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApp.Core.Tests.Services
{
    [TestClass]
    public class MutationSvcTests
    {
        private readonly Moq.Mock<Microsoft.Extensions.Configuration.IConfiguration> _configuration;
        private readonly Moq.Mock<Infrastructure.Services.Repositories.DataContext> _context;
        private readonly Moq.Mock<EntityGraphQL.Schema.SchemaProvider<Infrastructure.Services.Repositories.DataContext>> _schema;
        
        private readonly Moq.Mock<IServiceProvider> _serviceProvider;
        private readonly WebApp.Infrastructure.Services.GraphQL.MutationSvc _service;

        public MutationSvcTests()
        {
            _configuration = new Moq.Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _context = new Moq.Mock<Infrastructure.Services.Repositories.DataContext>();
            _schema = new Moq.Mock<EntityGraphQL.Schema.SchemaProvider<Infrastructure.Services.Repositories.DataContext>>();
            _serviceProvider = new Moq.Mock<IServiceProvider>();

            var gql = new Moq.Mock<EntityGraphQL.QueryRequest>();
            var claims = new System.Security.Claims.ClaimsIdentity();
            claims.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "domain\\user"));
            claims.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin"));            

            _schema.Object.ExecuteQuery(
                gql: gql.Object,
                context: _context.Object,
                serviceProvider: _serviceProvider.Object,
                claims: claims);

            _service = new Infrastructure.Services.GraphQL.MutationSvc(_schema.Object, _configuration.Object);
        }



    }
}
