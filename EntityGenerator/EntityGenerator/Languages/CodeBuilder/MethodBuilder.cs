using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public class MethodBuilder : CodeBuilderBase<MethodBuilder>
  {
    protected internal MethodBuilder(StringBuilder sb, string methodName, string returnType = null, string accessType = "public", bool isStatic = false)
    : base(sb)
    {
      _sb.Append(accessType + " ");
      if (isStatic) _sb.Append("static ");
      _sb.Append(returnType);
      _sb.AppendLine(" " + methodName);
      _sb.AppendLine("{");
    }
  }
}
