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
  [Description("Column extended property")]
  [StringValue("ColumnExtendedProperty")]
  ColumnExtendedProperty,
  ///<summary>table extended property SQL Server object</summary>
  [Description("Table extended property")]
  [StringValue("TableExtendedProperty")]
  TableExtendedProperty,
  ///<summary>uder defined table type column SQL Server object</summary>
  [Description("User defined table type column")]
  [StringValue("UserDefinedTableTypeColumn")]
  UserDefinedTableTypeColumn,
  ///<summary>CLR aggregate function SQL Server object</summary>
  [Description("Aggregate function (CLR)")]
  [StringValue("AggregateFunctionClr")]
  AF,
  ///<summary>check constraint SQL Server object</summary>
  [Description("Check Constraint")]
  [StringValue("CheckConstraint")]
  C,
  ///<summary>default constraint SQL Server object</summary>
  [Description("Default or DEFAULT Constraint")]
  [StringValue("DefaultOrDEFAULTConstraint")]
  D,
  ///<summary>foreign key constraint SQL Server object</summary>
  [Description("FOREIGN KEY Constraint")]
  [StringValue("ForeignKeyConstraint")]
  F,
  ///<summary>scalar function SQL Server object</summary>
  [Description("Scalar function")]
  [StringValue("ScalarFunction")]
  FN,
  ///<summary>CLR scalar function SQL Server object</summary>
  [Description("Assembly (CLR) Scalar-Function")]
  [StringValue("AssemblyClrScalarFunction")]
  FS,
  ///<summary>CLR table valued function SQL Server object</summary>
  [Description("Assembly (CLR) Table-Valued Function")]
  [StringValue("AssemblyClrTableValuedFunction")]
  FT,
  ///<summary>inline table function SQL Server object</summary>
  [Description("In-lined Table Function")]
  [StringValue("InLinedTableFunction")]
  IF,
  ///<summary>internal table SQL Server object</summary>
  [Description("Internal Table")]
  [StringValue("InternalTable")]
  IT,
  ///<summary>log SQL Server object</summary>
  [Description("Log")]
  [StringValue("Log")]
  L,
  ///<summary>stored procedure SQL Server object</summary>
  [Description("Stored Procedure")]
  [StringValue("StoredProcedure")]
  P,
  ///<summary>CLR stored procedure SQL Server object</summary>
  [Description("Assembly (CLR) Stored Procedure")]
  [StringValue("AssemblyClrStoredProcedure")]
  PC,
  ///<summary>primary key constraint SQL Server object</summary>
  [Description("PRIMARY KEY Constraint (Type is K)")]
  [StringValue("PrimaryKeyConstraint")]
  PK,
  ///<summary>replication filter stored procedure SQL Server object</summary>
  [Description("Replication Filter Stored Procedure")]
  [StringValue("ReplicationFilterStoredProcedure")]
  RF,
  ///<summary>system table SQL Server object</summary>
  [Description("System Table")]
  [StringValue("SystemTable")]
  S,
  ///<summary>synonym SQL Server object</summary>
  [Description("Synonym")]
  [StringValue("Synonym")]
  SN,
  ///<summary>service queue SQL Server object</summary>
  [Description("Service Queue")]
  [StringValue("ServiceQueue")]
  SQ,
  ///<summary>CLR trigger SQL Server object</summary>
  [Description("Assembly (CLR) Trigger")]
  [StringValue("AssemblyClrTrigger")]
  TA,
  ///<summary>table valued function SQL Server object</summary>
  [Description("Table Function")]
  [StringValue("TableFunction")]
  TF,
  ///<summary>sql trigger SQL Server object</summary>
  [Description("SQL Trigger")]
  [StringValue("SqlTrigger")]
  TR,
  ///<summary>table type SQL Server object</summary>
  [Description("Table Type")]
  [StringValue("TableType")]
  TT,
  ///<summary>user table SQL Server object</summary>
  [Description("User Table")]
  [StringValue("UserTable")]
  U,
  ///<summary>unique constraint SQL Server object</summary>
  [Description("UNIQUE Constraint")]
  [StringValue("UniqueConstraint")]
  UQ,
  ///<summary>view SQL Server object</summary>
  [Description("View")]
  [StringValue("View")]
  V,
  ///<summary>extende stored procedure SQL Server object</summary>
  [Description("Extended Stored Procedure")]
  [StringValue("ExtendedStoredProcedure")]
  X,

}
