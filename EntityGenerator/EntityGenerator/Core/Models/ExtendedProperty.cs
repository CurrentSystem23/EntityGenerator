using System;

namespace EntityGenerator.Core.Models
{
  [Serializable]
  public class ExtendedProperty
  {
    public string Name { get; set; }
    public long Id { get; set; }
    public long MinorId { get; set; }
    public long Value { get; set; }

    public override string ToString()
    {
      return Name;
    }

  }
}
