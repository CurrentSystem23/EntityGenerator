CREATE TABLE [core].[DomainValue]
(
    [Id]           BIGINT IDENTITY (1000000, 1) NOT NULL CONSTRAINT [pk_CoreDomainValue_Id] PRIMARY KEY CLUSTERED ([Id] DESC) WITH (ALLOW_PAGE_LOCKS = OFF),
    [ModifiedDate] DATETIME2      NOT NULL, 
    [ModifiedUser] BIGINT         NOT NULL CONSTRAINT [fk_CoreDomainValue_CoreUser_ModifiedUser] FOREIGN KEY ([ModifiedUser]) REFERENCES [core].[User] ([Id]),

    [TypeId]       BIGINT         NOT NULL CONSTRAINT [fk_CoreDomainValue_CoreDomainType_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [core].[DomainType] ([Id]),
    [ValueC]       NVARCHAR(400)  NULL,
    [ValueN]       BIGINT         NULL,
    [ValueD]       DATETIME2 (7)  NULL,
    [ValueF]       FLOAT (53)     NULL,
    [DivId]        NVARCHAR(MAX)  NULL,
    [Description]  NVARCHAR(4000) NOT NULL CONSTRAINT [dv_CoreDomainValue_Description] DEFAULT '',
    [Unit]         NVARCHAR(100)  NULL,
);
GO

CREATE INDEX [ix_coreDomainValue_ModifiedUser] ON [core].[DomainValue] ([ModifiedUser]);
GO

CREATE INDEX [ix_coreDomainValue_TypeId] ON [core].[DomainValue] ([TypeId]);
GO

