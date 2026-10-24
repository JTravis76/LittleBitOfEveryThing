DECLARE @DynamicConfigurations TABLE
                               (
                                   Name        VARCHAR(64) NOT NULL,
                                   ConfigValue VARCHAR(64) NOT NULL,
                                   DisplayName VARCHAR(64) NOT NULL
                               )

INSERT INTO @DynamicConfigurations (Name, ConfigValue, DisplayName)
VALUES ('configuration1', '7', 'sample configuration'),
       ('configuration2', 'true', 'another sample configuration')

MERGE Configuration AS Dest
USING @DynamicConfigurations AS Src
ON Src.Name = Dest.Name
WHEN MATCHED THEN
    UPDATE
    SET Dest.Name        = Src.Name,
        Dest.DisplayName = Src.DisplayName
WHEN NOT MATCHED THEN
    INSERT (Name, ConfigValue, DisplayName)
    VALUES (Src.Name, Src.ConfigValue, Src.DisplayName)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;