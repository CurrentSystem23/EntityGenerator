namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="BaseDto"/> is the abstract base class for transfer objects for information extraction from MS SqlServer.
/// </summary>
public abstract class BaseDto
{
  /// <summary>
  /// Get or set the database object name.
  /// </summary>
  public string ObjectName { get; set; }

  /// <summary>
  /// Get or set the database object id.
  /// </summary>
  public int ObjectId { get; set; }

  /// <summary>
  /// Get or set the database object type.
  /// </summary>
  public Enums.DatabaseObjects DatabaseObject { get; set; }

  /// <summary>
  /// Returns the name of the object.
  /// </summary>
  /// <returns>A <see cref="string"/> with the name of the object..</returns>
  public override string ToString()
  {
    return ObjectName;
  }
}

