
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/23/2014 18:53:22
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
IF OBJECT_ID(N'[dbo].[FK_FoodEntryFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntryFood] DROP CONSTRAINT [FK_FoodEntryFood];
GO
IF OBJECT_ID(N'[dbo].[FK_FoodOutgoingFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OutgoingFood] DROP CONSTRAINT [FK_FoodOutgoingFood];
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
IF OBJECT_ID(N'[dbo].[EntryFood]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntryFood];
GO
IF OBJECT_ID(N'[dbo].[OutgoingFood]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OutgoingFood];
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

-- Creating table 'EntryFood'
CREATE TABLE [dbo].[EntryFood] (
    [IdEntryFood] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [FoodIdFood] int  NOT NULL
);
GO

-- Creating table 'OutgoingFood'
CREATE TABLE [dbo].[OutgoingFood] (
    [IdOutgoingFood] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [FoodIdFood] int  NOT NULL
);
GO

-- Creating table 'Concession'
CREATE TABLE [dbo].[Concession] (
    [IdConcession] int IDENTITY(1,1) NOT NULL,
    [BeginDate] datetime  NOT NULL,
    [FinishDate] datetime  NOT NULL,
    [Observations] nvarchar(max)  NOT NULL
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

-- Creating primary key on [IdEntryFood] in table 'EntryFood'
ALTER TABLE [dbo].[EntryFood]
ADD CONSTRAINT [PK_EntryFood]
    PRIMARY KEY CLUSTERED ([IdEntryFood] ASC);
GO

-- Creating primary key on [IdOutgoingFood] in table 'OutgoingFood'
ALTER TABLE [dbo].[OutgoingFood]
ADD CONSTRAINT [PK_OutgoingFood]
    PRIMARY KEY CLUSTERED ([IdOutgoingFood] ASC);
GO

-- Creating primary key on [IdConcession] in table 'Concession'
ALTER TABLE [dbo].[Concession]
ADD CONSTRAINT [PK_Concession]
    PRIMARY KEY CLUSTERED ([IdConcession] ASC);
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

-- Creating foreign key on [FoodIdFood] in table 'EntryFood'
ALTER TABLE [dbo].[EntryFood]
ADD CONSTRAINT [FK_FoodEntryFood]
    FOREIGN KEY ([FoodIdFood])
    REFERENCES [dbo].[Food]
        ([IdFood])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FoodEntryFood'
CREATE INDEX [IX_FK_FoodEntryFood]
ON [dbo].[EntryFood]
    ([FoodIdFood]);
GO

-- Creating foreign key on [FoodIdFood] in table 'OutgoingFood'
ALTER TABLE [dbo].[OutgoingFood]
ADD CONSTRAINT [FK_FoodOutgoingFood]
    FOREIGN KEY ([FoodIdFood])
    REFERENCES [dbo].[Food]
        ([IdFood])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FoodOutgoingFood'
CREATE INDEX [IX_FK_FoodOutgoingFood]
ON [dbo].[OutgoingFood]
    ([FoodIdFood]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------