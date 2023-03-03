using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Profile.DataTransferObjects;
using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorCommonPresentationDto"/> models generator settings for the common presentation project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorCommonPresentationDto : ProfileCodeGenerationBase
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorCommonPresentationDto"/> class.
    /// </summary>
    public ProfileGeneratorCommonPresentationDto() : base(DataTransferObjects.Enums.CodeGenerationModules.CommonPresentationGenerator)
    {
    }

    /// <summary>
    /// Flag for generating interface converters.
    /// </summary>
    public bool InterfaceConverters { get; set; }
  }
}
