using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Angular.TypeScript.Angular15;
using EntityGenerator.Profile.DataTransferObjects;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorFrontendDto"/> models generator settings for the frontend project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorFrontendDto : ProfileCodeGenerationBase
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorFrontendDto"/> class.
    /// </summary>
    public ProfileGeneratorFrontendDto() : base(DataTransferObjects.Enums.CodeGenerationModules.FrontendGenerator)
    {
      Language = Languages.ANGULAR_15_TYPESCRIPT;
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
