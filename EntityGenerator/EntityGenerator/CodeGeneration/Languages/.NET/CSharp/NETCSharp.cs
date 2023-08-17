using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.CodeGeneration.Models.ModelObjects;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : NETLanguageBase
  {
    public bool CloseExistingScope { get; set; }

    private bool openMethod = false;
    private bool openStructure = false;
    private bool openNameSpace = false;

    public NETCSharp(StringBuilder sb, ProfileDto profile) : base(sb, profile, "CSharp")
    {
      CloseExistingScope = true;
    }

    public void ResetScopes()
    {
      openMethod = false;
      openStructure = false;
      openNameSpace = false;
    }
    public void BuildImports(List<string> imports)
    {
      foreach (string import in imports)
      {
        _sb.AppendLine($"using {import};");
      }
      _sb.AppendLine();
    }
    public void BuildNameSpace(string nameSpace)
    {
      if (!CloseExistingScope && openNameSpace)
      {
        throw new Exception("Error: Trying to open already existing namespace scope.");
      }

      CloseNameSpace();
      _sb.AppendLine($"namespace {nameSpace};");
      openNameSpace = true;
    }

    private void OpenStructure(StructureType structureType, string structureName, string structureBase = null, bool isStatic = false, bool isPartial = false, bool isAbstract = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      if (!CloseExistingScope && openStructure)
      {
        throw new Exception("Error: Trying to open already existing structure scope.");
      }

      CloseStructure();
      _sb.Append($"{accessModifier:g} ".ToLower());
      if (isPartial) _sb.Append("partial ");
      switch (structureType)
      {
        case StructureType.CLASS:
          _sb.Append("class");
          break;
        case StructureType.INTERFACE:
          _sb.Append("interface");
          break;
        case StructureType.ENUM:
          _sb.Append("enum");
          break;
        default:
          break;
      }
      _sb.Append(' ');
      _sb.AppendJoin(" : ", (new string[] { structureName, structureBase }).Where(c => c != null).ToArray()).AppendLine();
      _sb.AppendLine("{");

      openStructure = true;
    }


    public override void OpenClass(string className, string baseClass = null, bool isStatic = false, bool isPartial = false, bool isAbstract = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      OpenStructure(StructureType.CLASS, className, baseClass, isStatic: isStatic, isPartial: isPartial, accessModifier: accessModifier);
    }

    public override void OpenInterface(string interfaceName, string baseInterface = null, bool isPartial = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      OpenStructure(StructureType.INTERFACE, interfaceName, baseInterface, isPartial: isPartial, accessModifier: accessModifier);
    }

    public override void OpenEnum(string enumName, bool isPartial = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      OpenStructure(StructureType.ENUM, enumName, structureBase: null, isPartial: isPartial, accessModifier: accessModifier);
    }

    public override void OpenMethod(string methodName, string returnType = "void", AccessType accessModifier = AccessType.PUBLIC, bool isStatic = false)
    {
      if (!CloseExistingScope && openMethod)
      {
        throw new Exception("Error: Trying to open already existing method scope.");
      }

      CloseMethod();
      _sb.Append($"{accessModifier:g} ".ToLower());
      if (isStatic) _sb.Append("static ");
      _sb.Append(returnType);
      _sb.AppendLine(" " + methodName);
      _sb.AppendLine("{");

      openMethod = true;
    }

    public override void OpenMethod(string fullMethodSignature)
    {
      if (!CloseExistingScope && openMethod)
      {
        throw new Exception("Error: Trying to open already existing method scope.");
      }

      CloseMethod();
      _sb.AppendLine(fullMethodSignature);
      _sb.AppendLine("{");

      openMethod = true;
    }

    protected override void CloseNameSpace()
    {
      if (!openNameSpace)
      {
        return;
      }
      CloseStructure();
      _sb.AppendLine("}");
      openNameSpace = false;
    }

    public override void CloseStructure()
    {
      if (!openStructure)
      {
        return;
      }
      CloseMethod();
      _sb.AppendLine("}");
      openStructure = false;
    }

    public override void CloseMethod()
    {
      if (!openMethod)
      {
        return;
      }
      _sb.AppendLine("}");
      openMethod = false;
    }

    public override void BuildTraceLogCall(string message, string paramsStr, bool async)
    {
      // TODO use async toggle.
      _sb.AppendLine($@"_logger.LogTrace(""{message}"", {paramsStr});");
    }

    public override void BuildErrorLogCall(string message, string paramsStr, bool async)
    {
      // TODO use async toggle.
      _sb.AppendLine($@"_logger.LogError(""{message}"", {paramsStr});");
    }

    public override string GetColumnDataType(Column column)
    {
      switch (column.ColumnTypeDataType)
      {
        case DataTypes.Unknown:
          break;
        case DataTypes.Boolean:
          return "bool";
        case DataTypes.Byte:
          return "byte";
        case DataTypes.Int16:
          return "short";
        case DataTypes.Int32:
          return "int";
        case DataTypes.Int64:
          return "long";
        case DataTypes.SByte:
          // TODO
          throw new NotSupportedException();
        case DataTypes.UInt16:
          return "ushort";
        case DataTypes.UInt32:
          return "uint";
        case DataTypes.UInt64:
          return "ulong";
        case DataTypes.Decimal:
          return "decimal";
        case DataTypes.Single:
          return "float";
        case DataTypes.Double:
          return "double";
        case DataTypes.Enum:
          return "Enum";
        case DataTypes.Char:
          return "char";
        case DataTypes.String:
          return "string";
        case DataTypes.CharArray:
          return "char[]";
        case DataTypes.XDocument:
          return "XDocument";
        case DataTypes.XElement:
          return "XElement";
        case DataTypes.DateTime:
          return "DateTime";
        case DataTypes.DateTimeOffset:
          return "DateTimeOffset";
        case DataTypes.TimeSpan:
          return "TimeSpan";
        case DataTypes.DateOnly:
          return "DateOnly";
        case DataTypes.TimeOnly:
          return "TimeOnly";
        case DataTypes.Binary:
          return "byte[]";
        case DataTypes.ByteArray:
          return "byte[]";
        case DataTypes.ISerializeable:
          return "ISerializable";
        case DataTypes.Guid:
          return "Guid";
        case DataTypes.Object:
          return "object";
        default:
          throw new NotSupportedException($"Error: Type {column.ColumnTypeDataType} is not supported.");
      }
      return string.Empty;
    }
    protected List<string> GetExternalMethodSignatures(GeneratorBaseModel baseModel, MethodType methodType, bool async, string prefix = null)
    {
      if (!MethodHelper.IsValidMethodType(baseModel.DbObjectType, methodType))
      {
        return new List<string>();
      }

      List<string> signatures = new();
      string parametersWithTypeStr = ParameterHelper.GetParametersStringWithType(baseModel.Parameters, this);

      switch (methodType)
      {
        case MethodType.GET:
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{baseModel.Name}[] orderBy);");
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.DtoName}>>" : $"ICollection<{baseModel.DtoName}>")} {prefix}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{baseModel.Name}[] orderBy);");
          if (baseModel.DbObjectType == DbObjectType.TABLE)
          {
            signatures.Add($"{(async ? $"Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {prefix}Get{(async ? "Async" : "")}({(_profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            signatures.Add($"{(async ? $"Task<{baseModel.DtoName}>" : $"{baseModel.DtoName}")} {prefix}Get{(async ? "Async" : "")}(Guid globalId);");
          }
          break;
        case MethodType.SAVE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Save{(async ? "Async" : "")}({baseModel.DtoName} dto);");
          break;
        case MethodType.DELETE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          break;
        case MethodType.MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Merge{(async ? "Async" : "")}({baseModel.DtoName} dto);");
          break;
        case MethodType.COUNT:
          signatures.Add($"{(async ? $"Task<long>" : $"long")} {prefix}GetCount{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")});");
          break;
        case MethodType.HAS_CHANGED:
          signatures.Add($"{(async ? "Task<bool>" : "bool")} {prefix}HasChanged{(async ? "Async" : "")}({baseModel.DtoName} dto);");
          break;
        case MethodType.BUlK_INSERT:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_UPDATE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkUpdate{(async ? "Async" : "")}(ICollection<{baseModel.DtoName}> dtos);");
          break;
        case MethodType.HIST_GET:
          signatures.Add($"{(async ? $"Task<ICollection<{baseModel.HistDtoName}>>" : $"ICollection<{baseModel.HistDtoName}>")} {prefix}HistGets{(async ? "Async" : "")}({(_profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          signatures.Add($"{(async ? $"Task<{baseModel.HistDtoName}>" : $"{baseModel.HistDtoName}")} {prefix}HistEntryGet{(async ? "Async" : "")}(long histId);");
          break;
        default:
          break;
      }

      return signatures;
    }
  }
}
