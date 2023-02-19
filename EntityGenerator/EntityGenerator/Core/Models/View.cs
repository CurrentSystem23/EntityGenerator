using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class View
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public List<Column> Columns { get; } = new ();
    public List<ExtendedProperty> ExtendedProperties { get; } = new();
    public List<Constraint> Constraints { get; } = new();
    public List<Index> Indexes { get; } = new();
  }
}
