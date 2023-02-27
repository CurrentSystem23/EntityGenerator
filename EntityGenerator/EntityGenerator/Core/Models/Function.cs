﻿using System;
using System.Collections.Generic;
using EntityGenerator.Core.Models.Enums;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class Function
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public string FunctionType { get; set; }
    public List<Column> Parameters { get; } = new();
    public DataTypes ReturnType { get; set; }
    public List<Column> ReturnTable { get; } = new();
    public List<ExtendedProperty> ExtendedProperties { get; } = new();

    public string Definition { get; set; }

    public override string ToString()
    {
      return Name;
    }

  }
}
