using EntityGenerator.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Angular
{
  public abstract class AngularLanguageBase : LanguageBase
  {
    public override string GetDataType(DataTypes type)
    {
      throw new NotImplementedException();
    }
  }
}
