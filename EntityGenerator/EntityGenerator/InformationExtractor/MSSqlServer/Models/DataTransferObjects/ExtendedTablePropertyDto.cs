namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="ExtendedTablePropertyDto"/> is the transfer object class for extended table property information extraction from MS SqlServer.
/// </summary>
public class ExtendedTablePropertyDto : BaseDto
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
  /// Get or set the extended property value of the table, view or function.
  /// </summary>
  public string ExtendedPropertyValue { get; set; }
}
