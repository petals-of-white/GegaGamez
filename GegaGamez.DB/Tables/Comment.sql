CREATE TABLE [dbo].[Comment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GameId] INT NOT NULL,
    [UserId] INT NOT NULL, 
    [Text] NVARCHAR(1000) NOT NULL, 
    [CreatedAt] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_Comment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Comment_GameId] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id]) ON DELETE CASCADE
)

GO

CREATE INDEX [NIX_Comment_GameId] ON [dbo].[Comment] ([GameId])
