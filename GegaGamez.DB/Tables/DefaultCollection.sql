CREATE TABLE [dbo].[DefaultCollection]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserId] INT NOT NULL, 
    [DefaultCollectionTypeId] INT NOT NULL, 
    CONSTRAINT [FK_DefaultCollection_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_DefaultCollection_DefaultCollectionType] FOREIGN KEY ([DefaultCollectionTypeId]) REFERENCES [DefaultCollectionType]([Id])
    
)

GO

CREATE UNIQUE NONCLUSTERED INDEX [NIX_DefaultCollection_UserId_DefaultCollectionTypeId] ON [dbo].[DefaultCollection] ([UserId],[DefaultCollectionTypeId])
