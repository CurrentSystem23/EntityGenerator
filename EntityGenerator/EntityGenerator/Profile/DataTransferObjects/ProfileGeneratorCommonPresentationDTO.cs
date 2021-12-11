using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorCommonPresentationDTO"/> models generator settings for the common presentation project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorCommonPresentationDTO
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorCommonPresentationDTO"/> class.
    /// </summary>
    public ProfileGeneratorCommonPresentationDTO()
    {
    }

    /// <summary>
    /// Flag for generating interface converters.
    /// </summary>
    public bool InterfaceConverters { get; set; }
  }
}
