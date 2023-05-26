using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Helper
{
  public abstract class ParameterHelper
  {
    public static string GetParametersString(List<Column> parameters)
    {
      string outStr = string.Empty;
      foreach (Column param in parameters)
      {
        outStr += $"{param.Name}, ";
      }

      // Remove trailing comma and white-space
      outStr = outStr.Remove(outStr.Length - 2);

      return outStr;
    }

    public static string GetParametersStringWithType(List<Column> parameters, LanguageBase language)
    {
      string outStr = string.Empty;
      foreach (Column param in parameters)
      {
        outStr += String.Format(language.ParameterFormat, language.GetDataType(param.ColumnTypeDataType), param.Name);
      }

      // Remove trailing comma and white-space
      outStr = outStr.Remove(outStr.Length - 2);

      return outStr;
    }
  }
}
