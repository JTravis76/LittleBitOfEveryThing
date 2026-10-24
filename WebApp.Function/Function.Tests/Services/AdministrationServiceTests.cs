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
    public class AdministrationServiceTests
    {
        private Mock<ROWContext> _mockContext;

        private ROWContext _context { get; set; }
        private IEmailService _emailService { get; set; }
        private AuthModel authModel { get; set; }

        private GrantorType grantorType { get; set; }

        private List<GrantorType> grantorTypeList { get; set; }



        [SetUp]
        public void Setup()
        {
            grantorType = new GrantorType()
            {
                Id = 1,
                Active = true,
                Name = "GrantorType01"
            };

            grantorTypeList = new List<GrantorType>() { grantorType };

            authModel = new AuthModel()
            {
                AzureUserId = "AzureUserId",
                DisplayName = "AzureDisplayName",
                PreferredUsername = "AzurePreferredUsername@domain.com",
                Roles = Array.Empty<string>()
            };

            _context = CreateDbContext();
            _emailService = new Mock<IEmailService>().Object;

            _mockContext = new Mock<ROWContext>();
        }

        #region Grantor Type

        [Test]
        public void Has_GrantorTypes_Success()
        {
            var adminService = CreateAdministrationService();
            var grantorTypes = adminService.GetGrantorTypeAsync().Result;

            Assert.IsTrue(grantorTypes.Any());
        }

        [Test]
        public void Add_GrantorType_Success()
        {
            var grantor = new GrantorType()
            {
                Id = 0, // <- flag as a new record
                Name = "GrantorType02",
                Active = true
            };

            var adminService = CreateAdministrationService();
            var result = adminService.UpdateGrantorTypeAsync(grantor).Result;
            var grantorTypes = adminService.GetGrantorTypeAsync().Result;

            Assert.IsTrue(result == 1);
            Assert.IsTrue(grantorTypes.Any(x => x.Name == grantor.Name));
        }

        [Test]
        public void Update_GrantorType_Success()
        {
            // == MEMORY DB
            grantorType.Active = false;

            var adminService = CreateAdministrationService();
            var result = adminService.UpdateGrantorTypeAsync(grantorType).Result;
            var grantorTypes = adminService.GetGrantorTypeAsync().Result;

            Assert.IsTrue(result == 1);
            Assert.IsTrue(grantorTypes.Any(x => !x.Active));
        }

        [Test]
        public void Update_GrantorType_Success2()
        {
            // Arrange
            grantorType.Id = 1;
            grantorType.Active = false;

            var _mockSet = new Mock<DbSet<GrantorType>>();
            _mockContext.Setup(x => x.GrantorTypes).Returns(_mockSet.Object);
            //_mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            //_mockContext.Setup(x => x.SaveChangesAsync(default)).Returns(() => Task.Run(() => { })).Verifiable();

            // Act
            var mockAdminService = CreateAdministrationService(true);
            var result = mockAdminService.UpdateGrantorTypeAsync(grantorType).Result;


            // Assert
            _mockContext.Verify(x => x.Update(Moq.It.IsAny<GrantorType>()), Times.Once());
            _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once());
            Assert.IsTrue(result == 0);
        }

        #endregion



        private static ROWContext CreateDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ROWContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            return new ROWContext(dbContextOptions);
        }

        private IAdministrationService CreateAdministrationService(bool useMock = false)
        {
            // Setting up mock logger
            var logger = new Mock<ILogger<AdministrationService>>();

            // Setting up AutoMapper
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new GrantorTypeProfile()); });
            var mapper = mappingConfig.CreateMapper();

            if (useMock)
            {
                return new AdministrationService(_mockContext.Object, logger.Object, mapper);
            }

            // clear the in-memory database
            _context.Database.EnsureDeleted();

            // add models to database
            _context.GrantorTypes.AddRange(grantorTypeList);
            _context.SaveChanges();

            // Creating Service
            return new AdministrationService(_context, logger.Object, mapper);
        }
    }
}