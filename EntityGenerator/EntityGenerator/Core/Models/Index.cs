using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Index
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public List<Column> Columns { get; }= new ();

    public int IndexTypeId { get; set; }
    public bool IsUnique { get; set; }

    public string ObjectType { get; set; }
  }
}
