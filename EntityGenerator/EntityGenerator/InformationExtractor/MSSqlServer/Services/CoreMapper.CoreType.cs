using EntityGenerator.Core.Models.Enums;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public static partial class CoreMapper
  {
    public static DataTypes MapToCoreDataType(Models.Enums.DataTypes dataType)
    {
      DataTypes coreDataType = DataTypes.Unknown;
      switch (dataType)
      {
        case Models.Enums.DataTypes.Unknown:
          coreDataType = DataTypes.Unknown;
          break;
        case Models.Enums.DataTypes.Bit:
          coreDataType = DataTypes.Boolean;
          break;
        case Models.Enums.DataTypes.TinyInt:
          coreDataType = DataTypes.Byte;
          break;
        case Models.Enums.DataTypes.SmallInt:
          coreDataType = DataTypes.Int16;
          break;
        case Models.Enums.DataTypes.Int:
          coreDataType = DataTypes.Int32;
          break;
        case Models.Enums.DataTypes.BigInt:
          coreDataType = DataTypes.Int64;
          break;
        case Models.Enums.DataTypes.SmallMoney:
        case Models.Enums.DataTypes.Money:
        case Models.Enums.DataTypes.Decimal:
          coreDataType = DataTypes.Decimal;
          break;
        case Models.Enums.DataTypes.Real:
          coreDataType = DataTypes.Single;
          break;
        case Models.Enums.DataTypes.Float:
          coreDataType = DataTypes.Double;
          break;
        case Models.Enums.DataTypes.Char1:
        case Models.Enums.DataTypes.NChar1:
          coreDataType = DataTypes.Char;
          break;
        case Models.Enums.DataTypes.Char:
        case Models.Enums.DataTypes.NChar:
        case Models.Enums.DataTypes.VarChar:
        case Models.Enums.DataTypes.NVarChar:
        case Models.Enums.DataTypes.VarCharMax:
        case Models.Enums.DataTypes.NVarCharMax:
        case Models.Enums.DataTypes.Text:
        case Models.Enums.DataTypes.NText:
        case Models.Enums.DataTypes.Xml:
          coreDataType = DataTypes.String;
          break;
        case Models.Enums.DataTypes.SmallDateTime:
        case Models.Enums.DataTypes.DateTime:
        case Models.Enums.DataTypes.DateTime2:
          coreDataType = DataTypes.DateTime;
          break;
        case Models.Enums.DataTypes.DateTimeOffset:
          coreDataType = DataTypes.DateTimeOffset;
          break;
        case Models.Enums.DataTypes.Date:
          coreDataType = DataTypes.DateOnly;
          break;
        case Models.Enums.DataTypes.Time:
          coreDataType = DataTypes.TimeOnly;
          break;
        case Models.Enums.DataTypes.Binary:
        case Models.Enums.DataTypes.VarBinary:
        case Models.Enums.DataTypes.VarBinaryMax:
        case Models.Enums.DataTypes.Image:
        case Models.Enums.DataTypes.TimeStamp:
          coreDataType = DataTypes.ByteArray;
          break;
        case Models.Enums.DataTypes.FileStream:
          coreDataType = DataTypes.Binary;
          break;
        case Models.Enums.DataTypes.UniqueIdentifier:
          coreDataType = DataTypes.Guid;
          break;
        case Models.Enums.DataTypes.SqlVariant:
          coreDataType = DataTypes.Object;
          break;
      }

      return coreDataType;
    }
  }
}
