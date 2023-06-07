using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessFacadeGenerator
  {
    protected void BuildInterfaceBase(ProfileDto profile, Schema schema)
    {
      List<string> imports = new List<string>
      {
        $"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
        $"{profile.Global.ProjectName}.Common.DTOs.{schema.Name}",
        "System.Collections.Generic",
        "Microsoft.Data.SqlClient",
        "System.Threading.Tasks",
        "System",
      };

      BuildImports(imports);
      BuildNameSpace($"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}");
    }

    protected void BuildExternalInterfaceHeader(ProfileDto profile, Schema schema, string name, bool isTable)
    {
      BuildInterfaceBase(profile, schema);
      OpenInterface($"I{TypeHelper.GetDaoType(name, isTable)}", isPartial: true);
    }
    protected void BuildInternalInterfaceHeader(ProfileDto profile, Schema schema, string name, bool isTable)
    {
      if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
      {
        BuildInterfaceBase(profile, schema);
      }
      OpenInterface($"I{TypeHelper.GetInternalDaoType(name, isTable)}", baseInterface: $"I{TypeHelper.GetDaoType(name, isTable)}",isPartial: true);
    }

    protected void BuildDataAccessFacadeClassHeader(ProfileDto profile, Schema schema)
    {
      List<string> imports = new()
      {
        "System",
        "System.Threading.Tasks",
        "System.Collections.Generic",
        "Microsoft.Data.SqlClient",
        "Microsoft.Extensions.DependencyInjection",
        $"Microsoft.Extensions.Logging",
        $"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado",
        $"{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
      };

      BuildImports(imports);

      BuildNameSpace($"{profile.Global.ProjectName}.DataAccess");

      OpenClass("DataAccess", "IDataAccessInternal", false, true);
      _sb.AppendLine($"    private readonly ILogger<DataAccess> _logger;");
      _sb.AppendLine("    private readonly IServiceProvider _provider;");
    }

    protected void BuildDataAccessFacadeExternalMethod(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      foreach (string methodSignature in GetMethodSignatures(profile, schema, methodType, name, isTable, async, name, parametersStr, parametersWithTypeStr))
      {
        _sb.AppendLine(methodSignature);
      }
    }

    protected void BuildDataAccessFacadeInternalMethod(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      foreach (string methodSignature in GetInternalMethodSignatures(profile, schema, methodType, name, isTable, async, parametersStr, parametersWithTypeStr, true))
      {
        _sb.AppendLine(methodSignature);
      }
    }

    protected void BuildDataAccessFacadeClassMethod(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      string externalPrefix = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetDaoType(name, isTable)}";
      List<string> externalMethodSignatures = GetMethodSignatures(profile, schema, methodType, name, true, true, externalPrefix);
      List<string> internalMethodSignatures = GetInternalMethodSignatures(profile, schema, methodType, name, true, true, useNamespace: true);
      BuildDataAccessFacadeClassMethodBody(profile, schema, methodType, name, true, true, externalMethodSignatures, internalMethodSignatures, parametersStr, parametersWithTypeStr);
    }

    protected void BuildDataAccessFacadeClassMethodBody(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, List<string> externalMethodSignatures, List<string> internalMethodSignatures, string parametersStr = null, string parametersWithTypeStr = null)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));
      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{schema.Name}.I{TypeHelper.GetInternalDaoType(name, isTable)}";

      switch (methodType)
      {
        case MethodType.GET:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(con, cmd, new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(3));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Gets{(async ? "Async" : "")}(con, cmd, whereClause, distinct, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : "")};");

          if (isTable)
          {
            // Get
            OpenMethod(externalMethodSignatures.ElementAt(2));
            _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Get{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");
            OpenMethod(internalMethodSignatures.ElementAt(4));
            _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Get{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");

            // Get by GUID
            OpenMethod(externalMethodSignatures.ElementAt(3));
            _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Get{(async ? "Async" : "")}(globalId){(async ? ".ConfigureAwait(false)" : "")};");
            OpenMethod(internalMethodSignatures.ElementAt(5));
            _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Get{(async ? "Async" : "")}(con, cmd, globalId){(async ? ".ConfigureAwait(false)" : "")};");
          }
          break;

        case MethodType.SAVE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Save{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Save{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.DELETE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}().{name}Delete{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(whereClause){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Delete{(async ? "Async" : "")}(con, cmd, whereClause){(async ? ".ConfigureAwait(false)" : "")};");

          break;

        case MethodType.MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Merge{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}Merge{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
        break;

        case MethodType.COUNT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{name}GetCount{(async ? "Async" : string.Empty)}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(async ? ".ConfigureAwait(false)" : string.Empty)};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{name}GetCount{(async ? "Async" : string.Empty)}(whereClause{(parametersStr != "" ? $", {parametersStr}" : "")}){(async ? ".ConfigureAwait(false)" : string.Empty)};");
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HasChanged{(async ? "Async" : "")}(dto){(async ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HasChanged{(async ? "Async" : "")}(con, cmd, dto){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BUlK_INSERT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkInsert{(async ? "Async" : "")}(dtos, identityInsert){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(2));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}_TempBulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(3));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}_TempBulkInsert{(async ? "Async" : "")}(dtos, identityInsert){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkMerge{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkMerge{(async ? "Async" : "")}(dtos, identityInsert){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.BULK_UPDATE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}BulkUpdate{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          break;

        case MethodType.HIST_GET:
          // HistGet
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistGets{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistGets{(async ? "Async" : "")}(con, cmd, id){(async ? ".ConfigureAwait(false)" : "")};");

          // HistEntryGet
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistEntryGet{(async ? "Async" : "")}(histId){(async ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(internalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(async ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{name}HistEntryGet{(async ? "Async" : "")}(con, cmd, histId){(async ? ".ConfigureAwait(false)" : "")};");
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
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableExternalInterfaceHeader(ProfileDto profile, Schema schema, Table table)
    {
      BuildExternalInterfaceHeader(profile, schema, table.Name, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function function)
    {
      BuildExternalInterfaceHeader(profile, schema, function.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      BuildExternalInterfaceHeader(profile, schema, tableValuedFunction.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewExternalInterfaceHeader(ProfileDto profile, Schema schema, View view)
    {
      BuildExternalInterfaceHeader(profile, schema, view.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(profile, schema, methodType, function.Name, false, true, ParameterHelper.GetParametersString(function.Parameters), ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableExternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(profile, schema, methodType, table.Name, true, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(profile, schema, methodType, tableValuedFunction.Name, false, true, ParameterHelper.GetParametersString(tableValuedFunction.Parameters), ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewExternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      BuildDataAccessFacadeExternalMethod(profile, schema, methodType, view.Name, false, true);
    }
    #endregion

    #region Internal Interface
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableInternalInterfaceHeader(ProfileDto profile, Schema schema, Table table)
    {
      BuildInternalInterfaceHeader(profile, schema, table.Name, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function function)
    {
      BuildInternalInterfaceHeader(profile, schema, function.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      BuildInternalInterfaceHeader(profile, schema, tableValuedFunction.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewInternalInterfaceHeader(ProfileDto profile, Schema schema, View view)
    {
      BuildInternalInterfaceHeader(profile, schema, view.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableInternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      BuildDataAccessFacadeInternalMethod(profile, schema, methodType, table.Name, true, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      BuildDataAccessFacadeInternalMethod(profile, schema, methodType, function.Name, false, true, ParameterHelper.GetParametersString(function.Parameters), ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      BuildDataAccessFacadeInternalMethod(profile, schema, methodType, tableValuedFunction.Name, false, true, ParameterHelper.GetParametersString(tableValuedFunction.Parameters), ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewInternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      BuildDataAccessFacadeInternalMethod(profile, schema, methodType, view.Name, false, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableClassHeader(ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionClassHeader(ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionClassHeader(ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewClassHeader(ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableClassMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      BuildDataAccessFacadeClassMethod(profile, schema, methodType, table.Name, true, true);
    }
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionClassMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      BuildDataAccessFacadeClassMethod(profile, schema, methodType, function.Name, false, true, ParameterHelper.GetParametersString(function.Parameters), ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionClassMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      BuildDataAccessFacadeClassMethod(profile, schema, methodType, tableValuedFunction.Name, false, true, ParameterHelper.GetParametersString(tableValuedFunction.Parameters), ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }
    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewClassMethod(ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      BuildDataAccessFacadeClassMethod(profile, schema, methodType, view.Name, false, true);
    }
    #endregion
  }
}
