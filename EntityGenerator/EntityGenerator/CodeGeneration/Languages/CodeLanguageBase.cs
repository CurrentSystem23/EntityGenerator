using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages
{
  public abstract class CodeLanguageBase : LanguageBase
  {
    protected CodeLanguageBase(StringBuilder sb) : base(sb)
    {
    }

    public abstract List<string> GetMethodSignatures(ProfileDto profile, Schema schema, MethodType methodType, string name,
      bool isTable, bool async, string prefix, string parametersStr = null, string parametersWithTypeStr = null);
  }
}
