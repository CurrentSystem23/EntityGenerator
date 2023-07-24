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
    protected DatabaseLanguageBase _databaseLanguage;

    public NETLanguageBase(StringBuilder sb, DatabaseLanguageBase? databaseLanguage) : base(sb)
    {
      ParameterFormat = "{0} {1}";
      _databaseLanguage = databaseLanguage;
    }

    public abstract void OpenClass(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier);
    public abstract void OpenInterface(string interfaceName, string baseInterface, bool isPartial, AccessType accessModifier);
    //protected abstract void OpenInterface(string interfaceName, StructureOptions options);
    public abstract void BuildTraceLogCall(string message, string paramsStr, bool async);
    public abstract void BuildErrorLogCall(string message, string paramsStr, bool async);
    public abstract void OpenMethod(string methodName, string returnType, AccessType accessModifier, bool isStatic);
    public abstract void OpenMethod(string fullMethodSignature);
    protected abstract void CloseNameSpace();
    public abstract void CloseStructure();
    public abstract void CloseMethod();
    public abstract List<string> GetMethodSignatures(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string prefix, string parametersStr = null, string parametersWithTypeStr = null);
    public abstract List<string> GetInternalMethodSignatures(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null, bool useNamespace = false);
  }
}
