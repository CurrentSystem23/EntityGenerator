using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Schema
  {
    public string Name { get; set; }
    public long Id { get; set; }

    public List<Table> Tables { get; } = new();
    public List<View> Views { get; } = new();
    public List<Function> Functions { get; } = new();

  }
}
