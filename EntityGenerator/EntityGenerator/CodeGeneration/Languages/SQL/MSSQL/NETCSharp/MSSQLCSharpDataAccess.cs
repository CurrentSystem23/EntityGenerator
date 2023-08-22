using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;

namespace EntityGenerator.CodeGeneration.Languages.SQL.MSSQL.NETCSharp
{
  public partial class MSSQLCSharp : MSSQL
  {
    private string GetTypeReader(Column column)
    {
      if (column.ColumnTypeDataType == Core.Models.Enums.DataTypes.TableValue)
      {
        return "GetDataTable";
      }
      return column.ColumnTypeDataTypeSql switch
      {
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Bit => $"GetBoolean{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.TinyInt => $"GetByte{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.SmallInt => $"GetInt16{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Int => $"GetInt32{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.BigInt => $"GetInt64{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.SmallMoney => $"GetDecimal{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Money => $"GetDecimal{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Decimal => $"GetDecimal{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Real => $"GetFloat{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Float => $"GetDouble{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Char1 => $"GetSingleChar{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.NChar1 => $"GetSingleChar{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Char => $"GetSingleChar{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.NChar => $"GetSingleNChar{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.VarChar => $"GetString{(column.ColumnIsNullable ? "FromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.NVarChar => $"GetString{(column.ColumnIsNullable ? "FromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.VarCharMax => $"GetString{(column.ColumnIsNullable ? "FromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.NVarCharMax => $"GetString{(column.ColumnIsNullable ? "FromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Text => $"GetString{(column.ColumnIsNullable ? "FromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.NText => $"GetString{(column.ColumnIsNullable ? "FromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Xml => $"GetString{(column.ColumnIsNullable ? "FromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.SmallDateTime => $"GetDateTime{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.DateTime => $"GetDateTime{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.DateTime2 => $"GetDateTime{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.DateTimeOffset => $"GetDateTimeOffset{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Date => $"GetDateTime{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Time => $"GetDateTime{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Binary => $"GetBinaryFromNullableDbValue",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.VarBinary => $"GetBinaryFromNullableDbValue",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.VarBinaryMax => $"GetBinaryFromNullableDbValue",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.Image => $"GetBinaryFromNullableDbValue",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.TimeStamp => $"GetDateTime{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.FileStream => $"GetBinaryFromNullableDbValue",
        InformationExtractor.MSSqlServer.Models.Enums.DataTypes.UniqueIdentifier => $"GetGuid{(column.ColumnIsNullable ? "NullableFromNullableDbValue" : "")}",
          _ => throw new NotSupportedException()
      };
    }
    private void BuildGetReadMethod(GeneratorBaseModel baseModel)
    {
      _backendLanguage.OpenMethod("GetRead(SqlDataReader reader)", $"{baseModel.DtoName}", Enums.AccessType.PRIVATE);
      _sb.AppendLine($"{baseModel.DtoName} dto = new {baseModel.DtoName}();");

      int pos = 0;
      foreach (Column column in baseModel.Columns)
      {
        string readerType = column.ColumnTypeDataType == Core.Models.Enums.DataTypes.TableValue ? "GetDataTable" : GetTypeReader(column);
        _sb.AppendLine($"dto.{column.Name} = reader.{readerType}({pos});");
        pos++;
      }

      _sb.AppendLine("return dto;");
    }
    private void BuildSavePrepareCommand(GeneratorBaseModel baseModel)
    {
      _backendLanguage.OpenMethod($"SavePrepareCommand(SqlCommand cmd, {baseModel.DtoName} dto)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("      cmd.Parameters.Clear();");

      foreach (Column column in baseModel.Columns)
      {
        if (column.ColumnIsNullable)
        {
          _sb.AppendLine(column.ColumnTypeDataType is Core.Models.Enums.DataTypes.XDocument or Core.Models.Enums.DataTypes.XElement
            ? $@"        cmd.Parameters.Add(""@{column.Name}"", SqlDbType.{GetDataTypeTSqlCamelCase(column)}).Value =  new System.Data.SqlTypes.SqlXml(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(dto.{column.Name}.StartsWith(""<"") ? dto.{column.Name} : dto.{column.Name}.Substring(1))));"
            : $@"        cmd.Parameters.Add(""@{column.Name}"", SqlDbType.{GetDataTypeTSqlCamelCase(column)}).Value =  dto.{column.Name} == null ? (object)DBNull.Value : dto.{column.Name};");
        }
        else
        {
          switch (column.ColumnTypeDataType)
          {
            case Core.Models.Enums.DataTypes.String:
              _sb.AppendLine($@"cmd.Parameters.Add(""@{column.Name}"", SqlDbType.{GetDataTypeTSqlCamelCase(column)}).Value =  dto.{column.Name} ?? string.Empty;");
              break;
            case Core.Models.Enums.DataTypes.XDocument or Core.Models.Enums.DataTypes.XElement:
              _sb.AppendLine($@"cmd.Parameters.Add(""@{column.Name}"", SqlDbType.{GetDataTypeTSqlCamelCase(column)}).Value =  new System.Data.SqlTypes.SqlXml(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(dto.{column.Name}.StartsWith(""<"") ? dto.{column.Name} : dto.{column.Name}.Substring(1))));");
              break;
            default:
              _sb.AppendLine($@"cmd.Parameters.Add(""@{column.Name}"", SqlDbType.{GetDataTypeTSqlCamelCase(column)}).Value =  dto.{column.Name};");
              break;
          }
        }
      }
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
    }

    private void BuildSaveUpdateSqlStatement(GeneratorBaseModel baseModel)
    {
      _backendLanguage.OpenMethod("SaveUpdateSqlStatement()", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"return @""");
      _sb.AppendLine($"UPDATE [{baseModel.Schema.Name}].[{baseModel.Name}]");
      _sb.AppendLine($"SET");

      string prefix = " ";
      foreach (Column column in baseModel.Columns.Where(c => !c.ColumnIsIdentity && !c.ColumnIsComputed))
      {
        _sb.AppendLine($"{prefix}[{column.Name}] = @{column.Name}");
        prefix = ",";
      }
      _sb.AppendLine(@"WHERE [Id] = @Id"";");
    }

    private void BuildSaveInsertSqlStatement(GeneratorBaseModel baseModel)
    {
      _backendLanguage.OpenMethod("SaveInsertSqlStatement()", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"return @""");
      _sb.AppendLine($"      INSERT INTO [{baseModel.Schema.Name}].[{baseModel.Name}] (");

      string prefix = " ";
      foreach (Column column in baseModel.Columns.Where(c => !c.ColumnIsIdentity && !c.ColumnIsComputed))
      {
        _sb.AppendLine($"{prefix}[{column.Name}]");
        prefix = ",";
      }

      _sb.AppendLine("           )");
      _sb.AppendLine("     VALUES (");
      prefix = " ";

      foreach (Column column in baseModel.Columns.Where(c => !c.ColumnIsIdentity && !c.ColumnIsComputed))
      {
        if (column.Name == "Id" && _profile.Global.GuidIndexing)
        {
          _sb.AppendLine($"           NEWID()");
        }
        else
        {
          _sb.AppendLine($"           {prefix}@{column.Name}");
        }
        prefix = ",";
      }

      if (!_profile.Global.GuidIndexing)
      {
        _sb.AppendLine("           )")
          .AppendLine(@$"           SELECT CAST(scope_identity()  AS bigint)"";");
      }
      else
      {
        _sb.AppendLine($@""";");
      }
    }

    private void BuildMergeSqlStatement(GeneratorBaseModel baseModel)
    {
      _backendLanguage.OpenMethod("MergeSqlStatement()", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"      return @""");
      _sb.AppendLine($"  SET IDENTITY_INSERT [{baseModel.Schema.Name}].[{baseModel.Name}] ON;");
      _sb.AppendLine($"  MERGE INTO [{baseModel.Schema.Name}].[{baseModel.Name}] AS Target");
      _sb.AppendLine("  USING (VALUES");

      string rowData = "  (";
      bool isFirstColumn = true;

      foreach (Column column in baseModel.Columns)
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

      foreach (Column column in baseModel.Columns)
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

      foreach (Column column in baseModel.Columns)
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

      foreach (Column column in baseModel.Columns)
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

      foreach (Column column in baseModel.Columns)
      {
        if (column.ColumnIsIdentity)
          continue;

        string template = !isFirstColumn ? "        ," : "     SET ";
        isFirstColumn = false;

        _sb.AppendLine($"{template}[{column.Name}] = Source.[{column.Name}]");
      }

      _sb.AppendLine("  ;");
      _sb.AppendLine($"  SET IDENTITY_INSERT [{baseModel.Schema.Name}].[{baseModel.Name}] OFF;");

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

    private void BuildDeleteSqlStatement(GeneratorBaseModel baseModel)
    {
      _backendLanguage.OpenMethod("DeleteSqlStatement()", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"return @""");
      _sb.AppendLine($"DELETE FROM [{baseModel.Schema.Name}].[{baseModel.Name}]");
      _sb.AppendLine(@"WHERE [Id] = @Id"";");

      _backendLanguage.OpenMethod("DeleteSqlStatement(string where)", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"return $@""");
      _sb.AppendLine($"DELETE pt FROM [{baseModel.Schema.Name}].[{baseModel.Name}] AS pt");
      _sb.AppendLine(@"{where}"";");
    }

    private void BuildHasChangedWork(GeneratorBaseModel baseModel)
    {
      _backendLanguage.OpenMethod($"HasChangedWork({baseModel.DtoName} dto, {baseModel.DtoName} dtoDb)", returnType: "bool", accessModifier: Enums.AccessType.PRIVATE);
      _sb.AppendLine("if (dtoDb == null)");
      _sb.AppendLine("return true;");
      _sb.AppendLine("bool ret = true;");

      foreach (Column column in baseModel.Columns)
      {
        _sb.AppendLine(column.ColumnTypeDataType == Core.Models.Enums.DataTypes.ByteArray
          ? $"ret = ret && dto.{column.Name}.SequenceEqual(dtoDb.{column.Name});"
          : $"ret = ret && dto.{column.Name} == dtoDb.{column.Name};");
      }

      _sb.AppendLine("return !ret;");
    }

    public override void BuildGetPrepareCommand(GeneratorBaseModel baseModel, bool async)
    {
      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(baseModel.Parameters, _backendLanguage);

      // Table-exclusive Function for Global GUID
      if (baseModel.DbObjectType == DbObjectType.TABLE)
      {
        // GetPrepareCommand(SqlCommand cmd, Guid globalId)
        _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, Guid globalId)", "void", Enums.AccessType.PRIVATE);
        _sb.AppendLine("cmd.Parameters.Clear();");
        _sb.AppendLine(@$"cmd.Parameters.Add(""@globalId"", SqlDbType.UniqueIdentifier).Value = globalId;");
        _sb.AppendLine("cmd.CommandType = CommandType.Text;");
        _sb.AppendLine("cmd.CommandText = GetSqlStatement();");
      }

      // GetPrepareCommand(SqlCommand cmd, <params|id>, string where, bool distinct, int pageNum, int pageSize,params Order[] orderBy)
      _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, {(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : "long? id = null, ")}string where = """", bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{baseModel.Name}[] orderBy)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("cmd.Parameters.Clear();");
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
      if (baseModel.DbObjectType == DbObjectType.TABLE)
      {
        _sb.AppendLine("if (id != null)");
        _sb.AppendLine(@$"cmd.Parameters.Add(""@id"", {(_profile.Global.GuidIndexing ? "SqlDbType.UniqueIdentifier" : "SqlDbType.BigInt")}).Value = id;");
      }
      else
      {
        foreach (Column param in baseModel.Parameters.OrEmptyIfNull())
        {
          if (param.ColumnTypeDataType == Core.Models.Enums.DataTypes.TableValue)
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
      _sb.AppendLine($"cmd.CommandText = GetSqlStatement({(baseModel.DbObjectType == DbObjectType.TABLE ? "id, " : (parametersStr.Length > 0 ? $"{parametersStr}, " : string.Empty))}where, distinct, pageNum, pageSize, orderBy);");

      // GetPrepareCommand(SqlCommand cmd, <params>, WhereClause where, <id>, bool distinct, int pageNum, int pageSize,params Order[] orderBy)
      _backendLanguage.OpenMethod(@$"GetPrepareCommand(SqlCommand cmd, {(parametersWithTypeStr.Length > 0 ? $"{parametersWithTypeStr}, " : string.Empty)}WhereClause whereClause{(baseModel.DbObjectType == DbObjectType.TABLE ? ", long? id = null" : string.Empty)}, bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{baseModel.Name}[] orderBy)", "void", Enums.AccessType.PRIVATE);
      _sb.AppendLine("cmd.Parameters.Clear();");
      _sb.AppendLine("cmd.CommandType = CommandType.Text;");
      if (baseModel.DbObjectType == DbObjectType.TABLE)
      {
        _sb.AppendLine("if (id != null)");
        _sb.AppendLine(@$"cmd.Parameters.Add(""@id"", {(_profile.Global.GuidIndexing ? "SqlDbType.UniqueIdentifier" : "SqlDbType.BigInt")}).Value = id;");
      }
      _sb.AppendLine("foreach (IWhereParameter whereParameter in whereClause.Parameters)");
      _sb.AppendLine("{");
      _sb.AppendLine("cmd.Parameters.Add(whereParameter.ParameterName, whereParameter.ParameterType).Value = whereParameter.ParameterValue;");
      _sb.AppendLine("}");
      foreach (Column param in baseModel.Parameters.OrEmptyIfNull())
      {
        if (param.ColumnTypeDataType == Core.Models.Enums.DataTypes.TableValue)
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
      _sb.AppendLine($"cmd.CommandText = GetSqlStatement({(baseModel.DbObjectType == DbObjectType.TABLE ? "id, " : (parametersStr.Length > 0 ? $"{parametersStr}, " : string.Empty))}whereClause.Where, distinct, pageNum, pageSize, orderBy);");
    }

    public override void BuildGetSqlStatement(GeneratorBaseModel baseModel)
    {
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(baseModel.Parameters, this);
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(baseModel.Parameters);

      // GetSqlStatement
      _backendLanguage.OpenMethod(@$"GetSqlStatement({(parametersWithTypeStr.IsNullOrEmpty() ? $"{(baseModel.DbObjectType == DbObjectType.TABLE ? "long? id = null, " : string.Empty)}" : $"{parametersWithTypeStr}, ")}string where = """", bool distinct = false, int? pageNum = null, int? pageSize = null, params Order{baseModel.Name}[] orderBy)", "string", Enums.AccessType.PRIVATE);
      _sb.AppendLine(@"string sql =  $@""");
      _sb.AppendLine(@"SELECT {(distinct ? ""DISTINCT "": """")}");

      bool isFirst = true;
      foreach (Column column in baseModel.Columns)
      {
        _sb.AppendLine($"{(isFirst ? " " : ",")}pv.[{column.Name}]");
        isFirst = false;
      }

      _sb.AppendLine($"  FROM [{baseModel.Schema.Name}].[{baseModel.Name}]{(parametersSqlStr.IsNullOrEmpty() ? $"({parametersSqlStr})" : " AS")} pv");
      _sb.AppendLine(@""";");
      if (baseModel.DbObjectType == DbObjectType.TABLE)
      {
        _sb.AppendLine("if (id != null)");
        _sb.AppendLine(@"sql += ""WHERE pt.[Id] = @id"";");
        _sb.AppendLine("else");
        _sb.AppendLine("sql += where;");
      }
      else
      {
        _sb.AppendLine(@"sql += where;");
      }
      _sb.AppendLine("sql += GetOrderBy(orderBy);");
      _sb.AppendLine("sql += GetPagination(pageNum, pageSize, orderBy);");
      _sb.AppendLine("return sql;");
    }

    public override void BuildGetMethod(GeneratorBaseModel baseModel, MethodType methodType, bool async, List<string> externalMethodSignatures, List<string> internalMethodSignatures)
    {
      // Functions can only execute.
      if (baseModel.DbObjectType == DbObjectType.FUNCTION)
      {
        return;
      }
      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);

      BuildGetReadMethod(baseModel);

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0), isAsync: async);
      _sb.AppendLine($"return{(async ? " await" : "")} {baseModel.Name}Gets{(async ? "Async" : "")}(con, cmd, new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null){(async ? ".ConfigureAwait(false)" : "")};");

      // Gets(WhereClause, distinct, orderBy)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1), isAsync: async);
      _sb.AppendLine($"return {(async ? "await " : "")}{baseModel.Name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      // Gets(WhereClause, distinct, pageNum, pageSize, orderBy)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2), isAsync: async);
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine($"var ret = {(async ? "await " : "")}{baseModel.Name}Gets{(async ? "Async" : "")}(con, cmd, whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("return ret;");
      _sb.AppendLine("}");
      _sb.AppendLine("}");


      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(3), isAsync: async);
      _sb.AppendLine($"ICollection<{baseModel.DtoName}> dtos = new List<{baseModel.DtoName}>();");
      _sb.AppendLine("try");
      _sb.AppendLine("{");
      _sb.AppendLine($"GetPrepareCommand({(parametersStr != "" ? $"{parametersStr}, " : "")}cmd, whereClause, {(baseModel.DbObjectType == DbObjectType.TABLE ? " null," : "")} distinct, pageNum, pageSize, orderBy);");
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
      _backendLanguage.BuildErrorLogCall($"{{nameof({baseModel.Name}{(baseModel.DbObjectType == DbObjectType.TABLE ? "Dao" : "DaoV")})}}.{{nameof({baseModel.Name}Gets{(async ? "Async" : "")})}}", null, async);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
      _sb.AppendLine("return dtos;");

      if (baseModel.DbObjectType == DbObjectType.TABLE)
      {
        // Get
        _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(2), isAsync: async);
        _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
        _sb.AppendLine("{");
        _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
        _sb.AppendLine("{");
        _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
        _sb.AppendLine($"var ret = {(async ? "await " : "")}{baseModel.Name}Get{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
        _sb.AppendLine("con.Close();");
        _sb.AppendLine("return ret;");
        _sb.AppendLine("}");
        _sb.AppendLine("}");

        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(4), isAsync: async);
        _sb.AppendLine($"{baseModel.DtoName} dto = null;");
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
        _backendLanguage.BuildErrorLogCall($"{{nameof({baseModel.DaoName})}}.{{nameof({baseModel.Name}Get{(async ? "Async" : "")})}}", null, async);
        _sb.AppendLine("throw;");
        _sb.AppendLine("}");
        _sb.AppendLine("return dto;");

        // Get By GUID
        _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(3), isAsync: async);
        _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
        _sb.AppendLine("{");
        _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
        _sb.AppendLine("{");
        _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
        _sb.AppendLine($"var ret = {(async ? "await " : "")}{baseModel.Name}Get{(async ? "Async" : "")}(con, cmd, globalId){(async ? ".ConfigureAwait(false)" : "")};");
        _sb.AppendLine("con.Close();");
        _sb.AppendLine("return ret;");
        _sb.AppendLine("}");
        _sb.AppendLine("}");

        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(5), isAsync: async);
        _sb.AppendLine($"{baseModel.DtoName} dto = null;");
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
        _backendLanguage.BuildErrorLogCall($"{{nameof({baseModel.DaoName})}}.{{nameof({baseModel.Name}Get{(async ? "Async" : "")})}}", null, async);
        _sb.AppendLine("throw;");
        _sb.AppendLine("}");
        _sb.AppendLine("return dto;");
      }
    }

    public override void BuildSaveMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures)
    {
      // Only table objects can save data.
      if (baseModel.DbObjectType != DbObjectType.TABLE)
      {
        return;
      }
      // SavePrepareCommand
      BuildSavePrepareCommand(baseModel);

      // SaveUpdateSQLStatement
      BuildSaveUpdateSqlStatement(baseModel);

      // SaveInsertSQLStatement
      BuildSaveInsertSqlStatement(baseModel);

      // Save(Dto)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(0), isAsync: async);
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
        _sb.AppendLine("{");
        _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
        _sb.AppendLine("{");
        _sb.AppendLine($"{(async ? "await con.OpenAsync().ConfigureAwait(false)" : "con.Open()")};");
        _sb.AppendLine($"{(async ? "await " : "")}{baseModel.Name}Save{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
        _sb.AppendLine("con.Close();");
        _sb.AppendLine("}");
        _sb.AppendLine("}");

      // Save(SqlConnection, SqlCommand, Dto)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0), isAsync: async);
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
      _backendLanguage.BuildErrorLogCall($"{{nameof({baseModel.DaoName})}}.{{nameof({baseModel.Name}Save{(async ? "Async " : "")})}}", "dto", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
    }
    public override void BuildDeleteMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures)
    {
      // DeletePrepareCommand
      BuildDeletePrepareCommand();

      // DeleteSqlStatement
      BuildDeleteSqlStatement(baseModel);

      // Delete(id)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(0), isAsync: async);
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine($"{(async ? "await " : "")}{baseModel.Name}Delete{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // Delete(SqlConnection, SqlCommand, id)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0), isAsync: async);
      _sb.AppendLine("try");
      _sb.AppendLine("{");
      _sb.AppendLine("DeletePrepareCommand(cmd, id);");
      _sb.AppendLine($"{(async ? "await " : "")}cmd.ExecuteNonQuery{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("}");
      _sb.AppendLine("catch (Exception ex)");
      _sb.AppendLine("{");
      _backendLanguage.BuildErrorLogCall($"{{nameof({baseModel.DaoName})}}.{{nameof({baseModel.Name}Delete{(async ? "Async" : "")})}}", "id", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");

      // Delete(WhereClause)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1), isAsync: async);
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine($"{(async ? "await " : "")}{baseModel.Name}Delete{(async ? "Async" : "")}(con, cmd, whereClause){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // Delete(SqlConnection, SqlCommand, WhereClause)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2), isAsync: async);
      _sb.AppendLine("try");
      _sb.AppendLine("{");
      _sb.AppendLine("DeletePrepareCommand(cmd, whereClause);");
      _sb.AppendLine($"{(async ? "await " : "")}cmd.ExecuteNonQuery{(async ? "Async" : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("}");
      _sb.AppendLine("catch (Exception ex)");
      _sb.AppendLine("{");
      _backendLanguage.BuildErrorLogCall($"{{nameof({baseModel.DaoName} )}}.{{nameof({baseModel.Name}Delete{(async ? "Async" : "")})}}", "whereClause", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
    }
    public override void BuildMergeMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures)
    {
      // MergeSqlStatement
      BuildMergeSqlStatement(baseModel);

      // Merge(Dto)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(0), isAsync: async);
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      _sb.AppendLine($"{(async ? "await " : "")}con.Open{(async ? "Async " : "")}(){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine($"{(async ? "await " : "")}{baseModel.Name}Merge{(async ? "Async " : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // Merge(SqlConnection, SqlCommand, Dto)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0), isAsync: async);
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
      _backendLanguage.BuildErrorLogCall($"{{nameof({baseModel.DaoName})}}.{{nameof({baseModel.Name}Merge{(async ? "Async " : "")})}}", "dto", true);
      _sb.AppendLine("throw;");
      _sb.AppendLine("}");
    }

    public override void BuildInternalCountMethod(GeneratorBaseModel baseModel, bool async,
  List<string> internalMethodSignatures)
    {
      string parametersSqlStr = ParameterHelper.GetParametersSqlString(baseModel.Parameters);

      // GetCount(WhereClause)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0), isAsync: async);
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
      foreach (Column param in (baseModel.Parameters).OrEmptyIfNull())
      {
        if (param.ColumnTypeDataType == Core.Models.Enums.DataTypes.TableValue)
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
      _sb.AppendLine($"FROM [{baseModel.Schema.Name}].[{baseModel.Name}]{(parametersSqlStr.Length > 0 ? $"({parametersSqlStr})" : "")} pv");
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

    public override void BuildHasChangedMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures, List<string> externalMethodSignatures)
    {
      // HasChangedWork
      BuildHasChangedWork(baseModel);

      // HasChanged(Dto)
      _backendLanguage.OpenMethod(externalMethodSignatures.ElementAt(0), isAsync: async);
      _sb.AppendLine("using (SqlConnection con = new SqlConnection(DatabaseConnection.ConnectionString))");
      _sb.AppendLine("{");
      _sb.AppendLine("using (SqlCommand cmd = con.CreateCommand())");
      _sb.AppendLine("{");
      if (async)
      {
        _sb.AppendLine("await con.OpenAsync().ConfigureAwait(false);");
        _sb.AppendLine($"var ret = await {baseModel.Name}HasChangedAsync(con, cmd, dto).ConfigureAwait(false);");
      }
      else
      {
        _sb.AppendLine("con.Open();");
        _sb.AppendLine($"var ret = {baseModel.Name}HasChanged(con, cmd, dto);");
      }
      _sb.AppendLine("con.Close();");
      _sb.AppendLine("return ret;");
      _sb.AppendLine("}");
      _sb.AppendLine("}");

      // HasChanged(SqlConnection, SqlCommand, Dto)
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0), isAsync: async);
      _sb.AppendLine($"if (dto.Id {(_profile.Global.GuidIndexing ? "== Guid.Empty" : "<= 0")})");
      _sb.AppendLine("return true;");
      if (async)
      {
        _sb.AppendLine($"{baseModel.DtoName} dtoDb = await {baseModel.Name}GetAsync(con, cmd, dto.Id).ConfigureAwait(false);");
      }
      else
      {
        _sb.AppendLine($"{baseModel.DtoName} dtoDb = {baseModel.Name}Get(con, cmd, dto.Id);");
      }
      _sb.AppendLine("return HasChangedWork(dto, dtoDb);");
    }

    public override void BuildHistGetMethod(GeneratorBaseModel baseModel, bool async,
  List<string> internalMethodSignatures, List<string> externalMethodSignatures)
    {
      // TODO : Implement
    }

  }
}
