using System;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="DatabaseType"/> is the representation of database type model objects.
/// </summary>
[Serializable]
public class DatabaseType : BaseModel
{
  /// <summary>
  /// Get or set the database objects.
  /// </summary>
  public Enums.DatabaseObjects DatabaseObjects { get; set; }
}

