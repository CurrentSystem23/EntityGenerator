using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration
{
  public abstract class DatabaseLanguageBase : LanguageBase
  {
    protected CodeLanguageBase _backendLanguage;
    protected DatabaseLanguageBase(StringBuilder sb, CodeLanguageBase backendLanguage) : base(sb)
    {
      _backendLanguage = backendLanguage;
    }

    public abstract void BuildPrepareCommand(ProfileDto profile, Schema schema, string name, bool isTable, bool async, List<Column> parameters);

    /// <summary>
    /// Extract and map source SQL data type from column attribute value.
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public abstract string GetDataType(Column column);
  }
}
