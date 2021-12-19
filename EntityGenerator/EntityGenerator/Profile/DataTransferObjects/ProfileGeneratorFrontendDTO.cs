using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorFrontendDto"/> models generator settings for the frontend project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorFrontendDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorFrontendDto"/> class.
    /// </summary>
    public ProfileGeneratorFrontendDto()
    {
    }

    /// <summary>
    /// Flag for generating frontend constants.
    /// </summary>
    public bool Constants { get; set; }

    /// <summary>
    /// Flag for generating frontend viewmodels.
    /// </summary>
    public bool ViewModels { get; set; }
  }
}
