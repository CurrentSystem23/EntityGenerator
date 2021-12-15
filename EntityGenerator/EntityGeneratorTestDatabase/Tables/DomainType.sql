CREATE TABLE [core].[DomainType]
(
    [Id]           BIGINT IDENTITY (1000000, 1) NOT NULL CONSTRAINT [pk_CoreDomainType_Id] PRIMARY KEY CLUSTERED ([Id] DESC) WITH (ALLOW_PAGE_LOCKS = OFF),
    [ModifiedDate] DATETIME2      NOT NULL, 
    [ModifiedUser] BIGINT         NOT NULL CONSTRAINT [fk_CoreDomainType_CoreUser_ModifiedUser] FOREIGN KEY ([ModifiedUser]) REFERENCES [core].[User] ([Id]),

    [Type]         NVARCHAR(50)   NOT NULL,
    [Description]  NVARCHAR(MAX)  NOT NULL CONSTRAINT [dv_CoreDomainType_Description] DEFAULT '',
    [Mode]         CHAR (1)       NOT NULL CONSTRAINT [dv_CoreDomainType_Mode] DEFAULT 'C',
    [StandardId]   BIGINT         NULL,
    [Editable]     BIGINT         NOT NULL CONSTRAINT [dv_CoreDomainType_Editable] DEFAULT (0)
);
GO
