﻿using System;
using System.Collections.Generic;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Function
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public string FunctionType { get; set; }
    public List<Column> Parameters { get; } = new();
    public Column ReturnType { get; set; }
    public List<Column> ReturnTable { get; } = new();
    public string Definition { get; set; }

  }
}
