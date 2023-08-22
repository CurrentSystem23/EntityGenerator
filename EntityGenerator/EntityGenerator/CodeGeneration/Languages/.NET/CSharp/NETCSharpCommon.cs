using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : ICommonGenerator
  {
    protected bool IsBaseColumn(Column column)
    {
      return _profile.Generator.GeneratorCommon.BaseColumns.Contains(column.Name);
    }
    protected void BuildDTO(GeneratorBaseModel baseModel)
    {
      if (baseModel.DbObjectType == DbObjectType.FUNCTION)
      {
        return;
      }
      BuildImports(new List<string> {
        $"{_profile.Global.ProjectName}.Common.DTOs",
        "System",
        "System.Collections.Generic",
      });

      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DTOs.{baseModel.Schema.Name}");

      OpenEnum($"Order{baseModel.Name}");
      foreach (Column column in baseModel.Columns.OrEmptyIfNull())
      {
        _sb.AppendLine($"{column.Name}_Asc,");
        _sb.AppendLine($"{column.Name}_Desc,");
      }

      // TODO : Allow config to decide over special Dto (currently fixed to Tenant)
      OpenClass($"{baseModel.DtoName}", $"{(baseModel.Columns.Exists(column => column.Name == "TenantId") ? "DtoBaseTenant" : "DtoBase")}");
      foreach (Column column in baseModel.Columns.Where(column => !column.ColumnIsIdentity && !IsBaseColumn(column)))
      {
        string dataType = column.ColumnTypeDataType is DataTypes.XDocument or DataTypes.XElement ? "string" : GetColumnDataType(column);
        _sb.AppendLine($"public {String.Format(ParameterFormat, $"{dataType}{(column.ColumnIsNullable && column.ColumnTypeDataType != DataTypes.String ? "?" : string.Empty)}", column.Name)} {{ get; set; }} {(column.ColumnDefaultDefinition.IsNullOrEmpty() ? "" : $"= {column.ColumnDefaultDefinition}")};");
      }
    }

    protected void BuildHistDto(GeneratorBaseModel baseModel)
    {
      // Ignore history for logging tables
      if (baseModel.Schema.Name.ToLower().Equals("log") || (baseModel.Name.ToLower().Equals("logging") || baseModel.Name.ToLower().Equals("log")))
        return;

      OpenClass($"{baseModel.Name}HistDto", $"{baseModel.DtoName}");
      _sb.AppendLine("public long Hist_Id { get; set; }");
      _sb.AppendLine("public string Hist_Action { get; set; }");
      _sb.AppendLine("public DateTime Hist_Date { get; set; }");
    }

    protected void BuildInterfaceMethod(GeneratorBaseModel baseModel, MethodType methodType, bool isAsync)
    {
      List<string> methodSignatures = GetExternalMethodSignatures(baseModel, methodType, isAsync,
        GetFullFunctionPrefix(baseModel.Schema, baseModel.Name));

      foreach (string methodSignature in methodSignatures)
      {
        _sb.AppendLine($"{methodSignature};");
      }
    }

    void ICommonGenerator.BuildBaseDTO()
    {
      BuildImports(new List<string>() { "System" });
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DTOs");

      OpenClass("DtoBase", isAbstract: true);
      
      // TODO : Make configurable
      _sb.AppendLine($"public {((bool)_profile.Global.GuidIndexing ? "Guid" : "long")} Id {{ get; set; }} = {((bool)_profile.Global.GuidIndexing ? "Guid.Empty" : "-1")};");
      _sb.AppendLine("public Guid GlobalId { get; set; }");
      _sb.AppendLine("public DateTime ModifiedDate { get; set; }");
      _sb.AppendLine("public long ModifiedUser { get; set; }");
    }

    void ICommonGenerator.BuildTenantBase()
    {
      BuildImports(new List<string>() { "System" });
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DTOs");

      OpenClass("DtoBaseTenant", "DtoBase", isAbstract: true);
      _sb.AppendLine($"public {((bool)_profile.Global.GuidIndexing ? "Guid" : "long")} TenantId {{ get; set; }}");
    }

    void ICommonGenerator.BuildSchemaDTO(Database db)
    {
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DbSchemaInformation");

      OpenClass(db.Name, isStatic: true);
      foreach (Schema schema in db.Schemas)
      {
        // TODO : Schema class content
      }
    }

    void ICommonGenerator.BuildConstants(Database db)
    {
      BuildImports(new List<string>() { "System" });
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.Constants");
      
      OpenClass("Constants", isStatic: true, isPartial: true);

      foreach (ConstantTable constantTable in db.ConstantTables)
      {
        OpenClass(constantTable.Name, null, true);
        foreach (KeyValuePair<string, string> constant in constantTable.Constants)
        {
          _sb.AppendLine($"public const {GetColumnDataType(constantTable.ConstantColumn)} {constant.Key.TransformToCharOrDigitOnlyCamelCase()} = {TypeHelper.ParseTypeValue((DataTypes)Enum.Parse(typeof(DataTypes), constant.Key), constant.Value)};");
        }
      }
    }

    void ICommonGenerator.BuildTableDTO(Schema schema, Table table)
    {
      BuildDTO(new GeneratorBaseModel(table, schema));
    }

    void ICommonGenerator.BuildTableValuedFunctionDTO(Schema schema, Function tableValuedFunction)
    {
      BuildDTO(new GeneratorBaseModel(tableValuedFunction, schema));
    }

    void ICommonGenerator.BuildViewDTO(Schema schema, View view)
    {
      BuildDTO(new GeneratorBaseModel(view, schema));
    }

    void ICommonGenerator.BuildInterfaceHeader(Schema schema)
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

    void ICommonGenerator.BuildTableInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorBaseModel(table, schema), methodType, isAsync);
    }

    void ICommonGenerator.BuildScalarFunctionInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorBaseModel(function, schema), methodType, isAsync);
    }

    void ICommonGenerator.BuildTableValuedFunctionInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorBaseModel(tableValuedFunction, schema), methodType, isAsync);
    }

    void ICommonGenerator.BuildViewInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync)
    {
      BuildInterfaceMethod(new GeneratorBaseModel(view, schema), methodType, isAsync);
    }
  }
}
