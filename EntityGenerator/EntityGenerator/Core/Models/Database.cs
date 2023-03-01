using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Database
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public List<Schema> Schemas { get; } = new ();
    public List<DatabaseType> UsedDatabaseTypes { get; set; } = new ();

    public override string ToString()
    {
      return Name;
    }

  }
}
