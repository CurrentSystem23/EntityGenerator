using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET
{
  public abstract class NETLanguageBase : CodeLanguageBase
  { 
    protected List<DatabaseLanguageBase> _databaseLanguages;

    public NETLanguageBase(StringBuilder sb, List<DatabaseLanguageBase> databaseLanguages = null) : base(sb)
    {
      ParameterFormat = "{0} {1}";
      _databaseLanguages = databaseLanguages;
    }

    public abstract void OpenClass(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier);
    public abstract void OpenInterface(string interfaceName, string baseInterface, bool isPartial, AccessType accessModifier);
    public abstract void OpenEnum(string enumName, bool isPartial, AccessType accessModifier);
    public abstract void BuildTraceLogCall(string message, string paramsStr, bool async);
    public abstract void BuildErrorLogCall(string message, string paramsStr, bool async);
    public abstract void OpenMethod(string methodName, string returnType, AccessType accessModifier, bool isStatic);
    public abstract void OpenMethod(string fullMethodSignature);
    protected abstract void CloseNameSpace();
    public abstract void CloseStructure();
    public abstract void CloseMethod();
  }
}
