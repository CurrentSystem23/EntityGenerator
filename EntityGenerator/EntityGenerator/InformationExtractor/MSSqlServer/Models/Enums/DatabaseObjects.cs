
using System.ComponentModel;
using EntityGenerator.Core.Attributes;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums
{
  public enum DatabaseObjects
  {
    [Description("Unknown")]
    [StringValue("Unknown")]
    Unknown,
    [Description("Database")]
    [StringValue("Database")]
    Database,
    [Description("Schema")]
    [StringValue("Schema")]
    Schema,
    [Description("Type")]
    [StringValue("Type")]
    Type,
    [Description("Column")]
    [StringValue("Column")]
    Column,
    [Description("ColumnExtendedProperty")]
    [StringValue("ColumnExtendedProperty")]
    ColumnExtendedProperty,
    [Description("TableExtendedProperty")]
    [StringValue("TableExtendedProperty")]
    TableExtendedProperty,
    [Description("UserDefinedTableTypeColumn")]
    [StringValue("UserDefinedTableTypeColumn")]
    UserDefinedTableTypeColumn,

    [Description("Aggregate function(CLR)")]
    [StringValue("Aggregate function(CLR)")]
    AF,
    [Description("CHECK Constraint")]
    [StringValue("CHECK Constraint")]
    C,
    [Description("Default or DEFAULT Constraint")]
    [StringValue("Default or DEFAULT Constraint")]
    D,
    [Description("FOREIGN KEY Constraint")]
    [StringValue("FOREIGN KEY Constraint")]
    F,
    [Description("Scalar Function")]
    [StringValue("Scalar Function")]
    FN,
    [Description("Assembly (CLR) Scalar-Function")]
    [StringValue("Assembly (CLR) Scalar-Function")]
    FS,
    [Description("Assembly (CLR) Table-Valued Function")]
    [StringValue("Assembly (CLR) Table-Valued Function")]
    FT,
    [Description("In-lined Table Function")]
    [StringValue("In-lined Table Function")]
    IF,
    [Description("Internal Table")]
    [StringValue("Internal Table")]
    IT,
    [Description("Log")]
    [StringValue("Log")]
    L,
    [Description("Stored Procedure")]
    [StringValue("Stored Procedure")]
    P,
    [Description("Assembly (CLR) Stored Procedure")]
    [StringValue("Assembly (CLR) Stored Procedure")]
    PC,
    [Description("PRIMARY KEY Constraint (Type is K)")]
    [StringValue("PRIMARY KEY Constraint (Type is K)")]
    PK,
    [Description("Replication Filter Stored Procedure")]
    [StringValue("Replication Filter Stored Procedure")]
    RF,
    [Description("System Table")]
    [StringValue("System Table")]
    S,
    [Description("Synonym")]
    [StringValue("Synonym")]
    SN,
    [Description("Service Queue")]
    [StringValue("Service Queue")]
    SQ,
    [Description("Assembly (CLR) Trigger")]
    [StringValue("Assembly (CLR) Trigger")]
    TA,
    [Description("Table Function")]
    [StringValue("Table Function")]
    TF,
    [Description("SQL Trigger")]
    [StringValue("SQL Trigger")]
    TR,
    [Description("Table Type")]
    [StringValue("Table Type")]
    TT,
    [Description("User Table")]
    [StringValue("User Table")]
    U,
    [Description("UNIQUE Constraint")]
    [StringValue("UNIQUE Constraint")]
    UQ,
    [Description("View")]
    [StringValue("View")]
    V,
    [Description("Extended Stored Procedure")]
    [StringValue("Extended Stored Procedure")]
    X,

  }
}
