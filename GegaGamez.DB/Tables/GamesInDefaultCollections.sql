CREATE TABLE [dbo].[GamesInDefaultCollections]
(
	[DefaultCollectionId] INT NOT NULL , 
    [GameId] INT NOT NULL, 
    PRIMARY KEY ([DefaultCollectionId], [GameId]), 
    CONSTRAINT [FK_GamesInDefaultCollections_DefaultCollection] FOREIGN KEY ([DefaultCollectionId]) REFERENCES [DefaultCollection]([Id]), 
    CONSTRAINT [FK_GamesInDefaultCollections_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id])
)
