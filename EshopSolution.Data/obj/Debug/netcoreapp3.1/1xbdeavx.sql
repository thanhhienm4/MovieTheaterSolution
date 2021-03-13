IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AppConfigs] (
    [Key] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AppConfigs] PRIMARY KEY ([Key])
);

GO

CREATE TABLE [AppRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AppRoleClaims] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Description] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_AppRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserClaims] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppUserLogins] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [ProviderKey] nvarchar(max) NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserLogins] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [AppUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [AppUsers] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [FirstName] nvarchar(200) NOT NULL,
    [LastName] nvarchar(200) NOT NULL,
    [Dob] datetime2 NOT NULL,
    CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserTokens] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [SortOrder] int NOT NULL,
    [IsShowOnHome] bit NOT NULL,
    [ParentId] int NULL,
    [Status] int NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Contacts] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    [PhoneNumber] nvarchar(200) NOT NULL,
    [Message] nvarchar(max) NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Languages] (
    [Id] varchar(5) NOT NULL,
    [Name] nvarchar(20) NOT NULL,
    [IsDefault] bit NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Price] decimal(18,2) NOT NULL,
    [OriginalPrice] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    [ViewCount] int NOT NULL DEFAULT 0,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Promotions] (
    [Id] int NOT NULL IDENTITY,
    [FromDate] datetime2 NOT NULL,
    [ToDate] datetime2 NOT NULL,
    [ApplyForAll] bit NOT NULL,
    [DiscountPercent] int NULL,
    [DiscountAmount] decimal(18,2) NULL,
    [ProductIds] nvarchar(max) NULL,
    [ProductCategoryIds] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Promotions] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ShipName] nvarchar(max) NOT NULL,
    [ShipAddress] nvarchar(max) NOT NULL,
    [ShipEmail] nvarchar(max) NULL,
    [ShipPhoneNumber] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [AppUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_AppUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Transactions] (
    [Id] int NOT NULL IDENTITY,
    [TransactionDate] datetime2 NOT NULL,
    [ExternalTransactionId] nvarchar(max) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Fee] decimal(18,2) NOT NULL,
    [Result] nvarchar(max) NULL,
    [Message] nvarchar(max) NULL,
    [Status] int NOT NULL,
    [Provider] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [CategoryTranslations] (
    [Id] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [SeoDescription] nvarchar(500) NULL,
    [SeoTitle] nvarchar(200) NULL,
    [LanguageId] varchar(5) NOT NULL,
    [SeoAlias] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_CategoryTranslations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CategoryTranslations_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryTranslations_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Carts] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Carts_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Carts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductImages] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [ImagePath] nvarchar(200) NOT NULL,
    [Caption] nvarchar(200) NULL,
    [IsDefault] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [SortOrder] int NOT NULL,
    [FileSize] bigint NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductInCategories] (
    [ProductId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_ProductInCategories] PRIMARY KEY ([CategoryId], [ProductId]),
    CONSTRAINT [FK_ProductInCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductInCategories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductTranslations] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Details] nvarchar(500) NULL,
    [SeoDescription] nvarchar(max) NULL,
    [SeoTitle] nvarchar(max) NULL,
    [SeoAlias] nvarchar(200) NOT NULL,
    [LanguageId] varchar(5) NOT NULL,
    CONSTRAINT [PK_ProductTranslations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductTranslations_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductTranslations_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [OrderDetails] (
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Key', N'Value') AND [object_id] = OBJECT_ID(N'[AppConfigs]'))
    SET IDENTITY_INSERT [AppConfigs] ON;
INSERT INTO [AppConfigs] ([Key], [Value])
VALUES (N'HomeTitle', N'This is home page of EshopSolution'),
(N'HomeKeyword', N'This is keyword of EshopSolution'),
(N'HomeDescription', N'This is description of EshopSolution');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Key', N'Value') AND [object_id] = OBJECT_ID(N'[AppConfigs]'))
    SET IDENTITY_INSERT [AppConfigs] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Description', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AppRoles]'))
    SET IDENTITY_INSERT [AppRoles] ON;
INSERT INTO [AppRoles] ([Id], [ConcurrencyStamp], [Description], [Name], [NormalizedName])
VALUES ('8d04dce2-969a-435d-bba4-df3f325983dc', N'2c238edb-3acd-40cd-847b-a407312b710e', N'Administrator role', N'admin', N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Description', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AppRoles]'))
    SET IDENTITY_INSERT [AppRoles] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'RoleId') AND [object_id] = OBJECT_ID(N'[AppUserRoles]'))
    SET IDENTITY_INSERT [AppUserRoles] ON;
