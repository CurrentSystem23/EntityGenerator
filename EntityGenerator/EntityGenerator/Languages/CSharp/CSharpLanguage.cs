using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CSharp
{
  public class CSharpLanguage : LanguageBase
  {
    public override string Translate(in StringBuilder sb)
    {
      return sb.ToString();
    }
  }
}
