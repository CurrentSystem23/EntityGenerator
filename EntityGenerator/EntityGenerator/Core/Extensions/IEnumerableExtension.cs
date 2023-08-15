using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Core.Extensions
{
  public static class IEnumerableExtensions
  {
    /// <summary>
    /// Returns an empty IEnumerable object if the input object is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
    {
      return source ?? Enumerable.Empty<T>();
    }
  }
}
