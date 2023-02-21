using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public static partial class CoreMapper
  {
    public static Database MapToCoreModel(
      ICollection<DatabaseDto> databases,
      ICollection<SchemaDto> schemas,
      ICollection<DatabaseObjectDto> databaseObjects,
      ICollection<FunctionDto> functions,
      ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValues,
      ICollection<ColumnDto> columns,
      ICollection<ExtendedTablePropertyDto> extendedTableProperties,
      ICollection<ExtendedColumnPropertyDto> extendedColumnProperties,
      ICollection<TypeDto> types,
      ICollection<UserDefinedTableTypeColumnDto> userDefinedTableTypeColumns,
      ICollection<ConstraintDto> constraints,
      ICollection<ForeignKeyDto> foreignKeys,
      ICollection<IndexDto> indexes,
      ICollection<TriggerDto> triggers
    )
    {
      Database database = MapDatabase(databases);
      MapSchema(database, schemas, databaseObjects, columns);
      return database;
    }
  }
}
