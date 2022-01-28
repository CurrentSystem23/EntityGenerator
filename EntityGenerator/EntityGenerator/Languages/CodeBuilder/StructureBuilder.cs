using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public enum StructureType
  {
    CLASS = 0,
    INTERFACE = 1,
    ENUM = 2,
  }

  public class StructureBuilder : CodeBuilderBase<StructureBuilder>
  {
    protected internal StructureBuilder(StringBuilder sb, string name, string baseName, StructureType type, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier)
    : base(sb)
    {
      if (type == StructureType.INTERFACE && isStatic == true)
        throw new ArgumentException("Interfaces cannot be static");

      if (isAbstract && isStatic)
        throw new ArgumentException("Abstract and static modifiers cannot be used in the same context.");

      _sb.Append($"{accessModifier:g} ".ToLower());

      if (isAbstract) _sb.Append("abstract ");
      if (isStatic) _sb.Append("static ");
      if (isPartial) _sb.Append("partial ");

      switch (type)
      {
        case StructureType.CLASS:
          _sb.Append("class ");
          break;
        case StructureType.INTERFACE:
          _sb.Append("interface ");
          break;
        case StructureType.ENUM:
          _sb.Append("enum ");
          break;
        default:
          throw new ArgumentException("StructureType not supported");
      }

      _sb.AppendJoin(" : ", (new string[] { name, baseName }).Where(c => c != null).ToArray()).AppendLine();
      _sb.AppendLine("{");
    }

    public MethodBuilder BuildMethod(string methodName, string returnType = null, string accessType = "public", bool isStatic = false)
    {
      return new MethodBuilder(_sb, methodName, returnType, accessType, isStatic);
    }
  }
}
