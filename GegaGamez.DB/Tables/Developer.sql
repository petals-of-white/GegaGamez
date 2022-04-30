CREATE TABLE [dbo].[Developer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [BeginDate] DATE NOT NULL, 
    [EndDate] DATE NULL, 
    [Description] NVARCHAR(500) NOT NULL 
)

GO

CREATE INDEX [NIX_Developer_Name] ON [dbo].[Developer] ([Name])
