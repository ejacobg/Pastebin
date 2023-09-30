CREATE DATABASE Pastebin;
GO

USE [Pastebin];
GO

-- Needed to create indexes.
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;

-- All code below was generated using the `dotnet ef migrations script`command. See the Makefile.

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

CREATE TABLE [Pastes] (
    [PasteId] int NOT NULL IDENTITY,
    [Shortlink] nvarchar(450) NULL,
    [Content] nvarchar(max) NULL,
    [Created] datetime2 NOT NULL,
    [Expires] int NOT NULL,
    CONSTRAINT [PK_Pastes] PRIMARY KEY ([PasteId])
);
GO

CREATE TABLE [ViewCounts] (
    [Id] int NOT NULL IDENTITY,
    [PasteId] int NOT NULL,
    [Views] int NOT NULL,
    CONSTRAINT [PK_ViewCounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ViewCounts_Pastes_PasteId] FOREIGN KEY ([PasteId]) REFERENCES [Pastes] ([PasteId]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Pastes_Shortlink] ON [Pastes] ([Shortlink]) WHERE [Shortlink] IS NOT NULL;
GO

CREATE INDEX [IX_ViewCounts_PasteId] ON [ViewCounts] ([PasteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230922233042_InitialCreate', N'5.0.17');
GO

COMMIT;
GO

