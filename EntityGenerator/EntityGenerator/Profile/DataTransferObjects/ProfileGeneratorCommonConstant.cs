using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects
{
  [Serializable]
  public class ProfileGeneratorCommonConstant
  {
    public string TableName { get; set; }
    public string ColumnName { get; set; }
    public string IdColumn { get; set; } = "Id";
  }
}
