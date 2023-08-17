using System;
using System.Text.Json.Serialization;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfilePathDto"/> models path settings for the project.
  /// </summary>
  [Serializable]
  public class ProfilePathDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfilePathDto"/> class.
    /// </summary>
    public ProfilePathDto()
    {
    }

    /// <summary>
    /// The root directory of the project.
    /// </summary>
    public string RootDir { get; set; } = string.Empty;

    /// <summary>
    /// The database directory of the project.
    /// </summary>
    [JsonIgnore]
    public string SqlDir => $"{RootDir}/{SqlDirRelative}";
    [JsonPropertyName("SqlDir")]
    public string SqlDirRelative { get; set; } = string.Empty;


    /// <summary>
    /// The data access directory of the project - DataAccessObjects (Ends with server technology e.g. \DataAccess.MicrosoftSqlServer).
    /// </summary>
    [JsonIgnore]
    public string DataAccessDir => $"{RootDir}/{DataAccessDirRelative}";
    [JsonPropertyName("DataAccessDir")]
    public string DataAccessDirRelative { get; set; } = string.Empty;

    /// <summary>
    /// The data access facade directory of the project.
    /// </summary>
    [JsonIgnore]
    public string DataAccessFacadeDir => $"{RootDir}/{DataAccessFacadeDirRelative}";
    [JsonPropertyName("DataAccessFacadeDir")]
    public string DataAccessFacadeDirRelative { get; set; } = string.Empty;

    /// <summary>
    /// The common directory of the project - Constants and DataTransferObjects.
    /// </summary>
    [JsonIgnore]
    public string CommonDir => $"{RootDir}/{CommonDirRelative}";
    [JsonPropertyName("CommonDir")]
    public string CommonDirRelative { get; set; } = string.Empty;


    /// <summary>
    /// The abstractions directory of the project - Interfaces.
    /// </summary>
    [JsonIgnore]
    public string AbstractionsDir => $"{RootDir}/{AbstractionsDirRelative}";
    [JsonPropertyName("AbstractionsDir")]
    public string AbstractionsDirRelative { get; set; } = string.Empty;


    /// <summary>
    /// The businesslogic directory of the project.
    /// </summary>
    [JsonIgnore]
    public string BusinessLogicDir => $"{RootDir}/{BusinessLogicDirRelative}";
    [JsonPropertyName("BusinessLogicDir")]
    public string BusinessLogicDirRelative { get; set; } = string.Empty;


    /// <summary>
    /// The frontend directory of the project.
    /// </summary>
    [JsonIgnore]
    public string FrontendDir => $"{RootDir}/{FrontEndDirRelative}";
    [JsonPropertyName("FrontendDir")]
    public string FrontEndDirRelative { get; set; } = string.Empty;
  }
}