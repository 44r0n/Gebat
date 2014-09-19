
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/19/2014 12:04:31
-- Generated from EDMX file: D:\Proyectos\Gebat\Gebat\GebatModel\GebatContent.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GebatDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TypeFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Food] DROP CONSTRAINT [FK_TypeFood];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Type];
GO
IF OBJECT_ID(N'[dbo].[Food]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Food];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admin'
CREATE TABLE [dbo].[Admin] (
    [IdAdmin] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'Type'
CREATE TABLE [dbo].[Type] (
    [IdType] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Food'
CREATE TABLE [dbo].[Food] (
    [IdFood] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [TypeIdType] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdAdmin] in table 'Admin'
ALTER TABLE [dbo].[Admin]
ADD CONSTRAINT [PK_Admin]
    PRIMARY KEY CLUSTERED ([IdAdmin] ASC);
GO

-- Creating primary key on [IdType] in table 'Type'
ALTER TABLE [dbo].[Type]
ADD CONSTRAINT [PK_Type]
    PRIMARY KEY CLUSTERED ([IdType] ASC);
GO

-- Creating primary key on [IdFood] in table 'Food'
ALTER TABLE [dbo].[Food]
ADD CONSTRAINT [PK_Food]
    PRIMARY KEY CLUSTERED ([IdFood] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TypeIdType] in table 'Food'
ALTER TABLE [dbo].[Food]
ADD CONSTRAINT [FK_TypeFood]
    FOREIGN KEY ([TypeIdType])
    REFERENCES [dbo].[Type]
        ([IdType])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeFood'
CREATE INDEX [IX_FK_TypeFood]
ON [dbo].[Food]
    ([TypeIdType]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------