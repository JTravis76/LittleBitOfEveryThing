using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.FW.Tests
{
    public static class MockedData
    {
        /// <summary>
        /// Mock of the raw data entity set
        /// </summary>
        /// <typeparam name="T">Name of class entity</typeparam>
        /// <param name="data">collection of data</param>
        /// <returns></returns>
        public static Moq.Mock<System.Data.Entity.DbSet<T>> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            IQueryable<T> ts = data.AsQueryable();
            var mockDbSet = new Moq.Mock<System.Data.Entity.DbSet<T>>();

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(ts.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(ts.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(ts.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(ts.GetEnumerator());

            return mockDbSet;
        }

        public static List<string> GetUserRoles()
        {
            return new List<string> {
                { "Admin" }
            };
        }

        public static List<PORTS.Netcenter.Dto.UserDto> GetUsers()
        {
            return new List<PORTS.Netcenter.Dto.UserDto> {
                new PORTS.Netcenter.Dto.UserDto
                {
                    Fullname = "LNAME, FNAME",
                    NtUsername = "domain\\user",
                    Badge = "1"
                },
                new PORTS.Netcenter.Dto.UserDto
                {
                    Fullname = "Flintstone, Fred",
                    Badge = "2"
                },
                new PORTS.Netcenter.Dto.UserDto
                {
                    Fullname = "Rumble, Barney",
                    Badge = "3"
                }
            };
        }

        public static List<BusinessObject.Entities.Notification> GetNotifications()
        {
            return new List<BusinessObject.Entities.Notification>()
            {
                new BusinessObject.Entities.Notification
                {
                    CreatedBy = "user",
                    CreatedDate = DateTime.Now,
                    ExpiredDate = DateTime.Now.AddMinutes(30),
                    StartDate = DateTime.Now.AddMinutes(2),
                    Id = 1,
                    Message = "This is a test of the national broadcasting system.",
                    Status = "fa fa-circle"
                }
            };
        }

        public static List<BusinessObject.Entities.OnlineUsers> GetOnlineUsers()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddMinutes(32).AddSeconds(49);

            return new List<BusinessObject.Entities.OnlineUsers>()
            {
                new BusinessObject.Entities.OnlineUsers
                {
                    NtUsername = "domain\\user",
                    StartTime = start,
                    EndTime = end
                }
            };
        }

        public static List<BusinessObject.Entities.UserInfo> GetUserProfiles()
        {
            return new List<BusinessObject.Entities.UserInfo>()
            {
                new BusinessObject.Entities.UserInfo
                {
                    Bio = "I Love to write Unit Test!!",
                    NtUsername = "doman\\user",
                    Title ="Software Developer"
                }
            };
        }

        public static List<BusinessObject.Entities.GroupType> GetGroupTypes()
        {
            return new List<BusinessObject.Entities.GroupType> {
                new BusinessObject.Entities.GroupType
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

        public static List<BusinessObject.Entities.Group> GetGroups()
        {
            return new List<BusinessObject.Entities.Group>
            {
                new BusinessObject.Entities.Group
                {
                    CreatedBy = "",
                    CreatedDate = DateTime.Now,
                    GroupId = 1,
                    GroupMember = new List<BusinessObject.Entities.GroupMember> { new BusinessObject.Entities.GroupMember { GroupId = 1, NtUserName = "doman\\user"} },
                    GroupName = "Group I",
                    GroupType = new BusinessObject.Entities.GroupType{ GroupTypeId = 1, GroupTypeName = "Group Type I"},
                    GroupTypeId = 1,
                    IsDeleted = false,
                    UpdatedBy = null,
                    UpdatedDate = null
                }
            };
        }

        public static List<BusinessObject.Entities.GroupMember> GetGroupMembers()
        {
            return new List<BusinessObject.Entities.GroupMember>
            {
                new BusinessObject.Entities.GroupMember
                {
                    Email = "fname.lname@domain.com",
                    FullName = "FName LName",
                    Group = null,
                    GroupId = 1,
                    NtUserName = "domain\\user"
                }
            };
        }

        public static List<BusinessObject.Entities.Config> GetConfigs()
        {
            return new List<BusinessObject.Entities.Config>
            {
                new BusinessObject.Entities.Config
                {
                    ConfigName = "String type - ON",
                    ConfigStatus = true,
                    ConfigType = null,
                    ConfigValue = "1",
                    Description = "sample configuration that stores a string",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                },
                new BusinessObject.Entities.Config
                {
                    ConfigName = "String type - OFF",
                    ConfigStatus = false,
                    ConfigType = null,
                    ConfigValue = "0",
                    Description = "sample configuration that stores a string",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                },
                new BusinessObject.Entities.Config
                {
                    ConfigName = "Array type",
                    ConfigStatus = true,
                    ConfigType = "Array",
                    ConfigValue = @"[""Value1"",""Value2""]",
                    Description = "sample configuration that stores a Array",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                },
                new BusinessObject.Entities.Config
                {
                    ConfigName = "Object type",
                    ConfigStatus = true,
                    ConfigType = "Object",
                    ConfigValue = @"[{""Name"":""key1"",""Value"":""1""},{""Name"":""key2"",""Value"":""2""}]",
                    Description = "sample configuration that stores a Object",
                    ModifiedBy = null,
                    ModifiedDate = DateTime.Now
                }
            };
        }

        public static List<BusinessObject.Entities.MasterWasteProfile> GetMasterWasteProfiles()
        {
            return new List<BusinessObject.Entities.MasterWasteProfile>
            {
                new BusinessObject.Entities.MasterWasteProfile
                {
                    Id = 1,
                    PrefixId = "MWP-1",
                    Revision = 0,
                    WasteType = "1 - Solid",
                    Description = "some random description",
                    ReviewedDate = DateTime.Now,
                    GeneratedBy = "domain\\user"
                },
                new BusinessObject.Entities.MasterWasteProfile
                {
                    Id = 2,
                    PrefixId = "MWP-2",
                    Revision = 0,
                    WasteType = "2 - Soil",
                    Description = "some random description",
                    ReviewedDate = DateTime.Now,
                    GeneratedBy = "domain\\fred"
                },
                new BusinessObject.Entities.MasterWasteProfile
                {
                    Id = 3,
                    PrefixId = "MWP-3",
                    Revision = 0,
                    WasteType = "3 - Large Debris",
                    Description = "some random description",
                    ReviewedDate = DateTime.Now,
                    GeneratedBy = "domain\\user"
                }
            };
        }

        public static List<BusinessObject.Entities.MaterialSourceLocation> GetMaterialSourceLocations()
        {
            return new List<BusinessObject.Entities.MaterialSourceLocation>
            {
                new BusinessObject.Entities.MaterialSourceLocation
                {
                    Id = 1,
                    PrefixId = "MSL-1",
                    Revision = 0,
                    GeneratedBy = "domain\\user",
                    ReviewedDate = DateTime.Now,
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        PrefixId = "PPCL-1",
                        ProjectName = "Project X"
                    }
                },
                new BusinessObject.Entities.MaterialSourceLocation
                {
                    Id = 2,
                    PrefixId = "MSL-2",
                    Revision = 0,
                    GeneratedBy = "domain\\fred",
                    ReviewedDate = DateTime.Now,
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        PrefixId = "PPCL-1",
                        ProjectName = "Project X"
                    }
                }
            };
        }

        public static List<BusinessObject.Entities.DataGroup> GetDataGroups()
        {
            return new List<BusinessObject.Entities.DataGroup>
            {
                new BusinessObject.Entities.DataGroup
                {
                    Id = 1,
                    ProjectPlanId = 1,
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        ProjectName = "Project X",
                        Revision = 0
                    },
                    GeneratedBy = "domain\\user"
                },
                new BusinessObject.Entities.DataGroup
                {
                    Id = 2,
                    ProjectPlanId = 1,
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        Revision = 0
                    },
                    GeneratedBy = "domain\\fred"
                }
            };
        }

        public static List<BusinessObject.Entities.ProjectPlan> GetProjectPlans()
        {
            return new List<BusinessObject.Entities.ProjectPlan>
            {
                new BusinessObject.Entities.ProjectPlan
                {
                    Id = 1,
                    ProjectName = "Project X",
                    ContactName = "user",
                    ContactPhone = "555-1234",
                    GeneratedBy = "domain\\user",
                    ApprovedDate = null
                },
                 new BusinessObject.Entities.ProjectPlan
                {
                    Id = 2,
                    ProjectName = "Project Y",
                    ContactName = "user",
                    ContactPhone = "555-1234",
                    GeneratedBy = "domain\\fred",
                    ApprovedDate = DateTime.Now
                }
            };
        }

        public static List<BusinessObject.Entities.MaterialInterimLocation> GetMaterialInterimLocations()
        {
            return new List<BusinessObject.Entities.MaterialInterimLocation>
            {
                new BusinessObject.Entities.MaterialInterimLocation
                {
                    Id = 1,
                    PrefixId = "MIL-1",
                    Revision = 0,
                    Description = "Test Description",
                    GeneratedBy = "domain\\fred",
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        ProjectName = "Project X"
                    }
                },
                new BusinessObject.Entities.MaterialInterimLocation
                {
                    Id = 2,
                    Revision = 0,
                    GeneratedBy = "domain\\fred",
                    ReviewedBy = "domain\\bob",
                    ReviewedDate = DateTime.Now,
                    ComplianceDate = null
                },
                new BusinessObject.Entities.MaterialInterimLocation
                {
                    Id = 3,
                    Revision = 0,
                    GeneratedBy = "domain\\user",
                    ReviewedBy = "domain\\bob",
                    ReviewedDate = DateTime.Now,
                    ComplianceDate = null
                },
                new BusinessObject.Entities.MaterialInterimLocation
                {
                    Id = 4,
                    Revision = 0,
                    GeneratedBy = "domain\\user",
                    ReviewedBy = null,
                    ReviewedDate = null,
                    ComplianceDate = null
                }
            };
        }

        public static List<BusinessObject.Entities.OSWDFWasteStream> GetOSWDFWasteStreams()
        {
            return new List<BusinessObject.Entities.OSWDFWasteStream>
            {
                new BusinessObject.Entities.OSWDFWasteStream
                {
                    ProjectPlanId = 1,
                    MwpId = 1,
                    MslId = 1,
                    MasterWasteProfile = new BusinessObject.Entities.MasterWasteProfile
                    {
                        Id = 1,
                        PrefixId = "MWP-1",
                        Revision = 0,
                        WasteType = "1 - Solids"
                    },
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan {
                        Id = 1,
                        ApprovedDate = null
                    },
                    GWMP_Prohibited = Newtonsoft.Json.JsonConvert.SerializeObject(new BusinessObject.Models.GWMP_Prohibited())
                },
                new BusinessObject.Entities.OSWDFWasteStream
                {
                    ProjectPlanId = 1,
                    MwpId = 2,
                    MslId = 1,
                    MasterWasteProfile = new BusinessObject.Entities.MasterWasteProfile
                    {
                        Id = 2,
                        PrefixId = "MWP-2",
                        Revision = 0,
                        WasteType = "2 - Dirt"
                    },
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan {
                        Id = 1,
                        ApprovedDate = DateTime.Now
                    }
                }
            };
        }

        public static List<BusinessObject.Entities.OffSiteWasteStream> GetOffSiteWasteStreams()
        {
            return new List<BusinessObject.Entities.OffSiteWasteStream>
            {
                new BusinessObject.Entities.OffSiteWasteStream
                {
                    Id = 1,
                    ProjectPlanId = 1,
                    PlannedFacility = "Facility 1",
                    PlannedUnit = "Pounds",
                    PlannedVolume = 23.5m,
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        ApprovedDate = null
                    }
                },
                 new BusinessObject.Entities.OffSiteWasteStream
                {
                    Id = 2,
                    ProjectPlanId = 1,
                    PlannedFacility = "Facility 1",
                    PlannedUnit = "Pounds",
                    PlannedVolume = 23.5m,
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        ApprovedDate = DateTime.Now
                    }
                }
            };
        }

        public static List<BusinessObject.Entities.InterimTransferLog> GetInterimTransferLogs()
        {
            return new List<BusinessObject.Entities.InterimTransferLog>
            {
                new BusinessObject.Entities.InterimTransferLog
                {
                    Id = 1,
                    PrefixId = "ITLOG-1",
                    ProjectPlanId = 1,
                    MwpId = 1,
                    MilId = 1,
                    MslId = 1
                },
                new BusinessObject.Entities.InterimTransferLog
                {
                    Id = 2,
                    PrefixId = "ITLOG-1",
                    ProjectPlanId = 1,
                    MwpId = 1,
                    MilId = 1,
                    MslId = 1,
                    CreatedBy = "domain\\fred",
                    VerificationDate = null
                },
                new BusinessObject.Entities.InterimTransferLog
                {
                    Id = 3,
                    PrefixId = "ITLOG-1",
                    ProjectPlanId = 1,
                    MwpId = 1,
                    MilId = 1,
                    MslId = 1,
                    CreatedBy = "domain\\user",
                    VerificationDate = null
                }
            };
        }

        public static List<BusinessObject.Entities.WasteComplianceChecklist> GetWasteComplianceChecklists()
        {
            return new List<BusinessObject.Entities.WasteComplianceChecklist>
            {
                new BusinessObject.Entities.WasteComplianceChecklist
                {
                    Id = 1,
                    Revision = 0,
                    PrefixId = "WCL-1",
                    FirstLoadDate = DateTime.Now,
                    TotalUnit = "Unit1",
                    TotalVolume = 12356.89m,
                    ProjectPlanId = 1,
                    ProjectPlan = new BusinessObject.Entities.ProjectPlan
                    {
                        Id = 1,
                        PrefixId = "PPCL-1"
                    },
                    WacActivity = Newtonsoft.Json.JsonConvert.SerializeObject(new BusinessObject.Models.WAC_Activity()),
                    WacEvaluation = Newtonsoft.Json.JsonConvert.SerializeObject(new BusinessObject.Models.WAC_Evaluation())
                }
            };
        }

        public static List<BusinessObject.Entities.WikiMenu> GetWikiMenus()
        {
            return new List<BusinessObject.Entities.WikiMenu>
            {
                new BusinessObject.Entities.WikiMenu
                {
                    ContentId = 1,
                    Id = 1,
                    IsPublish = true,
                    Order = 0,
                    Parent = 0,
                    Title = "Home",
                    WikiContent = null
                }
            };
        }

        public static List<BusinessObject.Entities.WikiContent> GetWikiContents()
        {
            return new List<BusinessObject.Entities.WikiContent>
            {
                new BusinessObject.Entities.WikiContent
                {
                    Content = "Markdown text goes here",
                    Hits = 0,
                    Id = 1,
                    IsHome = true,
                    IsPublish = true,
                    ModifiedBy = "domain\\user",
                    ModifiedDate = DateTime.Now,
                    Tags = "[]",
                    Title = "HOME",
                    Version = 2
                }
            };
        }

        public static List<BusinessObject.Entities.TransferLog> GetTransferLogs()
        {
            return new List<BusinessObject.Entities.TransferLog>
            {
                new BusinessObject.Entities.TransferLog
                {
                    Id = 1,
                    LogNumber = "DC-32601-20200101-001",
                    ComplianceId = 1
                },
                new BusinessObject.Entities.TransferLog
                {
                    Id = 2,
                    LogNumber = "PlacementApproved",
                    ComplianceId = 1,
                    PlacementApprovedDate = DateTime.Now
                },
                new BusinessObject.Entities.TransferLog
                {
                    Id = 2,
                    LogNumber = "RejectionApproved",
                    ComplianceId = 1,
                    ReworkApprovedDate = DateTime.Now
                }
            };
        }














        public static List<BusinessObject.Models.EM_SHIPMENTS> GetEM_SHIPMENTS()
        {
            return new List<BusinessObject.Models.EM_SHIPMENTS>
            {
                new BusinessObject.Models.EM_SHIPMENTS
                {
                    SHIPMENT_NBR = "8006-01-0194",
                    SHIPMENT_TYPE_CD = "BURIAL SHIPMENT",
                    SCHEDULED_SHIPMENT_DT = DateTime.Now,
                    CARRIER_SEQ_ID = 3129,
                    SHIPMENT_SEQ_ID = 907,
                    EM_OUTBOUND_SHIPMENTS = new List<BusinessObject.Models.EM_OUTBOUND_SHIPMENTS>
                    {
                        new BusinessObject.Models.EM_OUTBOUND_SHIPMENTS
                        {
                            SHIPMENT_STATUS = BusinessObject.Enum.ShipmentStatus.Shipped.ToString()
                        }
                    }
                }
            };
        }
    }
}
