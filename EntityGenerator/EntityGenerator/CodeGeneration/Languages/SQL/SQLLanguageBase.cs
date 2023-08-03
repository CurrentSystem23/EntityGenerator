using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.SQL
{
  public abstract class SQLLanguageBase : DatabaseLanguageBase
  {
    public SQLLanguageBase(StringBuilder sb, CodeLanguageBase backendLanguage, ProfileDto profile) : base(sb, backendLanguage, profile)
    {
      ParameterFormat = "{1} AS {0}";
    }

    public abstract void BuildGetSqlStatement(Schema schema, string name, bool isTable, List<Column> parameters, List<Column> columns);
  }
}
