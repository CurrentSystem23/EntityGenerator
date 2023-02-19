using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Column
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public List<ExtendedProperty> ExtendedProperties { get; } = new ();
    public List<ForeignKeyConstraint> ForeignKeyConstraints { get; } = new ();

  }
}
