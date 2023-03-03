using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Index"/> is the representation of index model objects.
/// </summary>
[Serializable]
public class Index : BaseModel
{
  /// <summary>
  /// Get or set the indexed columns as a <see cref="List&lt;Column&gt;"/>.
  /// </summary>
  public List<Column> ColumnsIndexed { get; } = new();

  /// <summary>
  /// Get or set the included columns as a <see cref="List&lt;Column&gt;"/>.
  /// </summary>
  public List<Column> ColumnsIncluded { get; } = new();

  /// <summary>
  /// Get or set the index type id.
  /// </summary>
  public int IndexTypeId { get; set; }

  /// <summary>
  /// Get or set the unique flag.
  /// </summary>
  public bool IsUnique { get; set; }

  /// <summary>
  /// Get or set the type of the index.
  /// </summary>
  public string ObjectType { get; set; }
}

