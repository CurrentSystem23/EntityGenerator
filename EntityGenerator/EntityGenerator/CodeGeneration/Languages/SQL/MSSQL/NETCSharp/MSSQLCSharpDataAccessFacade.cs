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
    public override void BuildInternalGetFacadeMethod(Schema schema, MethodType methodType, string name, bool isTable, bool async, List<string> internalMethodSignatures, List<Column> parameters)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";
      string parametersStr = ParameterHelper.GetParametersString(parameters);

      // Gets
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(con, cmd, new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(3));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(con, cmd, whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

      if (isTable)
      {
        // Get
        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(4));
        _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Get{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");

        // Get by GUID
        _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(5));
        _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Get{(async ? "Async" : "")}(con, cmd, globalId){(async ? ".ConfigureAwait(false)" : "")};");
      }
    }

    public override void BuildInternalSaveFacadeMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Save{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalDeleteFacadeMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(whereClause){(async ? ".ConfigureAwait(false)" : "")};");
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(2));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(con, cmd, whereClause){(async ? ".ConfigureAwait(false)" : "")};");

      // Delete by ID
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}().{name}Delete{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalMergeFacadeMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Merge{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalCountFacadeMethod(Schema schema, string name, bool isTable, bool async,
  List<string> internalMethodSignatures, List<Column> parameters)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";
      string parametersStr = ParameterHelper.GetParametersString(parameters);

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{name}GetCount{(async ? "Async" : string.Empty)}(whereClause{(parametersStr != "" ? $", {parametersStr}" : "")}){(async ? ".ConfigureAwait(false)" : string.Empty)};");

    }

    public override void BuildInternalHasChangedFacadeMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";

      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HasChanged{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
    }

    public override void BuildInternalHistFacadeMethod(Schema schema, string name, bool isTable, bool async,
      List<string> internalMethodSignatures)
    {
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";

      // HistGets
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(0));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistGets{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");

      // HistEntryGet
      _backendLanguage.OpenMethod(internalMethodSignatures.ElementAt(1));
      _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistEntryGet{(async ? "Async" : "")}(con, cmd, histId){(async ? ".ConfigureAwait(false)" : "")};");
    }

  }
}
