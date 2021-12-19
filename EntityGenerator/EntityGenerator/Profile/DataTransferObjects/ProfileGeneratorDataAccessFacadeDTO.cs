using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorDataAccessFacadeDto"/> models generator settings for the data access facade project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorDataAccessFacadeDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorDataAccessFacadeDto"/> class.
    /// </summary>
    public ProfileGeneratorDataAccessFacadeDto()
    {
    }

    /// <summary>
    /// Flag for generating data access facade objects.
    /// </summary>
    public bool DataAccessFacade { get; set; }

  }
}
