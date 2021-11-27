namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <c>ProfileGenerator</c> models generator settings for a project.
  /// </summary>
  public class ProfileGenerator
  {
    /// <summary>
    /// The generator settings for the frontend project.
    /// </summary>
    public ProfileGeneratorFrontend GeneratorFrontend { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorBusinessLogic GeneratorBusinessLogic { get; set; }

    /// <summary>
    /// The generator settings for the common project.
    /// </summary>
    public ProfileGeneratorBusinessLogic GeneratorCommon { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorDatabase GeneratorDatabase { get; set; }
  }
}