using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.ModelObjects;
using System.Collections.Generic;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IBusinessLogicGenerator
  {
    protected string GetFullFunctionPrefix(Schema schema, string name)
    {
      return $"{schema.Name.FirstCharToUpper()}_{name}";
    }

    protected void BuildClassMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync)
    {
      if (!MethodHelper.IsValidMethodType(baseModel.DbObjectType, methodType))
      {
        return;
      }

      string dataAccessName = $"((Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.DaoName})_dataAccess).{baseModel.Name}";
      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(baseModel.Parameters, this);

      switch (methodType)
      {
        case MethodType.GET:
          OpenMethod($"public {(isAsync ? $"async Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}Gets{(isAsync ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{baseModel.Name}[] orderBy)");
          _sb.AppendLine($"var dto = {(isAsync ? "await " : string.Empty)}{dataAccessName}Gets{(isAsync ? "Async" : string.Empty)}({(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(isAsync ? ".ConfigureAwait(false)" : string.Empty)};");
          BuildTraceLogCall($"Logic.{baseModel.Name}Gets{(isAsync ? "Async" : "")}()", $"{(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy", isAsync);
          _sb.AppendLine($"return dto;");
          CloseMethod();

          OpenMethod($"public {(isAsync ? $"async Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}Gets{(isAsync ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{baseModel.Name}[] orderBy)");
          _sb.AppendLine($"var dto = {(isAsync ? "await " : string.Empty)}{dataAccessName}Gets{(isAsync ? "Async" : string.Empty)}({(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(isAsync ? ".ConfigureAwait(false)" : string.Empty)};");
          BuildTraceLogCall($"Logic.{baseModel.Name}Gets{(isAsync ? "Async" : "")}()", $"dto, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy", isAsync);
          _sb.AppendLine($"return dto;");
          CloseMethod();

          if (baseModel.DbObjectType == DbObjectType.TABLE) {
            OpenMethod($"public {(isAsync ? $"async Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}Get{(isAsync ? "Async" : "")}({(_profile.Global.GuidIndexing ? "Guid" : "long")} id)");
            _sb.AppendLine($"var dto = {(isAsync ? "await " : "")}{dataAccessName}Get{(isAsync ? "Async" : "")}(id){(isAsync ? ".ConfigureAwait(false)" : "")};");
            BuildTraceLogCall($"Logic.{baseModel.Name}Get{(isAsync ? "Async" : "")}({{id}})", $"dto", isAsync);
            _sb.AppendLine($"return ({baseModel.DtoName})dto ?? null;");
            CloseMethod();

            OpenMethod($"public {(isAsync ? $"async Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}Get{(isAsync ? "Async" : "")}(Guid globalId)");
            _sb.AppendLine($"var dto = {(isAsync ? "await " : "")}{dataAccessName}Get{(isAsync ? "Async" : "")}(globalId){(isAsync ? ".ConfigureAwait(false)" : "")};");
            BuildTraceLogCall($"Logic.{baseModel.Name}Get{(isAsync ? "Async" : "")}({{globalId}})", $"dto", isAsync);
            _sb.AppendLine($"return ({baseModel.DtoName})dto ?? null;");
            CloseMethod();
          }
          break;

        case MethodType.SAVE:
          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}Save{(isAsync ? "Async" : "")}({baseModel.DtoName} dto)");
          BuildTraceLogCall($"Logic.{baseModel.Name}Save{(isAsync ? "Async" : "")}()", $"dto", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}Save{(isAsync ? "Async" : "")}(({baseModel.DtoName})dto){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.DELETE:
          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}Delete{(isAsync ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id)");
          BuildTraceLogCall($"Logic.{baseModel.Name}Delete{(isAsync ? "Async" : "")}({{id}})", string.Empty, isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}Delete{(isAsync ? "Async" : "")}(id){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();
          break;

        case MethodType.MERGE:
          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}Merge{(isAsync ? "Async" : "")}({baseModel.DtoName} dto)");
          BuildTraceLogCall($"Logic.{baseModel.Name}Merge{(isAsync ? "Async" : "")}()", $"dto", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}Merge{(isAsync ? "Async" : "")}(({baseModel.DtoName}) dto){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

         break;

        case MethodType.COUNT:
          OpenMethod($"public {(isAsync ? $"async Task<long>" : $"long")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}GetCount{(isAsync ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")})");
          _sb.AppendLine($"var count = {(isAsync ? "await " : string.Empty)}((Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}.I{baseModel.DaoName})_dataAccess).{baseModel.Name}GetCount{(isAsync ? "Async" : "")}({(parametersStr != "" ? $"{parametersStr}" : "")}){(isAsync ? ".ConfigureAwait(false)" : string.Empty)};");
          BuildTraceLogCall($"Logic.{GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}GetCount{(isAsync ? "Async" : string.Empty)}()", $"{(parametersStr != "" ? $"{parametersStr}, " : "")}count", isAsync);
          _sb.AppendLine("return count;");
          CloseMethod();
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod($"public {(isAsync ? "async Task<bool>" : "bool")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}HasChanged{(isAsync ? "Async" : "")}({baseModel.DtoName} dto)");
          BuildTraceLogCall($"Logic.{baseModel.Name}HasChanged{(isAsync ? "Async" : "")}()", $"dto", isAsync);
          _sb.AppendLine($"return {(isAsync ? "await " : "")}{dataAccessName}HasChanged{(isAsync ? "Async" : "")}(({baseModel.DtoName})dto){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.BUlK_INSERT:
          _sb.AppendLine($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}BulkInsert{(isAsync ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          BuildTraceLogCall($"Logic.{baseModel.Name}BulkInsert{(isAsync ? "Async" : "")}()", $"dtos", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}BulkInsert{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}BulkInsert{(isAsync ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert)");
          BuildTraceLogCall($"Logic.{baseModel.Name}BulkInsert{(isAsync ? "Async" : "")}()", $"dtos", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}BulkInsert{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}_TempBulkInsert{(isAsync ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          BuildTraceLogCall($"Logic.{baseModel.Name}_TempBulkInsert{(isAsync ? "Async" : "")}()", $"dtos", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}TempBulkInsert{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}_TempBulkInsert{(isAsync ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert)");
          BuildTraceLogCall($"Logic.{baseModel.Name}_TempBulkInsert{(isAsync ? "Async" : "")}()", $"dtos", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}TempBulkInsert{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.BULK_MERGE:
          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}BulkMerge{(isAsync ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          BuildTraceLogCall($"Logic.{baseModel.Name}BulkMerge{(isAsync ? "Async" : "")}()", $"dtos", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}BulkMerge{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}BulkMerge{(isAsync ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert)");
          BuildTraceLogCall($"Logic.{baseModel.Name}BulkMerge{(isAsync ? "Async" : "")}()", $"dtos", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}BulkMerge{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.BULK_UPDATE:
          OpenMethod($"public {(isAsync ? "async Task" : "void")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}BulkUpdate{(isAsync ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos)");
          BuildTraceLogCall($"Logic.{baseModel.Name}BulkUpdate{(isAsync ? "Async" : "")}()", $"dtos", isAsync);
          _sb.AppendLine($"{(isAsync ? "await " : "")}{dataAccessName}BulkUpdate{(isAsync ? "Async" : "")}(dtos){(isAsync ? ".ConfigureAwait(false)" : "")};");
          CloseMethod();

          break;

        case MethodType.HIST_GET:
          OpenMethod($"public {(isAsync ? $"async Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}HistGets{(isAsync ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id)");
          _sb.AppendLine($"var dto = {(isAsync ? "await " : "")}{dataAccessName}HistGets{(isAsync ? "Async" : "")}(id){(isAsync ? ".ConfigureAwait(false)" : "")};");
          BuildTraceLogCall($"Logic.{baseModel.Name}HistGets{(isAsync ? "Async" : "")}({{id}})", $"dto", isAsync);
          _sb.AppendLine($"return dto;");
          CloseMethod();

          OpenMethod($"public {(isAsync ? $"async Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {GetFullFunctionPrefix(baseModel.Schema, baseModel.Name)}HistEntryGet{(isAsync ? "Async" : "")}(long histId)");
          _sb.AppendLine($"var dto = {(isAsync ? "await " : "")}{dataAccessName}HistEntryGet{(isAsync ? "Async" : "")}(histId){(isAsync ? ".ConfigureAwait(false)" : "")};");
          BuildTraceLogCall($"Logic.{baseModel.Name}HistEntryGet{(isAsync ? "Async" : "")}({{histId}})", $"dto", isAsync);
          _sb.AppendLine($"return ({baseModel.Name}HistDto)dto ?? null;");
          CloseMethod();

          break;

        default:
          break;
      }
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
      BuildClassMethod(new GeneratorBaseModel(function, schema), methodType, isAsync);
    }

    void IBusinessLogicGenerator.BuildTableClassMethod(Schema schema, Table table, MethodType methodType, bool isAsync)
    {
      BuildClassMethod(new GeneratorBaseModel(table, schema), methodType, isAsync);
    }

    void IBusinessLogicGenerator.BuildTableValuedFunctionClassMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync)
    {
      BuildClassMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync);
    }

    void IBusinessLogicGenerator.BuildViewClassMethod(Schema schema, View view, MethodType methodType, bool isAsync)
    {
      BuildClassMethod(new GeneratorBaseModel(view, schema), methodType, isAsync);
    }
  }
}
