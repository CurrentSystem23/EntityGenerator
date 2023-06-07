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
  public abstract class NETLanguageBase : LanguageBase
  {
    public NETLanguageBase(StringBuilder sb) : base(sb)
    {
      ParameterFormat = "{0} {1}";
    }

    protected abstract void OpenClass(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier);
    protected abstract void OpenInterface(string interfaceName, string baseInterface, bool isPartial, AccessType accessModifier);
    //protected abstract void OpenInterface(string interfaceName, StructureOptions options);
    protected abstract void BuildTraceLogCall(string message, string paramsStr, bool async);
    protected abstract void OpenMethod(string methodName, string returnType, AccessType accessModifier, bool isStatic);
    protected abstract void OpenMethod(string fullMethodSignature);
    protected abstract void CloseNameSpace();
    protected abstract void CloseStructure();
    protected abstract void CloseMethod();
    protected abstract List<string> GetMethodSignatures(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string prefix, string parametersStr = null, string parametersWithTypeStr = null);
    protected abstract List<string> GetInternalMethodSignatures(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null, bool useNamespace = false);
  }
}
