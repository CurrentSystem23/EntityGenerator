using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Schema
  {
    public string Name { get; set; }
    public long Id { get; set; }

    public List<Table> Tables { get; } = new();
    public List<View> Views { get; } = new();
    public List<Function> FunctionsAggregate { get; } = new();
    public List<Function> FunctionsSqlScalar { get; } = new();
    public List<Function> FunctionsSqlInlineTableValued { get; } = new();
    public List<Function> FunctionsSqlTableValued { get; } = new();
    public List<Function> FunctionsClrScalar { get; } = new();
    public List<Function> FunctionsClrTableValued { get; } = new();
    public List<Function> FunctionsStoredProcedure { get; } = new();
    public List<Trigger> Triggers { get; } = new();
    public List<UserDefinedTableTypeColumn> UserDefinedTableTypes { get; } = new();
    /*
         WHEN 'AF' THEN 'Aggregate function'
         WHEN 'FN' THEN 'SQL scalar function'
         WHEN 'TF' THEN 'SQL inline table-valued function'
         WHEN 'IF' THEN 'SQL table-valued-function'
         WHEN 'FS' THEN 'Assembly (CLR) Scalar-Function'
         WHEN 'FT' THEN 'Assembly (CLR) Table-Valued Function'
         WHEN 'P'  THEN 'Stored Procedure'
     */
    public override string ToString()
    {
      return Name;
    }

  }
}
