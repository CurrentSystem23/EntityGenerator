using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="TableOrView"/> is the abstract representation of base class for table and view model objects.
/// </summary>
[Serializable]
public abstract class TableOrView : BaseModel
{
  /// <summary>
  /// Get or set the columns as a <see cref="List&lt;Column&gt;"/>.
  /// </summary>
  public List<Column> Columns { get; } = new();

  /// <summary>
  /// Get or set the extended properties as a <see cref="List&lt;ExtendedProperty&gt;"/>.
  /// </summary>
  public List<ExtendedProperty> ExtendedProperties { get; } = new();

  /// <summary>
  /// Get or set the indexes as a <see cref="List&lt;Index&gt;"/>.
  /// </summary>
  public List<Index> Indexes { get; } = new();
}

