using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System.Text;

namespace EntityGenerator.CodeGeneration.Languages.SQL
{
  public abstract class SQLLanguageBase : DatabaseLanguageBase
  {
    public SQLLanguageBase(StringBuilder sb, CodeLanguageBase backendLanguage, ProfileDto profile, string name) : base(sb, backendLanguage, profile, name)
    {
      ParameterFormat = "{1} AS {0}";
    }

    public abstract void BuildGetSqlStatement(GeneratorBaseModel baseModel);
  }
}
