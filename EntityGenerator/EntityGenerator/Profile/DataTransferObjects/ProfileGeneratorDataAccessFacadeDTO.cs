using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorDataAccessFacadeDTO"/> models generator settings for the data access facade project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorDataAccessFacadeDTO
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorDataAccessFacadeDTO"/> class.
    /// </summary>
    public ProfileGeneratorDataAccessFacadeDTO()
    {
    }

    /// <summary>
    /// Flag for generating data access facade objects.
    /// </summary>
    public bool DataAccessFacade { get; set; }

  }
}