INSERT INTO [AppUserRoles] ([UserId], [RoleId])
VALUES ('69bd714f-9576-45ba-b5b7-f00649be00de', '8d04dce2-969a-435d-bba4-df3f325983dc');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'RoleId') AND [object_id] = OBJECT_ID(N'[AppUserRoles]'))
    SET IDENTITY_INSERT [AppUserRoles] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Dob', N'Email', N'EmailConfirmed', N'FirstName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AppUsers]'))
    SET IDENTITY_INSERT [AppUsers] ON;
INSERT INTO [AppUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Dob], [Email], [EmailConfirmed], [FirstName], [LastName], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES ('69bd714f-9576-45ba-b5b7-f00649be00de', 0, N'', '2020-01-31T00:00:00.0000000', N'Mistake4@gmail.com', CAST(1 AS bit), N'Hien', N'Nguyen Thanh', CAST(0 AS bit), '0001-01-01T00:00:00.0000000+00:00', N'Mistakem4@gmail.com', N'admin', N'AQAAAAEAACcQAAAAEDfDY4X6sVuO61QkFLXgOVBPiiSOu2DN+i6xtbJjxGy4sBOEXp+yn+CsSR6Lqrfc4A==', N'0912413908', CAST(1 AS bit), N'', CAST(0 AS bit), N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Dob', N'Email', N'EmailConfirmed', N'FirstName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AppUsers]'))
    SET IDENTITY_INSERT [AppUsers] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsShowOnHome', N'ParentId', N'SortOrder', N'Status') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([Id], [IsShowOnHome], [ParentId], [SortOrder], [Status])
VALUES (1, CAST(1 AS bit), NULL, 1, 1),
(2, CAST(1 AS bit), NULL, 2, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsShowOnHome', N'ParentId', N'SortOrder', N'Status') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDefault', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] ON;
INSERT INTO [Languages] ([Id], [IsDefault], [Name])
VALUES ('vi', CAST(1 AS bit), N'Tiếng Việt'),
('en', CAST(0 AS bit), N'English');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDefault', N'Name') AND [object_id] = OBJECT_ID(N'[Languages]'))
    SET IDENTITY_INSERT [Languages] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'OriginalPrice', N'Price', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [DateCreated], [OriginalPrice], [Price], [Stock])
VALUES (1, '2020-08-18T18:57:35.6547078+07:00', 100000.0, 200000.0, 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'OriginalPrice', N'Price', N'Stock') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'LanguageId', N'Name', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[CategoryTranslations]'))
    SET IDENTITY_INSERT [CategoryTranslations] ON;
INSERT INTO [CategoryTranslations] ([Id], [CategoryId], [LanguageId], [Name], [SeoAlias], [SeoDescription], [SeoTitle])
VALUES (1, 1, 'vi', N'Áo nam', N'ao-nam', N'Sản phẩm áo thời trang nam', N'Sản phẩm áo thời trang nam'),
(3, 2, 'vi', N'Áo nữ', N'ao-nu', N'Sản phẩm áo thời trang nữ', N'Sản phẩm áo thời trang women'),
(2, 1, 'en', N'Men Shirt', N'men-shirt', N'The shirt products for men', N'The shirt products for men'),
(4, 2, 'en', N'Women Shirt', N'women-shirt', N'The shirt products for women', N'The shirt products for women');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'LanguageId', N'Name', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[CategoryTranslations]'))
    SET IDENTITY_INSERT [CategoryTranslations] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId') AND [object_id] = OBJECT_ID(N'[ProductInCategories]'))
    SET IDENTITY_INSERT [ProductInCategories] ON;
INSERT INTO [ProductInCategories] ([CategoryId], [ProductId])
VALUES (1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'ProductId') AND [object_id] = OBJECT_ID(N'[ProductInCategories]'))
    SET IDENTITY_INSERT [ProductInCategories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Details', N'LanguageId', N'Name', N'ProductId', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[ProductTranslations]'))
    SET IDENTITY_INSERT [ProductTranslations] ON;
INSERT INTO [ProductTranslations] ([Id], [Description], [Details], [LanguageId], [Name], [ProductId], [SeoAlias], [SeoDescription], [SeoTitle])
VALUES (1, N'Áo sơ mi nam trắng Việt Tiến', N'Áo sơ mi nam trắng Việt Tiến', 'vi', N'Áo sơ mi nam trắng Việt Tiến', 1, N'ao-so-mi-nam-trang-viet-tien', N'Áo sơ mi nam trắng Việt Tiến', N'Áo sơ mi nam trắng Việt Tiến'),
(2, N'Viet Tien Men T-Shirt', N'Viet Tien Men T-Shirt', 'en', N'Viet Tien Men T-Shirt', 1, N'viet-tien-men-t-shirt', N'Viet Tien Men T-Shirt', N'Viet Tien Men T-Shirt');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Details', N'LanguageId', N'Name', N'ProductId', N'SeoAlias', N'SeoDescription', N'SeoTitle') AND [object_id] = OBJECT_ID(N'[ProductTranslations]'))
    SET IDENTITY_INSERT [ProductTranslations] OFF;

