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
      _sb.AppendLine($"    private readonly ILogger<DataAccess> _logger;");
      _sb.AppendLine("    private readonly IServiceProvider _provider;");
    }

    protected void BuildDataAccessFacadeExternalMethod(Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      foreach (string methodSignature in GetMethodSignatures(schema, methodType, name, isTable, async, name, parametersStr, parametersWithTypeStr))
      {
        _sb.AppendLine(methodSignature);
      }
    }

    protected void BuildDataAccessFacadeInternalMethod(GeneratorParameterObject parameters, string parametersStr = null, string parametersWithTypeStr = null)
    {
      foreach (string methodSignature in DatabaseLanguages[parameters.DatabaseId].GetInternalMethodSignatures(parameters.Schema, parameters.MethodType, parameters.Name, parameters.IsTable, parameters.IsAsync, parametersStr, parametersWithTypeStr, true))
      {
        _sb.AppendLine(methodSignature);
      }
    }

    protected void BuildDataAccessFacadeClassMethod(GeneratorParameterObject parameters)
    {
      InternalGeneratorParameterObject internalParameters = TinyMapper.Map<InternalGeneratorParameterObject>(parameters);
      internalParameters.ExternalMethodSignatures = GetMethodSignatures(parameters.Schema, parameters.MethodType, parameters.Name, true, true, 
        $"Common.DataAccess.Interfaces.Ado.{parameters.Schema.Name}.I{TypeHelper.GetDaoType(parameters.Name, parameters.IsTable)}");
      internalParameters.InternalMethodSignatures = DatabaseLanguages[parameters.DatabaseId].GetInternalMethodSignatures(parameters.Schema, parameters.MethodType, parameters.Name, parameters.IsTable, parameters.IsAsync, useNamespace: true);

      BuildDataAccessFacadeClassMethodBody(internalParameters);
    }

    protected void BuildDataAccessFacadeClassMethodBody(InternalGeneratorParameterObject parameters)
    {
      string parametersStr = ParameterHelper.GetParametersString(parameters.Parameters);
      string dtoName = TypeHelper.GetDtoType(parameters.Name, parameters.IsTable, (parameters.MethodType == MethodType.HIST_GET));
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{parameters.Schema.Name}.I{TypeHelper.GetInternalDaoType(parameters.Name, parameters.IsTable)}";

      switch (parameters.MethodType)
      {
        case MethodType.GET:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}Gets{(parameters.IsAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}Gets{(parameters.IsAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[parameters.DatabaseId].BuildInternalGetFacadeMethod(parameters.Schema, parameters.MethodType, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures, parameters.Parameters);
          break;

        case MethodType.SAVE:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}Save{(parameters.IsAsync ? "Async" : "")}(dto){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[parameters.DatabaseId].BuildInternalSaveFacadeMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures);
          break;

        case MethodType.DELETE:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}Delete{(parameters.IsAsync ? "Async" : "")}(id){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[parameters.DatabaseId].BuildInternalDeleteFacadeMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures);
          break;

        case MethodType.MERGE:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}Merge{(parameters.IsAsync ? "Async" : "")}(dto){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          DatabaseLanguages[parameters.DatabaseId].BuildInternalMergeFacadeMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures);
          break;
          
        case MethodType.COUNT:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(parameters.IsAsync ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}GetCount{(parameters.IsAsync ? "Async" : string.Empty)}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(parameters.IsAsync ? ".ConfigureAwait(false)" : string.Empty)};");
          DatabaseLanguages[parameters.DatabaseId].BuildInternalCountFacadeMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures, parameters.Parameters);
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}HasChanged{(parameters.IsAsync ? "Async" : "")}(dto){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[parameters.DatabaseId].BuildInternalHasChangedFacadeMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures);
          break;

        case MethodType.BUlK_INSERT:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}BulkInsert{(parameters.IsAsync ? "Async" : "")}(dtos){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}BulkInsert{(parameters.IsAsync ? "Async" : "")}(dtos, identityInsert){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}_TempBulkInsert{(parameters.IsAsync ? "Async" : "")}(dtos){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(3));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}_TempBulkInsert{(parameters.IsAsync ? "Async" : "")}(dtos, identityInsert){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_MERGE:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}BulkMerge{(parameters.IsAsync ? "Async" : "")}(dtos){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}BulkMerge{(parameters.IsAsync ? "Async" : "")}(dtos, identityInsert){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_UPDATE:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}BulkUpdate{(parameters.IsAsync ? "Async" : "")}(dtos){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.HIST_GET:
          // HistGet
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}HistGets{(parameters.IsAsync ? "Async" : "")}(id){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          // HistEntryGet
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(parameters.IsAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{parameters.Name}HistEntryGet{(parameters.IsAsync ? "Async" : "")}(histId){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[parameters.DatabaseId].BuildInternalHistFacadeMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures);
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

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionExternalInterfaceMethod(Schema schema, Function function, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(schema, methodType, function.Name, false, true, ParameterHelper.GetParametersString(function.Parameters), ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableExternalInterfaceMethod(Schema schema, Table table, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(schema, methodType, table.Name, true, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(schema, methodType, tableValuedFunction.Name, false, true, ParameterHelper.GetParametersString(tableValuedFunction.Parameters), ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewExternalInterfaceMethod(Schema schema, View view, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(schema, methodType, view.Name, false, true);
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
      BuildDataAccessFacadeInternalMethod(new GeneratorParameterObject(table)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = true,
        DatabaseId = databaseId,
        MethodType = methodType,        
      });
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionInternalInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorParameterObject(function)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        DatabaseId = databaseId,
        MethodType = methodType
      },
        ParameterHelper.GetParametersString(function.Parameters),
        ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId)
    {

      BuildDataAccessFacadeInternalMethod(new GeneratorParameterObject(tableValuedFunction)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        DatabaseId = databaseId,
        MethodType = methodType
      },
        ParameterHelper.GetParametersString(tableValuedFunction.Parameters), 
        ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewInternalInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorParameterObject(view)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        DatabaseId = databaseId,
        MethodType = methodType
      });
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
      BuildDataAccessFacadeClassMethod(new GeneratorParameterObject(table)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        DatabaseId = databaseId,
        MethodType = methodType
      });
    }
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionClassMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeClassMethod(new GeneratorParameterObject(function)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        DatabaseId = databaseId,
        MethodType = methodType
      });
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionClassMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeClassMethod(new GeneratorParameterObject(tableValuedFunction)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        DatabaseId = databaseId,
        MethodType = methodType
      });
    }
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewClassMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeClassMethod(new GeneratorParameterObject(view)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        DatabaseId = databaseId,
        MethodType = methodType
      });
    }
    #endregion
  }
}
