using EntityGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Helper
{
  public abstract class ParameterHelper
  {
    public virtual string GetFunctionParameterString(Function function, LanguageBase language)
    {
      string outStr = string.Empty;
      foreach (Column param in function.Parameters)
      {
        outStr += $"{language.GetDataType(param.ColumnTypeDataType)} {param.Name}, ";
      }

      // Remove trailing comma and white-space
      outStr = outStr.Remove(outStr.Length - 2);

      return outStr;
    }
  }
}
