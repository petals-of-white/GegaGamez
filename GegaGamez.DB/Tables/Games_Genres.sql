CREATE TABLE [dbo].[Games_Genres]
(
	[GameId] int NOT NULL, 
    [GenreId] INT NOT NULL, 
    CONSTRAINT [PK_Games_Genres] PRIMARY KEY ([GameId], [GenreId]), 
    CONSTRAINT [FK_Games_Genres_GameId] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id]),
    CONSTRAINT [FK_Games_Genres_GenreID] FOREIGN KEY ([GenreId]) REFERENCES [Genre]([Id])
)
