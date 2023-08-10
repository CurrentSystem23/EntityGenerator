using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Languages.SQL;
using EntityGenerator.CodeGeneration.Languages.SQL.MSSQL.NETCSharp;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessFacadeGenerator
  {
    protected List<string> GetFacadeMethodSignatures(GeneratorBaseModel baseModel, MethodType methodType, bool async, string prefix)
    {
      List<string> signatures = new();
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(baseModel.Parameters, this);

      switch (methodType)
      {
        case MethodType.GET:
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{baseModel.Name}[] orderBy);");
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{baseModel.Name}[] orderBy);");
          if (baseModel.DbObjectType == DbObjectType.TABLE)
          {
            signatures.Add($"{(async ? $"Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {prefix}Get{(async ? "Async" : "")}({(_profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            signatures.Add($"{(async ? $"Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {prefix}Get{(async ? "Async" : "")}(Guid globalId);");
          }
          break;
        case MethodType.SAVE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Save{(async ? "Async" : "")}({baseModel.DtoName} dto);");
          break;
        case MethodType.DELETE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          break;
        case MethodType.MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Merge{(async ? "Async" : "")}({baseModel.DtoName} dto);");
          break;
        case MethodType.COUNT:
          signatures.Add($"{(async ? $"Task<long>" : $"long")} {prefix}GetCount{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")});");
          break;
        case MethodType.HAS_CHANGED:
          signatures.Add($"{(async ? "Task<bool>" : "bool")} {prefix}HasChanged{(async ? "Async" : "")}({baseModel.DtoName} dto);");
          break;
        case MethodType.BUlK_INSERT:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_UPDATE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkUpdate{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          break;
        case MethodType.HIST_GET:
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.HistDtoName}>>" : $"ICollection<{baseModel.HistDtoName}>")} {prefix}HistGets{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          signatures.Add($"{(async ? $"Task<{baseModel.HistDtoName}>" : $"{baseModel.HistDtoName}")} {prefix}HistEntryGet{(async ? "Async" : "")}(long histId);");
          break;
        default:
          break;
      }

      return signatures;
    }
    protected void BuildInterfaceBase(Schema schema)
    {
      List<string> imports = new List<string>
      {
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
        $"{_profile.Global.ProjectName}.Common.DTOs.{schema.Name}",
        "System.Collections.Generic",
        "Microsoft.Data.SqlClient",
        "System.Threading.Tasks",
        "System",
      };

      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}");
    }

    protected void BuildExternalInterfaceHeader(Schema schema, string name, bool isTable)
    {
      BuildInterfaceBase(schema);
      OpenInterface($"I{TypeHelper.GetDaoType(name, isTable)}", isPartial: true);
    }
    protected void BuildInternalInterfaceHeader(Schema schema, string name, bool isTable)
    {
      if (!_profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
      {
        BuildInterfaceBase(schema);
      }
      OpenInterface($"I{TypeHelper.GetInternalDaoType(name, isTable)}", baseInterface: $"I{TypeHelper.GetDaoType(name, isTable)}",isPartial: true);
    }

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

    protected void BuildDataAccessFacadeExternalMethod(GeneratorBaseModel baseModel, MethodType methodType, bool async)
    {
      foreach (string methodSignature in GetFacadeMethodSignatures(baseModel, methodType, async, baseModel.Name))
      {
        _sb.AppendLine(methodSignature);
      }
    }

    protected void BuildDataAccessFacadeInternalMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync, int databaseId)
    {
      foreach (string methodSignature in DatabaseLanguages[databaseId].GetInternalMethodSignatures(baseModel.Schema, methodType, baseModel.Name, 
        baseModel.DbObjectType, isAsync, ParameterHelper.GetParametersString(baseModel.Parameters), 
        ParameterHelper.GetParametersStringWithType(baseModel.Parameters, this), true))
      {
        _sb.AppendLine(methodSignature);
      }
    }

    protected void BuildDataAccessFacadeClassMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync, int databaseId)
    {
      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.InternalDaoName}";
      List<string> externalMethodSignatures = GetFacadeMethodSignatures(baseModel, methodType, isAsync,
        $"Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.DaoName}");
      List<string> internalMethodSignatures = DatabaseLanguages[databaseId].GetInternalMethodSignatures(baseModel.Schema, methodType, baseModel.Name, baseModel.DbObjectType, isAsync, useNamespace: true);


      switch (methodType)
      {
        case MethodType.GET:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalGetFacadeMethod(baseModel.Schema, methodType, baseModel.Name, baseModel.DbObjectType, isAsync, internalMethodSignatures, baseModel.Parameters);
          break;

        case MethodType.SAVE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Save{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalSaveFacadeMethod(baseModel.Schema, baseModel.Name, baseModel.DbObjectType, isAsync, internalMethodSignatures);
          break;

        case MethodType.DELETE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Delete{(isAsync ? "Async" : "")}(id){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalDeleteFacadeMethod(baseModel.Schema, baseModel.Name, baseModel.DbObjectType, isAsync, internalMethodSignatures);
          break;

        case MethodType.MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Merge{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");
          DatabaseLanguages[databaseId].BuildInternalMergeFacadeMethod(baseModel.Schema, baseModel.Name, baseModel.DbObjectType, isAsync, internalMethodSignatures);
          break;
          
        case MethodType.COUNT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}GetCount{(isAsync ? "Async" : string.Empty)}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(isAsync ? ".ConfigureAwait(false)" : string.Empty)};");
          DatabaseLanguages[databaseId].BuildInternalCountFacadeMethod(baseModel.Schema, baseModel.Name, baseModel.DbObjectType, isAsync, internalMethodSignatures, baseModel.Parameters);
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HasChanged{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalHasChangedFacadeMethod(baseModel.Schema, baseModel.Name, baseModel.DbObjectType, isAsync, internalMethodSignatures);
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

          DatabaseLanguages[databaseId].BuildInternalHistFacadeMethod(baseModel.Schema, baseModel.Name, baseModel.DbObjectType, isAsync, internalMethodSignatures);
          break;

        default:
          break;
      }
    }

    void IDataAccessFacadeGenerator.BuildWhereParameterClass(ProfileDto profile)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildADOInterface(ProfileDto profile, Database db)
    {
      BuildNameSpace($"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado");

      List<string> baseInterfaces = new();
      List<string> baseInterfacesInternal = new() { "IDataAccess" };

      foreach (Schema schema in db.Schemas)
      {
        //foreach (BaseModel baseModel in schema.Tables.Concat<BaseModel>(schema.Views).Concat(schema.Functions).ToList())
        foreach (Table table in schema.Tables)
        {
          baseInterfaces.Add($"Ado.{schema.Name}.I{table.Name}Dao");
          baseInterfacesInternal.Add($"Ado.{schema.Name}.I{table.Name}InternalDao");
        }
        foreach (View view in schema.Views)
        {
          baseInterfaces.Add($"Ado.{schema.Name}.I{view.Name}Dao");
          baseInterfacesInternal.Add($"Ado.{schema.Name}.I{view.Name}InternalDao");
        }
        foreach (Function tableValuedFunction in schema.FunctionsTableValued)
        {
          baseInterfaces.Add($"Ado.{schema.Name}.I{tableValuedFunction.Name}Dao");
          baseInterfacesInternal.Add($"Ado.{schema.Name}.I{tableValuedFunction.Name}InternalDao");
        }
        foreach (Function scalarFunction in schema.FunctionsScalar)
        {
          baseInterfaces.Add($"Ado.{schema.Name}.I{scalarFunction.Name}DaoS");
          baseInterfacesInternal.Add($"Ado.{schema.Name}.I{scalarFunction.Name}InternalDaoS");
        }
      }

      OpenInterface("IDataAccess", String.Join(", ", baseInterfaces), true);
      OpenInterface("IDataAccessInternal", String.Join(", ", baseInterfacesInternal), true);
    }

    #region External Interface
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableExternalInterfaceHeader(Schema schema, Table table)
    {
      BuildExternalInterfaceHeader(schema, table.Name, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionExternalInterfaceHeader(Schema schema, Function function)
    {
      BuildExternalInterfaceHeader(schema, function.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceHeader(Schema schema, Function tableValuedFunction)
    {
      BuildExternalInterfaceHeader(schema, tableValuedFunction.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewExternalInterfaceHeader(Schema schema, View view)
    {
      BuildExternalInterfaceHeader(schema, view.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionExternalInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(function, schema), methodType, isAsync);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableExternalInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(table, schema), methodType, isAsync);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewExternalInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(view, schema), methodType, isAsync);
    }
    #endregion

    #region Internal Interface
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableInternalInterfaceHeader(Schema schema, Table table)
    {
      BuildInternalInterfaceHeader(schema, table.Name, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionInternalInterfaceHeader(Schema schema, Function function)
    {
      BuildInternalInterfaceHeader(schema, function.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(Schema schema, Function tableValuedFunction)
    {
      BuildInternalInterfaceHeader(schema, tableValuedFunction.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewInternalInterfaceHeader(Schema schema, View view)
    {
      BuildInternalInterfaceHeader(schema, view.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableInternalInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(table, schema), methodType, isAsync, databaseId);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionInternalInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(function, schema), methodType, isAsync, databaseId);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync, databaseId);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewInternalInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(view, schema), methodType, isAsync, databaseId);
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
    #endregion
  }
}
