using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorBusinessLogicDto"/> models generator settings for the business logic project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorBusinessLogicDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorBusinessLogicDto"/> class.
    /// </summary>
    public ProfileGeneratorBusinessLogicDto()
    {
    }

    /// <summary>
    /// Flag for generating data access methods in the business logic layer.
    /// </summary>
    public bool Logic { get; set; }
  }
}
