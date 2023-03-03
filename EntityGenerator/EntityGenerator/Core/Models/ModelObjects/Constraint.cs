using System;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Constraint"/> is the representation of constraint model objects.
/// </summary>
[Serializable]
public class Constraint : BaseModel
{
  /// <summary>
  /// Get or set the constraint type.
  /// </summary>
  public string Type { get; set; }

  /// <summary>
  /// Get or set the target schema.
  /// </summary>
  public string TargetSchema { get; set; }

  /// <summary>
  /// Get or set the target table.
  /// </summary>
  public string TargetTable { get; set; }

  /// <summary>
  /// Get or set the constraint definition.
  /// </summary>
  public string ConstraintDefinition { get; set; }
}

