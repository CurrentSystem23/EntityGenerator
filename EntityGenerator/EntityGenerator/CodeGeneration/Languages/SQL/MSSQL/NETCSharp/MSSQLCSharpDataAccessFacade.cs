using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.CodeGeneration.Languages.SQL.MSSQL.NETCSharp
{
  public partial class MSSQLCSharp : MSSQL
  {
    public override void BuildInternalGetFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";
      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);

      // Gets
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(async ? "Async" : "")}(con, cmd, new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(3));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(async ? "Async" : "")}(con, cmd, whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      if (baseModel.DbObjectType == DbObjectType.TABLE)
      {
        // Get
        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(4));
        _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Get{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");

        // Get by GUID
        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(5));
        _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Get{(async ? "Async" : "")}(con, cmd, globalId){(async ? ".ConfigureAwait(false)" : "")};");
      }
    }

    public override void BuildInternalSaveFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Save{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalDeleteFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Delete{(async ? "Async" : "")}(whereClause){(async ? ".ConfigureAwait(false)" : "")};");
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Delete{(async ? "Async" : "")}(con, cmd, whereClause){(async ? ".ConfigureAwait(false)" : "")};");

      // Delete by ID
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}().{baseModel.Name}Delete{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalMergeFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Merge{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalCountFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";
      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}GetCount{(async ? "Async" : string.Empty)}(whereClause{(parametersStr != "" ? $", {parametersStr}" : "")}){(async ? ".ConfigureAwait(false)" : string.Empty)};");

    }

    public override void BuildInternalHasChangedFacadeMethod(GeneratorBaseModel baseModel, bool async, List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HasChanged{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalHistFacadeMethod(GeneratorBaseModel baseModel, bool async,
      List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";

      // HistGets
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HistGets{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");

      // HistEntryGet
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HistEntryGet{(async ? "Async" : "")}(con, cmd, histId){(async ? ".ConfigureAwait(false)" : "")};");
    }

  }
}
