using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Helper
{
  public abstract class MethodHelper
  {
    public static List<MethodType> GetMethodTypes()
    {
      return new List<MethodType>((IEnumerable<MethodType>)Enum.GetValues(typeof(MethodType)));
    }
  }
}
