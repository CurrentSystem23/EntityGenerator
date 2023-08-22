using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Languages.SQL;
using EntityGenerator.Core.Models.ModelObjects;
using System.Collections.Generic;
using System.Linq;
using EntityGenerator.CodeGeneration.Models.ModelObjects;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessGenerator
  {
    protected void BuildGetOrderByMethod(GeneratorBaseModel baseModel)
    {
      OpenMethod($"private string GetOrderBy(params Order{baseModel.Name}[] orderBy)");
      _sb.AppendLine("string result = string.Empty;");
      _sb.AppendLine("if (orderBy == null)");
      _sb.AppendLine("return result;");
      _sb.AppendLine("bool first = true;");
      _sb.AppendLine($"foreach (Order{baseModel.Name} order in orderBy)");
      _sb.AppendLine("{");
      _sb.AppendLine($"string orderName = Enum.GetName(typeof(Order{baseModel.Name}), order);");
      _sb.AppendLine("string[] orderArray = orderName.Split('_');");
      _sb.AppendLine("if (orderArray.Length == 2)");
      _sb.AppendLine("{");
      _sb.AppendLine("if (first)");
      _sb.AppendLine("{");
      _sb.AppendLine(@"result = $"" ORDER BY {orderArray[0]} {orderArray[1]}"";");
      _sb.AppendLine("first = false;");
      _sb.AppendLine("}");
      _sb.AppendLine("else");
      _sb.AppendLine("{");
      _sb.AppendLine(@"result += $"", {orderArray[0]} {orderArray[1]}"";");
      _sb.AppendLine("}");
      _sb.AppendLine("}");
      _sb.AppendLine("}");
      _sb.AppendLine("return result;");
    }

    protected void BuildGetPaginationMethod(GeneratorBaseModel baseModel)
    {
      OpenMethod($"private string GetPagination(int? pageNum = null, int? pageSize = null, params Order{baseModel.Name}[] orderBy)");
      _sb.AppendLine("string result = string.Empty;");
      _sb.AppendLine("if (pageNum != null && pageSize != null && orderBy?.Length > 0)");
      _sb.AppendLine("{");
      _sb.AppendLine(@"result = @""");
      _sb.AppendLine("OFFSET (@pageNum - 1) * @pageSize ROWS");
      _sb.AppendLine(@"FETCH NEXT @pageSize ROWS ONLY;"";");
      _sb.AppendLine("}");
      _sb.AppendLine("return result;");
    }

    protected void BuildDAOClassHeader(GeneratorBaseModel baseModel, int databaseId)
    {
      List<string> imports = new()
      {
        "System",
        "System.Collections.Generic",
        "System.Data",
        "System.Linq",
        "System.Threading.Tasks",
        "System.Transactions",
        "Microsoft.Extensions.Logging",
        $"{_profile.Global.ProjectName}.Common",
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{baseModel.Schema.Name}",
        $"{_profile.Global.ProjectName}.Common.DTOs.{baseModel.Schema.Name}",
        $"{_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.BaseClasses",
      };

      BuildImports(imports.Concat(_databaseLanguages[databaseId].GetClientImports()).ToList());
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{baseModel.Schema.Name}");

      // Create Dao class
      OpenClass(baseModel.DaoName,
        $"Dao, I{baseModel.DaoName}, I{baseModel.InternalDaoName}",
        isStatic: false, isPartial: true);

      // Add service members
      _sb.AppendLine($"private readonly ILogger<{baseModel.DaoName}> _logger;");
      _sb.AppendLine($"private readonly IServiceProvider _provider;");

      // Constructor
      OpenMethod($"{baseModel.DaoName}(IServiceProvider provider, ILogger<{baseModel.DaoName}> logger = null)", null);
      _sb.AppendLine($"_logger = logger;");
      _sb.AppendLine("_provider = provider;");
    }

    protected void BuildDAOMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync, int databaseId)
    {
      if (!MethodHelper.IsValidMethodType(baseModel.DbObjectType, methodType))
      {
        return;
      }
      SQLLanguageBase _databaseLanguage = this._databaseLanguages[databaseId] as SQLLanguageBase;

      string parametersStr = ParameterHelper.GetParametersString(baseModel.Parameters);
      List<string> externalMethodSignatures = GetExternalMethodSignatures(baseModel, methodType, isAsync);
      List<string> internalMethodSignatures = _databaseLanguage.GetInternalMethodSignatures(baseModel, methodType, isAsync);

      switch (methodType)
      {
        case MethodType.GET:
          BuildGetOrderByMethod(baseModel);
          BuildGetPaginationMethod(baseModel);
          _databaseLanguage.BuildGetPrepareCommand(baseModel, isAsync);
          _databaseLanguage.BuildGetSqlStatement(baseModel);

          // Non DB-specific methods
          OpenMethod(externalMethodSignatures.ElementAt(0), isAsync: isAsync);
          _sb.AppendLine($"return{(isAsync ? " await" : "")} {baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}null, null, orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1), isAsync: isAsync);
          _sb.AppendLine($"return{(isAsync ? " await" : "")} {baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildGetMethod(baseModel, methodType, isAsync, externalMethodSignatures, internalMethodSignatures);

          break;

        case MethodType.SAVE:
          _databaseLanguage.BuildSaveMethod(baseModel, isAsync, internalMethodSignatures,
            externalMethodSignatures);
          break;

        case MethodType.DELETE:
          _databaseLanguage.BuildDeleteMethod(baseModel, isAsync,
            internalMethodSignatures, externalMethodSignatures);
          break;

        case MethodType.MERGE:
          _databaseLanguage.BuildMergeMethod(baseModel, isAsync, internalMethodSignatures,
            externalMethodSignatures);
          break;

        case MethodType.COUNT:
          // Non DB-specific method
          OpenMethod(externalMethodSignatures.ElementAt(0), isAsync: isAsync);
          _sb.AppendLine($"return{(isAsync ? " await" : "")} {baseModel.Name}GetCount{(isAsync ? "Async" : "")}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(isAsync ? ".ConfigureAwait(false)" : "")};");

          _databaseLanguage.BuildInternalCountMethod(baseModel, isAsync,
            internalMethodSignatures);
          break;

        case MethodType.HAS_CHANGED:
          _databaseLanguage.BuildHasChangedMethod(baseModel, isAsync,
            internalMethodSignatures, externalMethodSignatures);
          break;

        case MethodType.BUlK_INSERT:
          break;

        case MethodType.BULK_MERGE:
          break;

        case MethodType.BULK_UPDATE:
          break;

        case MethodType.HIST_GET:
          _databaseLanguage.BuildHistGetMethod(baseModel, isAsync, internalMethodSignatures,
            externalMethodSignatures);
          break;

        default:
          break;
      }
    }

    void IDataAccessGenerator.BuildBaseFile(int databaseId)
    {
      List<string> imports = new() { "System", "System.Collections.Generic", "System.Text", $"{_profile.Global.ProjectName}.Common.DTOs" };
      BuildImports(imports.Concat(_databaseLanguages[databaseId].GetClientImports()).ToList());
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.BaseClasses");
      OpenClass("Dao");

      _databaseLanguages[databaseId].BuildBeforeSaveMethod();
      _databaseLanguages[databaseId].BuildAfterSaveMethod();
    }

    void IDataAccessGenerator.BuildDependencyInjections(Database db, int databaseId)
    {
      // Add all available schemata as imports
      List<string> imports = new();
      foreach (Schema schema in db.Schemas)
      {
        imports.Add($"{_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}");
      }

      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.Helper");

      OpenClass($"DataAccess{_databaseLanguages[databaseId].Name}Initializer", isStatic: true, isPartial: true);

      OpenMethod($"InitializeGeneratedDataAccess(IServiceCollection services)", returnType: "void", isStatic: true);
      foreach (Schema schema in db.Schemas)
      {
        foreach (Table table in schema.Tables)
        {
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{table.Name}Dao, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{table.Name}Dao>();");
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{table.Name}InternalDao, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{table.Name}Dao>();");
        }
        foreach (Function function in schema.FunctionsScalar)
        {
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}DaoS, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{function.Name}DaoS>();");
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}InternalDaoS, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{function.Name}DaoS>();");
        }
        foreach (Function function in schema.FunctionsTableValued)
        {
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}DaoV, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{function.Name}DaoV>();");
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}InternalDaoV, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{function.Name}DaoV>();");
        }
        foreach (View view in schema.Views)
        {
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{view.Name}DaoV, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{view.Name}DaoV>();");
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{view.Name}InternalDaoV, {_profile.Global.ProjectName}.DataAccess.{_databaseLanguages[databaseId].Name}.{schema.Name}.{view.Name}DaoV>();");
        }
      }
    }

    void IDataAccessGenerator.BuildFunctionDAOHeader(Schema schema, Function function, int databaseId)
    {
      BuildDAOClassHeader(new GeneratorBaseModel(function, schema), databaseId);
    }

    void IDataAccessGenerator.BuildFunctionDAOMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDAOMethod(new GeneratorBaseModel(function, schema), methodType, isAsync, databaseId);
    }

    void IDataAccessGenerator.BuildTableDAOHeader(Schema schema, Table table, int databaseId)
    {
      BuildDAOClassHeader(new GeneratorBaseModel(table, schema), databaseId);
    }

    void IDataAccessGenerator.BuildTableDAOMethod(Schema schema, Table table, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDAOMethod(new GeneratorBaseModel(table, schema), methodType, isAsync, databaseId);
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOHeader(Schema schema, Function tableValuedFunction, int databaseId)
    {
      BuildDAOClassHeader(new GeneratorBaseModel(tableValuedFunction, schema), databaseId);
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDAOMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync, databaseId);
    }

    void IDataAccessGenerator.BuildViewDAOHeader(Schema schema, View view, int databaseId)
    {
      BuildDAOClassHeader(new GeneratorBaseModel(view, schema), databaseId);
    }

    void IDataAccessGenerator.BuildViewDAOMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDAOMethod(new GeneratorBaseModel(view, schema), methodType, isAsync, databaseId);
    }
  }
}
