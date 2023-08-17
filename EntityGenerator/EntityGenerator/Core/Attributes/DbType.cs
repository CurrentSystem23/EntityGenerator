using EntityGenerator.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Core.Attributes
{
  [AttributeUsage(AttributeTargets.All)]
  public class DbType : Attribute
  {
    public InformationExtractor.MSSqlServer.Models.Enums.DataTypes Type { get; set; }
    public DbType(InformationExtractor.MSSqlServer.Models.Enums.DataTypes type)
    {
      Type = type;
    }
  }
}
