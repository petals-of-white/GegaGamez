CREATE TABLE [dbo].[Rating]
(
    [UserId] INT NOT NULL, 
    [GameId] INT NOT NULL,
    [RatingScore] TINYINT NOT NULL, 
    CONSTRAINT [FK_Rating_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id]), 
    CONSTRAINT [FK_Rating_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [CK_Rating_RatingScore] CHECK (RatingScore Between 1 and 10), 
    CONSTRAINT [PK_Rating] PRIMARY KEY ([UserId], [RatingScore], [GameId])
)

GO

CREATE NONCLUSTERED INDEX [NIX_Rating_GameId] ON [dbo].[Rating] ([GameId])

GO

CREATE NONCLUSTERED INDEX [NIX_Rating_RatingScore] ON [dbo].[Rating] ([RatingScore])
