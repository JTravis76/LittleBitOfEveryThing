CREATE TABLE [dbo].[WikiContent]
(
	[WikiContentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] VARCHAR(150) NOT NULL, 
    [Content] NVARCHAR(MAX) NULL, 
    [ModifiedDate] DATETIME NULL, 
    [ModifiedBy] VARCHAR(50) NULL, 
    [IsHome] BIT NOT NULL DEFAULT 0, 
    [Hits] INT NULL, 
    [Version] INT NULL, 
    [IsPublish] BIT NOT NULL DEFAULT 0, 
    [Tags] NVARCHAR(MAX) NULL
)

