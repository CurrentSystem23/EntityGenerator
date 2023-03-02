using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  public interface IFormatterService
  {
    int IndentSize { get; set; }

    void ApplyIndentation(StringBuilder stringBuilder);
  }
}
