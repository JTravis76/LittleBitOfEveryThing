CREATE TABLE [dbo].[Config] (
    [ConfigName]   VARCHAR (50)   NOT NULL,
    [ConfigValue]  NVARCHAR (MAX) NOT NULL,
    [ModifiedBy]   VARCHAR (100)  NOT NULL,
    [ModifiedDate] DATETIME       NOT NULL,
    [ConfigStatus] BIT            NULL,
    [Description]  VARCHAR (300)  NULL,
    [ConfigType]   VARCHAR (50)   NULL,
    CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED ([ConfigName] ASC)
);