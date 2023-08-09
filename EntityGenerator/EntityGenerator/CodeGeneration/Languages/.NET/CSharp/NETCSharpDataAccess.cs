using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessGenerator
  {
    void IDataAccessGenerator.BuildBaseFile(int databaseId)
    {
      BuildImports(new List<string> { "System", "System.Collections.Generic", "Microsoft.Data.SqlClient", "System.Text", $"{_profile.Global.ProjectName}.Common.DTOs" });
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.{DatabaseLanguages[databaseId].Name}.BaseClasses");

      OpenClass("Dao", isPartial: true);
    }

    void IDataAccessGenerator.BuildDependencyInjections(Database db)
    {
      List<string> imports = new()
      {
        "Microsoft.Extensions.DependencyInjection"
      };

      foreach (Schema schema in db.Schemas)
      {
        imports.Add($"{_profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}");
      }
      BuildImports(imports);
      BuildNameSpace($"{_profile.Global.ProjectName}.DataAccess.{db.Name}.Helper");

      OpenClass("DataAccessAdoInitializer", null, false, true);
      OpenMethod("InitializeGenerated(IServiceCollection services)", accessModifier: Enums.AccessType.PRIVATE);

      foreach (Schema schema in db.Schemas)
      {
        foreach (Table table in schema.Tables)
        {
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{table.Name}Dao, {_profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{table.Name}Dao>();");
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{table.Name}InternalDao, {_profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{table.Name}Dao>();");
        }
        foreach (Function function in schema.FunctionsScalar)
        {
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}DaoS, {_profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{function.Name}DaoS>();");
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}InternalDaoS, {_profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{function.Name}DaoS>();");
        }
        foreach (BaseModel baseModel in schema.FunctionsTableValued.Cast<BaseModel>().Concat(schema.Views))
        {
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{baseModel.Name}DaoV, {_profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{baseModel.Name}DaoV>();");
          _sb.AppendLine($"services.AddTransient<{_profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{baseModel.Name}InternalDaoV, {_profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{baseModel.Name}DaoV>();");
        }
      }
    }

    void IDataAccessSQLGenerator.BuildWhereParameterClass(int databaseId)
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

    void IDataAccessGenerator.BuildFunctionDAOHeader(Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildFunctionDAOMethod(Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOHeader(Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOMethod(Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOHeader(Schema schema, Function tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOMethod(Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOHeader(Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOMethod(Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
