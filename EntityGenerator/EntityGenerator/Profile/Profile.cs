namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <c>Profile</c> models the settings for the generators for a project.
  /// </summary>
  public class Profile
  {
    /// <summary>
    /// The global settings for the project.
    /// </summary>
    public ProfileGlobal Global { get; set; }

    /// <summary>
    /// The database settings for the project.
    /// </summary>
    public ProfileDatabase Database { get; set; }

    /// <summary>
    /// The path settings for the project.
    /// </summary>
    public ProfilePath Path { get; set; }

    /// <summary>
    /// The generator settings for the project.
    /// </summary>
    public ProfileGenerator Generator { get; set; }
  }
}