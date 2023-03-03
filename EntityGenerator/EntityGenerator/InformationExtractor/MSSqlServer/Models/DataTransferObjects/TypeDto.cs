namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="TypeDto"/> is the transfer object class for type information extraction from MS SqlServer.
/// </summary>
public class TypeDto : BaseDto
{
  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }
}
