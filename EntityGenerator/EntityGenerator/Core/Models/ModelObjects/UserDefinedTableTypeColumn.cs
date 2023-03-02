using System;
using EntityGenerator.Core.Models.Enums;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="UserDefinedTableTypeColumn"/> is the representation of user defined table type column model objects.
/// </summary>
[Serializable]
public class UserDefinedTableTypeColumn : BaseModel
{
  /// <summary>
  /// Get or set the field.
  /// </summary>
  public string Field { get; set; }

  /// <summary>
  /// Get or set the column data type.
  /// </summary>
  public DataTypes ColumnTypeDataType { get; set; }

  /// <summary>
  /// Get or set the identity flag.
  /// </summary>
  public bool ColumnIsIdentity { get; set; }

  /// <summary>
  /// Get or set the nullable flag.
  /// </summary>
  public bool ColumnIsNullable { get; set; }

  /// <summary>
  /// Get or set the default definition.
  /// </summary>
  public string ColumnDefaultDefinition { get; set; }

  /// <summary>
  /// Get or set the computed flag.
  /// </summary>
  public bool ColumnIsComputed { get; set; }

  /// <summary>
  /// Get or set the column max length.
  /// </summary>
  public short ColumnMaxLength { get; set; }
}

