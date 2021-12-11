using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileDTO"/> models the settings for a project.
  /// </summary>
  [Serializable]
  public class ProfileDTO
  {
    /// <summary>
    /// Constructor for <see cref="ProfileDTO"/> class.
    /// </summary>
    public ProfileDTO()
    {
      Global = new ProfileGlobalDTO();
      Database = new ProfileDatabaseDTO();
      Path = new ProfilePathDTO();
      Generator = new ProfileGeneratorDTO();
    }

    /// <summary>
    /// The global settings for the project.
    /// </summary>
    public ProfileGlobalDTO Global { get; set; }

    /// <summary>
    /// The database settings for the project.
    /// </summary>
    public ProfileDatabaseDTO Database { get; set; }

    /// <summary>
    /// The path settings for the project.
    /// </summary>
    public ProfilePathDTO Path { get; set; }

    /// <summary>
    /// The generator settings for the project.
    /// </summary>
    public ProfileGeneratorDTO Generator { get; set; }
  }
}