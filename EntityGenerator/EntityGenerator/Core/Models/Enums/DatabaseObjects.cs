using EntityGenerator.Core.Attributes;
using System.ComponentModel;

namespace EntityGenerator.Core.Models.Enums;

/// <summary>
/// Entity Generator Database server core object types
/// </summary>
public enum DatabaseObjects
{
  ///<summary>unknown server object</summary>
  [Description("Unknown")]
  [StringValue("Unknown")]
  Unknown,
  ///<summary>database object</summary>
  [Description("Database")]
  [StringValue("Database")]
  Database,
  ///<summary>schema object</summary>
  [Description("Schema")]
  [StringValue("Schema")]
  Schema,
  ///<summary>type object</summary>
  [Description("Type")]
  [StringValue("Type")]
  Type,
  ///<summary>column object</summary>
  [Description("Column")]
  [StringValue("Column")]
  Column,
  ///<summary>column extended property object</summary>
  [Description("Column Extended Property")]
  [StringValue("ColumnExtendedProperty")]
  ColumnExtendedProperty,
  ///<summary>table extended property object</summary>
  [Description("Table Extended Property")]
  [StringValue("TableExtendedProperty")]
  TableExtendedProperty,
  ///<summary>uder defined table type column object</summary>
  [Description("User Defined Table Type Column")]
  [StringValue("UserDefinedTableTypeColumn")]
  UserDefinedTableTypeColumn,
  ///<summary>CLR aggregate function object</summary>
  [Description("Aggregate function(CLR)")]
  [StringValue("AggregateFunctionClr")]
  AF,
  ///<summary>check constraint object</summary>
  [Description("CHECK Constraint")]
  [StringValue("CheckConstraint")]
  C,
  ///<summary>default constraint object</summary>
  [Description("Default or DEFAULT Constraint")]
  [StringValue("DefaultOrDEFAULTConstraint")]
  D,
  ///<summary>foreign key constraint object</summary>
  [Description("FOREIGN KEY Constraint")]
  [StringValue("ForeignKeyConstraint")]
  F,
  ///<summary>scalar function object</summary>
  [Description("Scalar Function")]
  [StringValue("ScalarFunction")]
  FN,
  ///<summary>CLR scalar function object</summary>
  [Description("Assembly (CLR) Scalar-Function")]
  [StringValue("AssemblyClrScalarFunction")]
  FS,
  ///<summary>CLR table valued function object</summary>
  [Description("Assembly (CLR) Table-Valued Function")]
  [StringValue("AssemblyClrTableValuedFunction")]
  FT,
  ///<summary>inline table function object</summary>
  [Description("In-line Table Function")]
  [StringValue("InLineTableFunction")]
  IF,
  ///<summary>internal table object</summary>
  [Description("Internal Table")]
  [StringValue("InternalTable")]
  IT,
  ///<summary>log object</summary>
  [Description("Log")]
  [StringValue("Log")]
  L,
  ///<summary>stored procedure object</summary>
  [Description("Stored Procedure")]
  [StringValue("StoredProcedure")]
  P,
  ///<summary>CLR stored procedure object</summary>
  [Description("Assembly (CLR) Stored Procedure")]
  [StringValue("AssemblyClrStoredProcedure")]
  PC,
  ///<summary>primary key constraint object</summary>
  [Description("PRIMARY KEY Constraint (Type is K)")]
  [StringValue("PrimaryKeyConstraint")]
  PK,
  ///<summary>replication filter stored procedure object</summary>
  [Description("Replication Filter Stored Procedure")]
  [StringValue("ReplicationFilterStoredProcedure")]
  RF,
  ///<summary>system table object</summary>
  [Description("System Table")]
  [StringValue("SystemTable")]
  S,
  ///<summary>synonym object</summary>
  [Description("Synonym")]
  [StringValue("Synonym")]
  SN,
  ///<summary>service queue object</summary>
  [Description("Service Queue")]
  [StringValue("ServiceQueue")]
  SQ,
  ///<summary>CLR trigger object</summary>
  [Description("Assembly (CLR) Trigger")]
  [StringValue("AssemblyClrTrigger")]
  TA,
  ///<summary>table valued function object</summary>
  [Description("Table Function")]
  [StringValue("TableFunction")]
  TF,
  ///<summary>sql trigger object</summary>
  [Description("SQL Trigger")]
  [StringValue("SqlTrigger")]
  TR,
  ///<summary>table type object</summary>
  [Description("Table Type")]
  [StringValue("TableType")]
  TT,
  ///<summary>user table object</summary>
  [Description("User Table")]
  [StringValue("UserTable")]
  U,
  ///<summary>unique constraint object</summary>
  [Description("UNIQUE Constraint")]
  [StringValue("UniqueConstraint")]
  UQ,
  ///<summary>view object</summary>
  [Description("View")]
  [StringValue("View")]
  V,
  ///<summary>extende stored procedure object</summary>
  [Description("Extended Stored Procedure")]
  [StringValue("ExtendedStoredProcedure")]
  X,

}

