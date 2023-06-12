using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObject.Enums;
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

    protected void BuildDataAccessSQLClassMethodBody(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, List<string> externalMethodSignatures, List<string> internalMethodSignatures, List<Column> parameters)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";
      string parametersStr = ParameterHelper.GetParametersString(parameters);
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(parameters, this);
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(parameters);

      switch (methodType)
      {
        case MethodType.GET:
          // From View
          // GetPrepareCommand
          OpenMethod(@$"GetPrepareCommand({(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : string.Empty)}SqlCommand cmd, string where = """", bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{name}[] orderBy)", "void", Enums.AccessType.PRIVATE);
          _sb.AppendLine("      cmd.Parameters.Clear();");
          _sb.AppendLine("      cmd.CommandType = CommandType.Text;");
          if (ti.TableType == TableType.FUNCTIONVIEW)
          {
            foreach (Column param in parameters)
            {
              if (param.ColumnTypeDataType == DataTypes.TableValue)
              {
                _sb.AppendLine($@"cmd.Parameters.Add(GetCustomTypeSqlParameter(""@{param.Name}"", ""core.{param.DataTypeTSql}"", {param.Name}.Cast<object>().ToArray(), typeof({param.DataType.Remove(param.DataType.Length - 2)})));");
              }
              else
              {
                _sb.AppendLine($@"cmd.Parameters.Add(""@{param.Name}"", SqlDbType.{param.DataTypeTSqlCamelCase}).Value = {param.Name} != null ? {param.Name} : DBNull.Value;");
              }
            }
          }
          methodBuilder.AppendLine("if (pageNum != null && pageSize != null && orderBy?.Length > 0)");
            .AppendLine("{");
            .AppendLine(@"cmd.Parameters.Add(""@pageNum"", SqlDbType.Int).Value = pageNum;");
            .AppendLine(@"cmd.Parameters.Add(""@pageSize"", SqlDbType.Int).Value = pageSize;");
            .AppendLine("}");
            .AppendLine($"cmd.CommandText = GetSqlStatement({(parametersStr.Length > 0 ? $"{parametersStr}, " : string.Empty)}where, distinct, pageNum, pageSize, orderBy);");

          methodBuilder = classBuilder.BuildMethod(@$"GetPrepareCommand({(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : string.Empty)}SqlCommand cmd, WhereClause whereClause, bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{ti.TableName}[] orderBy)", "void", "private");
            .AppendLine("      cmd.Parameters.Clear();");
            .AppendLine("      cmd.CommandType = CommandType.Text;");
            .AppendLine("      foreach (IWhereParameter whereParameter in whereClause.Parameters)");
            .AppendLine("      {");
            .AppendLine("        cmd.Parameters.Add(whereParameter.ParameterName, whereParameter.ParameterType).Value = whereParameter.ParameterValue;");
            .AppendLine("      }");
          if (ti.TableType == TableType.FUNCTIONVIEW)
          {
            foreach (var param in ((FunctionViewInformation)ti).Parameters)
            {
              if (param.IsCustomType)
              {
                methodBuilder.AppendLine($@"cmd.Parameters.Add(GetCustomTypeSqlParameter(""@{param.Name}"", ""core.{param.DataTypeTSql}"", {param.Name}.Cast<object>().ToArray(), typeof({param.DataType.Remove(param.DataType.Length - 2)})));");
              }
              else
              {
                methodBuilder.AppendLine($@"cmd.Parameters.Add(""@{param.Name}"", SqlDbType.{param.DataTypeTSqlCamelCase}).Value = {param.Name} != null ? {param.Name} : DBNull.Value;");
              }
            }
          }
          methodBuilder.AppendLine("if (pageNum != null && pageSize != null && orderBy?.Length > 0)");
            .AppendLine("{");
            .AppendLine(@"cmd.Parameters.Add(""@pageNum"", SqlDbType.Int).Value = pageNum;");
            .AppendLine(@"cmd.Parameters.Add(""@pageSize"", SqlDbType.Int).Value = pageSize;");
            .AppendLine("}");
            .AppendLine($"cmd.CommandText = GetSqlStatement({(parametersStr.Length > 0 ? $"{parametersStr}, " : string.Empty)}whereClause.Where, distinct, pageNum, pageSize, orderBy);");

          // GetSqlStatement
          methodBuilder = classBuilder.BuildMethod(@$"GetSqlStatement({(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : string.Empty)}string where = """", bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{ti.TableName}[] orderBy)", "string", "private");
            .AppendLine(@"      string sql =  $@""");
            .AppendLine(@"SELECT {(distinct ? ""DISTINCT "": """")}");

          var isFirst = true;
          foreach (var ci in ti.Columns)
          {
            methodBuilder.AppendLine($"      {(isFirst ? " " : ",")}pv.[{ci.ColumnName}]");
            isFirst = false;
          }

          methodBuilder.AppendLine($"  FROM [{ti.TableSchema}].[{ti.TableName}]{(ti.TableType == TableType.FUNCTIONVIEW ? $"({parametersAsSqlStr})" : " AS")} pv");
          methodBuilder.AppendLine(@""";");
          methodBuilder.AppendLine(@"      sql += where;");
          methodBuilder.AppendLine("sql += GetOrderBy(orderBy);");
          methodBuilder.AppendLine("sql += GetPagination(pageNum, pageSize, orderBy);");
          methodBuilder.AppendLine("      return sql;");

          classBuilder.BuildMethod($@"GetCustomTypeSqlParameter(string parameterName, string typeName, object[] value, Type type)", "SqlParameter", "private");
            .AppendLine("SqlParameter param = new SqlParameter();");
            .AppendLine("param.ParameterName = parameterName;");
            .AppendLine("param.SqlDbType = SqlDbType.Structured;");
            .AppendLine(@"param.TypeName = typeName;");
            .AppendLine();
            .AppendLine("DataTable dataTable = new DataTable();");
            .AppendLine(@"dataTable.Columns.Add(""val"", type);");
            .AppendLine("if (value != null && value.Length > 0)");
            .AppendLine("{");
            .AppendLine("foreach (var item in value)");
            .AppendLine("{");
            .AppendLine("dataTable.Rows.Add(item);");
            .AppendLine("}");
            .AppendLine("}");
            .AppendLine("param.Value = dataTable;");
            .AppendLine("return param;");

          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return{(async ? " await" : "")} {name}Gets{(async ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return{(async ? " await" : "")} {name}Gets{(async ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return{(async ? " await" : "")} {name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(1));
          _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
          _sb.AppendLine("{");
          _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
          _sb.AppendLine("{");
          _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
          _sb.AppendLine($"var ret = {(async ? "await " : "")}{name}Gets{(async ? "Async" : "")}(con, cmd, whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");
          _sb.AppendLine("con.Close();");
          _sb.AppendLine("return ret;");
          _sb.AppendLine("}");
          _sb.AppendLine("}");

          OpenMethod(internalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"return {(async ? "await " : "")}{name}Gets{(async ? "Async" : "")}(con, cmd, new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(3));
          _sb.AppendLine($"ICollection<{dtoName}> dtos = new List<{dtoName}>();");
          _sb.AppendLine("try");
          _sb.AppendLine("{");
          _sb.AppendLine($"GetPrepareCommand({(parametersStr != "" ? $"{parametersStr}, " : "")}cmd, whereClause, {(isTable ? " null," : "")} distinct, pageNum, pageSize, orderBy);");
          _sb.AppendLine($"using (SqlDataReader reader = {(async ? "await " : "")}cmd.ExecuteReader{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")})");
          _sb.AppendLine("{");
          _sb.AppendLine($"while ({(async ? "await " : "")}reader.Read{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")})");
          _sb.AppendLine("{");
          _sb.AppendLine("dtos.Add(GetRead(reader));");
          _sb.AppendLine("}");
          _sb.AppendLine("reader.Close();");
          _sb.AppendLine("}");
          _sb.AppendLine("}");
          _sb.AppendLine("catch (Exception ex)");
          _sb.AppendLine("{");
          BuildErrorLogCall($"{{nameof({name}{(isTable ? "Dao" : "DaoV")})}}.{{nameof({name}Gets{(async ? "Async" : "")})}}", null, async);
          _sb.AppendLine("throw;");
          _sb.AppendLine("}");
          _sb.AppendLine("return dtos;");

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

            OpenMethod(internalMethodSignatures.ElementAt(4));
            _sb.AppendLine($"{name}Dto dto = null;");
            _sb.AppendLine("try");
            _sb.AppendLine("{");
            _sb.AppendLine("GetPrepareCommand(cmd, id);");
            _sb.AppendLine($"using (SqlDataReader reader = {(async ? "await " : "")}cmd.ExecuteReader{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")})");
            _sb.AppendLine("{");
            _sb.AppendLine($"while ({(async ? "await " : "")}reader.Read{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")})");
            _sb.AppendLine("{");
            _sb.AppendLine("dto = GetRead(reader);");
            _sb.AppendLine("break;");
            _sb.AppendLine("}");
            _sb.AppendLine("reader.Close();");
            _sb.AppendLine("}");
            _sb.AppendLine("}");
            _sb.AppendLine("catch (Exception ex)");
            _sb.AppendLine("{");
            BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Get{(async ? "Async" : "")})}}", null, async);
            _sb.AppendLine("throw;");
            _sb.AppendLine("}");
            _sb.AppendLine("return dto;");

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

            OpenMethod(internalMethodSignatures.ElementAt(5));
            _sb.AppendLine($"{name}Dto dto = null;");
            _sb.AppendLine("try");
            _sb.AppendLine("{");
            _sb.AppendLine("GetPrepareCommand(cmd, globalId);");
            _sb.AppendLine($"using (SqlDataReader reader = {(async ? "await " : "")}cmd.ExecuteReader{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")})");
            _sb.AppendLine("{");
            _sb.AppendLine($"while ({(async ? "await " : "")}reader.Read{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")})");
            _sb.AppendLine("{");
            _sb.AppendLine("dto = GetRead(reader);");
            _sb.AppendLine("break;");
            _sb.AppendLine("}");
            _sb.AppendLine("reader.Close();");
            _sb.AppendLine("}");
            _sb.AppendLine("}");
            _sb.AppendLine("catch (Exception ex)");
            _sb.AppendLine("{");
            BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Get{(async ? "Async" : "")})}}", null, async);
            _sb.AppendLine("throw;");
            _sb.AppendLine("}");
            _sb.AppendLine("return dto;");
          }
          break;

        case MethodType.SAVE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Save{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Save{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.DELETE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}().{name}Delete{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(whereClause){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(con, cmd, whereClause){(async ? ".ConfigureAwait(false)" : "")};");

          break;

        case MethodType.MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Merge{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Merge{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.COUNT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return{(async ? " await" : "")} {name}GetCount{(async ? "Async" : "")}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
          _sb.AppendLine("{");
          _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
          _sb.AppendLine("{");
          _sb.AppendLine("cmd.Parameters.Clear();");
          _sb.AppendLine("foreach (IWhereParameter whereParameter in whereClause.Parameters)");
          _sb.AppendLine("{");
          _sb.AppendLine("cmd.Parameters.Add(whereParameter.ParameterName, whereParameter.ParameterType).Value = whereParameter.ParameterValue;");
          _sb.AppendLine("}");

          // Add function parameters
          foreach (Column param in (parameters))
          {
            if (param.ColumnTypeDataType == DataTypes.TableValue)
            {
              throw new ApplicationException("Table-valued function parameters are not supported.");
              //_sb.AppendLine($@"cmd.Parameters.Add(GetCustomTypeSqlParameter(""@{param.Name}"", ""core.{param.DataTypeTSql}"", {param.Name}.Cast<object>().ToArray(), typeof({param.DataType.Remove(param.DataType.Length - 2)})));");
            }
            else
            {
              _sb.AppendLine($@"cmd.Parameters.Add(""@{param.Name}"", SqlDbType.{param.ColumnTypeDataType}).Value = {param.Name} != null ? {param.Name} : DBNull.Value;");
            }
          }

          _sb.AppendLine("cmd.CommandType = CommandType.Text;");
          _sb.AppendLine(@"string sql = $@""");
          _sb.AppendLine("SELECT COUNT_BIG(*)");
          _sb.AppendLine($"FROM [{schema.Name}].[{name}]{(parametersSqlStr.Length > 0 ? $"({parametersSqlStr})" : "")} pv");
          _sb.AppendLine(@""";");
          _sb.AppendLine("sql += whereClause.Where;");
          _sb.AppendLine(@$"cmd.CommandText = sql;");
          _sb.AppendLine();
          _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
          _sb.AppendLine($"var ret = (long)({(async ? "await " : "")}cmd.ExecuteScalar{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")});");
          _sb.AppendLine("con.Close();");
          _sb.AppendLine("return ret;");
          _sb.AppendLine("}");
          _sb.AppendLine("}");

          break;

        case MethodType.HAS_CHANGED:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HasChanged{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HasChanged{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
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
          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistGets{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");

          // HistEntryGet
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistEntryGet{(async ? "Async" : "")}(histId){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistEntryGet{(async ? "Async" : "")}(con, cmd, histId){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        default:
          break;
      }
    }
  }
}
