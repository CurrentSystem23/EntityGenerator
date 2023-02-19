using EntityGenerator.Core.Interfaces;

namespace EntityGeneratorCLI
{
  public class CliOutputProvider : IOutputProvider
  {
    public string OutputTitle { get; set; }
    public long MaxCount { get; set; }
    public long CurrentPosition { get; private set; }
    public void IncreasePosition()
    {
      CurrentPosition++;
    }

    public void DecreasePosition()
    {
      CurrentPosition--;
    }

    public void Reset()
    {
      OutputTitle = string.Empty;
      MaxCount = 0;
      CurrentPosition = 0;
    }
  }
}
