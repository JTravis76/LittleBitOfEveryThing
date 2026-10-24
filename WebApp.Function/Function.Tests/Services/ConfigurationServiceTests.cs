using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using WebAppFunction.Api.Database;
using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Models;
using WebAppFunction.Api.Profiles;
using WebAppFunction.Api.Services;
using WebAppFunction.Api.Services.Interfaces;

#nullable disable

namespace PermittingTool.Test.Services
{
    [TestFixture]
    public class ConfigurationServiceTests
    {
        private ROWContext _context { get; set; }
        private IEmailService _emailService { get; set; }
        private AuthModel authModel { get; set; }
        private DynamicConfigurations configurations1 { get; set; }
        private DynamicConfigurations configurations2 { get; set; }
        private List<DynamicConfigurations> configurationList { get; set; }


        [SetUp]
        public void Setup()
        {
            configurations1 = new DynamicConfigurations()
            {
                Id = 1,
                ConfigValue = "7",
                DisplayName = "Sample Configuration",
                Name = "configuration1"
            };

            configurations2 = new DynamicConfigurations()
            {
                Id = 2,
                ConfigValue = "true",
                DisplayName = "another sample configuration",
                Name = "configuration2"
            };

            configurationList = new List<DynamicConfigurations>() { configurations1, configurations2 };

            authModel = new AuthModel()
            {
                AzureUserId = "AzureUserId",
                DisplayName = "AzureDisplayName",
                PreferredUsername = "AzurePreferredUsername@domain.com",
                Roles = Array.Empty<string>()
            };

            _context = CreateDbContext();
            _emailService = new Mock<IEmailService>().Object;
        }

        [Test]
        public void HasConfigurations_Success()
        {
            var configService = CreateConfigService(configurationList);

            var configurationModel = configService.GetConfigurationModelAsync().Result;

            Assert.IsTrue(configurationModel.DatabaseConfigurations.Any());
        }


        private static ROWContext CreateDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ROWContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            return new ROWContext(dbContextOptions);
        }

        private IConfigurationService CreateConfigService(IEnumerable<DynamicConfigurations> configurations)
        {
            // clear the in-memory database
            _context.Database.EnsureDeleted();

            // add all Configurations
            _context.Configuration.AddRange(configurations);
            _context.SaveChanges();

            // Setting up mock logger
            var logger = new Mock<ILogger<ConfigurationService>>();

            // Setting up AutoMapper
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new DynamicConfigurationProfile()); });
            var mapper = mappingConfig.CreateMapper();

            // Creating Service
            return new ConfigurationService(_context, logger.Object, mapper);
        }
    }
}