using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="UserDefinedTableTypeColumnDto"/> is the transfer object class for user defined table type column information extraction from MS SqlServer.
/// </summary>
public class UserDefinedTableTypeColumnDto : BaseDto
{
  /// <summary>
  /// Get or set the database table name.
  /// </summary>
  public string TableName { get; set; }

  /// <summary>
  /// Get or set the database schema name.
  /// </summary>
  public string SchemaName { get; set; }

  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }

  /// <summary>
  /// Get or set the field.
  /// </summary>
  public string Field { get; set; }

  /// <summary>
  /// Get or set the type of the column as string.
  /// </summary>
  public string ColumnType { get; set; }

  /// <summary>
  /// Get or set the type of the column.
  /// </summary>
  public DataTypes ColumnTypeDataType { get; set; }

  /// <summary>
  /// Get or set the identity flag.
  /// </summary>
  public bool ColumnIsIdentity { get; set; }

  /// <summary>
  /// Get or set the nullable flag.
  /// </summary>
  public bool ColumnIsNullable { get; set; }

  /// <summary>
  /// Get or set the column default definition.
  /// </summary>
  public string ColumnDefaultDefinition { get; set; }

  /// <summary>
  /// Get or set the computed flag.
  /// </summary>
  public bool ColumnIsComputed { get; set; }

  /// <summary>
  /// Get or set the max length of the column.
  /// </summary>
  public short ColumnMaxLength { get; set; }
}
