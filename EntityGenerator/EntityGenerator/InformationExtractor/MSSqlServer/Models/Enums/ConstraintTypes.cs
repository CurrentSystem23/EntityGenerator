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
  [StringValue("Check constraint")]
  CheckConstraint,
  ///<summary>primary key SQL Server constraint</summary>
  [Description("Primary key")]
  [StringValue("Primary key")]
  PrimaryKeyConstraint,
  ///<summary>unique SQL Server constraint</summary>
  [Description("Unique constraint")]
  [StringValue("Unique constraint")]
  UniqueConstraint,
  ///<summary>unique clustered index SQL Server constraint</summary>
  [Description("Unique clustered index")]
  [StringValue("Unique clustered index")]
  UniqueClusteredIndex,
  ///<summary>unique index SQL Server constraint</summary>
  [Description("Unique index")]
  [StringValue("Unique index")]
  UniqueIndex,
  ///<summary>default SQL Server constraint</summary>
  [Description("Default constraint")]
  [StringValue("Default constraint")]
  DefaultConstraint,
  ///<summary>foreign key SQL Server constraint</summary>
  [Description("Foreign key constraint")]
  [StringValue("Foreign key constraint")]
  ForeignKeyConstraint,
}

