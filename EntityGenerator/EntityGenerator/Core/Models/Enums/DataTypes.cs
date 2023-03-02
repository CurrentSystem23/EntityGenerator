namespace EntityGenerator.Core.Models.Enums;

/// <summary>
/// Entity Generator core data types
/// </summary>
public enum DataTypes
{
  ///<summary>unknown data type</summary>
  Unknown,
  ///<summary>boolean data type</summary>
  Boolean,
  ///<summary>byte data type</summary>
  Byte,
  ///<summary>integer 16 bit long data type</summary>
  Int16,
  ///<summary>integer 32 bit long data type</summary>
  Int32,
  ///<summary>integer 64 bit long data type</summary>
  Int64,
  ///<summary>unsigned integer 8 bit long data type</summary>
  SByte,
  ///<summary>unsigned integer 16 bit long data type</summary>
  UInt16,
  ///<summary>unsigned integer 32 bit long data type</summary>
  UInt32,
  ///<summary>unsigned integer 64 bit long data type</summary>
  UInt64,
  ///<summary>decimal data type</summary>
  Decimal,
  ///<summary>single data type</summary>
  Single,
  ///<summary>double data type</summary>
  Double,
  ///<summary>enum data type</summary>
  Enum,
  ///<summary>char data type</summary>
  Char,
  ///<summary>string data type</summary>
  String,
  ///<summary>array of chars data type</summary>
  CharArray,
  ///<summary>Xml Document (XDocument) data type</summary>
  XDocument,
  ///<summary>Xml Element (XElement) data type</summary>
  XElement,
  ///<summary>datetime data type</summary>
  DateTime,
  ///<summary>datetimeoffset data type</summary>
  DateTimeOffset,
  ///<summary>timespan data type</summary>
  TimeSpan,
  ///<summary>datonly data type</summary>
  DateOnly,
  ///<summary>time only data type</summary>
  TimeOnly,
  ///<summary>binary data type</summary>
  Binary,
  ///<summary>array of bytes data type</summary>
  ByteArray,
  ///<summary>ISerializeable data type</summary>
  ISerializeable,
  ///<summary>unique id data type</summary>
  Guid,
  ///<summary>object data type</summary>
  Object,
  ///<summary>table value data type</summary>
  TableValue,
}

