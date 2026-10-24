CREATE TABLE [dbo].[Groups] (
    [Id]          INT       IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50) NULL,
    [CreatedBy]   VARCHAR (50) NULL,
    [CreatedDate] DATETIME     NULL,
    [UpdatedBy]   VARCHAR (50) NULL,
    [UpdatedDate] DATETIME     NULL,
    [IsDeleted]   BIT          NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC)
);
