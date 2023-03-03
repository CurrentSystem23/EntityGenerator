namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="BaseModel"/> is the abstract base class for core model objects.
/// </summary>
public abstract class BaseModel
{
  /// <summary>
  /// Get or set the object name.
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// Get or set the object id.
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// Returns the name of the object.
  /// </summary>
  /// <returns>A <see cref="string"/> with the name of the object..</returns>
  public override string ToString()
  {
    return Name;
  }
}

