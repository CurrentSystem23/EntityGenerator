using EntityGenerator.Core.Attributes;
using System.ComponentModel;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

/// <summary>
/// SQL Server object types
/// </summary>
public enum DatabaseObjects
{
  ///<summary>unknown SQL Server object</summary>
  [Description("Unknown")]
  [StringValue("Unknown")]
  Unknown,
  ///<summary>database SQL Server object</summary>
  [Description("Database")]
  [StringValue("Database")]
  Database,
  ///<summary>schema SQL Server object</summary>
  [Description("Schema")]
  [StringValue("Schema")]
  Schema,
  ///<summary>type SQL Server object</summary>
  [Description("Type")]
  [StringValue("Type")]
  Type,
  ///<summary>column SQL Server object</summary>
  [Description("Column")]
  [StringValue("Column")]
  Column,
  ///<summary>column extended property SQL Server object</summary>
  [Description("ColumnExtendedProperty")]
  [StringValue("ColumnExtendedProperty")]
  ColumnExtendedProperty,
  ///<summary>table extended property SQL Server object</summary>
  [Description("TableExtendedProperty")]
  [StringValue("TableExtendedProperty")]
  TableExtendedProperty,
  ///<summary>uder defined table type column SQL Server object</summary>
  [Description("UserDefinedTableTypeColumn")]
  [StringValue("UserDefinedTableTypeColumn")]
  UserDefinedTableTypeColumn,
  ///<summary>CLR aggregate function SQL Server object</summary>
  [Description("Aggregate function (CLR)")]
  [StringValue("Aggregate function (CLR)")]
  AF,
  ///<summary>check constraint SQL Server object</summary>
  [Description("CHECK Constraint")]
  [StringValue("CHECK Constraint")]
  C,
  ///<summary>default constraint SQL Server object</summary>
  [Description("Default or DEFAULT Constraint")]
  [StringValue("Default or DEFAULT Constraint")]
  D,
  ///<summary>foreign key constraint SQL Server object</summary>
  [Description("FOREIGN KEY Constraint")]
  [StringValue("FOREIGN KEY Constraint")]
  F,
  ///<summary>scalar function SQL Server object</summary>
  [Description("Scalar Function")]
  [StringValue("Scalar Function")]
  FN,
  ///<summary>CLR scalar function SQL Server object</summary>
  [Description("Assembly (CLR) Scalar-Function")]
  [StringValue("Assembly (CLR) Scalar-Function")]
  FS,
  ///<summary>CLR table valued function SQL Server object</summary>
  [Description("Assembly (CLR) Table-Valued Function")]
  [StringValue("Assembly (CLR) Table-Valued Function")]
  FT,
  ///<summary>inline table function SQL Server object</summary>
  [Description("In-lined Table Function")]
  [StringValue("In-lined Table Function")]
  IF,
  ///<summary>internal table SQL Server object</summary>
  [Description("Internal Table")]
  [StringValue("Internal Table")]
  IT,
  ///<summary>log SQL Server object</summary>
  [Description("Log")]
  [StringValue("Log")]
  L,
  ///<summary>stored procedure SQL Server object</summary>
  [Description("Stored Procedure")]
  [StringValue("Stored Procedure")]
  P,
  ///<summary>CLR stored procedure SQL Server object</summary>
  [Description("Assembly (CLR) Stored Procedure")]
  [StringValue("Assembly (CLR) Stored Procedure")]
  PC,
  ///<summary>primary key constraint SQL Server object</summary>
  [Description("PRIMARY KEY Constraint (Type is K)")]
  [StringValue("PRIMARY KEY Constraint (Type is K)")]
  PK,
  ///<summary>replication filter stored procedure SQL Server object</summary>
  [Description("Replication Filter Stored Procedure")]
  [StringValue("Replication Filter Stored Procedure")]
  RF,
  ///<summary>system table SQL Server object</summary>
  [Description("System Table")]
  [StringValue("System Table")]
  S,
  ///<summary>synonym SQL Server object</summary>
  [Description("Synonym")]
  [StringValue("Synonym")]
  SN,
  ///<summary>service queue SQL Server object</summary>
  [Description("Service Queue")]
  [StringValue("Service Queue")]
  SQ,
  ///<summary>CLR trigger SQL Server object</summary>
  [Description("Assembly (CLR) Trigger")]
  [StringValue("Assembly (CLR) Trigger")]
  TA,
  ///<summary>table valued function SQL Server object</summary>
  [Description("Table Function")]
  [StringValue("Table Function")]
  TF,
  ///<summary>sql trigger SQL Server object</summary>
  [Description("SQL Trigger")]
  [StringValue("SQL Trigger")]
  TR,
  ///<summary>table type SQL Server object</summary>
  [Description("Table Type")]
  [StringValue("Table Type")]
  TT,
  ///<summary>user table SQL Server object</summary>
  [Description("User Table")]
  [StringValue("User Table")]
  U,
  ///<summary>unique constraint SQL Server object</summary>
  [Description("UNIQUE Constraint")]
  [StringValue("UNIQUE Constraint")]
  UQ,
  ///<summary>view SQL Server object</summary>
  [Description("View")]
  [StringValue("View")]
  V,
  ///<summary>extende stored procedure SQL Server object</summary>
  [Description("Extended Stored Procedure")]
  [StringValue("Extended Stored Procedure")]
  X,

}

