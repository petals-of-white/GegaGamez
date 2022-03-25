CREATE TABLE [dbo].[DefaultCollectionType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(500) NULL
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [NIX_DefaultCollectionType_Name] ON [dbo].[DefaultCollectionType] ([Name])
