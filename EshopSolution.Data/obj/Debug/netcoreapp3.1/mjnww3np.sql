IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'7ae82edf-4659-470a-b812-8846728235d7'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEPcIK18K5kAAGwZwUIZmjWWD74arH6AMDWAkdpLxl9+aaHiE+1BWTX0G+kOn0X4m7g=='
WHERE [Id] = '69bd714f-9576-45ba-b5b7-f00649be00de';
SELECT @@ROWCOUNT;


GO

UPDATE [Categories] SET [Status] = 1
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [Categories] SET [Status] = 1
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [Products] SET [DateCreated] = '2021-02-03T22:43:21.4323262+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210203154323_test', N'5.0.0-preview.7.20365.15');

GO

