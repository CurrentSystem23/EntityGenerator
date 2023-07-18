using EntityGenerator.Core.Attributes;
using EntityGenerator.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Column"/> is the representation of column model objects.
/// </summary>
[Serializable]
public class Column : BaseModel
{
  /// <summary>
  /// Get or set the data type.
  /// </summary>
  [DbType]
  public DataTypes ColumnTypeDataType { get; private set; }
  
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
  /// Get or set the max length.
  /// </summary>
  public short ColumnMaxLength { get; set; }

  /// <summary>
  /// Get or set the character ocetet length.
  /// </summary>
  public int? ColumnCharacterOctetLength { get; set; }

  /// <summary>
  /// Get or set the numeric precision.
  /// </summary>
  public byte? ColumnNumericPrecision { get; set; }

  /// <summary>
  /// Get or set the numeric precision radix.
  /// </summary>
  public short? ColumnNumericPrecisionRadix { get; set; }

  /// <summary>
  /// Get or set the numeric scale.
  /// </summary>
  public int? ColumnNumericScale { get; set; }

  /// <summary>
  /// Get or set the date time precision.
  /// </summary>
  public short? ColumnDatetimePrecision { get; set; }

  /// <summary>
  /// Get or set the extended properties.
  /// </summary>
  public List<ExtendedProperty> ExtendedProperties { get; } = new();

  /// <summary>
  /// Get or set the primary key constraints.
  /// </summary>
  public List<Constraint> ConstraintsPrimaryKey { get; } = new();

  /// <summary>
  /// Get or set the unique constraints.
  /// </summary>
  public List<Constraint> ConstraintsUnique { get; } = new();

  /// <summary>
  /// Get or set the unique clustered indexes.
  /// </summary>
  public List<Constraint> ConstraintsUniqueClusteredIndex { get; } = new();

  /// <summary>
  /// Get or set the unique indexes.
  /// </summary>
  public List<Constraint> ConstraintsUniqueIndex { get; } = new();

  /// <summary>
  /// Get or set the default constraints.
  /// </summary>
  public List<Constraint> ConstraintsDefault { get; } = new();

  /// <summary>
  /// Sets the column type data and its attribute to corresponding original SQL type.
  /// </summary>
  /// <param name="dataType"></param>
  /// <param name="sqlType"></param>
  public void SetColumnTypeData(DataTypes dataType, InformationExtractor.MSSqlServer.Models.Enums.DataTypes sqlType)
  {
    this.ColumnTypeDataType = dataType;

    PropertyInfo propertyInfo = typeof(Column).GetProperty(nameof(ColumnTypeDataType));
    DbType dbTypeAttribute = (DbType)propertyInfo.GetCustomAttribute(typeof(DbType));
    if (dbTypeAttribute != null)
    {
      dbTypeAttribute.Type = sqlType;
    }
  }

  public InformationExtractor.MSSqlServer.Models.Enums.DataTypes GetColumnSqlType()
  {
    PropertyInfo propertyInfo = typeof(DbType).GetProperty(nameof(ColumnTypeDataType));
    DbType attribute = (DbType)propertyInfo.GetCustomAttribute(typeof(DbType));

    return attribute.Type;
  }
}

