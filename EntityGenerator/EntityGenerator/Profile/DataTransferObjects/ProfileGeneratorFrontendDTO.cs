using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Profile.DataTransferObjects;
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
    public ProfileGeneratorFrontendDto() : base(DataTransferObjects.Enums.CodeGenerationModules.FRONTEND)
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
