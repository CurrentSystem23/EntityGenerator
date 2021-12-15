CREATE TABLE [core].[User]
(
	  [Id]                    BIGINT IDENTITY (1000000, 1) NOT NULL CONSTRAINT [pk_CoreUser_Id] PRIMARY KEY CLUSTERED ([Id] DESC) WITH (ALLOW_PAGE_LOCKS = OFF),
    [ModifiedDate]          DATETIME2        NOT NULL, 
    [ModifiedUser]          BIGINT           NOT NULL CONSTRAINT [fk_CoreUser_CoreUser_ModifiedUser] FOREIGN KEY ([ModifiedUser]) REFERENCES [core].[User] ([Id]),
                                        
    [Login]                 NVARCHAR(255)    NOT NULL CONSTRAINT [dv_CoreUser_Login] DEFAULT '' CONSTRAINT [ucUserLogin] UNIQUE([Login]), 
    [Password]              NVARCHAR(255)    NOT NULL CONSTRAINT [dv_CoreUser_Password] DEFAULT '', 
    [PasswordSalt]          NVARCHAR(255)    NOT NULL CONSTRAINT [dv_CoreUser_PasswordSalt] DEFAULT '', 
	  [Email]                 NVARCHAR(800)    NOT NULL CONSTRAINT [uc_CoreUser_Email] UNIQUE([Email]),
    [State]                 BIGINT           NOT NULL CONSTRAINT [dv_CoreUser_State] DEFAULT (376) CONSTRAINT [fkUserDomainValuesState] FOREIGN KEY ([State]) REFERENCES [core].[DomainValue] ([Id]),
    [FailedLoginCount]      BIGINT           NOT NULL,
    [LastLogin]             DATETIME2 (7)    NULL,
    [LastPasswordChange]    DATETIME2 (7)    NULL,
	  [DomainLogin]           NVARCHAR (60)    NOT NULL CONSTRAINT [dv_CoreUser_DomainLogin] DEFAULT (''),
    [ConditionsId]          BIGINT           NOT NULL CONSTRAINT [dv_CoreUser_ConditionsId] DEFAULT (0),
    [ConditionsFixed]       BIGINT           NOT NULL CONSTRAINT [dv_CoreUser_ConditionsFixed] DEFAULT (0),
    [PrivacyPolicyId]       BIGINT           NOT NULL CONSTRAINT [dv_CoreUser_PrivacyPolicyId] DEFAULT (0),
    [PrivacyPolicyFixed]    BIGINT           NOT NULL CONSTRAINT [dv_CoreUser_PrivacyPolicyFixed] DEFAULT (0),
    [PasswordLinkExtension] UNIQUEIDENTIFIER NULL,
    [PasswordDateOfExpiry]  DATETIME2        NULL,
    [NewEmail]              NVARCHAR (800)   NULL,
    [EmailLinkExtension]    UNIQUEIDENTIFIER NULL,
    [EmailDateOfExpiry]     DATETIME2        NULL
);
GO

CREATE INDEX [ix_coreUser_ModifiedUser] ON [core].[User] ([ModifiedUser]);
GO

CREATE INDEX [ix_coreUser_State] ON [core].[User] ([State]);
GO

CREATE INDEX [ix_coreUser_PasswordLinkExtension] ON [core].[User] ([PasswordLinkExtension]);
GO

CREATE INDEX [ix_coreUser_EmailLinkExtension] ON [core].[User] ([EmailLinkExtension]);
GO


