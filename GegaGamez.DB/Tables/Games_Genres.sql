CREATE TABLE [dbo].[Games_Genres]
(
	[GameId] int NOT NULL, 
    [GenreId] INT NOT NULL, 
    CONSTRAINT [PK_Games_Genres] PRIMARY KEY ([GameId], [GenreId]), 
    CONSTRAINT [FK_Games_Genres_GameId] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Games_Genres_GenreID] FOREIGN KEY ([GenreId]) REFERENCES [Genre]([Id]) ON DELETE CASCADE
)

GO

CREATE NONCLUSTERED INDEX [NIX_Games_Genres_GenreId] ON [dbo].[Games_Genres] ([GenreId])
