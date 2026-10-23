CREATE TABLE [dbo].[SectionPage]
(
	[PageId] INT NOT NULL, 
    [SectionId] INT NOT NULL,
	CONSTRAINT [PK_SectionPage] PRIMARY KEY (PageId,SectionId)
)
