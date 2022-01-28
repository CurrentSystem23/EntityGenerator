using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages
{
  public abstract class LanguageBase
  {
    public abstract string Translate(in StringBuilder sb);
  }
}
