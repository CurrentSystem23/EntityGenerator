using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public static partial class CoreMapper
  {
    private static void MapSchema(
      Database database,
      ICollection<SchemaDto> schemas,
      ICollection<DatabaseObjectDto> databaseObjects,
      ICollection<ColumnDto> columns)
    {
      if (database != null && schemas != null)
      {
        foreach (SchemaDto schemaDto in schemas.Where(w => w.DatabaseName.Equals(database.Name)))
        {
          database.Schemas.Add(MapSchema(schemaDto, databaseObjects, columns));
        }
      }
    }

    private static Schema MapSchema(
      SchemaDto schemaDto, 
      ICollection<DatabaseObjectDto> databaseObjects, 
      ICollection<ColumnDto> columns)
    {
      Schema schema = new()
      {
        Name = schemaDto.ObjectName,
        Id = schemaDto.ObjectId
      };

      MapTable(schema, schemaDto, databaseObjects, columns);

      return schema;
    }
  }
}
