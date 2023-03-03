using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Database"/> is the representation of database model objects.
/// </summary>
[Serializable]
public class Database : BaseModel
{
  /// <summary>
  /// Get or set the schemas.
  /// </summary>
  public List<Schema> Schemas { get; } = new();

  /// <summary>
  /// Get or set the used database types.
  /// </summary>
  public List<DatabaseType> UsedDatabaseTypes { get; set; } = new();
}

