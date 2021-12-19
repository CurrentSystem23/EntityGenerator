using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorCommonPresentationDto"/> models generator settings for the common presentation project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorCommonPresentationDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorCommonPresentationDto"/> class.
    /// </summary>
    public ProfileGeneratorCommonPresentationDto()
    {
    }

    /// <summary>
    /// Flag for generating interface converters.
    /// </summary>
    public bool InterfaceConverters { get; set; }
  }
}