GO

CREATE INDEX [IX_Carts_ProductId] ON [Carts] ([ProductId]);

GO

CREATE INDEX [IX_Carts_UserId] ON [Carts] ([UserId]);

GO

CREATE INDEX [IX_CategoryTranslations_CategoryId] ON [CategoryTranslations] ([CategoryId]);

GO

CREATE INDEX [IX_CategoryTranslations_LanguageId] ON [CategoryTranslations] ([LanguageId]);

GO

CREATE INDEX [IX_OrderDetails_ProductId] ON [OrderDetails] ([ProductId]);

GO

CREATE INDEX [IX_Orders_AppUserId] ON [Orders] ([AppUserId]);

GO

CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);

GO

CREATE INDEX [IX_ProductInCategories_ProductId] ON [ProductInCategories] ([ProductId]);

GO

CREATE INDEX [IX_ProductTranslations_LanguageId] ON [ProductTranslations] ([LanguageId]);

GO

CREATE INDEX [IX_ProductTranslations_ProductId] ON [ProductTranslations] ([ProductId]);

GO

CREATE INDEX [IX_Transactions_UserId] ON [Transactions] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200818115737_SeedIdentityUser', N'5.0.0-preview.7.20365.15');

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'434f0271-3d57-4117-8a20-cd09ecdcf889'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEPsbP+ZFA20nNtfJ2da++fL5c5yrFIRYZR+RS3zqrIIDuvjFPIYzldgyB/vCiJlRww=='
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

UPDATE [Products] SET [DateCreated] = '2020-08-21T09:08:33.2421013+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200821020836_ChangeSaveFile', N'5.0.0-preview.7.20365.15');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Price');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Products] ALTER COLUMN [Price] decimal(18,4) NOT NULL;

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'12ba8c70-2df4-4987-89a8-43250d9dc43c'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEHSAUd5YqNDmXIe4Lpe15RjZsBwmfpWSSd+kxxUmocGQr+0aqWhyNt+pfDG5WVRNBA=='
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

UPDATE [Products] SET [DateCreated] = '2020-09-01T16:04:28.7518671+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200901090430_update_fix_data', N'5.0.0-preview.7.20365.15');

GO

ALTER TABLE [AppUserRoles] DROP CONSTRAINT [PK_AppUserRoles];

GO

ALTER TABLE [AppUserRoles] ADD CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([UserId], [RoleId]);

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'104aa618-22d3-4608-8f49-54df687a85a7'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEO6wcq8AE4fTBJLrH0x9XKuROdSz0wOzegYyR0cBg1kNt8sv373NXSMD3FZVSSriFQ=='
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

UPDATE [Products] SET [DateCreated] = '2020-11-02T21:34:33.2288033+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201102143435_Initial', N'5.0.0-preview.7.20365.15');

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductTranslations]') AND [c].[name] = N'SeoAlias');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ProductTranslations] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ProductTranslations] ALTER COLUMN [SeoAlias] nvarchar(200) NULL;

GO

ALTER TABLE [Products] ADD [IsFeatured] bit NULL;

GO

CREATE TABLE [Slides] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Desciption] nvarchar(200) NOT NULL,
    [Url] nvarchar(200) NOT NULL,
    [Image] nvarchar(200) NOT NULL,
    [SortOther] int NOT NULL,
    [status] int NOT NULL,
    CONSTRAINT [PK_Slides] PRIMARY KEY ([Id])
);

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'3cbaa2b5-f8ec-47cd-9889-9d6e72d6c4c5'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEBGPM2HE8+2mU2aRzj1wiGw6xTul2TpelGAao1uQIUAdyUJVKuFdDW0EZit1Jw+zTQ=='
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

UPDATE [Products] SET [DateCreated] = '2021-01-05T22:59:24.6060252+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Desciption', N'Image', N'Name', N'SortOther', N'Url', N'status') AND [object_id] = OBJECT_ID(N'[Slides]'))
    SET IDENTITY_INSERT [Slides] ON;
