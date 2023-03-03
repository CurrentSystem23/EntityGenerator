using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all tables data and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="schema">The given <see cref="Schema"/></param>
  /// <param name="schemaDto">The given <see cref="SchemaDto"/></param>
  /// <param name="databaseObjectDtos">The given <see cref="ICollection&lt;DatabaseObjectDto&gt;"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedTablePropertyDtos">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  /// <param name="foreignKeyDtos">The given <see cref="ICollection&lt;ForeignKeyDto&gt;"/></param>
  /// <param name="triggerDtos">The given <see cref="ICollection&lt;TriggerDto&gt;"/></param>
  /// <param name="indexDtos">The given <see cref="ICollection&lt;IndexDto&gt;"/></param>
  private static void MapTable(
    Schema schema,
    SchemaDto schemaDto,
    ICollection<DatabaseObjectDto> databaseObjectDtos,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedTablePropertyDto> extendedTablePropertyDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<ConstraintDto> constraintDtos,
    ICollection<ForeignKeyDto> foreignKeyDtos,
    ICollection<TriggerDto> triggerDtos,
    ICollection<IndexDto> indexDtos
    )
  {
    if (schema != null && databaseObjectDtos != null)
    {
      foreach (DatabaseObjectDto databaseObjectDto in databaseObjectDtos.Where(w =>
                 w.DatabaseObject == Models.Enums.DatabaseObjects.U &&
                 w.DatabaseName.Equals(schemaDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(schemaDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        schema.Tables.Add(MapTable(databaseObjectDto, columnDtos, extendedTablePropertyDtos, extendedColumnPropertyDtos, constraintDtos, foreignKeyDtos, triggerDtos, indexDtos));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="SchemaDto"/> and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="databaseObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedTablePropertyDtos">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  /// <param name="foreignKeyDtos">The given <see cref="ICollection&lt;ForeignKeyDto&gt;"/></param>
  /// <param name="triggerDtos">The given <see cref="ICollection&lt;TriggerDto&gt;"/></param>
  /// <param name="indexDtos">The given <see cref="ICollection&lt;IndexDto&gt;"/></param>
  /// <returns>A <see cref="Table"/> with the core table structure.</returns>
  private static Table MapTable(
    DatabaseObjectDto databaseObjectDto,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedTablePropertyDto> extendedTablePropertyDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<ConstraintDto> constraintDtos,
    ICollection<ForeignKeyDto> foreignKeyDtos,
    ICollection<TriggerDto> triggerDtos,
    ICollection<IndexDto> indexDtos
    )
  {
    Table table = new()
    {
      Name = databaseObjectDto.ObjectName,
      Id = databaseObjectDto.ObjectId
    };

    MapColumn(table, databaseObjectDto, columnDtos, extendedColumnPropertyDtos, constraintDtos);
    MapExtendedProperty(table, databaseObjectDto, extendedTablePropertyDtos);
    MapConstraint(table, databaseObjectDto, constraintDtos);
    MapForeignKeyConstraint(table, databaseObjectDto, foreignKeyDtos);
    MapTrigger(table, databaseObjectDto, triggerDtos);
    MapIndex(table, databaseObjectDto, indexDtos);
    return table;
  }
}

