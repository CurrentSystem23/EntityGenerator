using EntityGenerator.Profile.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorCommonDto"/> models generator settings for the common project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorCommonDto : ProfileCodeGenerationBase
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorCommonDto"/> class.
    /// </summary>
    public ProfileGeneratorCommonDto() : base(Profile.DataTransferObjects.Enums.CodeGenerationModules.CommonGenerator)
    {
    }

    /// <summary>
    /// Flag for generating backend constants.
    /// </summary>
    public bool ConstantsBackend { get; set; }

    public List<ProfileGeneratorCommonConstant> Constants { get; set; }

    /// <summary>
    /// Flag for generating data transfer objects.
    /// </summary>
    public bool DataTransferObjects { get; set; }

    /// <summary>
    /// List of columns contained in every table. Will be part of BaseDto.
    /// </summary>
    public List<string>? BaseColumns { get; set; } = new() { "GlobalId", "ModifiedDate", "ModifiedUser" };
  }
}
