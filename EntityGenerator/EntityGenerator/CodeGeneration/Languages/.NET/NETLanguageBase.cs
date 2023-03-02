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
    public abstract StringBuilder BuildClassHeader(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier);
    public abstract StringBuilder BuildInterfaceHeader(string interfaceName, string baseInterface, bool isPartial, AccessType accessModifier);

    public abstract StringBuilder BuildTraceLogCall(string message, string paramsStr, bool async);
  }
}
