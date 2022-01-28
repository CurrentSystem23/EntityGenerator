using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public abstract class CodeBuilderBase<T>
    where T : CodeBuilderBase<T>
  {
    protected StringBuilder _sb;

    protected internal CodeBuilderBase(StringBuilder sb)
    {
      _sb = sb;
    }

    public T AppendLine()
    {
      _sb.AppendLine();
      return this as T;
    }

    public T AppendLine(string line)
    {
      _sb.AppendLine(line);
      return this as T;
    }

    public T Append(string str)
    {
      _sb.Append(str);
      return this as T;
    }
  }
}
