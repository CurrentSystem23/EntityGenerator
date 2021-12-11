using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorFrontendDTO"/> models generator settings for the frontend project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorFrontendDTO
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorFrontendDTO"/> class.
    /// </summary>
    public ProfileGeneratorFrontendDTO()
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
