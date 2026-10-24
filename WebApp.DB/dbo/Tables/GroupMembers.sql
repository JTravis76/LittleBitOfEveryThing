CREATE TABLE [dbo].[GroupMembers] (
    [GroupId]    INT       NOT NULL,
    [UserName]   VARCHAR (50) NOT NULL,
    [FullName]   VARCHAR (50) NULL,
    [Email]      VARCHAR (50) NULL,
    CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED ([GroupId] ASC, [UserName] ASC),
    CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id])
);