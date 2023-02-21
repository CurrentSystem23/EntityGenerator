namespace EntityGenerator.Core.Interfaces;

public interface IStandardOutput
{
  IOutputProvider PrimaryOutputProvider { get; set; }
  IOutputProvider SecondaryOutputProvider { get; set; }
}