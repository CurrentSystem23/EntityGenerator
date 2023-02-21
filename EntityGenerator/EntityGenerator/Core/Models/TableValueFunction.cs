using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class TableValueFunction
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public List<Column> Parameters { get; } = new();
    public List<Column> ReturnTable { get; } = new();
    public List<ExtendedProperty> ExtendedProperties { get; } = new();

    public override string ToString()
    {
      return Name;
    }

  }
}
