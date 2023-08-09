using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Angular
{
  public abstract class AngularLanguageBase : LanguageBase
  {
    public AngularLanguageBase(StringBuilder sb, ProfileDto profile) : base(sb, profile) { }

    public override string GetColumnDataType(Column column)
    {
      throw new NotImplementedException();
    }
  }
}
