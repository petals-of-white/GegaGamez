CREATE TABLE [dbo].[Developer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [BeginDate] DATE NOT NULL, 
    --[IsActive] BIT NOT NULL DEFAULT 1, 
    [EndDate] DATE NULL, 
    CONSTRAINT [CK_Developer_IsActive] CHECK (1 = 1)
)

GO

CREATE INDEX [NIX_Developer_Name] ON [dbo].[Developer] ([Name])
