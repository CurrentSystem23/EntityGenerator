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
using static Azure.Core.HttpHeader;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : NETLanguageBase
  {
    private bool CloseExistingScope { get; set; }

    private bool openMethod = false;
    private bool openStructure = false;
    private bool openNameSpace = false;

    public NETCSharp(StringBuilder sb) : base(sb)
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

    private void OpenStructure(bool isInterface, string structureName, string structureBase = null, bool isStatic = false, bool isPartial = false, bool isAbstract = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      if (!CloseExistingScope && openStructure)
      {
        throw new Exception("Error: Trying to open already existing structure scope.");
      }

      CloseStructure();
      _sb.Append($"{accessModifier:g} ".ToLower());
      if (isPartial) _sb.Append("partial ");
      _sb.Append($"{(isInterface ? "interface" : "class")} ");
      _sb.AppendJoin(" : ", (new string[] { structureName, structureBase }).Where(c => c != null).ToArray()).AppendLine();
      _sb.AppendLine("{");
    }


    protected override void OpenClass(string className, string baseClass = null, bool isStatic = false, bool isPartial = false, bool isAbstract = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      OpenStructure(true, className, baseClass, isStatic: isStatic, isPartial: isPartial, accessModifier: accessModifier);
    }

    protected override void OpenInterface(string interfaceName, string baseInterface = null, bool isPartial = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      OpenStructure(true, interfaceName, baseInterface, isPartial: isPartial, accessModifier: accessModifier);
    }

    protected override void OpenMethod(string methodName, string returnType = "void", AccessType accessModifier = AccessType.PUBLIC, bool isStatic = false)
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

    protected override void OpenMethod(string fullMethodSignature)
    {
      _sb.AppendLine(fullMethodSignature);
      _sb.AppendLine("{");
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

    protected override void CloseStructure()
    {
      if (!openStructure)
      {
        return;
      }
      CloseMethod();
      _sb.AppendLine("}");
      openStructure = false;
    }

    protected override void CloseMethod()
    {
      if (!openMethod)
      {
        return;
      }
      _sb.AppendLine("}");
      openMethod = false;
    }

    protected override void BuildTraceLogCall(string message, string paramsStr, bool async)
    {
      throw new NotImplementedException();
    }

    public override string GetDataType(DataTypes type)
    {
      switch (type)
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
          throw new NotSupportedException($"Error: Type {type} is not supported.");
      }
      return string.Empty;
    }

    protected override void BuildMethodSignature(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));

      switch (methodType)
      {
        case MethodType.GET:
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{name}[] orderBy);");
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}Gets{(async ? "Async" : "")}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");
          if (isTable)
          {
            _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}Get{(async ? "Async" : "")}({(profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}Get{(async ? "Async" : "")}(Guid globalId);");
          }
          break;
        case MethodType.SAVE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}Save{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.DELETE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}Delete{(async ? "Async" : "")}({(profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          break;
        case MethodType.MERGE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}Merge{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.COUNT:
          _sb.AppendLine($"{(async ? $"Task<long>" : $"long")} {GetFullFunctionPrefix(schema, name)}GetCount{(async ? "Async" : string.Empty)}({(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")});");
          break;
        case MethodType.HAS_CHANGED:
          _sb.AppendLine($"{(async ? "Task<bool>" : "bool")} {GetFullFunctionPrefix(schema, name)}HasChanged{(async ? "Async" : "")}({dtoName} dto);");
          break;
        case MethodType.BUlK_INSERT:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_MERGE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_UPDATE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkUpdate{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          break;
        case MethodType.HIST_GET:
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}HistGets{(async ? "Async" : "")}({(profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}HistEntryGet{(async ? "Async" : "")}(long histId);");
          break;
        default:
          break;
      }
    }

    protected override void BuildInternalMethodSignature(ProfileDto profile, Schema schema, MethodType methodType, string name, bool isTable, bool async, string parametersStr = null, string parametersWithTypeStr = null)
    {
      string dtoName = TypeHelper.GetDtoType(name, isTable, (methodType == MethodType.HIST_GET));

      switch (methodType)
      {
        case MethodType.GET:
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {name}Gets{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd{(parametersWithTypeStr != "" ? $", {parametersWithTypeStr}" : "")});");
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {name}Gets{(async ? "Async" : "")}(WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}params Order{name}[] orderBy);");
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {name}Gets{(async ? "Async" : "")}(WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{name}>")} {name}Gets{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, WhereClause whereClause, bool distinct, {(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}, " : "")}int? pageNum, int? pageSize, params Order{name}[] orderBy);");

          if (isTable)
          {
            _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {name}Get{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {(profile.Global.GuidIndexing ? "Guid" : "long")} id);");
            _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {name}Get{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, Guid globalId);");
          }
          break;
        case MethodType.SAVE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {name}Save{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {name}Dto dto);");
          break;
        case MethodType.DELETE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {name}Delete{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {(profile.Global.GuidIndexing ? "Guid" : "long")} id);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {name}Delete{(async ? "Async" : "")}(WhereClause whereClause);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {name}Delete{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, WhereClause whereClause);");
          break;
        case MethodType.MERGE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {name}Merge{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {name}Dto dto);");
          break;
        case MethodType.COUNT:
          _sb.AppendLine($"{(async ? $"Task<long>" : $"long")} {name}GetCount{(async ? "Async" : string.Empty)}(WhereClause whereClause{(parametersWithTypeStr != "" ? $"{parametersWithTypeStr}" : "")});");
          break;
        case MethodType.HAS_CHANGED:
          _sb.AppendLine($"{(async ? "Task<bool>" : "bool")} {name}HasChanged{(async ? "Async" : "")}(SqlConnection con, SqlCommand cmd, {name}Dto dto);");
          break;
        case MethodType.BUlK_INSERT:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}_TempBulkInsert{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_MERGE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkMerge{(async ? "Async" : "")}(ICollection<{dtoName}> dtos, bool identityInsert);");
          break;
        case MethodType.BULK_UPDATE:
          _sb.AppendLine($"{(async ? "Task" : "void")} {GetFullFunctionPrefix(schema, name)}BulkUpdate{(async ? "Async" : "")}(ICollection<{dtoName}> dtos);");
          break;
        case MethodType.HIST_GET:
          _sb.AppendLine($"{(async ? $"Task<ICollection<{dtoName}>>" : $"ICollection<{dtoName}>")} {GetFullFunctionPrefix(schema, name)}HistGets{(async ? "Async" : "")}({(profile.Database.GuidIndexing ? "Guid" : "long")} id);");
          _sb.AppendLine($"{(async ? $"Task<{dtoName}>" : $"{dtoName}")} {GetFullFunctionPrefix(schema, name)}HistEntryGet{(async ? "Async" : "")}(long histId);");
          break;
        default:
          break;
      }
    }
  }
}
