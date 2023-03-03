using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="TableValueFunctionsReturnValueDto"/> is the transfer object class for table valued function return value information extraction from MS SqlServer.
/// </summary>
public class TableValueFunctionsReturnValueDto : BaseDto
{
  /// <summary>
  /// Get or set the name of the function.
  /// </summary>
  public string FunctionName { get; set; }

  /// <summary>
  /// Get or set the database schema name.
  /// </summary>
  public string SchemaName { get; set; }

  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }

  /// <summary>
  /// Get or set the type of the column as string.
  /// </summary>
  public string ColumnType { get; set; }

  /// <summary>
  /// Get or set the type of the column.
  /// </summary>
  public DataTypes ColumnTypeDataType { get; set; }

  /// <summary>
  /// Get or set the identity flag of the column.
  /// </summary>
  public bool ColumnIsIdentity { get; set; }

  /// <summary>
  /// Get or set the nullable flag of the column.
  /// </summary>
  public bool ColumnIsNullable { get; set; }

  /// <summary>
  /// Get or set the default definition of the column.
  /// </summary>
  public string ColumnDefaultDefinition { get; set; }

  /// <summary>
  /// Get or set the computed flag of the column.
  /// </summary>
  public bool ColumnIsComputed { get; set; }

  /// <summary>
  /// Get or set the max length of the column.
  /// </summary>
  public short ColumnMaxLength { get; set; }

  /// <summary>
  /// Get or set the numeric precision of the column.
  /// </summary>
  public byte? ColumnNumericPrecision { get; set; }

  /// <summary>
  /// Get or set the numeric scale of the column.
  /// </summary>
  public byte? ColumnNumericScale { get; set; }
}
