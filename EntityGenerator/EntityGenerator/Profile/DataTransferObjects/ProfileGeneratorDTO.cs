using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorDto"/> models generator settings for a project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorDto"/> class.
    /// </summary>
    public ProfileGeneratorDto()
    {
      GeneratorFrontend = new ProfileGeneratorFrontendDto();
      GeneratorBusinessLogic = new ProfileGeneratorBusinessLogicDto();
      GeneratorCommon = new ProfileGeneratorCommonDto();
      GeneratorDatabase = new ProfileGeneratorDatabaseDto();
    }

    /// <summary>
    /// The generator settings for the frontend project.
    /// </summary>
    public ProfileGeneratorFrontendDto GeneratorFrontend { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorBusinessLogicDto GeneratorBusinessLogic { get; set; }

    /// <summary>
    /// The generator settings for the common project.
    /// </summary>
    public ProfileGeneratorCommonDto GeneratorCommon { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorDatabaseDto GeneratorDatabase { get; set; }
  }
}