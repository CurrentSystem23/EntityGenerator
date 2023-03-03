using System;
using System.Collections.Generic;
using EntityGenerator.Core.Models.Enums;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Function"/> is the representation of function model objects.
/// </summary>
[Serializable]
public class Function : BaseModel
{
  /// <summary>
  /// Get or set the type of the function.
  /// </summary>
  public string FunctionType { get; set; }

  /// <summary>
  /// Get or set the parameters as a <see cref="List&lt;Column&gt;"/>.
  /// </summary>
  public List<Column> Parameters { get; } = new();

  /// <summary>
  /// Get or set the return type.
  /// </summary>
  public DataTypes ReturnType { get; set; }

  /// <summary>
  /// Get or set the return table as a <see cref="List&lt;Column&gt;"/>.
  /// </summary>
  public List<Column> ReturnTable { get; } = new();

  /// <summary>
  /// Get or set the definition.
  /// </summary>
  public string Definition { get; set; }
}

