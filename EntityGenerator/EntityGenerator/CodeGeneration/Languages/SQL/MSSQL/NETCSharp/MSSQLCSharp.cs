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
    protected new CodeGeneration.Languages.NET.CSharp.NETCSharp _backendLanguage
    {
      get { return (CodeGeneration.Languages.NET.CSharp.NETCSharp)base._backendLanguage; }
    }

    public MSSQLCSharp(StringBuilder sb, CodeGeneration.Languages.NET.CSharp.NETCSharp backendLanguage, ProfileDto profile)
      : base(sb, backendLanguage, profile)
    {
    }

    public static List<string> GetClientImports()
    {
      return new List<string> { "Microsoft.Data.SqlClient" };
    }

    public override void BuildBeforeSaveMethod()
    {
      _backendLanguage.OpenMethod("virtual BeforeSave(SqlConnection connection, SqlCommand command, DtoBase dto)", "bool", Enums.AccessType.PUBLIC);
      _sb.AppendLine("return true;");
    }

    public override void BuildAfterSaveMethod()
    {
      _backendLanguage.OpenMethod("virtual AfterSave(SqlConnection connection, SqlCommand command, DtoBase dto)", "bool", Enums.AccessType.PUBLIC);
      _sb.AppendLine("return true;");
    }

    public override List<string> GetInternalMethodSignatures(Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null, bool useNamespace = false)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));
      string prefix = $"{(useNamespace ? $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}." : "")}{name}";
      List<string> signatures = new();

      switch (methodType)
      {
        case MethodType.GET:
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {prefix}Gets{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd{(parametersWithTypeStr != "" ? $", {parametersWithTypeStr}" : "")});");
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {prefix}Gets{(async ? "Async" : "")}(WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{name}[] orderBy);");
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {prefix}Gets{(async ? "Async" : "")}(WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {prefix}Gets{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");

          if (isTable)
          {
            signatures.Add($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {prefix}Get{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {(_profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            signatures.Add($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {prefix}Get{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, Guid globalId);");
          }
          break;
        case MethodType.SAVE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Save{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {name}Dto dto);");
          break;
        case MethodType.DELETE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {(_profile.Global.GuidIndexing ? "Guid" : "long")} id);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}(WhereClause whereClause);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, WhereClause whereClause);");
          break;
        case MethodType.MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Merge{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {name}Dto dto);");
          break;
        case MethodType.COUNT:
          signatures.Add($"{(async ? $"Task<long>" : $"long")} {prefix}GetCount{(async ? "Async" : string.Empty)}(WhereClause whereClause{(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")});");
          break;
        case MethodType.HAS_CHANGED:
          signatures.Add($"{(async ? "Task<bool>" : "bool")} {prefix}HasChanged{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {name}Dto dto);");
          break;
        case MethodType.BUlK_INSERT:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_UPDATE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkUpdate{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          break;
        case MethodType.HIST_GET:
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {prefix}HistGets{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          signatures.Add($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {prefix}HistEntryGet{(async ? "Async" : "")}(long histId);");
          break;
        default:
          break;
      }

      return signatures;
    }
  }
}
