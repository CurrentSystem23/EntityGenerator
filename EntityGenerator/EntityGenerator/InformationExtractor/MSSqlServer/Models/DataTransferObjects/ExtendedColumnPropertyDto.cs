namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="ExtendedColumnPropertyDto"/> is the transfer object class for extended column property information extraction from MS SqlServer.
/// </summary>
public class ExtendedColumnPropertyDto : BaseDto
{
  /// <summary>
  /// Get or set the column name.
  /// </summary>
  public string ColumnName { get; set; }

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
  /// Get or set the extended property value of the column.
  /// </summary>
  public string ExtendedPropertyValue { get; set; }

  /// <summary>
  /// Get or set the object minor id.
  /// </summary>
  public int ObjectMinorId { get; set; }
}
