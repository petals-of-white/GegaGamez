CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(512) NOT NULL, 
    [Name] NVARCHAR(100) NULL, 
    [CountryId] INT NULL, 
    [About] NVARCHAR(500) NULL, 
    CONSTRAINT [FK_User_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id])
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [NIX_User_Username] ON [dbo].[User] ([Username])
