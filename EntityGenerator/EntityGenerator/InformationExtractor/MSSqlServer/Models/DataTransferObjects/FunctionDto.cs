namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="FunctionDto"/> is the transfer object class for function information extraction from MS SqlServer.
/// </summary>
public class FunctionDto : BaseDto
{
  /// <summary>
  /// Get or set the database schema name.
  /// </summary>
  public string SchemaName { get; set; }

  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }

  /// <summary>
  /// Get or set the object type name.
  /// </summary>
  public string ObjectTypeName { get; set; }

  /// <summary>
  /// Get or set the function parameters.
  /// </summary>
  public string FunctionParameters { get; set; }

  /// <summary>
  /// Get or set the function return type.
  /// </summary>
  public string FunctionReturnType { get; set; }

  /// <summary>
  /// Get or set the function definition.
  /// </summary>
  public string FunctionDefinition { get; set; }
}
