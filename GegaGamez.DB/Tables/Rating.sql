CREATE TABLE [dbo].[Rating]
(
    [Id] INT NOT NULL IDENTITY, 
    [UserId] INT NOT NULL, 
    [GameId] INT NOT NULL,
    [RatingScore] TINYINT NOT NULL, 
    CONSTRAINT [FK_Rating_Game] FOREIGN KEY ([GameId]) REFERENCES [Game]([Id])  ON DELETE CASCADE, 
    CONSTRAINT [FK_Rating_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])  ON DELETE CASCADE, 
    CONSTRAINT [CK_Rating_RatingScore] CHECK (RatingScore Between 1 and 10), 
    CONSTRAINT [PK_Rating] PRIMARY KEY ([Id])
)

GO

CREATE NONCLUSTERED INDEX [NIX_Rating_GameId] ON [dbo].[Rating] ([GameId])

GO

CREATE NONCLUSTERED INDEX [NIX_Rating_RatingScore] ON [dbo].[Rating] ([RatingScore])

GO

CREATE UNIQUE INDEX [NIX_Rating_UserId_GameId_RatingScore] ON [dbo].[Rating] ([UserId], [GameId], [RatingScore])
