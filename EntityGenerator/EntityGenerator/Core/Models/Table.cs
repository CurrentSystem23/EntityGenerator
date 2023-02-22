using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Table
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public List<Column> Columns { get; } = new ();
    public List<ExtendedProperty> ExtendedProperties { get; } = new();
    public List<Constraint> Constraints { get; } = new();
    public List<Index> Indexes { get; } = new();

    public override string ToString()
    {
      return Name;
    }

  }
}
