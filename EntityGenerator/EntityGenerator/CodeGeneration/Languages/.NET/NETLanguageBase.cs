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
    public NETLanguageBase(StringBuilder sb) : base(sb) { }

    protected abstract void BuildClassHeader(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier);
    protected abstract void BuildInterfaceHeader(string interfaceName, string baseInterface, bool isPartial, AccessType accessModifier);
    protected abstract void BuildTraceLogCall(string message, string paramsStr, bool async);
  }
}
