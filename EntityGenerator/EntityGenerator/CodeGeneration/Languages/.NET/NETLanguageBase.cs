using EntityGenerator.CodeGeneration.Languages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET
{
  public abstract class NETLanguageBase : LanguageBase
  {
    protected abstract StringBuilder BuildClass(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier);
    protected abstract StringBuilder BuildTraceLogCall(string message, string paramsStr, bool async);
  }
}
