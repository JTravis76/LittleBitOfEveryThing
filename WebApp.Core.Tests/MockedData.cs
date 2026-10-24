using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMAS.Core.Entities;

namespace UMAS.Tests
{
    public static class MockedData
    {
        /// <summary>
        /// Mock of the raw data entity set
        /// </summary>
        /// <typeparam name="T">Name of class entity</typeparam>
        /// <param name="data">collection of data</param>
        /// <returns></returns>
        public static Moq.Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            IQueryable<T> ts = data.AsQueryable();
            var mockDbSet = new Moq.Mock<DbSet<T>>();

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(ts.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(ts.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(ts.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(ts.GetEnumerator());

            return mockDbSet;
        }

        /// <summary>
        /// This is used to mockup the context Database object such as; BeginTransaction()
        /// </summary>
        /// <param name="args">mocked context object EX: _dbcontext.Object</param>
        /// <returns></returns>
        public static DatabaseFacade MockDatabaseFacade(params object[] args)
        {
            //https://stackoverflow.com/questions/26664431/moq-entity-frameworks-executesqlcommand
            //Able to mock the Database.ExecuteSqlCommand()
            int expectedResult = 0;
            var relationalCommand = new Moq.Mock<RelationalCommand>();
            relationalCommand.Setup(m => m.ExecuteNonQuery(Moq.It.IsAny<IRelationalConnection>(), Moq.It.IsAny<IReadOnlyDictionary<string, object>>())).Returns(() => expectedResult);

            var rawSqlCommand = new Moq.Mock<RawSqlCommand>(Moq.MockBehavior.Strict, relationalCommand.Object, new Dictionary<string, object>());
            rawSqlCommand.Setup(m => m.RelationalCommand).Returns(() => relationalCommand.Object);
            rawSqlCommand.Setup(m => m.ParameterValues).Returns(new Dictionary<string, object>());

            var rawSqlCommandBuilder = new Moq.Mock<IRawSqlCommandBuilder>();
            rawSqlCommandBuilder.Setup(m => m.Build(Moq.It.IsAny<string>(), Moq.It.IsAny<IEnumerable<object>>())).Returns(rawSqlCommand.Object);

            //....
            var contextTransaction = new Moq.Mock<IDbContextTransaction>();
            var databaseFacade = new Moq.Mock<DatabaseFacade>(args);

            databaseFacade.Setup(x => x.BeginTransaction()).Returns(contextTransaction.Object);

            //Mock up DB Provider
            databaseFacade.As<IInfrastructure<IServiceProvider>>().Setup(m => m.Instance.GetService(Moq.It.Is<Type>(t => t == typeof(IConcurrencyDetector)))).Returns(new Moq.Mock<IConcurrencyDetector>().Object);
            databaseFacade.As<IInfrastructure<IServiceProvider>>().Setup(m => m.Instance.GetService(Moq.It.Is<Type>(t => t == typeof(IRawSqlCommandBuilder)))).Returns(rawSqlCommandBuilder.Object);
            databaseFacade.As<IInfrastructure<IServiceProvider>>().Setup(m => m.Instance.GetService(Moq.It.Is<Type>(t => t == typeof(IRelationalConnection)))).Returns(new Moq.Mock<IRelationalConnection>().Object);


            //This is extension static class and cannot mock !!
            //https://www.teos-technology.com/single-post/2018/05/22/Mocking-Static-Extension-Methods-for-Unit-Testing
            //databaseFacade.Setup(x => x.ExecuteSqlCommand(Moq.It.IsAny<string>(), Moq.It.IsAny<object>())).Returns(0);

            return databaseFacade.Object;
        }

        public static List<PORTS.Netcenter.Dto.UserDto> GetUsers()
        {
            return new List<PORTS.Netcenter.Dto.UserDto>
            {
                new PORTS.Netcenter.Dto.UserDto
                {
                    Id = 1,
                    Fullname = "Last, First",
                    NtUsername = "domain\\user",
                    Badge = "ABC123",
                    Email = "first.last@domain.com"
                }
            };
        }

        public static List<string> GetRoles()
        {
            return new List<string>
            {
                new string("Admin")
            };
        }

        

        public static List<Config> GetConfigs()
        {
            return new List<Config> {
                new Config
                {
                    ConfigName = "String type - ON",
                    ConfigStatus = true,
                    ConfigType = null,
                    ConfigValue = "1",
                    Description = "sample configuration that stores a string",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                },
                new Config
                {
                    ConfigName = "String type - OFF",
                    ConfigStatus = false,
                    ConfigType = null,
                    ConfigValue = "0",
                    Description = "sample configuration that stores a string",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                },
                new Config
                {
                    ConfigName = "Array type",
                    ConfigStatus = true,
                    ConfigType = "Array",
                    ConfigValue = @"[""Value1"",""Value2""]",
                    Description = "sample configuration that stores a Array",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                },
                new Config
                {
                    ConfigName = "Object type",
                    ConfigStatus = true,
                    ConfigType = "Object",
                    ConfigValue = @"[{""Name"":""key1"",""Value"":""1""},{""Name"":""key2"",""Value"":""2""}]",
                    Description = "sample configuration that stores a Object",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                },
                new Config
                {
                    ConfigName = "ContainerCodes",
                    ConfigStatus = true,
                    ConfigType = "Array",
                    ConfigValue = @"[""ABC"",""123""]",
                    Description = "",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                }
            };
        }

        public static List<GroupType> GetGroupTypes()
        {
            return new List<GroupType>
            {
                new GroupType
                {
                    CreatedBy = "domain\\user",
                    CreatedDate = DateTime.Now,
                    Group = null,
                    GroupTypeId = 1,
                    GroupTypeName = "Group Type I",
                    IsDeleted = false,
                    UpdatedBy = null,
                    UpdatedDate = null
                }
            };
        }

        public static List<Group> GetGroups()
        {
            return new List<Group>
            {
                new Group
                {
                    CreatedBy = "",
                    CreatedDate = DateTime.Now,
                    GroupId = 1,
                    GroupMember = new List<GroupMember> { new GroupMember { GroupId = 1, NtUserName = "doman\\user", Email = "user@domain.com"} },
                    GroupName = "Group I",
                    GroupType = new GroupType{ GroupTypeId = 1, GroupTypeName = "Group Type I"},
                    GroupTypeId = 1,
                    IsDeleted = false,
                    UpdatedBy = null,
                    UpdatedDate = null
                }
            };
        }

        public static List<GroupMember> GetGroupMembers()
        {
            return new List<GroupMember>
            {
                new GroupMember
                {
                    Email = "fname.lname@domain.com",
                    FullName = "FName LName",
                    Group = null,
                    GroupId = 1,
                    NtUserName = "domain\\user"
                }
            };
        }

        public static List<Room> GetRooms()
        {
            return new List<Room>
            {
                new Room
                {
                    BUILDING = "100",
                    ROOM_NO = "1",
                    G2_LIMIT = 0.0m,
                    GRAMS_GII = 0.0m
                },
                new Room
                {
                    BUILDING = "100",
                    ROOM_NO = "2",
                    G2_LIMIT = 25.5m,
                    GRAMS_GII = 3.124m
                }
            };
        }

        public static List<Container> GetContainers()
        {
            return new List<Container>
            {
                new Container
                {
                    CONTAINER_ID = "HT910100",
                    ENTRY_DATE = DateTime.Now,
                    LOC = "1",
                    GROSS = 12.53m
                },
                new Container
                {
                    CONTAINER_ID = "510100",
                    ENTRY_DATE = DateTime.Now,
                    LOC = "2",
                    GROSS = 10.3m
                },
                new Container
                {
                    CONTAINER_ID = "LSL10100",
                    ENTRY_DATE = DateTime.Now,
                    LOC = "2",
                    GROSS = 1034.3m,
                    STATUS = true
                }
            };
        }

        public static List<UserInfo> GetUserInfos()
        {
            return new List<UserInfo>
            {
                new UserInfo
                {
                    NtUsername = "domain\\user",
                    Title = "Software Developer I",
                    Bio = "I Love to write programs!!"
                }
            };
        }

        public static List<History> GetHistories()
        {
            return new List<History>
            {
                new History
                {
                    CONTAINER_ID = "ABC123"
                }
            };
        }
    }
}
