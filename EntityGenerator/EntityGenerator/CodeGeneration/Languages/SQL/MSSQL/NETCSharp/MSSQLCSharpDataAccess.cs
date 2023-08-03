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
    private void BuildSavePrepareCommand(string name, List<Column> columns)
    {
      _backendLanguage.OpenMethod($"SavePrepareCommand(SqlCommand cmd, {name}Dto dto)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("      cmd.Parameters.Clear();");

      foreach (Column column in columns)
      {
        if (column.ColumnIsNullable)
        {
          _sb.AppendLine(column.ColumnTypeDataType is DataTypes.XDocument or DataTypes.XElement
            ? $@"        cmd.Parameters.Add(""@{column.Name}"", {GetColumnDataType(column)}).Value =  new System.Data.SqlTypes.SqlXml(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(dto.{column.Name}.StartsWith(""<"") ? dto.{column.Name} : dto.{column.Name}.Substring(1))));"
            : $@"        cmd.Parameters.Add(""@{column.Name}"", {GetColumnDataType(column)}).Value =  dto.{column.Name} == null ? (object)DBNull.Value : dto.{column.Name};");
        }
        else
        {
          switch (column.ColumnTypeDataType)
          {
            case DataTypes.String:
              _sb.AppendLine($@"        cmd.Parameters.Add(""@{column.Name}"", {GetColumnDataType(column)}).Value =  dto.{column.Name} ?? string.Empty;");
              break;
            case DataTypes.XDocument or DataTypes.XElement:
              _sb.AppendLine($@"        cmd.Parameters.Add(""@{column.Name}"", {GetColumnDataType(column)}).Value =  new System.Data.SqlTypes.SqlXml(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(dto.{column.Name}.StartsWith(""<"") ? dto.{column.Name} : dto.{column.Name}.Substring(1))));");
              break;
            default:
              _sb.AppendLine($@"        cmd.Parameters.Add(""@{column.Name}"", {GetColumnDataType(column)}).Value =  dto.{column.Name};");
              break;
          }
        }
      }
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
    }

    private void BuildSaveUpdateSqlStatement(string name, string schemaName, List<Column> columns)
    {
      _backendLanguage.OpenMethod("SaveUpdateSqlStatement()", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"return @""");
      _sb.AppendLine($"UPDATE [{schemaName}].[{name}]");
      _sb.AppendLine($"SET");

      string prefix = " ";
      foreach (Column column in columns.Where(c => !c.ColumnIsIdentity && !c.ColumnIsComputed))
      {
        _sb.AppendLine($"{prefix}[{column.Name}] = @{column.Name}");
        prefix = ",";
      }
      _sb.AppendLine(@"WHERE [Id] = @Id"";");
    }

    private void BuildMergeSqlStatement(string name, string schemaName, List<Column> columns)
    {
      _backendLanguage.OpenMethod("MergeSqlStatement()", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"      return @""");
      _sb.AppendLine($"  SET IDENTITY_INSERT [{schemaName}].[{name}] ON;");
      _sb.AppendLine($"  MERGE INTO [{schemaName}].[{name}] AS Target");
      _sb.AppendLine("  USING (VALUES");

      string rowData = "  (";
      bool isFirstColumn = true;

      foreach (Column column in columns)
      {
        string sTemplate = string.Empty;

        if (!isFirstColumn)
          sTemplate += ", ";

        isFirstColumn = false;

        sTemplate += $"@{column.Name}";
        rowData += sTemplate;
      }

      rowData += ")";
      _sb.AppendLine(rowData);
      _sb.AppendLine("  )");

      rowData = "  AS Source (";
      isFirstColumn = true;

      foreach (Column column in columns)
      {
        string template = " ";

        if (!isFirstColumn)
          template = ",";

        isFirstColumn = false;

        template += $"[{column.Name}]";
        rowData += template;
      }

      rowData += ")";
      _sb.AppendLine(rowData);
      _sb.AppendLine("  ON Target.Id = Source.Id");
      _sb.AppendLine("  WHEN NOT MATCHED BY TARGET THEN");
      _sb.AppendLine("  INSERT (");

      isFirstColumn = true;

      foreach (Column column in columns)
      {
        string template = " ";

        if (!isFirstColumn)
          template = ",";

        isFirstColumn = false;

        _sb.AppendLine($"          {template}[{column.Name}]");
      }

      _sb.AppendLine("         )");
      _sb.AppendLine("  VALUES (");

      isFirstColumn = true;

      foreach (Column column in columns)
      {
        string template = " ";

        if (!isFirstColumn)
          template = ",";

        isFirstColumn = false;

        _sb.AppendLine($"         {template}Source.[{column.Name}]");
      }

      _sb.AppendLine("         )");
      _sb.AppendLine("  WHEN MATCHED THEN");
      _sb.AppendLine("  UPDATE");

      isFirstColumn = true;

      foreach (Column column in columns)
      {
        if (column.ColumnIsIdentity)
          continue;

        string template = !isFirstColumn ? "        ," : "     SET ";
        isFirstColumn = false;

        _sb.AppendLine($"{template}[{column.Name}] = Source.[{column.Name}]");
      }

      _sb.AppendLine("  ;");
      _sb.AppendLine($"  SET IDENTITY_INSERT [{schemaName}].[{name}] OFF;");

      _sb.AppendLine(@""";");
    }

    private void BuildDeletePrepareCommand()
    {
      _backendLanguage.OpenMethod($"DeletePrepareCommand(SqlCommand cmd, {(_profile.Global.GuidIndexing ? "Guid" : "long")} id)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("cmd.Parameters.Clear();");
      _sb.AppendLine(@$"cmd.Parameters.Add(""@Id"", {(_profile.Global.GuidIndexing ? "SqlDbType.UniqueIdentifier" : "SqlDbType.BigInt")}).Value = id;");
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
      _sb.AppendLine("cmd.CommandText = DeleteSqlStatement();");

      _backendLanguage.OpenMethod("DeletePrepareCommand(SqlCommand cmd, WhereClause whereClause)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("cmd.Parameters.Clear();");
      _sb.AppendLine("foreach (IWhereParameter whereParameter in whereClause.Parameters)");
      _sb.AppendLine("{");
      _sb.AppendLine("cmd.Parameters.Add(whereParameter.ParameterName, whereParameter.ParameterType).Value = whereParameter.ParameterValue;");
      _sb.AppendLine("}");
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
      _sb.AppendLine("cmd.CommandText = DeleteSqlStatement(whereClause.Where);");
    }

    private void BuildDeleteSqlStatement(string name, string schemaName)
    {
      _backendLanguage.OpenMethod("DeleteSqlStatement()", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"return @""");
      _sb.AppendLine($"DELETE FROM [{schemaName}].[{name}]");
      _sb.AppendLine(@"WHERE [Id] = @Id"";");

      _backendLanguage.OpenMethod("DeleteSqlStatement(string where)", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"return $@""");
      _sb.AppendLine($"DELETE pt FROM [{schemaName}].[{name}] AS pt");
      _sb.AppendLine(@"{where}"";");
    }

    private void BuildHasChangedWork(string name, List<Column> columns)
    {
      _backendLanguage.OpenMethod($"HasChangedWork({name}Dto dto, {name}Dto dtoDb)", returnType: "bool", accessModifier: Enums.AccessType.PRIVATE);
      _sb.AppendLine("if (dtoDb == null)");
      _sb.AppendLine("return true;");
      _sb.AppendLine("bool ret = true;");

      foreach (Column column in columns)
      {
        _sb.AppendLine(column.ColumnTypeDataType == DataTypes.ByteArray
          ? $"ret = ret && dto.{column.Name}.SequenceEqual(dtoDb.{column.Name});"
          : $"ret = ret && dto.{column.Name} == dtoDb.{column.Name};");
      }

      _sb.AppendLine("return !ret;");
    }

    public override void BuildPrepareCommand(Schema schema, string name, bool isTable, bool async, List<Column> parameters)
    {
      string parametersStr = ParameterHelper.GetParametersString(parameters);
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(parameters, this);
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(parameters);

      // Table-exclusive Function for Global GUID
      if (isTable)
      {
        // GetPrepareCommand(SqlCommand cmd, Guid globalId)
        _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, Guid globalId)", "void", Enums.AccessType.PRIVATE);
        _sb.AppendLine("cmd.Parameters.Clear();");
        _sb.AppendLine(@$"cmd.Parameters.Add(""@globalId"", SqlDbType.UniqueIdentifier).Value = globalId;");
        _sb.AppendLine("cmd.CommandType = CommandType.Text;");
        _sb.AppendLine("cmd.CommandText = GetSqlStatement();");
      }

      // GetPrepareCommand(SqlCommand cmd, <params|id>, string where, bool distinct, int pageNum, int pageSize,params Order[] orderBy)
      _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, {(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : string.Empty)}string where = """", bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{name}[] orderBy)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("cmd.Parameters.Clear();");
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
      if (isTable)
      {
        _sb.AppendLine("if (id != null)");
        _sb.AppendLine(@$"cmd.Parameters.Add(""@id"", {(_profile.Global.GuidIndexing ? "SqlDbType.UniqueIdentifier" : "SqlDbType.BigInt")}).Value = id;");
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
        _sb.AppendLine(@$"cmd.Parameters.Add(""@id"", {(_profile.Global.GuidIndexing ? "SqlDbType.UniqueIdentifier" : "SqlDbType.BigInt")}).Value = id;");
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

    public override void BuildGetSqlStatement(Schema schema, string name, bool isTable, List<Column> parameters, List<Column> columns)
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
      _sb.AppendLine(@"sql += where;");
      _sb.AppendLine("sql += GetOrderBy(orderBy);");
      _sb.AppendLine("sql += GetPagination(pageNum, pageSize, orderBy);");
      _sb.AppendLine("return sql;");
    }

    public override void BuildGetMethod(Schema schema, MethodType methodType, string name,
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

    public override void BuildSaveMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures, List<Column> columns)
    {
      // SavePrepareCommand
      BuildSavePrepareCommand(name, columns);

      // SaveUpdateSQLStatement
      BuildSaveUpdateSqlStatement(name, schema.Name, columns);

      // Save(Dto)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(1));
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
        _sb.AppendLine("{");
        _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
        _sb.AppendLine("{");
        _sb.AppendLine($"{(async ? "await con.OpenAsync().ConfigureAwait(false)" : "con.Open()")};");
        _sb.AppendLine($"{(async ? "await " : "")}{name}Save{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
        _sb.AppendLine("con.Close();");
        _sb.AppendLine("}");
        _sb.AppendLine("}");

      // Save(SqlConnection, SqlCommand, Dto)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine("try");
      _sb.AppendLine("{");
      _sb.AppendLine("if (!this.BeforeSave(con, cmd, dto))");
      _sb.AppendLine("return;");
      _sb.AppendLine("SavePrepareCommand(cmd, dto);");
      _sb.AppendLine($"if (dto.Id {(_profile.Global.GuidIndexing ? "== Guid.Empty" : "<= 0")})");
      _sb.AppendLine("{");
      _sb.AppendLine("cmd.CommandText = SaveInsertSqlStatement();");
      _sb.AppendLine($"{(_profile.Global.GuidIndexing ? "" : "dto.Id = Convert.ToInt64(await cmd.ExecuteScalarAsync().ConfigureAwait(false));")}");
      _sb.AppendLine("}");
      _sb.AppendLine("else");
      _sb.AppendLine("{");
      _sb.AppendLine("cmd.CommandText = SaveUpdateSqlStatement();");
      _sb.AppendLine($"{(async ? "await " : "")}cmd.ExecuteNonQuery{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("}");
      _sb.AppendLine("if (!this.AfterSave(con, cmd, dto))");
      _sb.AppendLine("return;");
      _sb.AppendLine();
      _sb.AppendLine("}");
      _sb.AppendLine("catch (Exception ex)");
      _sb.AppendLine("{");
      _backendLanguage.BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Save{(async ? "Async " : "")})}}", "dto", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
    }
    public override void BuildDeleteMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures)
    {
      // DeletePrepareCommand
      BuildDeletePrepareCommand();

      // DeleteSqlStatement
      BuildDeleteSqlStatement(name, schema.Name);

      // Delete(id)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(0));
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine($"{(async ? "await " : "")}{name}Delete{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // Delete(WhereClause)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine($"{(async ? "await " : "")}{name}Delete{(async ? "Async" : "")}(con, cmd, whereClause){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // Delete(SqlConnection, SqlCommand, id)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
      _sb.AppendLine("try");
      _sb.AppendLine("{");
      _sb.AppendLine("DeletePrepareCommand(cmd, id);");
      _sb.AppendLine($"{(async ? "await " : "")}cmd.ExecuteNonQuery{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("}");
      _sb.AppendLine("catch (Exception ex)");
      _sb.AppendLine("{");
      _backendLanguage.BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Delete{(async ? "Async" : "")})}}", "id", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");

      // Delete(SqlConnection, SqlCommand, WhereClause)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2));
      _sb.AppendLine("try");
      _sb.AppendLine("{");
      _sb.AppendLine("DeletePrepareCommand(cmd, whereClause);");
      _sb.AppendLine($"{(async ? "await " : "")}cmd.ExecuteNonQuery{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("}");
      _sb.AppendLine("catch (Exception ex)");
      _sb.AppendLine("{");
      _backendLanguage.BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Delete{(async ? "Async" : "")})}}", "whereClause", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
    }
    public override void BuildMergeMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures, List<Column> columns)
    {
      // MergeSqlStatement
      BuildMergeSqlStatement(name, schema.Name, columns);

      // Merge(Dto)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(0));
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async " : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine($"{(async ? "await " : "")}{name}Merge{(async ? "Async " : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // Merge(SqlConnection, SqlCommand, Dto)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine("try");
      _sb.AppendLine("{");
      _sb.AppendLine("if (!this.BeforeSave(con, cmd, dto))");
      _sb.AppendLine("return;");
      _sb.AppendLine();
      _sb.AppendLine("SavePrepareCommand(cmd, dto);");
      _sb.AppendLine("cmd.CommandText = MergeSqlStatement();");
      _sb.AppendLine($"{(async ? "await " : "")}cmd.ExecuteNonQuery{(async ? "Async " : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine();
      _sb.AppendLine("if (!this.AfterSave(con, cmd, dto))");
      _sb.AppendLine("return;");
      _sb.AppendLine();
      _sb.AppendLine("}");
      _sb.AppendLine("catch (Exception ex)");
      _sb.AppendLine("{");
      _backendLanguage.BuildErrorLogCall($"{{nameof({name}Dao)}}.{{nameof({name}Merge{(async ? "Async " : "")})}}", "dto", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
    }

    public override void BuildInternalCountMethod(Schema schema, string name, bool isTable, bool async,
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

    public override void BuildHasChangedMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures, List<Column> columns)
    {
      // HasChangedWork
      BuildHasChangedWork(name, columns);

      // HasChanged(Dto)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(0));
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      if (async)
      {
        _sb.AppendLine("await con.OpenAsync().ConfigureAwait(false);");
        _sb.AppendLine($"var ret = await {name}HasChangedAsync(con, cmd, dto).ConfigureAwait(false);");
      }
      else
      {
        _sb.AppendLine("con.Open();");
        _sb.AppendLine($"var ret = {name}HasChanged(con, cmd, dto);");
      }
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("return ret;");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // HasChanged(SqlConnection, SqlCommand, Dto)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"if (dto.Id {(_profile.Global.GuidIndexing ? "== Guid.Empty" : "<= 0")})");
      _sb.AppendLine("return true;");
      if (async)
      {
        _sb.AppendLine($"{name}Dto dtoDb = await {name}GetAsync(con, cmd, dto.Id).ConfigureAwait(false);");
      }
      else
      {
        _sb.AppendLine($"{name}Dto dtoDb = {name}Get(con, cmd, dto.Id);");
      }
      _sb.AppendLine("return HasChangedWork(dto, dtoDb);");
    }
  }
}
