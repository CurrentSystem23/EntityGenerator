using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : ICommonGenerator
  {
    protected void BuildDTO(GeneratorBaseModel parameters)
    {
      BuildImports(new List<string> {
        $"{_profile.Global.ProjectName}.Common.DTOs",
        "System",
        "System.Collections.Generic",
      });

      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DTOs.{parameters.Schema.Name}");

      OpenEnum($"Order{parameters.Name}");
      foreach (Column column in parameters.Columns)
      {
        _sb.AppendLine($"{column.Name}_Asc,");
        _sb.AppendLine($"{column.Name}_Desc,");
      }

      foreach (Column column in parameters.Columns.Where(column => !column.ColumnIsIdentity))
      {
        _sb.AppendLine($"public {(column.ColumnTypeDataType is DataTypes.XDocument or DataTypes.XElement ? "string" : GetColumnDataType(column))} {column.Name} {{ get; set; }}{column.ColumnDefaultDefinition}");
      }

      // TODO : Allow config to decide over special Dto (currently fixed to Tenant)
      OpenClass($"{parameters.DtoName}", $"{(parameters.Columns.Exists(column => column.Name == "TenantId") ? "DtoBaseTenant" : "DtoBase")}");
      foreach (Column column in parameters.Columns.Where(column => !column.ColumnIsIdentity || (parameters.DbObjectType == DbObjectType.TABLE)))
      {
        string dataType = column.ColumnTypeDataType is DataTypes.XDocument or DataTypes.XElement ? "string" : GetColumnDataType(column);
        _sb.AppendLine($"public {String.Format(ParameterFormat, dataType, column.Name)} {{ get; set; }} {(column.ColumnDefaultDefinition.IsNullOrEmpty() ? $"= {column.ColumnDefaultDefinition}" : "")}");
      }
    }

    protected void BuildHistDto(GeneratorBaseModel parameters)
    {
      // Ignore history for logging tables
      if (parameters.Schema.Name.ToLower().Equals("log") || (parameters.Name.ToLower().Equals("logging") || parameters.Name.ToLower().Equals("log")))
        return;

      OpenClass($"{parameters.Name}HistDto", $"{parameters.DtoName}");
      _sb.AppendLine("public long Hist_Id { get; set; }");
      _sb.AppendLine("public string Hist_Action { get; set; }");
      _sb.AppendLine("public DateTime Hist_Date { get; set; }");
    }
    void ICommonGenerator.BuildBaseDTO()
    {
      BuildImports(new List<string>() { "System" });
      BuildNameSpace($"{_profile.Global.ProjectName}.Common.DTOs");

      OpenClass("DtoBase", isAbstract: true);
      _sb.AppendLine();
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
        OpenClass($"public static Schema{schema.Name}Class {schema.Name} => new Schema{schema.Name}Class(nameof({db.Name}));");
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
  }
}
