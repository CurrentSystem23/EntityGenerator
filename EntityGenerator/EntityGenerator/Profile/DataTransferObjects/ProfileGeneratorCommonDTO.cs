using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorCommonDTO"/> models generator settings for the common project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorCommonDTO
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorCommonDTO"/> class.
    /// </summary>
    public ProfileGeneratorCommonDTO()
    {
    }

    /// <summary>
    /// Flag for generating backend constants.
    /// </summary>
    public bool ConstantsBackend { get; set; }

    /// <summary>
    /// Flag for generating data transfer objects.
    /// </summary>
    public bool DataTransferObjects { get; set; }
  }
}
