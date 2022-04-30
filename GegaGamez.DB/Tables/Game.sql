CREATE TABLE [dbo].[Game]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [ReleaseDate] DATE NOT NULL, 
    [Description] NVARCHAR(1000) NOT NULL, 
    [DeveloperId] INT NOT NULL, 
    CONSTRAINT [FK_Game_Developer] FOREIGN KEY (DeveloperId) REFERENCES [Developer]([Id])
)

GO

CREATE NONCLUSTERED INDEX [NIX_Game_Title] ON [dbo].[Game] ([Title])

GO

CREATE NONCLUSTERED INDEX [NIX_Game_DeveloperId] ON [dbo].[Game] ([DeveloperId])

GO

CREATE INDEX [NIX_Game_ReleaseDate] ON [dbo].[Game] ([ReleaseDate])
