using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Helper
{
  public abstract class TypeHelper
  {
    public static string GetDaoType(string name, bool isTable)
    {
      string daoName = $"{name}Dao";
      return isTable ? daoName : $"{daoName}V";
    }

    public static string GetInternalDaoType(string name, DbObjectType dbObjectType)
    {
      string daoName = $"{name}InternalDao";
      switch (dbObjectType)
      {
        case DbObjectType.TABLE:
          return daoName;
        case DbObjectType.VIEW:
          return $"{daoName}V";
        case DbObjectType.TABLEVALUEFUNCTION:
          return $"{daoName}V";
        default:
          throw new NotSupportedException();
      }
    }

    /// <summary>
    /// Parses value into type appropriate format.
    /// </summary>
    /// <returns></returns>
    public static string ParseTypeValue(DataTypes type, string value)
    {
      switch (type)
      {
        case DataTypes.Boolean:
          return value;
        case DataTypes.Byte:
          return @$"""{value}""";
        case DataTypes.Int16:
          return value;
        case DataTypes.Int32:
          return value;
        case DataTypes.Int64:
          return value;
        case DataTypes.SByte:
          return @$"""{value}""";
        case DataTypes.UInt16:
          return value;
        case DataTypes.UInt32:
          return value;
        case DataTypes.UInt64:
          return value;
        case DataTypes.Decimal:
          return value;
        case DataTypes.Single:
          return value;
        case DataTypes.Double:
          return value;
        case DataTypes.Enum:
          return @$"""{value}""";
        case DataTypes.Char:
          return $"'{value}'";
        case DataTypes.String:
          return @$"""{value}""";
        case DataTypes.CharArray:
          return value;
        case DataTypes.XDocument:
          return @$"""{value}""";
        case DataTypes.XElement:
          return @$"""{value}""";
        case DataTypes.DateTime:
          return $"new(DateTime.Parse({value}))";
        case DataTypes.DateTimeOffset:
          return $"new(DateTimeOffset.Parse({value}))";
        case DataTypes.TimeSpan:
          return $"new(TimeSpan.Parse({value}))";
        case DataTypes.DateOnly:
          return $"new(DateOnly.Parse({value}))";
        case DataTypes.TimeOnly:
          return $"new(TimeOnly.Parse({value}))";
        case DataTypes.Binary:
          return @$"""{value}""";
        case DataTypes.ByteArray:
          return value;
        case DataTypes.ISerializeable:
          return @$"""{value}""";
        case DataTypes.Guid:
          return $"Guid.Parse({value})";
        case DataTypes.Object:
          return @$"""{value}""";
        case DataTypes.TableValue:
          return @$"""{value}""";
        default:
          throw new NotSupportedException($"Error: Datatype {type} is not supported.");
      }
    }
  }
}
