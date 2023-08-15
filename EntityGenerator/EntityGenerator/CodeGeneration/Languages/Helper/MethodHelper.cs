using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Models.Enums;

namespace EntityGenerator.CodeGeneration.Languages.Helper
{
  public abstract class MethodHelper
  {
    /// <summary>
    /// Checks if a given MethodType can be generated for DbObjectType.
    /// </summary>
    /// <param name="objectType"></param>
    /// <param name="methodType"></param>
    /// <returns></returns>
    public static bool IsValidMethodType(DbObjectType objectType, MethodType methodType)
    {
      return objectType switch
      {
        DbObjectType.TABLE => methodType switch
        {
          MethodType.EXECUTE => false,
          _ => true,
        },
        DbObjectType.FUNCTION => methodType switch
        {
          MethodType.EXECUTE => true,
          _ => false,
        },
        DbObjectType.VIEW => methodType switch
        {
          MethodType.GET => true,
          MethodType.COUNT => true,
          _ => false,
        },
        DbObjectType.TABLEVALUEFUNCTION => methodType switch
        {
          MethodType.GET => true,
          MethodType.COUNT => true,
          _ => false,
        },
        _ => false,
      };
    }
  }
}
