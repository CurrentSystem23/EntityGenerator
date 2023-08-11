using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  public enum MethodType
  {
    GET,
    SAVE,
    DELETE,
    MERGE,
    COUNT,
    HAS_CHANGED,
    BUlK_INSERT,
    BULK_MERGE,
    BULK_UPDATE,
    HIST_GET,
    EXECUTE
  }
}
