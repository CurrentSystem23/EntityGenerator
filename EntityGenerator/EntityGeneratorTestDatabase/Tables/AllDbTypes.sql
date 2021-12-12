﻿CREATE TABLE [dbo].[AllDbTypes]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [BigIntValue] BIGINT NOT NULL, 
    [BigIntNullableValue] BIGINT NULL, 
    [BinaryValue] BINARY(50) NOT NULL, 
    [BinaryNullableValue] BINARY(50) NULL, 
    [BitValue] BIT NOT NULL, 
    [BitNullableValue] BIT NULL, 
    [CharValue] CHAR(10) NOT NULL, 
    [CharNullableValue] CHAR(10) NULL, 
    [DateValue] DATE NOT NULL, 
    [DateNullableValue] DATE NULL, 
    [DateTimeValue] DATETIME NOT NULL, 
    [DateTimeNullableValue] DATETIME NULL, 
    [DateTime2Value] DATETIME2 NOT NULL, 
    [DateTime2NullableValue] DATETIME2 NULL, 
    [DateTimeOffsetValue] DATETIMEOFFSET NOT NULL, 
    [DateTimeOffsetNullableValue] DATETIMEOFFSET NULL, 
    [DecimalValue] DECIMAL NOT NULL, 
    [DecimalNullableValue] DECIMAL NULL, 
    [FloatValue] FLOAT NOT NULL, 
    [FloatNullableValue] FLOAT NULL, 
    [ImageValue] IMAGE NOT NULL, 
    [ImageNullableValue] IMAGE NULL, 
    [IntValue] INT NOT NULL, 
    [IntNullableValue] INT NULL, 
    [MoneyValue] MONEY NOT NULL, 
    [MoneyNullableValue] MONEY NULL, 
    [NCharValue] NCHAR(10) NOT NULL, 
    [NCharNullableValue] NCHAR(10) NULL, 
    [NTextValue] NTEXT NOT NULL, 
    [NTextNullableValue] NTEXT NULL, 
    [NumericValue] NUMERIC NOT NULL, 
    [NumericNullableValue] NUMERIC NULL, 
    [NVarCharValue] NVARCHAR(50) NOT NULL, 
    [NVarCharNullableValue] NVARCHAR(50) NULL, 
    [NVarCharMaxValue] NVARCHAR(MAX) NOT NULL, 
    [NVarCharMaxNullableValue] NVARCHAR(MAX) NULL, 
    [RealValue] REAL NOT NULL, 
    [RealNullableValue] REAL NULL, 
    [RowVersionValue] ROWVERSION NOT NULL, 
    [SmallDateTimeValue] SMALLDATETIME NOT NULL, 
    [SmallDateTimeNullableValue] SMALLDATETIME NULL, 
    [SmallIntValue] SMALLINT NOT NULL, 
    [SmallIntNullableValue] SMALLINT NULL, 
    [SmallMoneyValue] SMALLMONEY NOT NULL, 
    [SmallMoneyNullableValue] SMALLMONEY NULL, 
    [Sql_VariantValue] SQL_VARIANT NOT NULL, 
    [Sql_VariantNullableValue] SQL_VARIANT NULL, 
    [TextValue] TEXT NOT NULL, 
    [TextNullableValue] TEXT NULL, 
    [TimeValue] TIME NOT NULL, 
    [TimeNullableValue] TIME NULL, 
--    [TimeStampValue] TIMESTAMP NOT NULL,
--    [TimeStampNullableValue] TIMESTAMP NULL, 
    [TinyIntValue] TINYINT NOT NULL, 
    [TinyIntNullableValue] TINYINT NULL, 
    [UniqueIdentifierValue] UNIQUEIDENTIFIER NOT NULL, 
    [UniqueIdentifierNullableValue] UNIQUEIDENTIFIER NULL, 
    [VarBinaryValue] VARBINARY(50) NOT NULL, 
    [VarBinaryNullableValue] VARBINARY(50) NULL, 
    [VarBinaryMaxValue] VARBINARY(MAX) NOT NULL, 
    [VarBinaryMaxNullableValue] VARBINARY(MAX) NULL, 
    [VarCharValue] VARCHAR(50) NOT NULL, 
    [VarCharNullableValue] VARCHAR(50) NULL, 
    [VarCharMaxValue] VARCHAR(MAX) NOT NULL, 
    [VarCharMaxNullableValue] VARCHAR(MAX) NULL, 
    [XmlValue] XML NOT NULL,
    [XmlNullableValue] XML NULL
)
