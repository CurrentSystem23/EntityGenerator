using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorBusinessLogicDTO"/> models generator settings for the business logic project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorBusinessLogicDTO
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorBusinessLogicDTO"/> class.
    /// </summary>
    public ProfileGeneratorBusinessLogicDTO()
    {
    }

    /// <summary>
    /// Flag for generating data access methods in the business logic layer.
    /// </summary>
    public bool Logic { get; set; }
  }
}
