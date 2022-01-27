using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public interface ICodeBuilder
  {
    public LanguageBase languageBase { get; }
    public IIndentationProvider indentationProvider { get; }
  }
}
