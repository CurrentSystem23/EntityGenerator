using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using EntityGenerator.Core.Models.Enums;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;TableValueFunctionsReturnValueDto&gt;"/> and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="schema">The given <see cref="Schema"/></param>
  /// <param name="schemaDto">The given <see cref="SchemaDto"/></param>
  /// <param name="functionDtos">The given <see cref="ICollection&lt;FunctionDto&gt;"/></param>
  /// <param name="tableValueFunctionsReturnValueDtos">The given <see cref="ICollection&lt;TableValueFunctionsReturnValueDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  private static void MapFunction(
    Schema schema,
    SchemaDto schemaDto,
    ICollection<FunctionDto> functionDtos,
    ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValueDtos,
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos
    )
  {
    if (schema != null && schemaDto != null && functionDtos != null)
    {
      foreach (FunctionDto functionDto in functionDtos.Where(w =>
                 w.DatabaseName.Equals(schemaDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(schemaDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        switch (functionDto.DatabaseObject)
        {
          case Models.Enums.DatabaseObjects.AF:
            // Aggregate function
            schema.FunctionsAggregate.Add(MapFunction(functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos));
            break;
          case Models.Enums.DatabaseObjects.FN:
            // SQL scalar function
            schema.FunctionsSqlScalar.Add(MapFunction(functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos));
            break;
          case Models.Enums.DatabaseObjects.TF:
            // SQL inline table-valued function
            schema.FunctionsSqlInlineTableValued.Add(MapFunction(functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos));
            break;
          case Models.Enums.DatabaseObjects.IF:
            // SQL table-valued-function
            schema.FunctionsSqlTableValued.Add(MapFunction(functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos));
            break;
          case Models.Enums.DatabaseObjects.FS:
            // Assembly (CLR) Scalar-Function
            schema.FunctionsClrScalar.Add(MapFunction(functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos));
            break;
          case Models.Enums.DatabaseObjects.FT:
            // Assembly (CLR) Table-Valued Function
            schema.FunctionsClrTableValued.Add(MapFunction(functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos));
            break;
          case Models.Enums.DatabaseObjects.P:
            // Stored Procedure
            schema.FunctionsClrTableValued.Add(MapFunction(functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos));
            break;
        }
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="FunctionDto"/> and all subnodes into a <see cref="Function"/>.
  /// </summary>
  /// <param name="functionDto">The given <see cref="FunctionDto"/></param>
  /// <param name="tableValueFunctionsReturnValueDtos">The given <see cref="ICollection&lt;TableValueFunctionsReturnValueDto&gt;"/></param>
  /// <param name="extendedColumnPropertyDtos">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  private static Function MapFunction(
    FunctionDto functionDto, 
    ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValueDtos, 
    ICollection<ExtendedColumnPropertyDto> extendedColumnPropertyDtos
    )
  {
    Function function = new()
    {
      Name = functionDto.ObjectName,
      Id = functionDto.ObjectId,
      FunctionType = functionDto.DatabaseObject.ToString(),
      Definition = functionDto.FunctionDefinition,
    };

    if (functionDto.FunctionParameters != null)
    {
      string[] parameters = functionDto.FunctionParameters.Split(',');
      foreach (string parameter in parameters)
      {
        function.Parameters.Add(ExtractParameterColumn(parameter));
      }
    }

    switch (functionDto.DatabaseObject)
    {
      case Models.Enums.DatabaseObjects.AF:
      case Models.Enums.DatabaseObjects.FN:
      case Models.Enums.DatabaseObjects.FS:
        // Aggregate function
        // SQL scalar function
        // Assembly (CLR) Scalar-Function
        function.ReturnType = MapToCoreDataType(MapDatabaseColumnType(InformationExtractor.InformationExtractor.NormalizeTypeName(functionDto.FunctionReturnType)));
        break;
      case Models.Enums.DatabaseObjects.TF:
      case Models.Enums.DatabaseObjects.IF:
      case Models.Enums.DatabaseObjects.FT:
        // SQL inline table-valued function
        // SQL table-valued-function
        // Assembly (CLR) Table-Valued Function
        MapColumn(function, functionDto, tableValueFunctionsReturnValueDtos, extendedColumnPropertyDtos);
        break;
    }

    return function;
  }

  /// <summary>
  /// Maps a <see cref="string"/> with parameter information into a <see cref="Column"/>.
  /// </summary>
  /// <param name="parameter">The given <see cref="string"/></param>
  /// <returns>A <see cref="Column"/> with the parameter column structure.</returns>
  private static Column ExtractParameterColumn(string parameter)
  {
    string[] parameters = parameter.Trim().Split(" ");
    parameters[0] = parameters[0].Substring(1).Trim();
    parameters[1] = parameters[1].Trim();
    Models.Enums.DataTypes sqlType = MapDatabaseColumnType(InformationExtractor.InformationExtractor.NormalizeTypeName(parameters[1]));

    Column column = new()
    {
      Name = parameters[0],
      Id = -1,
      ColumnIsIdentity = false,
      ColumnIsNullable = true,
      ColumnDefaultDefinition = null,
      ColumnIsComputed = false,
      ColumnMaxLength = 0,
      ColumnCharacterOctetLength = null,
      ColumnNumericPrecision = null,
      ColumnNumericPrecisionRadix = null,
      ColumnNumericScale = null,
      ColumnDatetimePrecision = null,
    };

    column.SetColumnTypeData(MapToCoreDataType(sqlType), sqlType);

    return column;
  }

  /// <summary>
  /// Maps a <see cref="string"/> into a <see cref="Models.Enums.DataTypes"/>.
  /// </summary>
  /// <param name="typeName">The given <see cref="string"/></param>
  /// <returns>A <see cref="Models.Enums.DataTypes"/>.</returns>
  private static Models.Enums.DataTypes MapDatabaseColumnType(string typeName)
  {
    if (Enum.TryParse(typeName, out Models.Enums.DataTypes databaseObjectType))
      return databaseObjectType;

    return Models.Enums.DataTypes.Unknown;
  }
}

