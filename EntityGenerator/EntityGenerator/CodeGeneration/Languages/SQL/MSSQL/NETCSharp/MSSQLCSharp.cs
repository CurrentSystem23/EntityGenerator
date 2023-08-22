using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp.NET_6;
using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System.Collections.Generic;
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

    public override List<string> GetClientImports()
    {
      return new List<string> { "Microsoft.Data.SqlClient" };
    }

    public override void BuildBeforeSaveMethod()
    {
      _backendLanguage.OpenMethod("BeforeSave(SqlConnection connection, SqlCommand command, DtoBase dto)", "bool", Enums.AccessType.PUBLIC, isVirtual: true);
      _sb.AppendLine("return true;");
    }

    public override void BuildAfterSaveMethod()
    {
      _backendLanguage.OpenMethod("AfterSave(SqlConnection connection, SqlCommand command, DtoBase dto)", "bool", Enums.AccessType.PUBLIC, isVirtual: true);
      _sb.AppendLine("return true;");
    }

    public override List<string> GetInternalMethodSignatures(GeneratorBaseModel baseModel, MethodType methodType, bool async, bool useNamespace = false)
    {
      if (!MethodHelper.IsValidMethodType(baseModel.DbObjectType, methodType))
      {
        return new List<string>();
      }

      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(baseModel.Parameters, _backendLanguage);
      string prefix = $"{(useNamespace ? $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}." : "")}{baseModel.Name}";
      List<string> signatures = new();

      switch (methodType)
      {
        case MethodType.GET:          
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd{(parametersWithTypeStr != "" ? $", {parametersWithTypeStr}" : "")})");
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}(WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{baseModel.Name}[] orderBy)");
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}(WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{baseModel.Name}[] orderBy)");
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{baseModel.Name}[] orderBy)");

          if (baseModel.DbObjectType == DbObjectType.TABLE)
          {
            signatures.Add($"{(async ? $"Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {prefix}Get{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {(_profile.Global.GuidIndexing ? "Guid" : "long")} id)");
            signatures.Add($"{(async ? $"Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {prefix}Get{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, Guid globalId)");
          }
          break;
        case MethodType.SAVE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Save{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {baseModel.DtoName} dto)");
          break;
        case MethodType.DELETE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {(_profile.Global.GuidIndexing ? "Guid" : "long")} id)");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}(WhereClause whereClause)");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, WhereClause whereClause)");
          break;
        case MethodType.MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Merge{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {baseModel.DtoName} dto)");
          break;
        case MethodType.COUNT:
          signatures.Add($"{(async ? $"Task<long>" : $"long")} {prefix}GetCount{(async ? "Async" : string.Empty)}(WhereClause whereClause{(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")})");
          break;
        case MethodType.HAS_CHANGED:
          signatures.Add($"{(async ? "Task<bool>" : "bool")} {prefix}HasChanged{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {baseModel.DtoName} dto)");
          break;
        case MethodType.BUlK_INSERT:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert)");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert)");
          break;
        case MethodType.BULK_MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert)");
          break;
        case MethodType.BULK_UPDATE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkUpdate{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          break;
        case MethodType.HIST_GET:
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.HistDtoName}>>" : $"ICollection<{baseModel.HistDtoName}>")} {prefix}HistGets{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id)");
          signatures.Add($"{(async ? $"Task<{baseModel.HistDtoName}>" : $"{baseModel.HistDtoName}")} {prefix}HistEntryGet{(async ? "Async" : "")}(long histId)");
          break;
        default:
          break;
      }

      return signatures;
    }
  }
}
