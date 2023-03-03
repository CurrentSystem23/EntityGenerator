using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="View"/> and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="schema">The given <see cref="Schema"/></param>
  /// <param name="schemaDto">The given <see cref="SchemaDto"/></param>
  /// <param name="databaseObjectDtos">The given <see cref="ICollection&lt;DatabaseObjectDto&gt;"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedTablePropertyDtos">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  private static void MapView(
    Schema schema,
    SchemaDto schemaDto,
    ICollection<DatabaseObjectDto> databaseObjectDtos,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedTablePropertyDto> extendedTablePropertyDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<ConstraintDto> constraintDtos
    )
  {
    if (schema != null && databaseObjectDtos != null)
    {
      foreach (DatabaseObjectDto databaseObjectDto in databaseObjectDtos.Where(w =>
                 w.DatabaseObject == Models.Enums.DatabaseObjects.V &&
                 w.DatabaseName.Equals(schemaDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(schemaDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        schema.Views.Add(MapView(databaseObjectDto, columnDtos, extendedTablePropertyDtos, extendedColumnPropertyDtos, constraintDtos));
      }
    }
  }

  /// <summary>
  /// Maps a <see cref="View"/> and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="databaseObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedTablePropertyDtos">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  private static View MapView(
    DatabaseObjectDto databaseObjectDto,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedTablePropertyDto> extendedTablePropertyDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<ConstraintDto> constraintDtos)
  {
    View view = new()
    {
      Name = databaseObjectDto.ObjectName,
      Id = databaseObjectDto.ObjectId
    };

    MapColumn(view, databaseObjectDto, columnDtos, extendedColumnPropertyDtos, constraintDtos);
    MapExtendedProperty(view, databaseObjectDto, extendedTablePropertyDtos);

    return view;
  }
}

