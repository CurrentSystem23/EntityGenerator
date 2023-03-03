using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;ForeignKeyDto&gt;"/> into a <see cref="Table"/>.
  /// </summary>
  /// <param name="table">The given <see cref="Table"/></param>
  /// <param name="parentObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="foreignKeyDtos">The given <see cref="ICollection&lt;ForeignKeyDto&gt;"/></param>
  private static void MapForeignKeyConstraint(
    Table table,
    DatabaseObjectDto parentObjectDto,
    ICollection<ForeignKeyDto> foreignKeyDtos
    )
  {
    if (parentObjectDto != null && foreignKeyDtos != null)
    {
      // nur über die Check Constraints gehen!
      foreach (ForeignKeyDto foreignKeyDto in foreignKeyDtos.Where(w =>
                 w.DatabaseName.Equals(parentObjectDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.FromSchemaName.Equals(parentObjectDto.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.FromTableName.Equals(parentObjectDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        table.ConstraintsForeignKey.Add(MapForeignKeyConstraint(foreignKeyDto));
      }
    }
  }

  /// <summary>
  /// Maps a <see cref="ForeignKeyDto"/> into a <see cref="ForeignKeyConstraint"/>.
  /// </summary>
  /// <param name="foreignKeyDto">The given <see cref="ForeignKeyDto"/></param>
  /// <returns>A <see cref="Database"/> with the core database structure.</returns>
  private static ForeignKeyConstraint MapForeignKeyConstraint(ForeignKeyDto foreignKeyDto)
  {
    ForeignKeyConstraint constraint = new()
    {
      Name = foreignKeyDto.ObjectName,
      Id = foreignKeyDto.ObjectId,
      SourceColumn = foreignKeyDto.FromColumnName,
      Schema = foreignKeyDto.ReferencedSchemaName,
      Table = foreignKeyDto.ReferencedTableName,
      Column = foreignKeyDto.ReferencedColumnName,
    };

    return constraint;
  }
}

