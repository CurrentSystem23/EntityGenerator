using System;

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
    public string SqlDir { get; set; } = string.Empty;

    /// <summary>
    /// The data access directory of the project - DataAccessObjects (Ends with server technology e.g. \DataAccess.MicrosoftSqlServer).
    /// </summary>
    public string DataAccessDir { get; set; } = string.Empty;

    /// <summary>
    /// The data access facade directory of the project.
    /// </summary>
    public string DataAccessFacadeDir { get; set; } = string.Empty;

    /// <summary>
    /// The common directory of the project - Constants and DataTransferObjects.
    /// </summary>
    public string CommonDir { get; set; } = string.Empty;

    /// <summary>
    /// The abstractions directory of the project - Interfaces.
    /// </summary>
    public string AbstractionsDir { get; set; } = string.Empty;

    /// <summary>
    /// The businesslogic directory of the project.
    /// </summary>
    public string BusinessLogicDir { get; set; } = string.Empty;

    /// <summary>
    /// The frontend directory of the project.
    /// </summary>
    public string FrontendDir { get; set; } = string.Empty;
  }
}