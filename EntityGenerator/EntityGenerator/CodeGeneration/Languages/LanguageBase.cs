using EntityGenerator.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages
{
  public abstract class LanguageBase
  {
    string Name { get; }

    protected StringBuilder _sb = new StringBuilder();

    public abstract string GetDataType(DataTypes type);
  }
}
