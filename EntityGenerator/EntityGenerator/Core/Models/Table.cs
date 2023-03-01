using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Table : TableOrView
  {
    public List<Constraint> ConstraintsCheck { get; } = new();
    public List<ForeignKeyConstraint> ConstraintsForeignKey { get; } = new();
    public List<Trigger> Triggers { get; } = new();
  }
}
