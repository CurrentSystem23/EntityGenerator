using EntityGenerator.Core.Interfaces;

namespace EntityGeneratorCLI
{
  public class CliStandardOutput : IStandardOutput
  {
    public IOutputProvider PrimaryOutputProvider { get; set; }
    public IOutputProvider SecondaryOutputProvider { get; set; }
  }
}
