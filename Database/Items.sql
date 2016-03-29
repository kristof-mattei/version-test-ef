CREATE TABLE [dbo].[Items]
(
    [Id] INT NOT NULL IDENTITY(1, 1), 
    [VersionTrackerId] INT NOT NULL, 
    CONSTRAINT [PK_Items] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Items_VersionTracker] FOREIGN KEY ([VersionTrackerId]) REFERENCES [VersionTracker]([Id]),
)