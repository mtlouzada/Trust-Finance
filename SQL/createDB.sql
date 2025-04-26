CREATE DATABASE [Finance];
GO

USE [Finance];
GO

-- DROP TABLE [Transaction];
-- DROP TABLE [Category];
-- DROP TABLE [User];

CREATE TABLE [User] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [CreatedAt] DATETIME NOT NULL DEFAULT(GETDATE()),

    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [UQ_User_Email] UNIQUE ([Email])
);
GO

CREATE NONCLUSTERED INDEX [IX_User_Email] ON [User]([Email]);
GO

CREATE TABLE [Category] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(100) NOT NULL,
    [Type] NVARCHAR(50) NOT NULL,
    [UserId] INT NOT NULL,

    CONSTRAINT [PK_Category] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Category_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
);
GO

CREATE NONCLUSTERED INDEX [IX_Category_Name] ON [Category]([Name]);
GO

CREATE TABLE [Transaction] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Description] NVARCHAR(255) NOT NULL,
    [Amount] DECIMAL(18,2) NOT NULL,
    [TransactionDate] DATE NOT NULL,
    [CategoryId] INT NOT NULL,
    [UserId] INT NOT NULL,

    CONSTRAINT [PK_Transaction] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transaction_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]),
    CONSTRAINT [FK_Transaction_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
);
GO

CREATE NONCLUSTERED INDEX [IX_Transaction_TransactionDate] ON [Transaction]([TransactionDate]);
GO
