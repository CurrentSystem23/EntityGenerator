using EntityGenerator.Core.Attributes;
using System.ComponentModel;

namespace EntityGenerator.Core.Models.Enums
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
    [Description("Column Extended Property")]
    [StringValue("ColumnExtendedProperty")]
    ColumnExtendedProperty,
    [Description("Table Extended Property")]
    [StringValue("TableExtendedProperty")]
    TableExtendedProperty,
    [Description("User Defined Table Type Column")]
    [StringValue("UserDefinedTableTypeColumn")]
    UserDefinedTableTypeColumn,

    [Description("Aggregate function(CLR)")]
    [StringValue("AggregateFunctionClr")]
    AF,
    [Description("CHECK Constraint")]
    [StringValue("CheckConstraint")]
    C,
    [Description("Default or DEFAULT Constraint")]
    [StringValue("DefaultOrDEFAULTConstraint")]
    D,
    [Description("FOREIGN KEY Constraint")]
    [StringValue("ForeignKeyConstraint")]
    F,
    [Description("Scalar Function")]
    [StringValue("ScalarFunction")]
    FN,
    [Description("Assembly (CLR) Scalar-Function")]
    [StringValue("AssemblyClrScalarFunction")]
    FS,
    [Description("Assembly (CLR) Table-Valued Function")]
    [StringValue("AssemblyClrTableValuedFunction")]
    FT,
    [Description("In-line Table Function")]
    [StringValue("InLineTableFunction")]
    IF,
    [Description("Internal Table")]
    [StringValue("InternalTable")]
    IT,
    [Description("Log")]
    [StringValue("Log")]
    L,
    [Description("Stored Procedure")]
    [StringValue("StoredProcedure")]
    P,
    [Description("Assembly (CLR) Stored Procedure")]
    [StringValue("AssemblyClrStoredProcedure")]
    PC,
    [Description("PRIMARY KEY Constraint (Type is K)")]
    [StringValue("PrimaryKeyConstraint")]
    PK,
    [Description("Replication Filter Stored Procedure")]
    [StringValue("ReplicationFilterStoredProcedure")]
    RF,
    [Description("System Table")]
    [StringValue("SystemTable")]
    S,
    [Description("Synonym")]
    [StringValue("Synonym")]
    SN,
    [Description("Service Queue")]
    [StringValue("ServiceQueue")]
    SQ,
    [Description("Assembly (CLR) Trigger")]
    [StringValue("AssemblyClrTrigger")]
    TA,
    [Description("Table Function")]
    [StringValue("TableFunction")]
    TF,
    [Description("SQL Trigger")]
    [StringValue("SqlTrigger")]
    TR,
    [Description("Table Type")]
    [StringValue("TableType")]
    TT,
    [Description("User Table")]
    [StringValue("UserTable")]
    U,
    [Description("UNIQUE Constraint")]
    [StringValue("UniqueConstraint")]
    UQ,
    [Description("View")]
    [StringValue("View")]
    V,
    [Description("Extended Stored Procedure")]
    [StringValue("ExtendedStoredProcedure")]
    X,

  }
}
