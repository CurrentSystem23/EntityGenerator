using EntityGenerator.Profile.DataTransferObjects.Enums;
using EntityGenerator.Profile.SerializingHelper;
using System;
using System.Text.Json.Serialization;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGlobalDto"/> models global settings for the project.
  /// </summary>
  [Serializable]
  public class ProfileGlobalDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGlobalDto"/> class.
    /// </summary>
    public ProfileGlobalDto()
    {
    }

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
    /// Flag for clear start.
    /// </summary>
    public bool NoWipe { get; set; }

    /// <summary>
    /// Use GUIDs as indexing method instead of numbers
    /// </summary>
    public bool GuidIndexing { get; set; }
  }
}
