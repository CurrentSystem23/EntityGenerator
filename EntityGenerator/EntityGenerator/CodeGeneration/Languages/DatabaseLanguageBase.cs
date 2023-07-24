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
    protected DatabaseLanguageBase(StringBuilder sb, CodeLanguageBase backendLanguage) : base(sb)
    {
      _backendLanguage = backendLanguage;
    }

    public abstract void BuildPrepareCommand(ProfileDto profile, Schema schema, string name, bool isTable, bool async, 
      List<Column> parameters);

    public abstract void BuildInternalGetMethods(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable,
      bool async, List<string> internalMethodSignatures, List<Column> parameters);
    public abstract void BuildInternalSaveMethod(ProfileDto profile, Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalDeleteMethod(ProfileDto profile, Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalMergeMethod(ProfileDto profile, Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalCountMethod(ProfileDto profile, Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures, List<Column> parameters);
    public abstract void BuildInternalHasChangedMethod(ProfileDto profile, Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures);
    public abstract void BuildInternalHistMethod(ProfileDto profile, Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures);
  }
}
