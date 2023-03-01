namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

/// <summary>
/// SQL Server data types
/// </summary>
public enum DataTypes
{
  ///<summary>unknown data type</summary>
  Unknown,
  ///<summary>bit data type</summary>
  Bit,
  ///<summary>tinyint data type</summary>
  TinyInt,
  ///<summary>smallint data type</summary>
  SmallInt,
  ///<summary>int data type</summary>
  Int,
  ///<summary>bigint data type</summary>
  BigInt,
  ///<summary>small money data type</summary>
  SmallMoney,
  ///<summary>money data type</summary>
  Money,
  ///<summary>decimal data type</summary>
  Decimal,
  ///<summary>real data type</summary>
  Real,
  ///<summary>float data type</summary>
  Float,
  ///<summary>char data type with fixed size of one char</summary>
  Char1,
  ///<summary>nchar data type with fixed size of one char</summary>
  NChar1,
  ///<summary>char data type with fixed size</summary>
  Char,
  ///<summary>nchar data type with fixed size</summary>
  NChar,
  ///<summary>varchar data type with limited size</summary>
  VarChar,
  ///<summary>nvarchar data type with limited size</summary>
  NVarChar,
  ///<summary>varchar data type with unlimited size</summary>
  VarCharMax,
  ///<summary>nvarchar data type with unlimited size</summary>
  NVarCharMax,
  ///<summary>text data type</summary>
  Text,
  ///<summary>unichar text data type</summary>
  NText,
  ///<summary>xml data type</summary>
  Xml,
  ///<summary>small datetime data type</summary>
  SmallDateTime,
  ///<summary>datetime data type</summary>
  DateTime,
  ///<summary>datetim2 data type</summary>
  DateTime2,
  ///<summary>date time offset data type</summary>
  DateTimeOffset,
  ///<summary>date only data type</summary>
  Date,
  ///<summary>time only data type</summary>
  Time,
  ///<summary>binary data type with fixed size</summary>
  Binary,
  ///<summary>var binary data type with limited size</summary>
  VarBinary,
  ///<summary>var binary data type with unlimited size</summary>
  VarBinaryMax,
  ///<summary>image data type</summary>
  Image,
  ///<summary>timestamp data type</summary>
  TimeStamp,
  ///<summary>filestream data type</summary>
  FileStream,
  ///<summary>global id data type</summary>
  UniqueIdentifier,
  ///<summary>sql variant data type</summary>
  SqlVariant,
}

