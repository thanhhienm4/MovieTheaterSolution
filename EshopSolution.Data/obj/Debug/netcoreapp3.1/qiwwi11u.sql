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
    CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([UserId], [RoleId])
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

CREATE TABLE [Promotions] (
    [Id] int NOT NULL IDENTITY,
    [FromDate] datetime2 NOT NULL,
    [ToDate] datetime2 NOT NULL,
    [ApplyForAll] bit NOT NULL,
    [DiscountPercent] int NULL,
    [DiscountAmount] decimal(18,4) NULL,
    [ProductId] int NOT NULL,
    [ProductCategoryId] int NOT NULL,
    [Status] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Promotions] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Slides] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Desciption] nvarchar(200) NOT NULL,
    [Url] nvarchar(200) NOT NULL,
    [Image] nvarchar(200) NOT NULL,
    [SortOrder] int NOT NULL,
    [status] int NOT NULL,
    CONSTRAINT [PK_Slides] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ShipName] nvarchar(max) NOT NULL,
    [ShipAddress] nvarchar(max) NOT NULL,
    [ShipEmail] nvarchar(max) NOT NULL,
    [ShipPhoneNumber] nvarchar(max) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Transactions] (
    [Id] int NOT NULL IDENTITY,
    [TransactionDate] datetime2 NOT NULL,
    [ExternalTransactionId] nvarchar(max) NULL,
    [Amount] decimal(18,4) NOT NULL,
    [Fee] decimal(18,4) NOT NULL,
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
    [Price] decimal(18,4) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Carts_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductInCategories] (
    [ProductId] int NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_ProductInCategories] PRIMARY KEY ([CategoryId], [ProductId]),
    CONSTRAINT [FK_ProductInCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProductTranslations] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Details] nvarchar(500) NOT NULL,
    [SeoDescription] nvarchar(max) NULL,
    [SeoTitle] nvarchar(max) NULL,
    [SeoAlias] nvarchar(200) NULL,
    [LanguageId] varchar(5) NOT NULL,
    CONSTRAINT [PK_ProductTranslations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductTranslations_Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [OrderDetails] (
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Price] decimal(18,4) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderId], [ProductId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Price] decimal(18,4) NOT NULL,
    [IsFeatured] bit NOT NULL,
    [OriginalPrice] decimal(18,4) NOT NULL,
    [Stock] int NOT NULL,
    [ViewCount] int NOT NULL DEFAULT 0,
    [ThumnailImageID] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
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
VALUES ('8d04dce2-969a-435d-bba4-df3f325983dc', N'd2eb1983-639d-4d70-a0f2-2c195fec0bc2', N'Administrator role', N'admin', N'admin');
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
VALUES ('69bd714f-9576-45ba-b5b7-f00649be00de', 0, N'', '2020-01-31T00:00:00.0000000', N'Mistake4@gmail.com', CAST(1 AS bit), N'Hien', N'Nguyen Thanh', CAST(0 AS bit), '0001-01-01T00:00:00.0000000+00:00', N'Mistakem4@gmail.com', N'admin', N'AQAAAAEAACcQAAAAEDjDG2a4KAEIpkrwcmxO45uiqr80HX2ymI/H8Dc8bjRp7OGsw/GkTwNM9UfS4qQk4w==', N'0912413908', CAST(1 AS bit), N'', CAST(0 AS bit), N'admin');
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

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'IsFeatured', N'OriginalPrice', N'Price', N'Stock', N'ThumnailImageID') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [DateCreated], [IsFeatured], [OriginalPrice], [Price], [Stock], [ThumnailImageID])
VALUES (1, '2021-02-03T22:50:18.5456068+07:00', CAST(0 AS bit), 100000.0, 200000.0, 0, 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'IsFeatured', N'OriginalPrice', N'Price', N'Stock', N'ThumnailImageID') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Desciption', N'Image', N'Name', N'SortOrder', N'Url', N'status') AND [object_id] = OBJECT_ID(N'[Slides]'))
    SET IDENTITY_INSERT [Slides] ON;
INSERT INTO [Slides] ([Id], [Desciption], [Image], [Name], [SortOrder], [Url], [status])
VALUES (1, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.,', N'themes/images/carousel/4.png', N'First product', 1, N'#', 1),
(2, N'Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.,', N'themes/images/carousel/1.png', N'First product', 1, N'#', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Desciption', N'Image', N'Name', N'SortOrder', N'Url', N'status') AND [object_id] = OBJECT_ID(N'[Slides]'))
    SET IDENTITY_INSERT [Slides] OFF;

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

CREATE INDEX [IX_Orders_UserId] ON [Orders] ([UserId]);

GO

CREATE INDEX [IX_ProductImages_ProductId] ON [ProductImages] ([ProductId]);

GO

CREATE INDEX [IX_ProductInCategories_ProductId] ON [ProductInCategories] ([ProductId]);

GO

CREATE INDEX [IX_Products_ThumnailImageID] ON [Products] ([ThumnailImageID]);

GO

CREATE INDEX [IX_ProductTranslations_LanguageId] ON [ProductTranslations] ([LanguageId]);

GO

CREATE INDEX [IX_ProductTranslations_ProductId] ON [ProductTranslations] ([ProductId]);

GO

CREATE INDEX [IX_Transactions_UserId] ON [Transactions] ([UserId]);

GO

ALTER TABLE [Carts] ADD CONSTRAINT [FK_Carts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [ProductInCategories] ADD CONSTRAINT [FK_ProductInCategories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [ProductTranslations] ADD CONSTRAINT [FK_ProductTranslations_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [OrderDetails] ADD CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_ProductImages_ThumnailImageID] FOREIGN KEY ([ThumnailImageID]) REFERENCES [ProductImages] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210203155020_test1', N'5.0.0-preview.7.20365.15');

GO

