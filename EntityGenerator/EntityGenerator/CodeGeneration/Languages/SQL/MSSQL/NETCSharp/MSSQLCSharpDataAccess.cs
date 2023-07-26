using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityGenerator.CodeGeneration.Languages.SQL.MSSQL.NETCSharp
{
  public partial class MSSQLCSharp : MSSQL
  {
    public override void BuildPrepareCommand(ProfileDto profile, Schema schema, string name, bool isTable, bool async, List<Column> parameters)
    {
      string parametersStr = ParameterHelper.GetParametersString(parameters);
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(parameters, this);
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(parameters);

      // Table-exclusive Function for Global GUID
      if (isTable)
      {
        // GetPrepareCommand(SqlCommand cmd, Guid globalId)
        _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, Guid globalId)", "void", Enums.AccessType.PRIVATE);
        _sb.AppendLine("      cmd.Parameters.Clear();");
        _sb.AppendLine(@$"      cmd.Parameters.Add(""@globalId"", SqlDbType.UniqueIdentifier).Value = globalId;");
        _sb.AppendLine("      cmd.CommandType = CommandType.Text;");
        _sb.AppendLine("      cmd.CommandText = GetSqlStatement();");
      }

      // GetPrepareCommand(SqlCommand cmd, <params|id>, string where, bool distinct, int pageNum, int pageSize,params Order[] orderBy)
      _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, {(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : string.Empty)}string where = """", bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{name}[] orderBy)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("cmd.Parameters.Clear();");
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
      if (isTable)
      {
        _sb.AppendLine("if (id != null)");
        _sb.AppendLine(@$"cmd.Parameters.Add(""@id"", {(profile.Global.GuidIndexing ? "SqlDbType.UniqueIdentifier" : "SqlDbType.BigInt")}).Value = id;");
      }
      else
      {
        foreach (Column param in parameters)
        {
          if (param.ColumnTypeDataType == DataTypes.TableValue)
          {
            _sb.AppendLine($@"cmd.Parameters.Add(GetCustomTypeSqlParameter(""@{param.Name}"", ""core.{GetColumnDataType(param)}"", {param.Name}.Cast<object>().ToArray(), typeof({GetColumnDataType(param).Remove(GetColumnDataType(param).Length - 2)})));");
          }
          else
          {
            _sb.AppendLine($@"cmd.Parameters.Add(""@{param.Name}"", SqlDbType.{GetDataTypeTSqlCamelCase(param)}).Value = {param.Name} != null ? {param.Name} : DBNull.Value;");
          }
        }
      }

      _sb.AppendLine("if (pageNum != null && pageSize != null && orderBy?.Length > 0)");
      _sb.AppendLine("{");
      _sb.AppendLine(@"cmd.Parameters.Add(""@pageNum"", SqlDbType.Int).Value = pageNum;");
      _sb.AppendLine(@"cmd.Parameters.Add(""@pageSize"", SqlDbType.Int).Value = pageSize;");
      _sb.AppendLine("}");
      _sb.AppendLine($"cmd.CommandText = GetSqlStatement({(isTable ? "id" : (parametersStr.Length > 0 ? $"{parametersStr}, " : string.Empty))}where, distinct, pageNum, pageSize, orderBy);");

      // GetPrepareCommand(SqlCommand cmd, <params|id>, WhereClause where, bool distinct, int pageNum, int pageSize,params Order[] orderBy)
      _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, {(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : string.Empty)}WhereClause whereClause, bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{name}[] orderBy)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("cmd.Parameters.Clear();");
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
      if (isTable)
      {
        _sb.AppendLine("if (id != null)");
        _sb.AppendLine(@$"cmd.Parameters.Add(""@id"", {(profile.Global.GuidIndexing ? "SqlDbType.UniqueIdentifier" : "SqlDbType.BigInt")}).Value = id;");
      }
      _sb.AppendLine("foreach (IWhereParameter whereParameter in whereClause.Parameters)");
      _sb.AppendLine("{");
      _sb.AppendLine("cmd.Parameters.Add(whereParameter.ParameterName, whereParameter.ParameterType).Value = whereParameter.ParameterValue;");
      _sb.AppendLine("}");
      foreach (Column param in parameters)
      {
        if (param.ColumnTypeDataType == DataTypes.TableValue)
        {
          _sb.AppendLine($@"cmd.Parameters.Add(GetCustomTypeSqlParameter(""@{param.Name}"", ""core.{GetColumnDataType(param)}"", {param.Name}.Cast<object>().ToArray(), typeof({param.ColumnTypeDataType.ToString().Remove(param.ColumnTypeDataType.ToString().Length - 2)})));");
        }
        else
        {
          _sb.AppendLine($@"cmd.Parameters.Add(""@{param.Name}"", SqlDbType.{GetDataTypeTSqlCamelCase(param)}).Value = {param.Name} != null ? {param.Name} : DBNull.Value;");
        }
      }
      _sb.AppendLine("if (pageNum != null && pageSize != null && orderBy?.Length > 0)");
      _sb.AppendLine("{");
      _sb.AppendLine(@"cmd.Parameters.Add(""@pageNum"", SqlDbType.Int).Value = pageNum;");
      _sb.AppendLine(@"cmd.Parameters.Add(""@pageSize"", SqlDbType.Int).Value = pageSize;");
      _sb.AppendLine("}");
      _sb.AppendLine($"cmd.CommandText = GetSqlStatement({(isTable ? "id" : (parametersStr.Length > 0 ? $"{parametersStr}, " : string.Empty))}whereClause.Where, distinct, pageNum, pageSize, orderBy);");
    }

    public override void BuildGetSqlStatement(ProfileDto profile, Schema schema, string name, bool isTable, List<Column> parameters, List<Column> columns)
    {
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(parameters, this);
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(parameters);

      // GetSqlStatement
      _backendLanguage.OpenMethod(@$"GetSqlStatement({(parametersWithTypeStr.IsNullOrEmpty() ? string.Empty : $"{parametersWithTypeStr}, ")}string where = """", bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{name}[] orderBy)", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"string sql =  $@""");
      _sb.AppendLine(@"SELECT {(distinct ? ""DISTINCT "": """")}");

      bool isFirst = true;
      foreach (Column column in columns)
      {
        _sb.AppendLine($"{(isFirst ? " " : ",")}pv.[{column.Name}]");
        isFirst = false;
      }

      _sb.AppendLine($"  FROM [{schema.Name}].[{name}]{(parametersSqlStr.IsNullOrEmpty() ? $"({parametersSqlStr})" : " AS")} pv");
      _sb.AppendLine(@""";");
      _sb.AppendLine(@"      sql += where;");
      _sb.AppendLine("sql += GetOrderBy(orderBy);");
      _sb.AppendLine("sql += GetPagination(pageNum, pageSize, orderBy);");
      _sb.AppendLine("      return sql;");
    }

    public override void BuildGetMethod(ProfileDto profile, Schema schema, MethodType methodType, string name,
      bool isTable, bool async, List<string> externalMethodSignatures, List<string> internalMethodSignatures,
      List<Column> parameters)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));
      string parametersStr = ParameterHelper.GetParametersString(parameters);

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return{(async ? " await" : "")} {name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
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

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2));
      _sb.AppendLine($"return {(async ? "await " : "")}{name}Gets{(async ? "Async" : "")}(con, cmd, new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(3));
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
      _backendLanguage.BuildErrorLogCall($"{{nameof({name}{(isTable ? "Dao" : "DaoV")})}}.{{nameof({name}Gets{(async ? "Async" : "")})}}", null, async);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
      _sb.AppendLine("return dtos;");

      if (isTable)
      {
        // Get
        _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(2));
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

        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(4));
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
        _backendLanguage.BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Get{(async ? "Async" : "")})}}", null, async);
        _sb.AppendLine("throw;");
        _sb.AppendLine("}");
        _sb.AppendLine("return dto;");

        // Get By GUID
        _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(3));
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

        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(5));
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
        _backendLanguage.BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Get{(async ? "Async" : "")})}}", null, async);
        _sb.AppendLine("throw;");
        _sb.AppendLine("}");
        _sb.AppendLine("return dto;");
      }
    }

    public override void BuildInternalCountMethod(ProfileDto profile, Schema schema, string name, bool isTable, bool async,
  List<string> internalMethodSignatures, List<Column> parameters)
    {
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(parameters);

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
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
    }
  }
}
