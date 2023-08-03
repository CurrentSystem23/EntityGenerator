using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.SQL.MSSQL
{
  public abstract partial class MSSQL : SQLLanguageBase
  {
    public new string Name = "MSSQL";

    public MSSQL(StringBuilder sb, CodeLanguageBase backendLanguage, ProfileDto profile) : base(sb, backendLanguage, profile) { }

    public override string GetColumnDataType(Column column)
    {
      return column.GetColumnSqlType().ToString().ToLower();
    }

    protected string GetDataTypeTSqlCamelCase(Column column)
    {
      return column.GetColumnSqlType().ToString();
    }
  }
}
