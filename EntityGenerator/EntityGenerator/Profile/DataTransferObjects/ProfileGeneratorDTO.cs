using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorDTO"/> models generator settings for a project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorDTO
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorDTO"/> class.
    /// </summary>
    public ProfileGeneratorDTO()
    {
      GeneratorFrontend = new ProfileGeneratorFrontendDTO();
      GeneratorBusinessLogic = new ProfileGeneratorBusinessLogicDTO();
      GeneratorCommon = new ProfileGeneratorCommonDTO();
      GeneratorDatabase = new ProfileGeneratorDatabaseDTO();
    }

    /// <summary>
    /// The generator settings for the frontend project.
    /// </summary>
    public ProfileGeneratorFrontendDTO GeneratorFrontend { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorBusinessLogicDTO GeneratorBusinessLogic { get; set; }

    /// <summary>
    /// The generator settings for the common project.
    /// </summary>
    public ProfileGeneratorCommonDTO GeneratorCommon { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorDatabaseDTO GeneratorDatabase { get; set; }
  }
}