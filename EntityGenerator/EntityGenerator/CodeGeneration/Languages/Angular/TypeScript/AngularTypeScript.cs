using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Angular.TypeScript
{
  public abstract partial class AngularTypeScript : AngularLanguageBase
  {
    public AngularTypeScript(StringBuilder sb, ProfileDto profile) : base(sb, profile) { }
  }
}
