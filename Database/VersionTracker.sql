CREATE TABLE [dbo].[VersionTracker]
(
    [Id] INT NOT NULL IDENTITY(1, 1), 
    [Version] TIMESTAMP NOT NULL, 
    [LastChanged] DATETIME2 NOT NULL, 
    CONSTRAINT [PK_VersionTracker] PRIMARY KEY ([Id]),
)