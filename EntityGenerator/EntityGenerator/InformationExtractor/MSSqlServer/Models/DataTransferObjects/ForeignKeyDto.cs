namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="ForeignKeyDto"/> is the transfer object class for foreign key information extraction from MS SqlServer.
/// </summary>
public class ForeignKeyDto : BaseDto
{
  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }

  /// <summary>
  /// Get or set the database schema name.
  /// </summary>
  public string FromSchemaName { get; set; }

  /// <summary>
  /// Get or set the database table name.
  /// </summary>
  public string FromTableName { get; set; }

  /// <summary>
  /// Get or set the column name.
  /// </summary>
  public string FromColumnName { get; set; }

  /// <summary>
  /// Get or set the database referenced schema name.
  /// </summary>
  public string ReferencedSchemaName { get; set; }

  /// <summary>
  /// Get or set the database referenced table name.
  /// </summary>
  public string ReferencedTableName { get; set; }

  /// <summary>
  /// Get or set the database referenced column.
  /// </summary>
  public string ReferencedColumnName { get; set; }
}
