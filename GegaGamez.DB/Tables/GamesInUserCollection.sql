CREATE TABLE [dbo].[GamesInUserCollections]
(
	[UserCollectionId] INT NOT NULL , 
    [GameId] INT NOT NULL, 
    PRIMARY KEY ([UserCollectionId], [GameId]), 
    CONSTRAINT [FK_GamesInUserCollections_Collection] FOREIGN KEY ([UserCollectionId]) REFERENCES [UserCollection]([Id])  ON DELETE CASCADE, 
    CONSTRAINT [FK_GamesInUserCollections_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id])  ON DELETE CASCADE
)
