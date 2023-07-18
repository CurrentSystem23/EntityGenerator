using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.SQL
{
  public abstract class SQLLanguageBase : DatabaseLanguageBase
  {
    public SQLLanguageBase(StringBuilder sb, CodeLanguageBase backendLanguage) : base(sb, backendLanguage)
    {
      ParameterFormat = "{1} AS {0}";
    }
  }
}
