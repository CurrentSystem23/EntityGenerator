using System;
using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public static partial class CoreMapper
  {
    private static void MapColumn(
      Table table,
      DatabaseObjectDto parentObject,
      ICollection<ColumnDto> columns
    )
    {
      if (parentObject != null && columns != null)
      {
        foreach (ColumnDto columnDto in columns.Where(w =>
                   w.DatabaseName.Equals(parentObject.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                   w.SchemaName.Equals(parentObject.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                   w.TableName.Equals(parentObject.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
        {
          table.Columns.Add(MapColumn(columnDto));
        }
      }
    }

    private static Column MapColumn(ColumnDto columnDto)
    {
      Column column = new()
      {
        Name = columnDto.ObjectName,
        Id = columnDto.ObjectId,
        ColumnTypeDataType = MapToCoreDataType(columnDto.ColumnTypeDataType),
        ColumnIsIdentity = columnDto.ColumnIsIdentity,
        ColumnIsNullable = columnDto.ColumnIsNullable,
        ColumnDefaultDefinition = columnDto.ColumnDefaultDefinition,
        ColumnIsComputed = columnDto.ColumnIsComputed,
        ColumnMaxLength = columnDto.ColumnMaxLength,
        ColumnCharacterOctetLength = columnDto.ColumnCharacterOctetLength,
        ColumnNumericPrecision = columnDto.ColumnNumericPrecision,
        ColumnNumericPrecisionRadix = columnDto.ColumnNumericPrecisionRadix,
        ColumnNumericScale = columnDto.ColumnNumericScale,
        ColumnDatetimePrecision = columnDto.ColumnDatetimePrecision,
      };

      return column;
    }
  }
}
