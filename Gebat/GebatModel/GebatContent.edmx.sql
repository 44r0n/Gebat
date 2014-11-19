
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/18/2014 20:53:01
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
IF OBJECT_ID(N'[dbo].[FK_ConcessionConcessionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Concession] DROP CONSTRAINT [FK_ConcessionConcessionType];
GO
IF OBJECT_ID(N'[dbo].[FK_ConcessionTypeDateRestriction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConcessionType] DROP CONSTRAINT [FK_ConcessionTypeDateRestriction];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonalDossierFamiliar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Person_Familiar] DROP CONSTRAINT [FK_PersonalDossierFamiliar];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonalDossierConcession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Concession] DROP CONSTRAINT [FK_PersonalDossierConcession];
GO
IF OBJECT_ID(N'[dbo].[FK_ConcessionTypeFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Food] DROP CONSTRAINT [FK_ConcessionTypeFood];
GO
IF OBJECT_ID(N'[dbo].[FK_Familiar_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Person_Familiar] DROP CONSTRAINT [FK_Familiar_inherits_Person];
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
IF OBJECT_ID(N'[dbo].[Concession]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Concession];
GO
IF OBJECT_ID(N'[dbo].[ConcessionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConcessionType];
GO
IF OBJECT_ID(N'[dbo].[DateRestriction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DateRestriction];
GO
IF OBJECT_ID(N'[dbo].[PersonalDossier]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalDossier];
GO
IF OBJECT_ID(N'[dbo].[Person]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Person];
GO
IF OBJECT_ID(N'[dbo].[Person_Familiar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Person_Familiar];
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
    [TypeIdType] int  NOT NULL,
    [ConcessionTypeIdConcessionType] int  NOT NULL
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
    [Observations] nvarchar(max)  NULL,
    [PersonalDossierId] int  NOT NULL,
    [Type_IdConcessionType] int  NOT NULL
);
GO

-- Creating table 'ConcessionType'
CREATE TABLE [dbo].[ConcessionType] (
    [IdConcessionType] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DateRestriction_IdDateRestriction] int  NULL
);
GO

-- Creating table 'DateRestriction'
CREATE TABLE [dbo].[DateRestriction] (
    [IdDateRestriction] int IDENTITY(1,1) NOT NULL,
    [Interval] int  NOT NULL
);
GO

-- Creating table 'PersonalDossier'
CREATE TABLE [dbo].[PersonalDossier] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Observations] nvarchar(max)  NULL
);
GO

-- Creating table 'Person'
CREATE TABLE [dbo].[Person] (
    [IdPerson] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [DNI] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Gender] int  NOT NULL
);
GO

-- Creating table 'Crime'
CREATE TABLE [dbo].[Crime] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Person_Familiar'
CREATE TABLE [dbo].[Person_Familiar] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Income] int  NOT NULL,
    [PersonalDossierId] int  NOT NULL,
    [IdPerson] int  NOT NULL
);
GO

-- Creating table 'Person_TBC'
CREATE TABLE [dbo].[Person_TBC] (
    [Court] nvarchar(max)  NOT NULL,
    [Judgement] nvarchar(max)  NOT NULL,
    [BeginDate] datetime  NOT NULL,
    [Monday] bit  NOT NULL,
    [Tuesday] bit  NOT NULL,
    [Wednesday] bit  NOT NULL,
    [Thursday] bit  NOT NULL,
    [Friday] bit  NOT NULL,
    [Saturday] bit  NOT NULL,
    [Sunday] bit  NOT NULL,
    [BeginHour] nvarchar(max)  NOT NULL,
    [FinishHour] nvarchar(max)  NOT NULL,
    [FinishDate] datetime  NOT NULL,
    [CrimeId] int  NOT NULL,
    [IdPerson] int  NOT NULL
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

-- Creating primary key on [IdConcessionType] in table 'ConcessionType'
ALTER TABLE [dbo].[ConcessionType]
ADD CONSTRAINT [PK_ConcessionType]
    PRIMARY KEY CLUSTERED ([IdConcessionType] ASC);
GO

-- Creating primary key on [IdDateRestriction] in table 'DateRestriction'
ALTER TABLE [dbo].[DateRestriction]
ADD CONSTRAINT [PK_DateRestriction]
    PRIMARY KEY CLUSTERED ([IdDateRestriction] ASC);
GO

-- Creating primary key on [Id] in table 'PersonalDossier'
ALTER TABLE [dbo].[PersonalDossier]
ADD CONSTRAINT [PK_PersonalDossier]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdPerson] in table 'Person'
ALTER TABLE [dbo].[Person]
ADD CONSTRAINT [PK_Person]
    PRIMARY KEY CLUSTERED ([IdPerson] ASC);
GO

-- Creating primary key on [Id] in table 'Crime'
ALTER TABLE [dbo].[Crime]
ADD CONSTRAINT [PK_Crime]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdPerson] in table 'Person_Familiar'
ALTER TABLE [dbo].[Person_Familiar]
ADD CONSTRAINT [PK_Person_Familiar]
    PRIMARY KEY CLUSTERED ([IdPerson] ASC);
GO

-- Creating primary key on [IdPerson] in table 'Person_TBC'
ALTER TABLE [dbo].[Person_TBC]
ADD CONSTRAINT [PK_Person_TBC]
    PRIMARY KEY CLUSTERED ([IdPerson] ASC);
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

-- Creating foreign key on [Type_IdConcessionType] in table 'Concession'
ALTER TABLE [dbo].[Concession]
ADD CONSTRAINT [FK_ConcessionConcessionType]
    FOREIGN KEY ([Type_IdConcessionType])
    REFERENCES [dbo].[ConcessionType]
        ([IdConcessionType])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConcessionConcessionType'
CREATE INDEX [IX_FK_ConcessionConcessionType]
ON [dbo].[Concession]
    ([Type_IdConcessionType]);
GO

-- Creating foreign key on [DateRestriction_IdDateRestriction] in table 'ConcessionType'
ALTER TABLE [dbo].[ConcessionType]
ADD CONSTRAINT [FK_ConcessionTypeDateRestriction]
    FOREIGN KEY ([DateRestriction_IdDateRestriction])
    REFERENCES [dbo].[DateRestriction]
        ([IdDateRestriction])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConcessionTypeDateRestriction'
CREATE INDEX [IX_FK_ConcessionTypeDateRestriction]
ON [dbo].[ConcessionType]
    ([DateRestriction_IdDateRestriction]);
GO

-- Creating foreign key on [PersonalDossierId] in table 'Person_Familiar'
ALTER TABLE [dbo].[Person_Familiar]
ADD CONSTRAINT [FK_PersonalDossierFamiliar]
    FOREIGN KEY ([PersonalDossierId])
    REFERENCES [dbo].[PersonalDossier]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalDossierFamiliar'
CREATE INDEX [IX_FK_PersonalDossierFamiliar]
ON [dbo].[Person_Familiar]
    ([PersonalDossierId]);
GO

-- Creating foreign key on [PersonalDossierId] in table 'Concession'
ALTER TABLE [dbo].[Concession]
ADD CONSTRAINT [FK_PersonalDossierConcession]
    FOREIGN KEY ([PersonalDossierId])
    REFERENCES [dbo].[PersonalDossier]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalDossierConcession'
CREATE INDEX [IX_FK_PersonalDossierConcession]
ON [dbo].[Concession]
    ([PersonalDossierId]);
GO

-- Creating foreign key on [ConcessionTypeIdConcessionType] in table 'Food'
ALTER TABLE [dbo].[Food]
ADD CONSTRAINT [FK_ConcessionTypeFood]
    FOREIGN KEY ([ConcessionTypeIdConcessionType])
    REFERENCES [dbo].[ConcessionType]
        ([IdConcessionType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConcessionTypeFood'
CREATE INDEX [IX_FK_ConcessionTypeFood]
ON [dbo].[Food]
    ([ConcessionTypeIdConcessionType]);
GO

-- Creating foreign key on [CrimeId] in table 'Person_TBC'
ALTER TABLE [dbo].[Person_TBC]
ADD CONSTRAINT [FK_CrimeTBC]
    FOREIGN KEY ([CrimeId])
    REFERENCES [dbo].[Crime]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CrimeTBC'
CREATE INDEX [IX_FK_CrimeTBC]
ON [dbo].[Person_TBC]
    ([CrimeId]);
GO

-- Creating foreign key on [IdPerson] in table 'Person_Familiar'
ALTER TABLE [dbo].[Person_Familiar]
ADD CONSTRAINT [FK_Familiar_inherits_Person]
    FOREIGN KEY ([IdPerson])
    REFERENCES [dbo].[Person]
        ([IdPerson])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdPerson] in table 'Person_TBC'
ALTER TABLE [dbo].[Person_TBC]
ADD CONSTRAINT [FK_TBC_inherits_Person]
    FOREIGN KEY ([IdPerson])
    REFERENCES [dbo].[Person]
        ([IdPerson])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------