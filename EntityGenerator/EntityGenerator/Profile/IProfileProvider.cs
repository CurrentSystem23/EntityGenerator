using EntityGenerator.Profile.DataTransferObject;
using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <see cref="IProfileProvider"/> models profile provider interface.
  /// </summary>
  public interface IProfileProvider
  {
    /// <summary>
    /// Load profile data.
    /// </summary>
    /// <param name="data"> The profile data.</param>
    void LoadProfile(string data);

    /// <summary>
    /// Load profile data from json structure.
    /// </summary>
    /// <param name="profile"> The json structure of the json profile.</param>
    void LoadProfileFromJson(string profile);

    /// <summary>
    /// Load profile data from xml structure.
    /// </summary>
    /// <param name="profile"> The xml structure of the xml profile.</param>
    void LoadProfileFromXml(string profile);

    /// <summary>
    /// Load profile data from json file.
    /// </summary>
    /// <param name="profilePath"> The path of the json profile file.</param>
    void LoadProfileFromFileJson(string profilePath);

    /// <summary>
    /// Load profile data from xml file.
    /// </summary>
    /// <param name="profilePath"> The path of the xml profile file.</param>
    void LoadProfileFromFileXml(string profilePath);

    /// <summary>
    /// Save profile data to json file.
    /// </summary>
    /// <param name="profilePath"> The path of the json profile file.</param>
    void SaveProfileToFileJson(string profilePath);

    /// <summary>
    /// Save profile data to xml file.
    /// </summary>
    /// <param name="profilePath"> The path of the xml profile file.</param>
    void SaveProfileToFileXml(string profilePath);

    /// <summary>
    /// The current profile.
    /// </summary>
    ProfileDto Profile { get; }


    /// <summary>
    /// The connection string of the source database.
    /// </summary>
    string ConnectionString { get; }

    /// <summary>
    /// The database name of the source database.
    /// </summary>
    string DatabaseName { get; }
  }
}
