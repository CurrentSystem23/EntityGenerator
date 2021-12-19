using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorCommonDto"/> models generator settings for the common project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorCommonDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorCommonDto"/> class.
    /// </summary>
    public ProfileGeneratorCommonDto()
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
