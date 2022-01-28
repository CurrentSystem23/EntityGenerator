using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public interface IIndentationProvider
  {
    public int indentSize { get; set; }

    public StringBuilder ApplyIndentation(StringBuilder sb, bool openNameSpace);
  }
}
