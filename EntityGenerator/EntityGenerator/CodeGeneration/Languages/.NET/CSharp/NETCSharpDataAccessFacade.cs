using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

    protected void BuildInternalInterfaceMethod(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));

      switch (methodType)
      {
        case MethodType.GET:
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{name}[] orderBy);");
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");
          if (isTable)
          {
            _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}Get{(async ? "Async" : "")}({(profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}Get{(async ? "Async" : "")}(Guid globalId);");
          }
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
    void IDataAccessFacadeGenerator.BuildDataAccessTableExternalInterfaceHeader(ProfileDto profile, Schema schema, Table table)
    {
      BuildExternalInterfaceHeader(profile, schema, table.Name, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function function)
    {
      BuildExternalInterfaceHeader(profile, schema, function.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuedFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      BuildExternalInterfaceHeader(profile, schema, tableValuedFunction.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewExternalInterfaceHeader(ProfileDto profile, Schema schema, View view)
    {
      BuildExternalInterfaceHeader(profile, schema, view.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      BuildMethodSignature(profile, schema, methodType, function.Name, false, true, ParameterHelper.GetParametersString(function.Parameters), ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableExternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      BuildMethodSignature(profile, schema, methodType, table.Name, true, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuedFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      BuildMethodSignature(profile, schema, methodType, tableValuedFunction.Name, false, true, ParameterHelper.GetParametersString(tableValuedFunction.Parameters), ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewExternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      BuildMethodSignature(profile, schema, methodType, view.Name, false, true);
    }
    #endregion

    #region Internal Interface
    void IDataAccessFacadeGenerator.BuildDataAccessTableInternalInterfaceHeader(ProfileDto profile, Schema schema, Table table)
    {
      BuildInternalInterfaceHeader(profile, schema, table.Name, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function function)
    {
      BuildInternalInterfaceHeader(profile, schema, function.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuedFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      BuildInternalInterfaceHeader(profile, schema, tableValuedFunction.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewInternalInterfaceHeader(ProfileDto profile, Schema schema, View view)
    {
      BuildInternalInterfaceHeader(profile, schema, view.Name, false);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableInternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      BuildInternalMethodSignature(profile, schema, methodType, table.Name, true, true);
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      BuildInternalMethodSignature(profile, schema, methodType, function.Name, false, true, ParameterHelper.GetParametersString(function.Parameters), ParameterHelper.GetParametersStringWithType(function.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuedFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      BuildInternalMethodSignature(profile, schema, methodType, tableValuedFunction.Name, false, true, ParameterHelper.GetParametersString(tableValuedFunction.Parameters), ParameterHelper.GetParametersStringWithType(tableValuedFunction.Parameters, this));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewInternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      BuildInternalMethodSignature(profile, schema, methodType, view.Name, false, true);
    }
    #endregion
  }
}
