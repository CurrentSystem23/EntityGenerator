using System;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Trigger"/> is the representation of trigger model objects.
/// </summary>
[Serializable]
public class Trigger : BaseModel
{
  /// <summary>
  /// Get or set the Microsoft shipped flag.
  /// </summary>
  public bool IsMsShipped { get; set; }

  /// <summary>
  /// Get or set the disabled flag.
  /// </summary>
  public bool IsDisabled { get; set; }

  /// <summary>
  /// Get or set the not for replication flag.
  /// </summary>
  public bool IsNotForReplication { get; set; }

  /// <summary>
  /// Get or set the instead of trigger flag.
  /// </summary>
  public bool IsInsteadOfTrigger { get; set; }

  /// <summary>
  /// Get or set the trigger definition.
  /// </summary>
  public string TriggerDefinition { get; set; }
}