INSERT INTO [Slides] ([Id], [Desciption], [Image], [Name], [SortOther], [Url], [status])
VALUES (1, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.,', N'themes/images/carousel/4.png', N'First product', 1, N'#', 1),
(2, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.,', N'themes/images/carousel/1.png', N'First product', 1, N'#', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Desciption', N'Image', N'Name', N'SortOther', N'Url', N'status') AND [object_id] = OBJECT_ID(N'[Slides]'))
    SET IDENTITY_INSERT [Slides] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210105155926_Homedatas', N'5.0.0-preview.7.20365.15');

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'c68ab340-8a90-49e3-9a2e-acb2f247555a'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEFrSiedZPNij+bp7l6VXlFQVZKqpCxg7+2TLUyBlEG5H4jHG7YKnOrMeyG8PhRdnRg=='
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

UPDATE [Products] SET [DateCreated] = '2021-01-05T23:14:59.2210682+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210105161501_HomeData', N'5.0.0-preview.7.20365.15');

GO

EXEC sp_rename N'[Slides].[SortOther]', N'SortOrder', N'COLUMN';

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'bd13f2c4-49be-4d1e-b03a-cdb32f86b03a'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEFelaKO53Lgn/fKk6Tejc4Kvmjlhu6zxVrLScidI0S+jVPO7YpjPmYtw5oSzfad0yA=='
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

UPDATE [Products] SET [DateCreated] = '2021-01-06T14:02:56.0054196+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210106070258_Edit_Slide', N'5.0.0-preview.7.20365.15');

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'c5966c62-3e42-456a-b624-7ef68a1e9bf7'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEGxS1L4q3AHAC+WE34w3cRo5SGMAtZnVoIM9jNkkrhSg5T43TuAY6JSdEw3ZBkPTzw=='
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

UPDATE [Products] SET [DateCreated] = '2021-01-07T12:41:15.4874724+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210107054117_update_languageId', N'5.0.0-preview.7.20365.15');

GO

ALTER TABLE [Orders] DROP CONSTRAINT [FK_Orders_AppUsers_AppUserId];

GO

DROP INDEX [IX_Orders_AppUserId] ON [Orders];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'AppUserId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Orders] DROP COLUMN [AppUserId];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductTranslations]') AND [c].[name] = N'Details');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ProductTranslations] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [ProductTranslations] ALTER COLUMN [Details] nvarchar(500) NOT NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductTranslations]') AND [c].[name] = N'Description');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ProductTranslations] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [ProductTranslations] ALTER COLUMN [Description] nvarchar(500) NOT NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'IsFeatured');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Products] ALTER COLUMN [IsFeatured] bit NOT NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'ShipEmail');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Orders] ALTER COLUMN [ShipEmail] nvarchar(max) NOT NULL;

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'b5d7b1f7-9a76-4fcc-a8af-71c90ae5ecd0'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEIITlz37XZk2raeRqxiuTz6gk5fEu17KlnBubzxiEex1IXiSqCHA+D4m3Jnu8aYCzQ=='
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

UPDATE [Products] SET [DateCreated] = '2021-01-25T19:13:15.9760728+07:00', [IsFeatured] = CAST(0 AS bit)
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);

GO

ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210125121318_update-order', N'5.0.0-preview.7.20365.15');

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'6c5a407e-ddd3-4cfb-a370-6909320a7b5c'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEEJZjHofPxBQ3QASMfj/b5KZiMkNxvGLcu5s0zaWilzJxocJ7aX/e+JSTtukVbTNFA=='
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

UPDATE [Products] SET [DateCreated] = '2021-02-04T17:26:01.6998458+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210204102604_1', N'5.0.0-preview.7.20365.15');

GO

ALTER TABLE [Products] ADD [ThumnailId] int NULL;

GO

UPDATE [AppRoles] SET [ConcurrencyStamp] = N'2d1e27a8-490b-41ef-8011-859196d2adc6'
WHERE [Id] = '8d04dce2-969a-435d-bba4-df3f325983dc';
SELECT @@ROWCOUNT;


GO

UPDATE [AppUsers] SET [PasswordHash] = N'AQAAAAEAACcQAAAAEKfmpA83T5WIirpPLlA/FHzRFd1G1GOcScXoZzFcpL/77c9O/O1GpZqd2YxodwV2tA=='
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

UPDATE [Products] SET [DateCreated] = '2021-02-04T19:40:45.4872616+07:00'
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


GO

CREATE UNIQUE INDEX [IX_Products_ThumnailId] ON [Products] ([ThumnailId]) WHERE [ThumnailId] IS NOT NULL;

GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductImages_ThumnailId] FOREIGN KEY ([ThumnailId]) REFERENCES [ProductImages] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210204124050_config_thumnail', N'5.0.0-preview.7.20365.15');

GO

