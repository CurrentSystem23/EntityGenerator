using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Constraint
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public string Type { get; set; }
    public List<Column> Columns { get; set; } = new ();
    public string TargetSchema { get; set; }
    public string TargetTable { get; set; }
    public string ConstraintDefinition { get; set; }

    public override string ToString()
    {
      return Name;
    }

  }
}
