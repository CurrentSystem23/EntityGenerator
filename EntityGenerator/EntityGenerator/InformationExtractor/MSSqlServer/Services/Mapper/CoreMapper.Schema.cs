using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;SchemaDto&gt;"/> and all subnodes into a <see cref="Database"/>.
  /// </summary>
  /// <param name="database">The given <see cref="Database"/></param>
  /// <param name="schemaDtos">The given <see cref="ICollection&lt;SchemaDto&gt;"/></param>
  /// <param name="databaseObjectDtos">The given <see cref="ICollection&lt;DatabaseObjectDto&gt;"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedTablePropertyDtos">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  /// <param name="foreignKeyDtos">The given <see cref="ICollection&lt;ForeignKeyDto&gt;"/></param>
  /// <param name="triggerDtos">The given <see cref="ICollection&lt;TriggerDto&gt;"/></param>
  /// <param name="indexDtos">The given <see cref="ICollection&lt;IndexDto&gt;"/></param>
  /// <param name="userDefinedTableTypeColumnDtos">The given <see cref="ICollection&lt;UserDefinedTableTypeColumnDto&gt;"/></param>
  /// <param name="functionDtos">The given <see cref="ICollection&lt;FunctionDto&gt;"/></param>
  /// <param name="tableValueFunctionsReturnValueDtos">The given <see cref="ICollection&lt;TableValueFunctionsReturnValueDto&gt;"/></param>
  private static void MapSchema(
    Database database,
    ICollection<SchemaDto> schemaDtos,
    ICollection<DatabaseObjectDto> databaseObjectDtos,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedTablePropertyDto> extendedTablePropertyDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<ConstraintDto> constraintDtos,
    ICollection<ForeignKeyDto> foreignKeyDtos,
    ICollection<TriggerDto> triggerDtos,
    ICollection<IndexDto> indexDtos,
    ICollection<UserDefinedTableTypeColumnDto> userDefinedTableTypeColumnDtos,
    ICollection<FunctionDto> functionDtos,
    ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValueDtos
    )
  {
    if (database != null && schemaDtos != null)
    {
      foreach (SchemaDto schemaDto in schemaDtos.Where(w => w.DatabaseName.Equals(database.Name)))
      {
        database.Schemas.Add(MapSchema(schemaDto,
                                            databaseObjectDtos,
                                            columnDtos,
                                            extendedTablePropertyDtos,
                                            extendedColumnPropertyDtos,
                                            constraintDtos,
                                            foreignKeyDtos,
                                            triggerDtos,
                                            indexDtos,
                                            userDefinedTableTypeColumnDtos,
                                            functionDtos,
                                            tableValueFunctionsReturnValueDtos
                                            ));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="SchemaDto"/> and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="schemaDto">The given <see cref="SchemaDto"/></param>
  /// <param name="databaseObjectDtos">The given <see cref="ICollection&lt;DatabaseObjectDto&gt;"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedTablePropertyDtos">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  /// <param name="foreignKeyDtos">The given <see cref="ICollection&lt;ForeignKeyDto&gt;"/></param>
  /// <param name="triggerDtos">The given <see cref="ICollection&lt;TriggerDto&gt;"/></param>
  /// <param name="indexDtos">The given <see cref="ICollection&lt;IndexDto&gt;"/></param>
  /// <param name="userDefinedTableTypeColumnDtos">The given <see cref="ICollection&lt;UserDefinedTableTypeColumnDto&gt;"/></param>
  /// <param name="functionDtos">The given <see cref="ICollection&lt;FunctionDto&gt;"/></param>
  /// <param name="tableValueFunctionsReturnValueDtos">The given <see cref="ICollection&lt;TableValueFunctionsReturnValueDto&gt;"/></param>
  /// <returns>A <see cref="Schema"/> with the core schema structure.</returns>
  private static Schema MapSchema(
    SchemaDto schemaDto,
    ICollection<DatabaseObjectDto> databaseObjectDtos,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedTablePropertyDto> extendedTablePropertyDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<ConstraintDto> constraintDtos,
    ICollection<ForeignKeyDto> foreignKeyDtos,
    ICollection<TriggerDto> triggerDtos,
    ICollection<IndexDto> indexDtos,
    ICollection<UserDefinedTableTypeColumnDto> userDefinedTableTypeColumnDtos,
    ICollection<FunctionDto> functionDtos,
    ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValueDtos
    )
  {
    Schema schema = new()
    {
      Name = schemaDto.ObjectName,
      Id = schemaDto.ObjectId
    };

    MapTable(schema, schemaDto, databaseObjectDtos, columnDtos, extendedTablePropertyDtos, extendedColumnPropertyDtos, constraintDtos, foreignKeyDtos, triggerDtos, indexDtos);
    MapView(schema, schemaDto, databaseObjectDtos, columnDtos, extendedTablePropertyDtos, extendedColumnPropertyDtos, constraintDtos);
    MapTrigger(schema, triggerDtos);
    MapUserDefinedTableTypeColumn(schema, schemaDto, userDefinedTableTypeColumnDtos);
    MapFunction(schema, schemaDto, functionDtos, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos);
    return schema;
  }
}

