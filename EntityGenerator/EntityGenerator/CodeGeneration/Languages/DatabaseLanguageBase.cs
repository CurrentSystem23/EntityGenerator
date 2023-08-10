using EntityGenerator.CodeGeneration.Interfaces;
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
    protected DatabaseLanguageBase(StringBuilder sb, CodeLanguageBase backendLanguage, ProfileDto profile) : base(sb, profile)
    {
      _backendLanguage = backendLanguage;
    }
    public abstract List<string> GetInternalMethodSignatures(Schema schema, MethodType methodType, string name,
      DbObjectType dbObjectType, bool async, string parametersStr = null, string parametersWithTypeStr = null, bool useNamespace = false);

    public abstract void BuildBeforeSaveMethod();
    public abstract void BuildAfterSaveMethod();

    public abstract void BuildPrepareCommand(Schema schema, string name, DbObjectType dbObjectType, bool async, 
      List<Column> parameters);

    public abstract void BuildInternalGetFacadeMethod(Schema schema, MethodType methodType, string name, DbObjectType dbObjectType,
      bool async, List<string> internalMethodSignatures, List<Column> parameters);
    public abstract void BuildInternalSaveFacadeMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalDeleteFacadeMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalMergeFacadeMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalCountFacadeMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures, List<Column> parameters);
    public abstract void BuildInternalHasChangedFacadeMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalHistFacadeMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures);

    public abstract void BuildGetMethod(Schema schema, MethodType methodType, string name, DbObjectType dbObjectType,
      bool async, List<string> externalMethodSignatures, List<string> internalMethodSignatures, List<Column> parameters);
    public abstract void BuildSaveMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures, List<Column> columns);
    public abstract void BuildDeleteMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures);
    public abstract void BuildMergeMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures, List<Column> columns);
    public abstract void BuildInternalCountMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures, List<Column> parameters);
    public abstract void BuildHasChangedMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures, List<Column> columns);
    public abstract void BuildHistGetMethod(Schema schema, string name, DbObjectType dbObjectType, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures);
  }
}
