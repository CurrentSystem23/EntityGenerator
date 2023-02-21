namespace EntityGenerator.Core.Interfaces;

public interface IOutputProvider
{
  string OutputTitle { get; set; }
  long MaxCount { get; set; }
  long CurrentPosition { get; }

  void IncreasePosition();
  void DecreasePosition();
  void Reset();
}