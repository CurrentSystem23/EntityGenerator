using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="TableValuedFunction"/> is the representation of table valued function model objects.
/// </summary>
[Serializable]
public class TableValuedFunction : BaseModel
{
  /// <summary>
  /// Get or set the parameters as a <see cref="List&lt;Column&gt;"/>.
  /// </summary>
  public List<Column> Parameters { get; } = new();

  /// <summary>
  /// Get or set the return table as a <see cref="List&lt;Column&gt;"/>.
  /// </summary>
  public List<Column> ReturnTable { get; } = new();

  /// <summary>
  /// Get or set the extended properties as a <see cref="List&lt;ExtendedProperty&gt;"/>.
  /// </summary>
  public List<ExtendedProperty> ExtendedProperties { get; } = new();
}

