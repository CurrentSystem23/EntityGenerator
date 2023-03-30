using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IBusinessLogicGenerator
  {
    protected string GetFullFunctionPrefix(Schema schema, string name)
    {
      return $"{schema.Name.FirstCharToUpper()}_{name}";
    } 
    protected void BuildInterfaceMethod(ProfileDto profile, Schema schema, MethodType methodType, string name, string dtoName, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      switch (methodType)
      {
        case MethodType.GET:
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{name}[] orderBy);");
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");
          break;
        case MethodType.SAVE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}Save{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.DELETE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}Delete{(async ? "Async" : "")}({(profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          break;
        case MethodType.MERGE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}Merge{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.COUNT:
          _sb.AppendLine($"{(async ? $"Task<long>" : $"long")} {GetFullFunctionPrefix(schema, name)}GetCount{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")});");
          break;
        case MethodType.HAS_CHANGED:
          _sb.AppendLine($"{(async ? "Task<bool>" : "bool")} {GetFullFunctionPrefix(schema, name)}HasChanged{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.BUlK_INSERT:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_MERGE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_UPDATE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkUpdate{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          break;
        case MethodType.HIST_GET:
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}HistGets{(async ? "Async" : "")}({(profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}HistEntryGet{(async ? "Async" : "")}(long histId);");
          break;
        default:
          break;
      }
    }

    void IBusinessLogicGenerator.BuildInterfaceHeader(ProfileDto profile, Schema schema)
    {
      List<string> imports = new()
      {
        $"{profile.Global.ProjectName}.Common.DTOs.{schema.Name}",
        "System.Threading.Tasks",
        "System.Collections.Generic",
        "System",
      };

      BuildImports(imports);
      BuildNameSpace($"{profile.Global.ProjectName}.Common.Interfaces");
      BuildInterfaceHeader("ILogic", isPartial: true);
    }

    void IBusinessLogicGenerator.BuildFunctionClassHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildFunctionClassMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildFunctionInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableClassHeader(ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableClassMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      if (methodType == MethodType.HIST_GET)
      {
        BuildInterfaceMethod(profile, schema, methodType, table.Name, $"{table.Name}HistDto", true);
      }
      else
      {
        BuildInterfaceMethod(profile, schema, methodType, table.Name, $"{table.Name}Dto", true);
      }
    }

    void IBusinessLogicGenerator.BuildTableValuedFunctionClassHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableValuedFunctionClassMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableValuedFunctionInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildViewClassHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildViewClassMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildViewInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
