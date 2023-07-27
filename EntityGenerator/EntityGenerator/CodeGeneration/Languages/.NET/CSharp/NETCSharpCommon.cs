﻿using EntityGenerator.CodeGeneration.Interfaces.Modules;
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
    protected void BuildDTO(GeneratorParameterObject parameters)
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

      // TODO : Allow config to decide over special Dto (currently Tenant)
      OpenClass($"{parameters.DtoName}", $"{(parameters.Columns.Exists(column => column.Name == "TenantId") ? "DtoBaseTenant" : "DtoBase")}");
      foreach (Column column in parameters.Columns.Where(column => !column.ColumnIsIdentity || !parameters.IsTable))
      {
        string dataType = column.ColumnTypeDataType is DataTypes.XDocument or DataTypes.XElement ? "string" : GetColumnDataType(column);
        _sb.AppendLine($"public {String.Format(ParameterFormat, dataType, column.Name)} {{ get; set; }} {(column.ColumnDefaultDefinition.IsNullOrEmpty() ? $"= {column.ColumnDefaultDefinition}" : "")}");
      }
    }

    protected void BuildHistDto(GeneratorParameterObject parameters)
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
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildTableDTO(Schema schema, Table table)
    {
      BuildDTO(new GeneratorParameterObject
      { 
        Columns = table.Columns,
        IsTable = true,
        Name = table.Name,
        Schema = schema
      });
    }

    void ICommonGenerator.BuildTableValuedFunctionDTO(Schema schema, Function tableValuedFunction)
    {
      BuildDTO(new GeneratorParameterObject
      {
        Columns = tableValuedFunction.ReturnTable,
        IsTable = false,
        Name = tableValuedFunction.Name,
        Schema = schema
      });
    }

    void ICommonGenerator.BuildViewDTO(Schema schema, View view)
    {
      BuildDTO(new GeneratorParameterObject
      {
        Columns = view.Columns,
        IsTable = false,
        Name = view.Name,
        Schema = schema
      });
    }
  }
}
