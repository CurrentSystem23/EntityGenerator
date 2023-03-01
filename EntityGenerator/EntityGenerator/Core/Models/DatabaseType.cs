using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Core.Models
{
  public class DatabaseType
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public Enums.DatabaseObjects DatabaseObjects { get; set; }
  }
}
