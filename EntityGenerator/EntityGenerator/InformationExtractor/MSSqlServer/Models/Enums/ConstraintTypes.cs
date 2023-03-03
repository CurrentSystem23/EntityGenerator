using EntityGenerator.Core.Attributes;
using System.ComponentModel;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

/// <summary>
/// SQL Server constraint types
/// </summary>
public enum ConstraintTypes
{
  ///<summary>unknown SQL Server constraint</summary>
  [Description("Unknown")]
  [StringValue("Unknown")]
  Unknown,
  ///<summary>check SQL Server constraint</summary>
  [Description("Check constraint")]
  [StringValue("CheckConstraint")]
  CheckConstraint,
  ///<summary>primary key SQL Server constraint</summary>
  [Description("Primary key")]
  [StringValue("PrimaryKey")]
  PrimaryKeyConstraint,
  ///<summary>unique SQL Server constraint</summary>
  [Description("Unique constraint")]
  [StringValue("UniqueConstraint")]
  UniqueConstraint,
  ///<summary>unique clustered index SQL Server constraint</summary>
  [Description("Unique clustered index")]
  [StringValue("UniqueClusteredIndex")]
  UniqueClusteredIndex,
  ///<summary>unique index SQL Server constraint</summary>
  [Description("Unique index")]
  [StringValue("UniqueIndex")]
  UniqueIndex,
  ///<summary>default SQL Server constraint</summary>
  [Description("Default constraint")]
  [StringValue("DefaultConstraint")]
  DefaultConstraint,
  ///<summary>foreign key SQL Server constraint</summary>
  [Description("Foreign key constraint")]
  [StringValue("ForeignKeyConstraint")]
  ForeignKeyConstraint,
}