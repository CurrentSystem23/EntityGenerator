using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="Column"/> and all subnodes into a <see cref="TableOrView"/>.
  /// </summary>
  /// <param name="tableOrView">The given <see cref="TableOrView"/></param>
  /// <param name="parentObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="columnDtos">The given <see cref="ICollection&lt;ColumnDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  private static void MapColumn(
    TableOrView tableOrView,
    DatabaseObjectDto parentObjectDto,
    ICollection<ColumnDto> columnDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos,
    ICollection<ConstraintDto> constraintDtos
    )
  {
    if (parentObjectDto != null && columnDtos != null)
    {
      foreach (ColumnDto columnDto in columnDtos.Where(w =>
                 w.DatabaseName.Equals(parentObjectDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObjectDto.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.TableName.Equals(parentObjectDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        tableOrView.Columns.Add(MapColumn(columnDto, parentObjectDto, extendedColumnPropertyDtos, constraintDtos));
      }
    }
  }

  /// <summary>
  /// Maps a <see cref="Column"/> and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="columnDto">The given <see cref="ColumnDto"/></param>
  /// <param name="databaseObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  /// <param name="constraintDtos">The given <see cref="ICollection&lt;ConstraintDto&gt;"/></param>
  /// <returns>A <see cref="Column"/> with the mapped to core column structure.</returns>
  private static Column MapColumn(
    ColumnDto columnDto, 
    DatabaseObjectDto databaseObjectDto, 
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos, 
    ICollection<ConstraintDto> constraintDtos
    )
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

    MapExtendedProperty(column, databaseObjectDto, extendedColumnPropertyDtos);
    MapConstraint(column, databaseObjectDto, constraintDtos);
    return column;
  }

  /// <summary>
  /// Maps all <see cref="Column"/> and all subnodes into a <see cref="Function"/>.
  /// </summary>
  /// <param name="function">The given <see cref="Function"/></param>
  /// <param name="functionDto">The given <see cref="FunctionDto"/></param>
  /// <param name="tableValueFunctionsReturnValueDtos">The given <see cref="ICollection&lt;TableValueFunctionsReturnValueDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  private static void MapColumn(
    Function function,
    FunctionDto functionDto,
    ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValueDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos
    )
  {
    if (function != null && functionDto != null && tableValueFunctionsReturnValueDtos != null)
    {
      foreach (TableValueFunctionsReturnValueDto tableValueFunctionsReturnValueDto in tableValueFunctionsReturnValueDtos.Where(w =>
                 w.DatabaseName.Equals(functionDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(functionDto.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.FunctionName.Equals(functionDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        function.ReturnTable.Add(MapColumn(tableValueFunctionsReturnValueDto));
      }
    }
  }

  /// <summary>
  /// Maps a <see cref="Column"/> and all subnodes into a <see cref="TableValueFunction"/>.
  /// </summary>
  /// <param name="tableValueFunctionsReturnValueDto">The given <see cref="TableValueFunctionsReturnValueDto"/></param>
  /// <returns>A <see cref="Column"/> with the mapped to core column structure.</returns>
  private static Column MapColumn(TableValueFunctionsReturnValueDto tableValueFunctionsReturnValueDto)
  {
    Column column = new()
    {
      Name = tableValueFunctionsReturnValueDto.ObjectName,
      Id = tableValueFunctionsReturnValueDto.ObjectId,
      //ColumnTypeDataType = MapToCoreDataType(MapDatabaseColumnType(InformationExtractor.NormalizeTypeName(tableValueFunctionsReturnValueDto.ColumnTypeDataType)));
      ColumnTypeDataType = MapToCoreDataType(tableValueFunctionsReturnValueDto.ColumnTypeDataType),
      ColumnIsIdentity = tableValueFunctionsReturnValueDto.ColumnIsIdentity,
      ColumnIsNullable = tableValueFunctionsReturnValueDto.ColumnIsNullable,
      ColumnDefaultDefinition = tableValueFunctionsReturnValueDto.ColumnDefaultDefinition,
      ColumnIsComputed = tableValueFunctionsReturnValueDto.ColumnIsComputed,
      ColumnMaxLength = tableValueFunctionsReturnValueDto.ColumnMaxLength,
      ColumnCharacterOctetLength = null,
      ColumnNumericPrecision = tableValueFunctionsReturnValueDto.ColumnNumericPrecision,
      ColumnNumericPrecisionRadix = null,
      ColumnNumericScale = tableValueFunctionsReturnValueDto.ColumnNumericScale,
      ColumnDatetimePrecision = null,
    };

    return column;
  }
}

