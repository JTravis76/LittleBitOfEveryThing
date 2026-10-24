/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

print 'Inserting Config';
GO

INSERT INTO [dbo].[Config] ([ConfigName], [ConfigValue], [ModifiedBy], [ModifiedDate], [ConfigStatus], [Description], [ConfigType]) VALUES (N'StringOption', N'Some Changeable Value', N'DDPORTS\KDJ', GETDATE(), NULL, N'Sample String type', NULL)
INSERT INTO [dbo].[Config] ([ConfigName], [ConfigValue], [ModifiedBy], [ModifiedDate], [ConfigStatus], [Description], [ConfigType]) VALUES (N'FlagType', N'N/A', N'DDPORTS\KDJ', GETDATE(), 1, N'Switchable feature flag', null)
INSERT INTO [dbo].[Config] ([ConfigName], [ConfigValue], [ModifiedBy], [ModifiedDate], [ConfigStatus], [Description], [ConfigType]) VALUES (N'ArrayOption', N'["Value 1","Value 2"]', N'DDPORT\KDJ', GETDATE(), NULL, N'Sample Array type', N'Array')
INSERT INTO [dbo].[Config] ([ConfigName], [ConfigValue], [ModifiedBy], [ModifiedDate], [ConfigStatus], [Description], [ConfigType]) VALUES (N'ObjectOption', N'[{"Value":"Value1","Name":"Name1"}]', N'DDPORT\KDJ', GETDATE(), NULL, N'Sample Object type', N'Object')

GO

PRINT N'Inserting [Groups] data...';
GO

SET IDENTITY_INSERT [dbo].[Groups] ON
INSERT INTO [dbo].[Groups] ([Id], [Name], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsDeleted]) VALUES (1, N'Software Developers', N'DDPORTS\KDJ', GETDATE(), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Groups] OFF

PRINT N'Inserting [GroupMembers] data...';
GO

INSERT INTO [dbo].[GroupMembers] ([GroupId], [UserName], [FullName], [Email]) VALUES (1, N'DDPORTS\KDJ', N'Travis, Jeremy', N'jeremy.travis@ports.pppo.gov')