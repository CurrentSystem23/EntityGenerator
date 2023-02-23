using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  public interface ILinterService
  {
    void ApplyIndentation(ref StringBuilder stringBuilder);
  }
}
