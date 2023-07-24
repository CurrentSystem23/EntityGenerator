using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Languages.SQL;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObject.Enums;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessSQLGenerator
  {
    void IDataAccessSQLGenerator.BuildBaseFileExtension(ProfileDto profile)
    {
      BuildImports(new List<string> { "System", "System.Collections.Generic", "Microsoft.Data.SqlClient", "System.Text", $"{profile.Global.ProjectName}.Common.DTOs" });
      BuildNameSpace($"{profile.Global.ProjectName}.DataAccess.SQL.BaseClasses");

      OpenClass("Dao", isPartial: true);

      // Before Save
      OpenMethod("virtual BeforeSave(SqlConnection connection, SqlCommand command, DtoBase dto)", "bool", Enums.AccessType.PUBLIC);
      _sb.AppendLine("return true;");

      // After Save
      OpenMethod("virtual AfterSave(SqlConnection connection, SqlCommand command, DtoBase dto)", "bool", Enums.AccessType.PUBLIC);
      _sb.AppendLine("return true;");
    }

    void IDataAccessSQLGenerator.BuildWhereParameterClass(ProfileDto profile)
    {
      BuildImports(new List<string> { "System", "System.Collections.Generic", "System.Data" });
      BuildNameSpace($"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses");

      // WhereParameters Class
      OpenClass("WhereParameters");
      _sb.AppendLine("public List<IWhereParameter> Parameters { get; }");
      OpenMethod("WhereParameters()", null);
      _sb.AppendLine($"Parameters = new List<IWhereParameter>();");

      // WhereClause Class
      OpenClass("WhereClause : WhereParameters");
      _sb.AppendLine("public string Where { get; set; }");
      OpenMethod(@"WhereClause() : base()", null);
      OpenMethod(@"WhereClause(string where) : this()", null);
      _sb.AppendLine($"Where = where;");

      // IWhereParameter Interface
      OpenInterface("IWhereParameter");
      _sb.AppendLine("string ParameterName { get; }");
      _sb.AppendLine("SqlDbType ParameterType { get; }");
      _sb.AppendLine("object ParameterValue { get; }");

      // IWhereParameterTyped<T> Interface
      OpenInterface("IWhereParameterTyped<T> : IWhereParameter");
      _sb.AppendLine("T ParameterValueTyped { get; }");

      // WhereBaseParameter<T> Class
      OpenClass("WhereBaseParameter<T> : IWhereParameterTyped<T>", null, false, false, true);
      _sb.AppendLine("public T ParameterValueTyped { get; }");
      _sb.AppendLine("public string ParameterName { get; }");
      _sb.AppendLine("public abstract SqlDbType ParameterType { get; }");
      _sb.AppendLine("public object ParameterValue => ParameterValueTyped;");
      OpenMethod("WhereBaseParameter(string parameterName, T parameterValue)", null);
      _sb.AppendLine("ParameterName = parameterName.StartsWith(\"@\") ? parameterName : $\"@{ parameterName}\";");
      _sb.AppendLine("ParameterValueTyped = parameterValue;");

      // WhereBoolParameter Class
      OpenClass("WhereBoolParameter", "WhereBaseParameter<bool>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Bit;");
      OpenMethod(@"WhereBoolParameter(string parameterName, bool parameterValue) : base(parameterName, parameterValue)", null);
      
      // WhereByteParameter Class
      OpenClass("WhereByteParameter : WhereBaseParameter<byte>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.TinyInt;");
      OpenMethod(@"WhereByteParameter(string parameterName, byte parameterValue) : base(parameterName, parameterValue)", null);

      // WhereShortParameter Class
      OpenClass("WhereShortParameter : WhereBaseParameter<short>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.SmallInt;");
      OpenMethod(@"WhereShortParameter(string parameterName, short parameterValue) : base(parameterName, parameterValue)", null);

      // WhereIntParameter Class
      OpenClass("WhereIntParameter : WhereBaseParameter<int>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Int;");
      OpenMethod(@"WhereIntParameter(string parameterName, int parameterValue) : base(parameterName, parameterValue)", null);

      // WhereLongParameter Class
      OpenClass("WhereLongParameter : WhereBaseParameter<long>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.BigInt;");
      OpenMethod(@"WhereLongParameter(string parameterName, long parameterValue) : base(parameterName, parameterValue)", null);

      // WhereFloatParameter Class
      OpenClass("WhereFloatParameter : WhereBaseParameter<float>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Real;");
      OpenMethod(@"WhereFloatParameter(string parameterName, float parameterValue) : base(parameterName, parameterValue)", null);
      
      // WhereDoubleParameter Class
      OpenClass("WhereDoubleParameter : WhereBaseParameter<double>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Float;");
      OpenMethod(@"WhereDoubleParameter(string parameterName, double parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDecimalParameter Class
      OpenClass("WhereDecimalParameter : WhereBaseParameter<decimal>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Decimal;");
      OpenMethod(@"WhereDecimalParameter(string parameterName, decimal parameterValue) : base(parameterName, parameterValue)", null);

      // WhereCharParameter Class
      OpenClass("WhereCharParameter : WhereBaseParameter<char>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Char;");
      OpenMethod(@"WhereCharParameter(string parameterName, char parameterValue) : base(parameterName, parameterValue)", null);

      // WhereNCharParameter Class
      OpenClass("WhereNCharParameter : WhereBaseParameter<char>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.NChar;");
      OpenMethod(@"WhereNCharParameter(string parameterName, char parameterValue) : base(parameterName, parameterValue)", null);

      // WhereVarcharParameter Class
      OpenClass("WhereVarcharParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.VarChar;");
      OpenMethod(@"WhereVarcharParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereNVarcharParameter Class
      OpenClass("WhereNVarcharParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.NVarChar;");
      OpenMethod(@"WhereNVarcharParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereTextParameter Class
      OpenClass("WhereTextParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Text;");
      OpenMethod(@"WhereTextParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereXmlParameter Class
      OpenClass("WhereXmlParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Xml;");
      OpenMethod(@"WhereXmlParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDataTimeParameter Class
      OpenClass("WhereDateTimeParameter : WhereBaseParameter<DateTime>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.DateTime;");
      OpenMethod(@"WhereDateTimeParameter(string parameterName, DateTime parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDateTime2Parameter Class
      OpenClass("WhereDateTime2Parameter : WhereBaseParameter<DateTime>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.DateTime2;");
      OpenMethod(@"WhereDateTime2Parameter(string parameterName, DateTime parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDateTimeOffsetParameter Class
      OpenClass("WhereDateTimeOffsetParameter : WhereBaseParameter<DateTimeOffset>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.DateTimeOffset;");
      OpenMethod(@"WhereDateTimeOffsetParameter(string parameterName, DateTimeOffset parameterValue): base(parameterName, parameterValue)", null);

      // WhereDateParameter Class
      OpenClass("WhereDateParameter : WhereBaseParameter<DateTime>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Date;");
      OpenMethod(@"WhereDateParameter(string parameterName, DateTime parameterValue) : base(parameterName, parameterValue)", null);

      // WhereVarBinaryParameter Class
      OpenClass("WhereVarBinaryParameter : WhereBaseParameter<byte[]>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.VarBinary;");
      OpenMethod(@"WhereVarBinaryParameter(string parameterName, byte[] parameterValue) : base(parameterName, parameterValue)", null);

      // WhereImageParameter Class
      OpenClass("WhereImageParameter : WhereBaseParameter<byte[]>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Image;");
      OpenMethod(@"WhereImageParameter(string parameterName, byte[] parameterValue) : base(parameterName, parameterValue)", null);

      // WhereUniqueIdentifierParameter Class
      OpenClass("WhereUniqueIdentifierParameter : WhereBaseParameter<Guid>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.UniqueIdentifier;");
      OpenMethod(@"WhereUniqueIdentifierParameter(string parameterName, Guid parameterValue) : base(parameterName, parameterValue)", null);
    }

    protected void BuildDAOClassHeader(ProfileDto profile, Schema schema, string name, bool isTable)
    {
      List<string> imports = new()
      {
        "System",
        "System.Collections.Generic",
        "System.Data",
        "System.Linq",
        "System.Threading.Tasks",
        "System.Transactions",
        "Microsoft.Data.SqlClient",
        "Microsoft.Extensions.Logging",
        $"{profile.Global.ProjectName}.Common",
        $"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
        $"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}",
        $"{profile.Global.ProjectName}.Common.DTOs.{schema.Name}",
        $"{profile.Global.ProjectName}.DataAccess.SQL.BaseClasses",
      };

      BuildImports(imports);
      BuildNameSpace($"{profile.Global.ProjectName}.DataAccess.SQL.{schema.Name}");

      // Create Dao class
      OpenClass(TypeHelper.GetDaoType(name, isTable), $"Dao, I{TypeHelper.GetDaoType(name, isTable)}, I{TypeHelper.GetInternalDaoType(name, isTable)}", false, true);

      // Add service members
      _sb.AppendLine($"private readonly ILogger<{TypeHelper.GetDaoType(name, isTable)}> _logger;");
      _sb.AppendLine($"private readonly IServiceProvider _provider;");

      // Constructor
      OpenMethod($"{TypeHelper.GetDaoType(name, isTable)}(IServiceProvider provider, ILogger<{TypeHelper.GetDaoType(name, isTable)}> logger = null", null);
      _sb.AppendLine($"_logger = logger;");
      _sb.AppendLine("_provider = provider;");
    }

    protected void BuildDataAccessSQLClassMethodBody(ProfileDto profile, Schema schema, MethodType methodType, string name, 
      bool isTable, bool async, List<string> externalMethodSignatures, List<string> internalMethodSignatures, 
      List<Column> parameters, List<Column> columns)
    {
      SQLLanguageBase _databaseLanguage = this._databaseLanguage as SQLLanguageBase;

      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";
      string parametersStr = ParameterHelper.GetParametersString(parameters);
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(parameters, this);
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(parameters);

      switch (methodType)
      {
        case MethodType.GET:
          _databaseLanguage.BuildPrepareCommand(profile, schema, name, isTable, async, parameters);
          _databaseLanguage.BuildGetSqlStatement(profile, schema, name, isTable, parameters, columns);

          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return{(async ? " await" : "")} {name}Gets{(async ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return{(async ? " await" : "")} {name}Gets{(async ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

          if (isTable)
          {
            // Get
            OpenMethod(externalMethodSignatures.ElementAt(2));
            _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
            _sb.AppendLine("{");
            _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
            _sb.AppendLine("{");
            _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
            _sb.AppendLine($"var ret = {(async ? "await " : "")}{name}Get{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
            _sb.AppendLine("con.Close();");
            _sb.AppendLine("return ret;");
            _sb.AppendLine("}");
            _sb.AppendLine("}");

            // Get By GUID
            OpenMethod(externalMethodSignatures.ElementAt(3));
            _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
            _sb.AppendLine("{");
            _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
            _sb.AppendLine("{");
            _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
            _sb.AppendLine($"var ret = {(async ? "await " : "")}{name}Get{(async ? "Async" : "")}(con, cmd, globalId){(async ? ".ConfigureAwait(false)" : "")};");
            _sb.AppendLine("con.Close();");
            _sb.AppendLine("return ret;");
            _sb.AppendLine("}");
            _sb.AppendLine("}");
          }

          // Build internal methods
          _databaseLanguage.BuildInternalGetMethods(profile, schema, methodType, name, isTable, async, internalMethodSignatures, parameters);

          break;

        case MethodType.SAVE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Save{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalSaveMethod(profile, schema, name, isTable, async, internalMethodSignatures);
          break;

        case MethodType.DELETE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalDeleteMethod(profile, schema, name, isTable, async, internalMethodSignatures);
          break;

        case MethodType.MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Merge{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalMergeMethod(profile, schema, name, isTable, async, internalMethodSignatures);
          break;

        case MethodType.COUNT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return{(async ? " await" : "")} {name}GetCount{(async ? "Async" : "")}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(async ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalCountMethod(profile, schema, name, isTable, async, internalMethodSignatures, parameters);
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HasChanged{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalHasChangedMethod(profile, schema, name, isTable, async, internalMethodSignatures);
          break;

        case MethodType.BUlK_INSERT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkInsert{(async ? "Async" : "")}(dtos, identityInsert){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}_TempBulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(3));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}_TempBulkInsert{(async ? "Async" : "")}(dtos, identityInsert){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkMerge{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkMerge{(async ? "Async" : "")}(dtos, identityInsert){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_UPDATE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkUpdate{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.HIST_GET:
          // HistGet
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistGets{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");

          // HistEntryGet
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistEntryGet{(async ? "Async" : "")}(histId){(async ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalHistMethod(profile, schema, name, isTable, async, internalMethodSignatures);
          break;

        default:
          break;
      }
    }
  }
}
