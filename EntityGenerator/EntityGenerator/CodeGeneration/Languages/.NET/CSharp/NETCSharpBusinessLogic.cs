using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp.Helper;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IBusinessLogicGenerator
  {
    protected string GetFullFunctionPrefix(Schema schema, string name)
    {
      return $"{schema.Name.FirstCharToUpper()}_{name}";
    }

    protected void BuildClassMethod(Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));
      string daoName = TypeHelper.GetDaoType(name, isTable);
      string dataAccessName = $"((Common.DataAccess.Interfaces.Ado.{schema.Name}.I{daoName})_dataAccess).{name}";

      switch (methodType)
      {
        case MethodType.GET:
          OpenMethod($"public {(async ? $"async Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{name}[] orderBy)");
          _sb.AppendLine($"var dto = {(async ? "await " : string.Empty)}{dataAccessName}Gets{(async ? "Async" : string.Empty)}({(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(async ? ".ConfigureAwait(false)" : string.Empty)};");
          BuildTraceLogCall($"Logic.{name}Gets{(async ? "Async" : "")}()", $"{(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy", async);
          _sb.AppendLine($"return dto;");
          CloseMethod();

          OpenMethod($"public {(async ? $"async Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy)");
          _sb.AppendLine($"var dto = {(async ? "await " : string.Empty)}{dataAccessName}Gets{(async ? "Async" : string.Empty)}({(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(async ? ".ConfigureAwait(false)" : string.Empty)};");
          BuildTraceLogCall($"Logic.{name}Gets{(async ? "Async" : "")}()", $"dto, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy", async);
          _sb.AppendLine($"return dto;");
          CloseMethod();

          if (isTable) {
            OpenMethod($"public {(async ? $"async Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}Get{(async ? "Async" : "")}({(_profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            _sb.AppendLine($"var dto = {(async ? "await " : "")}{dataAccessName}Get{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");
            BuildTraceLogCall($"Logic.{name}Get{(async ? "Async" : "")}({{id}})", $"dto", async);
            _sb.AppendLine($"return ({dtoName})dto ?? null;");
            CloseMethod();

            OpenMethod($"public {(async ? $"async Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}Get{(async ? "Async" : "")}(Guid globalId);");
            _sb.AppendLine($"var dto = {(async ? "await " : "")}{dataAccessName}Get{(async ? "Async" : "")}(globalId){(async ? ".ConfigureAwait(false)" : "")};");
            BuildTraceLogCall($"Logic.{name}Get{(async ? "Async" : "")}({{globalId}})", $"dto", async);
            _sb.AppendLine($"return ({dtoName})dto ?? null;");
            CloseMethod();
          }
          break;

        case MethodType.SAVE:
          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}Save{(async ? "Async" : "")}({dtoName} dto);");
          BuildTraceLogCall($"Logic.{name}Save{(async ? "Async" : "")}()", $"dto", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}Save{(async ? "Async" : "")}(({dtoName})dto){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.DELETE:
          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}Delete{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          BuildTraceLogCall($"Logic.{name}Delete{(async ? "Async" : "")}({{id}})", string.Empty, async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}Delete{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();
          break;

        case MethodType.MERGE:
          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}Merge{(async ? "Async" : "")}({dtoName} dto);");
          BuildTraceLogCall($"Logic.{name}Merge{(async ? "Async" : "")}()", $"dto", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}Merge{(async ? "Async" : "")}(({dtoName}) dto){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

         break;

        case MethodType.COUNT:
          OpenMethod($"public {(async ? $"async Task<long>" : $"long")} {GetFullFunctionPrefix(schema, name)}GetCount{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")})");
          _sb.AppendLine($"var count = {(async ? "await " : string.Empty)}((Common.DataAccess.Interfaces.Ado.{schema.Name}.I{daoName})_dataAccess).{name}GetCount{(async ? "Async" : "")}({(parametersStr != "" ? $"{parametersStr}" : "")}){(async ? ".ConfigureAwait(false)" : string.Empty)};");
          BuildTraceLogCall($"Logic.{GetFullFunctionPrefix(schema, name)}GetCount{(async ? "Async" : string.Empty)}()", $"{(parametersStr != "" ? $"{parametersStr}, " : "")}count", async);
          _sb.AppendLine("return count;");
          CloseMethod();
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod($"public {(async ? "async Task<bool>" : "bool")} {GetFullFunctionPrefix(schema, name)}HasChanged{(async ? "Async" : "")}({dtoName} dto);");
          BuildTraceLogCall($"Logic.{name}HasChanged{(async ? "Async" : "")}()", $"dto", async);
          _sb.AppendLine($"return {(async ? "await " : "")}{dataAccessName}HasChanged{(async ? "Async" : "")}(({dtoName})dto){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.BUlK_INSERT:
          _sb.AppendLine($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          BuildTraceLogCall($"Logic.{name}BulkInsert{(async ? "Async" : "")}()", $"dtos", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}BulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          BuildTraceLogCall($"Logic.{name}BulkInsert{(async ? "Async" : "")}()", $"dtos", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}BulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          BuildTraceLogCall($"Logic.{name}_TempBulkInsert{(async ? "Async" : "")}()", $"dtos", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}TempBulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          BuildTraceLogCall($"Logic.{name}_TempBulkInsert{(async ? "Async" : "")}()", $"dtos", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}TempBulkInsert{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.BULK_MERGE:
          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          BuildTraceLogCall($"Logic.{name}BulkMerge{(async ? "Async" : "")}()", $"dtos", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}BulkMerge{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          BuildTraceLogCall($"Logic.{name}BulkMerge{(async ? "Async" : "")}()", $"dtos", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}BulkMerge{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.BULK_UPDATE:
          OpenMethod($"public {(async ? "async Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkUpdate{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          BuildTraceLogCall($"Logic.{name}BulkUpdate{(async ? "Async" : "")}()", $"dtos", async);
          _sb.AppendLine($"{(async ? "await " : "")}{dataAccessName}BulkUpdate{(async ? "Async" : "")}(dtos){(async ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.HIST_GET:
          OpenMethod($"public {(async ? $"async Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}HistGets{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          _sb.AppendLine($"var dto = {(async ? "await " : "")}{dataAccessName}HistGets{(async ? "Async" : "")}(id){(async ? ".ConfigureAwait(false)" : "")};");
          BuildTraceLogCall($"Logic.{name}HistGets{(async ? "Async" : "")}({{id}})", $"dto", async);
          _sb.AppendLine($"return dto;");
          CloseMethod();

          OpenMethod($"public {(async ? $"async Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}HistEntryGet{(async ? "Async" : "")}(long histId);");
          _sb.AppendLine($"var dto = {(async ? "await " : "")}{dataAccessName}HistEntryGet{(async ? "Async" : "")}(histId){(async ? ".ConfigureAwait(false)" : "")};");
          BuildTraceLogCall($"Logic.{name}HistEntryGet{(async ? "Async" : "")}({{histId}})", $"dto", async);
          _sb.AppendLine($"return ({name}HistDto)dto ?? null;");
          CloseMethod();

          break;

        default:
          break;
      }
    }

    protected void BuildInterfaceMethod(GeneratorParameterObject parameters, MethodType methodType)
    {
      List<string> methodSignatures = GetMethodSignatures(parameters.Schema, methodType, parameters.Name, parameters.IsTable, parameters.IsAsync, 
        GetFullFunctionPrefix(parameters.Schema, parameters.Name),
        ParameterHelper.GetParametersString(parameters.Parameters),
        ParameterHelper.GetParametersStringWithType(parameters.Parameters, this));

      foreach (string methodSignature in methodSignatures)
      {
        _sb.AppendLine(methodSignature);
      }
    }

    void IBusinessLogicGenerator.BuildInterfaceHeader(Schema schema)
    {
      List<string> imports = new()
      {
        $"{_profile.Global.ProjectName}.Common.DTOs.{schema.Name}",
        "System.Threading.Tasks",
        "System.Collections.Generic",
        "System",
      };

      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.Interfaces");
      OpenInterface("ILogic", isPartial: true);
    }

    void IBusinessLogicGenerator.BuildClassHeader(Schema schema)
    {
      List<string> imports = new()
      {
        $"{_profile.Global.ProjectName}.Common.DTOs.{schema.Name}",
        "System.Threading.Tasks",
        "System.Collections.Generic",
        "System",
        "Microsoft.Extensions.Logging",
        $"{_profile.Global.ProjectName}.Common.Interfaces",
      };

      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.BusinessLogic");
      OpenClass($"Logic", isPartial: true, accessModifier: AccessType.INTERNAL);
    }

    void IBusinessLogicGenerator.BuildScalarFunctionClassMethod(Schema schema, Function function, MethodType methodType, bool isAsync)
    {
      BuildClassMethod(schema, methodType, function.Name, false, isAsync, ParameterHelper.GetParametersString(function.Parameters), ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IBusinessLogicGenerator.BuildScalarFunctionInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorParameterObject(function)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        MethodType = methodType
      }, methodType);
    }

    void IBusinessLogicGenerator.BuildTableClassMethod(Schema schema, Table table, MethodType methodType, bool isAsync)
    {
      BuildClassMethod(schema, methodType, table.Name, true, isAsync);
    }

    void IBusinessLogicGenerator.BuildTableInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorParameterObject(table)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = true,
        MethodType = methodType
      }, methodType);
    }

    void IBusinessLogicGenerator.BuildTableValuedFunctionClassMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync)
    {
      BuildClassMethod(schema, methodType, tableValuedFunction.Name, false, isAsync, ParameterHelper.GetParametersString(tableValuedFunction.Parameters), ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }

    void IBusinessLogicGenerator.BuildTableValuedFunctionInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorParameterObject(tableValuedFunction)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        MethodType = methodType
      }, methodType);
    }

    void IBusinessLogicGenerator.BuildViewClassMethod(Schema schema, View view, MethodType methodType, bool isAsync)
    {
      BuildClassMethod(schema, methodType, view.Name, false, isAsync);
    }

    void IBusinessLogicGenerator.BuildViewInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorParameterObject(view)
      {
        Schema = schema,
        IsAsync = isAsync,
        IsTable = false,
        MethodType = methodType
      }, methodType);
    }
  }
}
