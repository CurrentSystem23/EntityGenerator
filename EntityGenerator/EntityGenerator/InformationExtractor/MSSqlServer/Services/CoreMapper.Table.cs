using System;
using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public static partial class CoreMapper
  {
    private static void MapTable(
      Schema schema,
      SchemaDto schemaDto,
      ICollection<DatabaseObjectDto> databaseObjectDtos,
      ICollection<ColumnDto> columns
    )
    {
      if (schema != null && databaseObjectDtos != null)
      {
        foreach (DatabaseObjectDto databaseObjectDto in databaseObjectDtos.Where(w => 
                   w.DatabaseObject == Models.Enums.DatabaseObjects.U && 
                   w.DatabaseName.Equals(schemaDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                   w.SchemaName.Equals(schemaDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
        {
          schema.Tables.Add(MapTable(databaseObjectDto, columns));
        }
      }
    }

    private static Table MapTable(DatabaseObjectDto databaseObjectDto, ICollection<ColumnDto> columns)
    {
      Table table = new()
      {
        Name = databaseObjectDto.ObjectName,
        Id = databaseObjectDto.ObjectId
      };

      MapColumn(table, databaseObjectDto, columns);

      return table;
    }
  }
}
