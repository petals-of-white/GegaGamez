CREATE TABLE [dbo].[Rating]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GameId] INT NOT NULL,
    [UserId] INT NOT NULL, 
    [RatingScore] TINYINT NOT NULL, 
    CONSTRAINT [FK_Rating_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id]), 
    CONSTRAINT [FK_Rating_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [CK_Rating_RatingScore] CHECK (RatingScore Between 1 and 10)
)

GO

CREATE NONCLUSTERED INDEX [NIX_Rating_GameId_UserId] ON [dbo].[Rating] ([GameId], [UserId])
