CREATE TABLE [dbo].[GamesInDefaultCollections]
(
	[DefaultCollectionId] INT NOT NULL , 
    [GameId] INT NOT NULL, 
    PRIMARY KEY ([DefaultCollectionId], [GameId]), 
    CONSTRAINT [FK_GamesInDefaultCollections_DefaultCollection] FOREIGN KEY ([DefaultCollectionId]) REFERENCES [DefaultCollection]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_GamesInDefaultCollections_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id])  ON DELETE CASCADE
)
