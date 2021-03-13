IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AppRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AppRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppUsers] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(200) NOT NULL,
    [LastName] nvarchar(200) NOT NULL,
    [Dob] datetime2 NOT NULL,
    [RoleId] int NOT NULL,
    [Status] int NOT NULL DEFAULT 0,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
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
    CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Bans] (
    [Id] int NOT NULL IDENTITY,
    [Name] int NOT NULL,
    CONSTRAINT [PK_Bans] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FilmGenres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_FilmGenres] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Formats] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Price] int NOT NULL,
    CONSTRAINT [PK_Formats] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [KindOfSeats] (
    [Id] int NOT NULL IDENTITY,
    [Name] int NOT NULL,
    [Surcharge] int NOT NULL,
    CONSTRAINT [PK_KindOfSeats] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Peoples] (
    [Id] int NOT NULL IDENTITY,
    [DOB] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Peoples] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Positions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Positions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ReservationTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] int NOT NULL,
    CONSTRAINT [PK_ReservationTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AppRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AppRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AppUserLogins] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [ProviderKey] nvarchar(max) NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserLogins] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_AppUserLogins_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AppUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AppUserRoles_AppRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AppRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AppUserRoles_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AppUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserTokens] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_AppUserTokens_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Films] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [PublishDate] datetime2 NOT NULL,
    [Length] int NOT NULL,
    [BanId] int NOT NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Films_Bans_BanId] FOREIGN KEY ([BanId]) REFERENCES [Bans] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Rooms] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [FormatId] int NOT NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rooms_Formats_FormatId] FOREIGN KEY ([FormatId]) REFERENCES [Formats] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FilmInGenres] (
    [FilmId] int NOT NULL,
    [FilmGenreId] int NOT NULL,
    CONSTRAINT [PK_FilmInGenres] PRIMARY KEY ([FilmId], [FilmGenreId]),
    CONSTRAINT [FK_FilmInGenres_FilmGenres_FilmGenreId] FOREIGN KEY ([FilmGenreId]) REFERENCES [FilmGenres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmInGenres_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Joinings] (
    [PeppleId] int NOT NULL,
    [FilmId] int NOT NULL,
    [PositionId] int NOT NULL,
    CONSTRAINT [PK_Joinings] PRIMARY KEY ([FilmId], [PeppleId], [PositionId]),
    CONSTRAINT [FK_Joinings_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Joinings_Peoples_PeppleId] FOREIGN KEY ([PeppleId]) REFERENCES [Peoples] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Joinings_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Screenings] (
    [Id] int NOT NULL IDENTITY,
    [TimeStart] datetime2 NOT NULL,
    [Surcharge] int NOT NULL DEFAULT 0,
    [FilmId] int NOT NULL,
    [RoomId] int NOT NULL,
    CONSTRAINT [PK_Screenings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Screenings_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Screenings_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Seats] (
    [Id] int NOT NULL IDENTITY,
    [Row] int NOT NULL,
    [Number] int NOT NULL,
    [KindOfSeatId] int NOT NULL,
    [RoomId] int NOT NULL,
    CONSTRAINT [PK_Seats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Seats_KindOfSeats_KindOfSeatId] FOREIGN KEY ([KindOfSeatId]) REFERENCES [KindOfSeats] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Seats_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reservations] (
    [Id] int NOT NULL IDENTITY,
    [Paid] bit NOT NULL DEFAULT CAST(0 AS bit),
    [Active] bit NOT NULL DEFAULT CAST(1 AS bit),
    [ReservationTypeId] int NOT NULL,
    [UserId] uniqueidentifier NULL,
    [ScreeningId] int NOT NULL,
    [EmployeeId] uniqueidentifier NULL,
    CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reservations_AppUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AppUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reservations_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reservations_ReservationTypes_ReservationTypeId] FOREIGN KEY ([ReservationTypeId]) REFERENCES [ReservationTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reservations_Screenings_ScreeningId] FOREIGN KEY ([ScreeningId]) REFERENCES [Screenings] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tickets] (
    [Id] int NOT NULL IDENTITY,
    [Price] int NOT NULL DEFAULT 0,
    [ReservationId] int NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tickets_Reservations_ReservationId] FOREIGN KEY ([ReservationId]) REFERENCES [Reservations] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AppRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AppUserRoles_RoleId] ON [AppUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AppUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AppUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_FilmInGenres_FilmGenreId] ON [FilmInGenres] ([FilmGenreId]);
GO

CREATE INDEX [IX_Films_BanId] ON [Films] ([BanId]);
GO

CREATE INDEX [IX_Joinings_PeppleId] ON [Joinings] ([PeppleId]);
GO

CREATE INDEX [IX_Joinings_PositionId] ON [Joinings] ([PositionId]);
GO

CREATE INDEX [IX_Reservations_EmployeeId] ON [Reservations] ([EmployeeId]);
GO

CREATE INDEX [IX_Reservations_ReservationTypeId] ON [Reservations] ([ReservationTypeId]);
GO

CREATE INDEX [IX_Reservations_ScreeningId] ON [Reservations] ([ScreeningId]);
GO

CREATE INDEX [IX_Reservations_UserId] ON [Reservations] ([UserId]);
GO

CREATE INDEX [IX_Rooms_FormatId] ON [Rooms] ([FormatId]);
GO

CREATE INDEX [IX_Screenings_FilmId] ON [Screenings] ([FilmId]);
GO

CREATE INDEX [IX_Screenings_RoomId] ON [Screenings] ([RoomId]);
GO

CREATE INDEX [IX_Seats_KindOfSeatId] ON [Seats] ([KindOfSeatId]);
GO

CREATE INDEX [IX_Seats_RoomId] ON [Seats] ([RoomId]);
GO

CREATE INDEX [IX_Tickets_ReservationId] ON [Tickets] ([ReservationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210313095243_1', N'5.0.4');
GO

COMMIT;
GO

