using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public static partial class CoreMapper
  {
    public static DataTypes MapToDbDataType(Core.Models.Enums.DataTypes dataType)
    {
      DataTypes coreDataType = DataTypes.Unknown;
      switch (dataType)
      {
        case Core.Models.Enums.DataTypes.Unknown:
          coreDataType = DataTypes.Unknown;
          break;
        case Core.Models.Enums.DataTypes.Boolean:
          coreDataType = DataTypes.Bit;
          break;
        case Core.Models.Enums.DataTypes.Byte:
        case Core.Models.Enums.DataTypes.SByte:
          coreDataType = DataTypes.TinyInt;
          break;
        case Core.Models.Enums.DataTypes.Int16:
        case Core.Models.Enums.DataTypes.UInt16:
          coreDataType = DataTypes.SmallInt;
          break;
        case Core.Models.Enums.DataTypes.Int32:
        case Core.Models.Enums.DataTypes.UInt32:
          coreDataType = DataTypes.Int;
          break;
        case Core.Models.Enums.DataTypes.Int64:
        case Core.Models.Enums.DataTypes.UInt64:
          coreDataType = DataTypes.BigInt;
          break;
        case Core.Models.Enums.DataTypes.Decimal:
          coreDataType = DataTypes.Decimal;
          break;
        case Core.Models.Enums.DataTypes.Single:
          coreDataType = DataTypes.Real;
          break;
        case Core.Models.Enums.DataTypes.Double:
          coreDataType = DataTypes.Float;
          break;
        case Core.Models.Enums.DataTypes.Enum:
          coreDataType = DataTypes.NVarChar;
          break;
        case Core.Models.Enums.DataTypes.Char:
          coreDataType = DataTypes.NChar1;
          break;
        case Core.Models.Enums.DataTypes.String:
        case Core.Models.Enums.DataTypes.CharArray:
        case Core.Models.Enums.DataTypes.XDocument:
        case Core.Models.Enums.DataTypes.XElement:
          coreDataType = DataTypes.NVarCharMax;
          break;
        case Core.Models.Enums.DataTypes.DateTime:
          coreDataType = DataTypes.DateTime2;
          break;
        case Core.Models.Enums.DataTypes.DateTimeOffset:
          coreDataType = DataTypes.DateTimeOffset;
          break;
        case Core.Models.Enums.DataTypes.TimeSpan:
        case Core.Models.Enums.DataTypes.TimeOnly:
          coreDataType = DataTypes.Time;
          break;
        case Core.Models.Enums.DataTypes.DateOnly:
          coreDataType = DataTypes.Date;
          break;
        case Core.Models.Enums.DataTypes.Binary:
          coreDataType = DataTypes.FileStream;
          break;
        case Core.Models.Enums.DataTypes.ByteArray:
        case Core.Models.Enums.DataTypes.ISerializeable:
          coreDataType = DataTypes.VarBinaryMax;
          break;
        case Core.Models.Enums.DataTypes.Guid:
          coreDataType = DataTypes.UniqueIdentifier;
          break;
        case Core.Models.Enums.DataTypes.Object:
          coreDataType = DataTypes.SqlVariant;
          break;
      }

      return coreDataType;
    }
  }
}
