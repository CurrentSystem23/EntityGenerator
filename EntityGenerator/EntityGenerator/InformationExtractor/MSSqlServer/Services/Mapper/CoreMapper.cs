using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

/// <summary>
/// Class <see cref="CoreMapper"/> contains methods for mapping extraction informations from MS SqlServer to core model.
/// </summary>
public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;DatabaseDto&gt;"/> and all subnodes into a <see cref="Database"/>.
  /// </summary>
  /// <param name="databaseDtos">The given <see cref="ICollection&lt;DatabaseDto&gt;"/></param>
  /// <param name="schemaDtos">The given <see cref="ICollection&lt;SchemaDto&gt;"/></param>
  /// <param name="databaseObjectDtos">The given <see cref="ICollection&lt;DatabaseObjectDto&gt;"/></param>
  /// <param name="functionDtos">The given <see cref="ICollection&lt;FunctionDto&gt;"/></param>
  /// <param name="tableValueFunctionsReturnValueDtos">The given <see cref="ICollection&lt;TableValueFunctionsReturnValueDto&gt;"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedTablePropertyDtos">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="typeDtos">The given <see cref="ICollection&lt;TypeDto&gt;"/></param>
  /// <param name="userDefinedTableTypeColumnDtos">The given <see cref="ICollection&lt;UserDefinedTableTypeColumnDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  /// <param name="foreignKeyDtos">The given <see cref="ICollection&lt;ForeignKeyDto&gt;"/></param>
  /// <param name="indexDtos">The given <see cref="ICollection&lt;IndexDto&gt;"/></param>
  /// <param name="triggerDtos">The given <see cref="ICollection&lt;TriggerDto&gt;"/></param>
  /// <returns>A <see cref="Database"/> with the core database structure.</returns>
  public static Database MapToCoreModel(
    ICollection<DatabaseDto> databaseDtos,
    ICollection<SchemaDto> schemaDtos,
    ICollection<DatabaseObjectDto> databaseObjectDtos,
    ICollection<FunctionDto> functionDtos,
    ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValueDtos,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedTablePropertyDto> extendedTablePropertyDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<TypeDto> typeDtos,
    ICollection<UserDefinedTableTypeColumnDto> userDefinedTableTypeColumnDtos,
    ICollection<ConstraintDto> constraintDtos,
    ICollection<ForeignKeyDto> foreignKeyDtos,
    ICollection<IndexDto> indexDtos,
    ICollection<TriggerDto> triggerDtos
  )
  {
    Database database = MapDatabase(databaseDtos);
    MapSchema(
      database,
      schemaDtos,
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
      );
    MapType(database, typeDtos);

    return database;
  }
}

