using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileDto"/> models the settings for a project.
  /// </summary>
  [Serializable]
  public class ProfileDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileDto"/> class.
    /// </summary>
    public ProfileDto()
    {
      Global = new ProfileGlobalDto();
      Database = new ProfileDatabaseDto();
      Path = new ProfilePathDto();
      Generator = new ProfileGeneratorDto();
    }

    /// <summary>
    /// The global settings for the project.
    /// </summary>
    public ProfileGlobalDto Global { get; set; }

    /// <summary>
    /// The database settings for the project.
    /// </summary>
    public ProfileDatabaseDto Database { get; set; }

    /// <summary>
    /// The path settings for the project.
    /// </summary>
    public ProfilePathDto Path { get; set; }

    /// <summary>
    /// The generator settings for the project.
    /// </summary>
    public ProfileGeneratorDto Generator { get; set; }
  }
}