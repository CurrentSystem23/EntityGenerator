using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Profile.DataTransferObjects;
using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorDataAccessFacadeDto"/> models generator settings for the data access facade project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorDataAccessFacadeDto : ProfileCodeGenerationBase
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorDataAccessFacadeDto"/> class.
    /// </summary>
    public ProfileGeneratorDataAccessFacadeDto() : base(DataTransferObjects.Enums.CodeGenerationModules.DataAccessFacadeGenerator)
    {
    }

    /// <summary>
    /// Flag for generating data access facade objects.
    /// </summary>
    public bool DataAccessFacade { get; set; }

    /// <summary>
    /// Flag for generating internal- and external inside the same file.
    /// </summary>
    public bool CombinedInterfaces { get; set; }
  }
}
