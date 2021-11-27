namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <c>ProfileGlobal</c> models global settings for the project.
  /// </summary>
  public class ProfileGlobal
  {
    /// <summary>
    /// The name of the project.
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// The prefix for generated files.
    /// </summary>
    public string GeneratedPrefix { get; set; } = string.Empty;

    /// <summary>
    /// The suffix for generated files.
    /// </summary>
    public string GeneratedSuffix { get; set; } = ".Generated";

    /// <summary>
    /// The folder name for generated files.
    /// </summary>
    public string GeneratedFolder { get; set; } = "_Generated";

    /// <summary>
    /// The language of the backend.
    /// </summary>
    public Enums.Languages LanguageBackend { get; set; } = Enums.Languages.CSharp;

    /// <summary>
    /// The language of the frontend.
    /// </summary>
    public Enums.Languages LanguageFrontend { get; set; } = Enums.Languages.TypeScript;

    /// <summary>
    /// Flag for clear start.
    /// </summary>
    public bool NoWipe { get; set; }
  }
}
