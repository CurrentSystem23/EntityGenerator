using EntityGenerator.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Column
  {
    public string Name { get; set; }
    public long Id { get; set; }

    public DataTypes ColumnTypeDataType { get; set; }
    public bool ColumnIsIdentity { get; set; }
    public bool ColumnIsNullable { get; set; }
    public string ColumnDefaultDefinition { get; set; }
    public bool ColumnIsComputed { get; set; }
    public short ColumnMaxLength { get; set; }
    public int? ColumnCharacterOctetLength { get; set; }
    public byte? ColumnNumericPrecision { get; set; }
    public short? ColumnNumericPrecisionRadix { get; set; }
    public int? ColumnNumericScale { get; set; }
    public short? ColumnDatetimePrecision { get; set; }

    public List<ExtendedProperty> ExtendedProperties { get; } = new ();
    public List<Constraint> ConstraintsPrimary { get; } = new();
    public List<Constraint> ConstraintsUnique { get; } = new();
    public List<Constraint> ConstraintsUniqueClusteredIndex{ get; } = new();
    public List<Constraint> ConstraintsUniqueIndex { get; } = new();
    public List<Constraint> ConstraintsDefault { get; } = new();

    public List<ForeignKeyConstraint> ForeignKeyConstraints { get; } = new ();

    public override string ToString()
    {
      return Name;
    }
  }
}
