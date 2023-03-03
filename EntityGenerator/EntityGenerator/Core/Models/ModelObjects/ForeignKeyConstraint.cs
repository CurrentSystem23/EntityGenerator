using System;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="ForeignKeyConstraint"/> is the representation of foreign key constraint model objects.
/// </summary>
[Serializable]
public class ForeignKeyConstraint : BaseModel
{
  /// <summary>
  /// Get or set the source column.
  /// </summary>
  public string SourceColumn { get; set; }

  /// <summary>
  /// Get or set the schema.
  /// </summary>
  public string Schema { get; set; }
  
  /// <summary>
  /// Get or set the table.
  /// </summary>
  public string Table { get; set; }

  /// <summary>
  /// Get or set the column.
  /// </summary>
  public string Column { get; set; }
}

