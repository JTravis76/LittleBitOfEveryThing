CREATE TABLE [dbo].[UserInfo] (
    [Username] VARCHAR (100) NOT NULL,
    [Title]    VARCHAR (200) NULL,
    [Bio]      VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Username] PRIMARY KEY CLUSTERED ([Username] ASC)
);
