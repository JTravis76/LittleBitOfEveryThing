DECLARE @GrantorType TABLE
                    (
                        [Name]   NVARCHAR(150) NULL,
                        [Active] BIT NULL
                    )

INSERT INTO [GrantorType] ([Name], [Active])
VALUES (N'Municipality', 1),
    (N'Private Individual(s)', 1),
    (N'Railroad', 1),
    (N'Corp, LLC, Partnership or Other Entity', 1),
    (N'Other', 1),
    (N'State', 1),
    (N'County - Allegheny', 1),
    (N'County - Beaver', 1),
    (N'County - Other', 1);

MERGE GrantorType AS Dest
USING [GrantorType] AS Src
ON Src.[Name] = Dest.[Name]
WHEN MATCHED THEN
    UPDATE
    SET Dest.[Name]           = Src.[Name],
        Dest.[Active] = Src.[Active]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Active])
    VALUES (Src.[Name], Src.[Active])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;