using EntityGenerator.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Core.Models.ModelObjects
{
  public class ConstantTable : BaseModel
  {
    public Column ConstantColumn { get; set; }

    public Dictionary<string, string> Constants { get; set; }
  }
}
