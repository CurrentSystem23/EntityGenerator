﻿using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.Core.Models;
using EntityGenerator.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : NETLanguageBase
  {
    protected bool openNamespace = false;
    protected bool embeddedClass = false;

    public override StringBuilder BuildClassHeader(string className, string baseClass = null, bool isStatic = false, bool isPartial = false, bool isAbstract = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      throw new NotImplementedException();
    }

    public override StringBuilder BuildInterfaceHeader(string interfaceName, string baseInterface = null, bool isPartial = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      throw new NotImplementedException();
    }

    public override StringBuilder BuildTraceLogCall(string message, string paramsStr, bool async)
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
  }
}