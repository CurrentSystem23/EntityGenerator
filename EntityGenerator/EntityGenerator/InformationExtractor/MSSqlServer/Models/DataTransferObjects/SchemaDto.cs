namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="SchemaDto"/> is the transfer object class for schema information extraction from MS SqlServer.
/// </summary>
public class SchemaDto : BaseDto
{
  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }
}
