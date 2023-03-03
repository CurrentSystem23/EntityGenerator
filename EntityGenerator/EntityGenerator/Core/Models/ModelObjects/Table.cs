using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Table"/> is the representation of table model objects.
/// </summary>
[Serializable]
public class Table : TableOrView
{
  /// <summary>
  /// Get or set the check constraints as a <see cref="List&lt;Constraint&gt;"/>.
  /// </summary>
  public List<Constraint> ConstraintsCheck { get; } = new();

  /// <summary>
  /// Get or set the foreign key constraints as a <see cref="List&lt;ForeignKeyConstraint&gt;"/>.
  /// </summary>
  public List<ForeignKeyConstraint> ConstraintsForeignKey { get; } = new();

  /// <summary>
  /// Get or set the triggers as a <see cref="List&lt;Trigger&gt;"/>.
  /// </summary>
  public List<Trigger> Triggers { get; } = new();
}

