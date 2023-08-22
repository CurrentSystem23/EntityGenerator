using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessFacadeGenerator
  {
    protected void BuildDataAccessFacadeClassHeader(Schema schema)
    {
      List<string> imports = new()
      {
        "System",
        "System.Threading.Tasks",
        "System.Collections.Generic",
        "Microsoft.Data.SqlClient",
        "Microsoft.Extensions.DependencyInjection",
        $"Microsoft.Extensions.Logging",
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado",
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
      };

      BuildImports(imports);

      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess");

      OpenClass("DataAccess", "IDataAccessInternal", isStatic: false, isPartial: true);
      _sb.AppendLine($"private readonly ILogger<DataAccess> _logger;");
      _sb.AppendLine("private readonly IServiceProvider _provider;");
    }

    protected void BuildDataAccessFacadeClassMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync, int databaseId)
    {
      if (!MethodHelper.IsValidMethodType(baseModel.DbObjectType, methodType))
      {
        return;
      }

      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";
      List<string> externalMethodSignatures = GetExternalMethodSignatures(baseModel, methodType, isAsync,
        $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.DaoName}");
      List<string> internalMethodSignatures = _databaseLanguages[databaseId].GetInternalMethodSignatures(baseModel, methodType, isAsync, useNamespace: true);


      switch (methodType)
      {
        case MethodType.GET:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguages[databaseId].BuildInternalGetFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.SAVE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Save{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguages[databaseId].BuildInternalSaveFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.DELETE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Delete{(isAsync ? "Async" : "")}(id){(isAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguages[databaseId].BuildInternalDeleteFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Merge{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");
          _databaseLanguages[databaseId].BuildInternalMergeFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;
          
        case MethodType.COUNT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}GetCount{(isAsync ? "Async" : string.Empty)}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(isAsync ? ".ConfigureAwait(false)" : string.Empty)};");
          _databaseLanguages[databaseId].BuildInternalCountFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HasChanged{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguages[databaseId].BuildInternalHasChangedFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.BUlK_INSERT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}BulkInsert{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}BulkInsert{(isAsync ? "Async" : "")}(dtos, identityInsert){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}_TempBulkInsert{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(3));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}_TempBulkInsert{(isAsync ? "Async" : "")}(dtos, identityInsert){(isAsync ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}BulkMerge{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}BulkMerge{(isAsync ? "Async" : "")}(dtos, identityInsert){(isAsync ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_UPDATE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}BulkUpdate{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.HIST_GET:
          // HistGet
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HistGets{(isAsync ? "Async" : "")}(id){(isAsync ? ".ConfigureAwait(false)" : "")};");

          // HistEntryGet
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HistEntryGet{(isAsync ? "Async" : "")}(histId){(isAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguages[databaseId].BuildInternalHistFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        default:
          break;
      }
    }

    void IDataAccessFacadeGenerator.BuildDependencyInjectionBaseFile()
    {
      List<string> imports = new()
      {
        "Microsoft.Extensions.DependencyInjection"
      };

      foreach (DatabaseLanguageBase databaseLanguage in _databaseLanguages)
      {
        imports.Add($"{_profile.Global.ProjectName}.DataAccess.{databaseLanguage.Name}.Helper");
      }

      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.Helper");
      OpenClass($"DataAccessInitializer", isStatic: true, isPartial: true);

      OpenMethod("InitializeGeneratedDataAccess(IServiceCollection services)", returnType: "void", isStatic: true);
      foreach (DatabaseLanguageBase databaseLanguage in _databaseLanguages)
      {
        _sb.AppendLine($"DataAccess{databaseLanguage.Name}Initializer.InitializeGeneratedDataAccess(services);");
      }
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableClassHeader(Schema schema, Table table)
    {
      BuildDataAccessFacadeClassHeader(schema);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionClassHeader(Schema schema, Function function)
    {
      BuildDataAccessFacadeClassHeader(schema);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionClassHeader(Schema schema, Function tableValuedFunction)
    {
      BuildDataAccessFacadeClassHeader(schema);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewClassHeader(Schema schema, View view)
    {
      BuildDataAccessFacadeClassHeader(schema);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableClassMethod(Schema schema, Table table, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeClassMethod(new GeneratorBaseModel(table, schema), methodType, isAsync, databaseId);
    }
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionClassMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeClassMethod(new GeneratorBaseModel(function, schema), methodType, isAsync, databaseId);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionClassMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeClassMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync, databaseId);
    }
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewClassMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeClassMethod(new GeneratorBaseModel(view, schema), methodType, isAsync, databaseId);
    }
  }
}
