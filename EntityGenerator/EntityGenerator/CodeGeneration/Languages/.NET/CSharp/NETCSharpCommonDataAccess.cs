using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public partial class NETCSharp : ICommonGenerator
  {
    protected void BuildDataAccessFacadeExternalMethod(GeneratorBaseModel baseModel, MethodType methodType, bool async)
    {
      foreach (string methodSignature in GetExternalMethodSignatures(baseModel, methodType, async, baseModel.Name))
      {
        _sb.AppendLine($"{methodSignature};");
      }
    }

    protected void BuildDataAccessFacadeInternalMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync, int databaseId)
    {
      foreach (string methodSignature in _databaseLanguages[databaseId].GetInternalMethodSignatures(baseModel, methodType, isAsync, useNamespace: false))
      {
        _sb.AppendLine($"{methodSignature};");
      }
    }

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
      foreach (DatabaseLanguageBase databaseLanguage in _databaseLanguages)
      {
        imports = imports.Concat(databaseLanguage.GetClientImports()).ToList();
      }
      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}");
    }

    protected void BuildExternalInterfaceHeader(GeneratorBaseModel baseModel)
    {
      BuildInterfaceBase(baseModel.Schema);
      OpenInterface($"I{baseModel.DaoName}", isPartial: true);
    }

    protected void BuildInternalInterfaceHeader(GeneratorBaseModel baseModel)
    {
      if (!_profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
      {
        BuildInterfaceBase(baseModel.Schema);
      }
      OpenInterface($"I{baseModel.InternalDaoName}", baseInterface: $"I{baseModel.DaoName}", isPartial: true);
    }

    void ICommonGenerator.BuildWhereParameterClass()
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
      OpenClass("WhereBaseParameter<T> : IWhereParameterTyped<T>", null, isStatic: false, isPartial: false, isAbstract: true);
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

    void ICommonGenerator.BuildADOInterface(Database db)
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

    void ICommonGenerator.BuildDataAccessFacadeTableExternalInterfaceHeader(Schema schema, Table table)
    {
      BuildExternalInterfaceHeader(new GeneratorBaseModel(table, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeFunctionExternalInterfaceHeader(Schema schema, Function function)
    {
      BuildExternalInterfaceHeader(new GeneratorBaseModel(function, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceHeader(Schema schema, Function tableValuedFunction)
    {
      BuildExternalInterfaceHeader(new GeneratorBaseModel(tableValuedFunction, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeViewExternalInterfaceHeader(Schema schema, View view)
    {
      BuildExternalInterfaceHeader(new GeneratorBaseModel(view, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeFunctionExternalInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(function, schema), methodType, isAsync);
    }

    void ICommonGenerator.BuildDataAccessFacadeTableExternalInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(table, schema), methodType, isAsync);
    }

    void ICommonGenerator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync);
    }

    void ICommonGenerator.BuildDataAccessFacadeViewExternalInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync)
    {
      BuildDataAccessFacadeExternalMethod(new GeneratorBaseModel(view, schema), methodType, isAsync);
    }
    void ICommonGenerator.BuildDataAccessFacadeTableInternalInterfaceHeader(Schema schema, Table table)
    {
      BuildInternalInterfaceHeader(new GeneratorBaseModel(table, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeFunctionInternalInterfaceHeader(Schema schema, Function function)
    {
      BuildInternalInterfaceHeader(new GeneratorBaseModel(function, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(Schema schema, Function tableValuedFunction)
    {
      BuildInternalInterfaceHeader(new GeneratorBaseModel(tableValuedFunction, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeViewInternalInterfaceHeader(Schema schema, View view)
    {
      BuildInternalInterfaceHeader(new GeneratorBaseModel(view, schema));
    }

    void ICommonGenerator.BuildDataAccessFacadeTableInternalInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(table, schema), methodType, isAsync, databaseId);
    }

    void ICommonGenerator.BuildDataAccessFacadeFunctionInternalInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(function, schema), methodType, isAsync, databaseId);
    }

    void ICommonGenerator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync, databaseId);
    }

    void ICommonGenerator.BuildDataAccessFacadeViewInternalInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId)
    {
      BuildDataAccessFacadeInternalMethod(new GeneratorBaseModel(view, schema), methodType, isAsync, databaseId);
    }
  }
}
