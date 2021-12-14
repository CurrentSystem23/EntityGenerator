using System.Diagnostics;
using Xunit;

namespace IntegrationTests
{
  /// <summary>
  /// Source from: https://lostechies.com/jimmybogard/2013/06/20/run-tests-explicitly-in-xunit-net/
  /// </summary>
  public class RunnableInDebugOnlyAttribute : FactAttribute
  {
    /// <summary>
    /// RunnableInDebugOnlyAttribute constructor
    /// </summary>
    public RunnableInDebugOnlyAttribute()
    {
      if (!Debugger.IsAttached)
      {
        Skip = "Only running in interactive mode.";
      }
    }
  }
}
