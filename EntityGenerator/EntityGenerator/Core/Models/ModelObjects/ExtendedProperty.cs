using System;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="ExtendedProperty"/> is the representation of extended property model objects.
/// </summary>
[Serializable]
public class ExtendedProperty : BaseModel
{
  /// <summary>
  /// Get or set the extended property minor id.
  /// </summary>
  public long MinorId { get; set; }

  /// <summary>
  /// Get or set the extended property value.
  /// </summary>
  public string Value { get; set; }
}

