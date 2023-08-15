using EntityGenerator.Core.Extensions;
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
      if (parameters == null || parameters.Count == 0) return string.Empty;

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
      if (parameters == null || parameters.Count == 0) return string.Empty;

      string outStr = string.Empty;
      foreach (Column param in parameters)
      {
        outStr += String.Format(language.ParameterFormat, language.GetColumnDataType(param), param.Name);
      }

      // Remove trailing comma and white-space
      outStr = outStr.Remove(outStr.Length - 2);

      return outStr;
    }

    public static string GetParametersSqlString(List<Column> parameters)
    {
      StringBuilder outStr = new();
      foreach (Column param in parameters.OrEmptyIfNull())
      {
        outStr.AppendJoin(", ", parameters.Select(param => { return $@"@{param.Name}"; }));
      }

      return outStr.ToString();
    }
  }
}
