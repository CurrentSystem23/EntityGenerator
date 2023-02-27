using System;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class ForeignKeyConstraint
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public string SourceColumn { get; set; }

    public string Schema { get; set; }
    public string Table { get; set; }
    public string Column { get; set; }

    public override string ToString()
    {
      return Name;
    }

  }
}
