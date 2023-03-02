using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="Constraint"/> and all subnodes into a <see cref="Table"/>.
  /// </summary>
  /// <param name="table">The given <see cref="Table"/></param>
  /// <param name="parentObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  private static void MapConstraint(
    Table table,
    DatabaseObjectDto parentObjectDto,
    ICollection<ConstraintDto> constraintDtos
    )
  {
    if (parentObjectDto != null && constraintDtos != null)
    {
      // nur über die Check Constraints gehen!
      foreach (ConstraintDto constraintDto in constraintDtos.Where(w =>
                 w.DatabaseName.Equals(parentObjectDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObjectDto.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.TableName.Equals(parentObjectDto.ObjectName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.ConstraintTypeType == ConstraintTypes.CheckConstraint))
      {
        table.ConstraintsCheck.Add(MapConstraint(constraintDto));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="Constraint"/> and all subnodes into a <see cref="Column"/>.
  /// </summary>
  /// <param name="column">The given <see cref="Column"/></param>
  /// <param name="parentObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  private static void MapConstraint(
    Column column,
    DatabaseObjectDto parentObjectDto,
    ICollection<ConstraintDto> constraintDtos
  )
  {
    if (parentObjectDto != null && constraintDtos != null)
    {
      // nur über die Check Constraints gehen!
      foreach (ConstraintDto constraintDto in constraintDtos.Where(w =>
                 w.DatabaseName.Equals(parentObjectDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObjectDto.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.TableName.Equals(parentObjectDto.ObjectName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.Columns.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase)))
      {
        if (constraintDto.ConstraintTypeType == ConstraintTypes.DefaultConstraint)
        {
          column.ConstraintsDefault.Add(MapConstraint(constraintDto));
        }
        else if (constraintDto.ConstraintTypeType == ConstraintTypes.PrimaryKeyConstraint)
        {
          column.ConstraintsPrimaryKey.Add(MapConstraint(constraintDto));
        }
        else if (constraintDto.ConstraintTypeType == ConstraintTypes.UniqueConstraint)
        {
          column.ConstraintsUnique.Add(MapConstraint(constraintDto));
        }
        else if (constraintDto.ConstraintTypeType == ConstraintTypes.UniqueClusteredIndex)
        {
          column.ConstraintsUniqueClusteredIndex.Add(MapConstraint(constraintDto));
        }
        else if (constraintDto.ConstraintTypeType == ConstraintTypes.UniqueIndex)
        {
          column.ConstraintsUniqueIndex.Add(MapConstraint(constraintDto));
        }
      }
    }
  }

  /// <summary>
  /// Maps a <see cref="Constraint"/> and all subnodes.
  /// </summary>
  /// <param name="constraintDto">The given <see cref="ConstraintDto"/></param>
  /// <returns>A <see cref="Constraint"/> with the mapped to core constraint structure.</returns>
  private static Constraint MapConstraint(ConstraintDto constraintDto)
  {
    Constraint constraint = new()
    {
      Name = constraintDto.ObjectName,
      Id = constraintDto.ObjectId,
      Type = constraintDto.ConstraintType,
      TargetSchema = constraintDto.TargetSchema,
      TargetTable = constraintDto.TargetTable,
      ConstraintDefinition = constraintDto.ConstraintDefinition,
    };

    return constraint;
  }

}

