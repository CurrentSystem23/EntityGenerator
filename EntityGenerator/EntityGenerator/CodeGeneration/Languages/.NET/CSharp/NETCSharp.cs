using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : NETLanguageBase
  {
    public bool CloseExistingScope { get; set; }

    private bool openMethod = false;
    private bool openStructure = false;
    private bool openNameSpace = false;

    public NETCSharp(StringBuilder sb, List<DatabaseLanguageBase> databaseLanguages) : base(sb, databaseLanguages)
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
      OpenStructure(StructureType.INTERFACE, enumName, structureBase: null, isPartial: isPartial, accessModifier: accessModifier);
    }

    public override void OpenMethod(string methodName, string returnType = "void", AccessType accessModifier = AccessType.PUBLIC, bool isStatic = false)
    {
      if (!CloseExistingScope && openMethod)
      {
        throw new Exception("Error: Trying to open already existing method scope.");
      }

      CloseMethod();
      _sb.Append(accessModifier + " ");
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
      throw new NotImplementedException();
    }

    public override void BuildErrorLogCall(string message, string paramsStr, bool async)
    {
      throw new NotImplementedException();
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
          break; // TODO
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

    public override List<string> GetMethodSignatures(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string prefix, string parametersStr = null, string parametersWithTypeStr = null)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));
      List<string> signatures = new();

      switch (methodType)
      {
        case MethodType.GET:
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {prefix}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{name}[] orderBy);");
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {prefix}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");
          if (isTable)
          {
            signatures.Add($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {prefix}Get{(async ? "Async" : "")}({(profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            signatures.Add($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {prefix}Get{(async ? "Async" : "")}(Guid globalId);");
          }
          break;
        case MethodType.SAVE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Save{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.DELETE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Delete{(async ? "Async" : "")}({(profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          break;
        case MethodType.MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}Merge{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.COUNT:
          signatures.Add($"{(async ? $"Task<long>" : $"long")} {prefix}GetCount{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")});");
          break;
        case MethodType.HAS_CHANGED:
          signatures.Add($"{(async ? "Task<bool>" : "bool")} {prefix}HasChanged{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.BUlK_INSERT:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_MERGE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_UPDATE:
          signatures.Add($"{(async ? "Task" : "void")} {prefix}BulkUpdate{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          break;
        case MethodType.HIST_GET:
          signatures.Add($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {prefix}HistGets{(async ? "Async" : "")}({(profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          signatures.Add($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {prefix}HistEntryGet{(async ? "Async" : "")}(long histId);");
          break;
        default:
          break;
      }

      return signatures;
    }
  }
}
