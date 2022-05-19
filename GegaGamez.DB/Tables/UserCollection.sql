
CREATE TABLE [dbo].[UserCollection]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_UserCollection_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])  ON DELETE CASCADE
 )
GO

CREATE UNIQUE NONCLUSTERED INDEX [NIX_UserCollection_UserId_Name] ON [dbo].[UserCollection] ([UserId], [Name])
   