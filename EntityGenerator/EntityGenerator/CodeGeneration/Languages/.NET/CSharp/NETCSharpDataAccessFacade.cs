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
      List<string> imports = new()
      {
        $"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses",
        $"{_profile.Global.ProjectName}.Common.DTOs.{schema.Name}",
        "System.Collections.Generic",
        "System.Threading.Tasks",
        "System",
      };
      foreach (DatabaseLanguageBase databaseLanguage in DatabaseLanguages)
      {
        imports = imports.Concat(databaseLanguage.GetClientImports()).ToList();
      }
      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}");
    }

    protected void BuildExternalInterfaceHeader(Schema schema, string name, bool isTable)
    {
      BuildInterfaceBase(schema);
      OpenInterface($"I{TypeHelper.GetDaoType(name, isTable)}", isPartial: true);
    }
    protected void BuildInternalInterfaceHeader(GeneratorBaseModel baseModel)
    {
      if (!_profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
      {
        BuildInterfaceBase(baseModel.Schema);
      }
      OpenInterface($"I{baseModel.InternalDaoName}", baseInterface: $"I{baseModel.DaoName}",isPartial: true);
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
      foreach (string methodSignature in GetExternalMethodSignatures(baseModel, methodType, async, baseModel.Name))
      {
        _sb.AppendLine(methodSignature);
      }
    }

    protected void BuildDataAccessFacadeInternalMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync, int databaseId)
    {
      foreach (string methodSignature in DatabaseLanguages[databaseId].GetInternalMethodSignatures(baseModel, methodType, isAsync, true))
      {
        _sb.AppendLine(methodSignature);
      }
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
      List<string> internalMethodSignatures = DatabaseLanguages[databaseId].GetInternalMethodSignatures(baseModel, methodType, isAsync, useNamespace: true);


      switch (methodType)
      {
        case MethodType.GET:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");
          OpenMethod(externalMethodSignatures.ElementAt(1));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Gets{(isAsync ? "Async" : "")}(new WhereClause(), false, {(parametersStr != "" ? $"{parametersStr}, " : "")}pageNum, pageSize, orderBy){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalGetFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.SAVE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Save{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalSaveFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.DELETE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Delete{(isAsync ? "Async" : "")}(id){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalDeleteFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.MERGE:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"{(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}Merge{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");
          DatabaseLanguages[databaseId].BuildInternalMergeFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;
          
        case MethodType.COUNT:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : string.Empty)} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}GetCount{(isAsync ? "Async" : string.Empty)}(new WhereClause(){(parametersStr != "" ? $", {parametersStr}" : "")}){(isAsync ? ".ConfigureAwait(false)" : string.Empty)};");
          DatabaseLanguages[databaseId].BuildInternalCountFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        case MethodType.HAS_CHANGED:
          OpenMethod(externalMethodSignatures.ElementAt(0));
          _sb.AppendLine($"return {(isAsync ? "await" : "")} _provider.GetRequiredService<{fullDaoName}>().{baseModel.Name}HasChanged{(isAsync ? "Async" : "")}(dto){(isAsync ? ".ConfigureAwait(false)" : "")};");

          DatabaseLanguages[databaseId].BuildInternalHasChangedFacadeMethod(baseModel, isAsync, internalMethodSignatures);
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

          DatabaseLanguages[databaseId].BuildInternalHistFacadeMethod(baseModel, isAsync, internalMethodSignatures);
          break;

        default:
          break;
      }
    }

    void IDataAccessFacadeGenerator.BuildWhereParameterClass()
    {
      BuildImports(new List<string> { "System", "System.Collections.Generic", "System.Data" });
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.BaseClasses");

      // WhereParameters Class
      OpenClass("WhereParameters");
      _sb.AppendLine("public List<IWhereParameter> Parameters { get; }");
      OpenMethod("WhereParameters()", null);
      _sb.AppendLine($"Parameters = new List<IWhereParameter>();");

      // WhereClause Class
      OpenClass("WhereClause : WhereParameters");
      _sb.AppendLine("public string Where { get; set; }");
      OpenMethod(@"WhereClause() : base()", null);
      OpenMethod(@"WhereClause(string where) : this()", null);
      _sb.AppendLine($"Where = where;");

      // IWhereParameter Interface
      OpenInterface("IWhereParameter");
      _sb.AppendLine("string ParameterName { get; }");
      _sb.AppendLine("SqlDbType ParameterType { get; }");
      _sb.AppendLine("object ParameterValue { get; }");

      // IWhereParameterTyped<T> Interface
      OpenInterface("IWhereParameterTyped<T> : IWhereParameter");
      _sb.AppendLine("T ParameterValueTyped { get; }");

      // WhereBaseParameter<T> Class
      OpenClass("WhereBaseParameter<T> : IWhereParameterTyped<T>", null, false, false, true);
      _sb.AppendLine("public T ParameterValueTyped { get; }");
      _sb.AppendLine("public string ParameterName { get; }");
      _sb.AppendLine("public abstract SqlDbType ParameterType { get; }");
      _sb.AppendLine("public object ParameterValue => ParameterValueTyped;");
      OpenMethod("WhereBaseParameter(string parameterName, T parameterValue)", null);
      _sb.AppendLine("ParameterName = parameterName.StartsWith(\"@\") ? parameterName : $\"@{ parameterName}\";");
      _sb.AppendLine("ParameterValueTyped = parameterValue;");

      // WhereBoolParameter Class
      OpenClass("WhereBoolParameter", "WhereBaseParameter<bool>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Bit;");
      OpenMethod(@"WhereBoolParameter(string parameterName, bool parameterValue) : base(parameterName, parameterValue)", null);

      // WhereByteParameter Class
      OpenClass("WhereByteParameter : WhereBaseParameter<byte>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.TinyInt;");
      OpenMethod(@"WhereByteParameter(string parameterName, byte parameterValue) : base(parameterName, parameterValue)", null);

      // WhereShortParameter Class
      OpenClass("WhereShortParameter : WhereBaseParameter<short>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.SmallInt;");
      OpenMethod(@"WhereShortParameter(string parameterName, short parameterValue) : base(parameterName, parameterValue)", null);

      // WhereIntParameter Class
      OpenClass("WhereIntParameter : WhereBaseParameter<int>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Int;");
      OpenMethod(@"WhereIntParameter(string parameterName, int parameterValue) : base(parameterName, parameterValue)", null);

      // WhereLongParameter Class
      OpenClass("WhereLongParameter : WhereBaseParameter<long>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.BigInt;");
      OpenMethod(@"WhereLongParameter(string parameterName, long parameterValue) : base(parameterName, parameterValue)", null);

      // WhereFloatParameter Class
      OpenClass("WhereFloatParameter : WhereBaseParameter<float>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Real;");
      OpenMethod(@"WhereFloatParameter(string parameterName, float parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDoubleParameter Class
      OpenClass("WhereDoubleParameter : WhereBaseParameter<double>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Float;");
      OpenMethod(@"WhereDoubleParameter(string parameterName, double parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDecimalParameter Class
      OpenClass("WhereDecimalParameter : WhereBaseParameter<decimal>");
      _sb.AppendLine("public override SqlDbType ParameterType => SqlDbType.Decimal;");
      OpenMethod(@"WhereDecimalParameter(string parameterName, decimal parameterValue) : base(parameterName, parameterValue)", null);

      // WhereCharParameter Class
      OpenClass("WhereCharParameter : WhereBaseParameter<char>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Char;");
      OpenMethod(@"WhereCharParameter(string parameterName, char parameterValue) : base(parameterName, parameterValue)", null);

      // WhereNCharParameter Class
      OpenClass("WhereNCharParameter : WhereBaseParameter<char>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.NChar;");
      OpenMethod(@"WhereNCharParameter(string parameterName, char parameterValue) : base(parameterName, parameterValue)", null);

      // WhereVarcharParameter Class
      OpenClass("WhereVarcharParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.VarChar;");
      OpenMethod(@"WhereVarcharParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereNVarcharParameter Class
      OpenClass("WhereNVarcharParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.NVarChar;");
      OpenMethod(@"WhereNVarcharParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereTextParameter Class
      OpenClass("WhereTextParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Text;");
      OpenMethod(@"WhereTextParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereXmlParameter Class
      OpenClass("WhereXmlParameter : WhereBaseParameter<string>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Xml;");
      OpenMethod(@"WhereXmlParameter(string parameterName, string parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDataTimeParameter Class
      OpenClass("WhereDateTimeParameter : WhereBaseParameter<DateTime>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.DateTime;");
      OpenMethod(@"WhereDateTimeParameter(string parameterName, DateTime parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDateTime2Parameter Class
      OpenClass("WhereDateTime2Parameter : WhereBaseParameter<DateTime>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.DateTime2;");
      OpenMethod(@"WhereDateTime2Parameter(string parameterName, DateTime parameterValue) : base(parameterName, parameterValue)", null);

      // WhereDateTimeOffsetParameter Class
      OpenClass("WhereDateTimeOffsetParameter : WhereBaseParameter<DateTimeOffset>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.DateTimeOffset;");
      OpenMethod(@"WhereDateTimeOffsetParameter(string parameterName, DateTimeOffset parameterValue): base(parameterName, parameterValue)", null);

      // WhereDateParameter Class
      OpenClass("WhereDateParameter : WhereBaseParameter<DateTime>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Date;");
      OpenMethod(@"WhereDateParameter(string parameterName, DateTime parameterValue) : base(parameterName, parameterValue)", null);

      // WhereVarBinaryParameter Class
      OpenClass("WhereVarBinaryParameter : WhereBaseParameter<byte[]>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.VarBinary;");
      OpenMethod(@"WhereVarBinaryParameter(string parameterName, byte[] parameterValue) : base(parameterName, parameterValue)", null);

      // WhereImageParameter Class
      OpenClass("WhereImageParameter : WhereBaseParameter<byte[]>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.Image;");
      OpenMethod(@"WhereImageParameter(string parameterName, byte[] parameterValue) : base(parameterName, parameterValue)", null);

      // WhereUniqueIdentifierParameter Class
      OpenClass("WhereUniqueIdentifierParameter : WhereBaseParameter<Guid>");
      _sb.AppendLine("    public override SqlDbType ParameterType => SqlDbType.UniqueIdentifier;");
      OpenMethod(@"WhereUniqueIdentifierParameter(string parameterName, Guid parameterValue) : base(parameterName, parameterValue)", null);
    }

    void IDataAccessFacadeGenerator.BuildADOInterface(Database db)
    {
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado");

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
      BuildInternalInterfaceHeader(new GeneratorBaseModel(table, schema));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeFunctionInternalInterfaceHeader(Schema schema, Function function)
    {
      BuildInternalInterfaceHeader(new GeneratorBaseModel(function, schema));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(Schema schema, Function tableValuedFunction)
    {
      BuildInternalInterfaceHeader(new GeneratorBaseModel(tableValuedFunction, schema));
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFacadeViewInternalInterfaceHeader(Schema schema, View view)
    {
      BuildInternalInterfaceHeader(new GeneratorBaseModel(view, schema));
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
