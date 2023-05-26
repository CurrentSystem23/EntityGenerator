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
    public NETLanguageBase(StringBuilder sb) : base(sb)
    {
      ParameterFormat = "{0} {1}";
    }

    protected abstract void OpenClass(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier);
    protected abstract void OpenInterface(string interfaceName, string baseInterface, bool isPartial, AccessType accessModifier);
    protected abstract void BuildTraceLogCall(string message, string paramsStr, bool async);
    protected abstract void OpenMethod(string methodName, string returnType, AccessType accessModifier, bool isStatic);
    protected abstract void OpenMethod(string fullMethodSignature);
    protected abstract void CloseMethod();
  }
}
