namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="DatabaseObjectDto"/> is the transfer object class for database object information extraction from MS SqlServer.
/// </summary>
public class DatabaseObjectDto : BaseDto
{
  /// <summary>
  /// Get or set the database schema name.
  /// </summary>
  public string SchemaName { get; set; }

  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }
}
