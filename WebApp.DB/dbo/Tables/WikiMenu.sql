CREATE TABLE [dbo].[WikiMenu]
(
	[WikiMenuId] INT NOT NULL PRIMARY KEY, 
    [Parent] INT NULL DEFAULT 0, 
    [Text] VARCHAR(150) NOT NULL, 
	[ContentId] int NOT NULL,
    [Ordering] INT NULL DEFAULT 0
)

