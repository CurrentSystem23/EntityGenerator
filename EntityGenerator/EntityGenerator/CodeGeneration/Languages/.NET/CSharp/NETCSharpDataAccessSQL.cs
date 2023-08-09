using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Languages.SQL;
using EntityGenerator.CodeGeneration.Languages.SQL.MSSQL.NETCSharp;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObject.Enums;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessSQLGenerator
  {
    void IDataAccessSQLGenerator.BuildBaseFileExtension(int databaseId)
    {
      List<string> imports = new() { "System", "System.Collections.Generic", "System.Text", $"{_profile.Global.ProjectName}.Common.DTOs" };
      BuildImports(imports.Concat(MSSQLCSharp.GetClientImports()).ToList());
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.{DatabaseLanguages[databaseId].Name}.BaseClasses");
      OpenClass("Dao", isPartial: true);

      DatabaseLanguages[databaseId].BuildBeforeSaveMethod();
      DatabaseLanguages[databaseId].BuildAfterSaveMethod();
    }

    protected void BuildDAOClassHeader(GeneratorParameterObject parameters)
    {
      List<string> imports = new()
      {
        "System",
        "System.Collections.Generic",
        "System.Data",
        "System.Linq",
        "System.Threading.Tasks",
        "System.Transactions",
        "Microsoft.Data.SqlClient",
        "Microsoft.Extensions.Logging",
        $"{_profile.Global.ProjectName}.Common",
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{parameters.Schema.Name}",
        $"{_profile.Global.ProjectName}.Common.DTOs.{parameters.Schema.Name}",
        $"{_profile.Global.ProjectName}.DataAccess.{DatabaseLanguages[parameters.DatabaseId].Name}.BaseClasses",
      };

      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.{DatabaseLanguages[parameters.DatabaseId].Name}.{parameters.Schema.Name}");

      // Create Dao class
      OpenClass(TypeHelper.GetDaoType(parameters.Name, parameters.IsTable), 
        $"Dao, I{TypeHelper.GetDaoType(parameters.Name, parameters.IsTable)}, I{TypeHelper.GetInternalDaoType(parameters.Name, parameters.IsTable)}",
        false, true);

      // Add service members
      _sb.AppendLine($"private readonly ILogger<{TypeHelper.GetDaoType(parameters.Name, parameters.IsTable)}> _logger;");
      _sb.AppendLine($"private readonly IServiceProvider _provider;");

      // Constructor
      OpenMethod($"{TypeHelper.GetDaoType(parameters.Name, parameters.IsTable)}(IServiceProvider provider, ILogger<{TypeHelper.GetDaoType(parameters.Name, parameters.IsTable)}> logger = null", null);
      _sb.AppendLine($"_logger = logger;");
      _sb.AppendLine("_provider = provider;");
    }

    protected void BuildDataAccessSQLClassMethodBody(InternalGeneratorParameterObject parameters)
    {
      SQLLanguageBase _databaseLanguage = this.DatabaseLanguages[parameters.DatabaseId] as SQLLanguageBase;

      string fullDaoName = $"Common.DataAccess.Interfaces.Ado.{parameters.Schema.Name}.I{TypeHelper.GetInternalDaoType(parameters.Name, parameters.IsTable)}";
      string parametersStr = ParameterHelper.GetParametersString(parameters.Parameters);

      switch (parameters.MethodType)
      {
        case MethodType.GET:
          _databaseLanguage.BuildPrepareCommand(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.Parameters);
          _databaseLanguage.BuildGetSqlStatement(parameters.Schema, parameters.Name, parameters.IsTable, parameters.Parameters, parameters.Columns);

          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return{(parameters.IsAsync ? " await" : "")} {parameters.Name}Gets{(parameters.IsAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null, orderBy){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return{(parameters.IsAsync ? " await" : "")} {parameters.Name}Gets{(parameters.IsAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildGetMethod(parameters.Schema, parameters.MethodType, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.ExternalMethodSignatures, parameters.InternalMethodSignatures, parameters.Parameters);

          break;

        case MethodType.SAVE:
          _databaseLanguage.BuildSaveMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures, 
            parameters.ExternalMethodSignatures, parameters.Columns);
          break;

        case MethodType.DELETE:
          _databaseLanguage.BuildDeleteMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, 
            parameters.InternalMethodSignatures, parameters.ExternalMethodSignatures);
          break;

        case MethodType.MERGE:
          _databaseLanguage.BuildMergeMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures, 
            parameters.ExternalMethodSignatures, parameters.Columns);
          break;

        case MethodType.COUNT:
          OpenMethod(parameters.ExternalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return{(parameters.IsAsync ? " await" : "")} {parameters.Name}GetCount{(parameters.IsAsync ? "Async" : "")}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(parameters.IsAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalCountMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync,
            parameters.InternalMethodSignatures, parameters.Parameters);
          break;

        case MethodType.HAS_CHANGED:
          _databaseLanguage.BuildHasChangedMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, 
            parameters.InternalMethodSignatures, parameters.ExternalMethodSignatures, parameters.Columns);
          break;

        case MethodType.BUlK_INSERT:
          break;

        case MethodType.BULK_MERGE:
          break;

        case MethodType.BULK_UPDATE:
          break;

        case MethodType.HIST_GET:
          _databaseLanguage.BuildHistGetMethod(parameters.Schema, parameters.Name, parameters.IsTable, parameters.IsAsync, parameters.InternalMethodSignatures, 
            parameters.ExternalMethodSignatures);
          break;

        default:
          break;
      }
    }

    void IDataAccessSQLGenerator.BuildDependencyInjections(Database db)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildBaseFile()
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildTableDAOHeader(Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildFunctionDAOHeader(Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildTableValuedFunctionDAOHeader(Schema schema, Function tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildViewDAOHeader(Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildTableDAOMethod(Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildFunctionDAOMethod(Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildTableValuedFunctionDAOMethod(Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessSQLGenerator.BuildViewDAOMethod(Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
