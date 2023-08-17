using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages
{
  public abstract class DatabaseLanguageBase : LanguageBase
  {
    protected CodeLanguageBase _backendLanguage;
    protected DatabaseLanguageBase(StringBuilder sb, CodeLanguageBase backendLanguage, ProfileDto profile, string name) : base(sb, profile, name)
    {
      _backendLanguage = backendLanguage;
    }
    public abstract List<string> GetInternalMethodSignatures(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync, bool useNamespace = false);

    public abstract void BuildBeforeSaveMethod();
    public abstract void BuildAfterSaveMethod();
    public abstract List<string> GetClientImports();
    public abstract void BuildPrepareCommand(GeneratorBaseModel baseModel, bool async);

    public abstract void BuildInternalGetFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures);
    public abstract void BuildInternalSaveFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures);
    public abstract void BuildInternalDeleteFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures);
    public abstract void BuildInternalMergeFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures);
    public abstract void BuildInternalCountFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures);
    public abstract void BuildInternalHasChangedFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures);
    public abstract void BuildInternalHistFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures);

    public abstract void BuildGetMethod(GeneratorBaseModel baseModel, MethodType methodType,
      bool async, List<string> externalMethodSignatures, List<string> internalMethodSignatures);
    public abstract void BuildSaveMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures);
    public abstract void BuildDeleteMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures);
    public abstract void BuildMergeMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures);
    public abstract void BuildInternalCountMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildHasChangedMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures);
    public abstract void BuildHistGetMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures);
  }
}
