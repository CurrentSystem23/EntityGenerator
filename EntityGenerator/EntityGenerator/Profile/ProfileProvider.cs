﻿using EntityGenerator.Profile.DataTransferObject;
using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <see cref="ProfileProvider"/> models profile provider.
  /// </summary>
  public class ProfileProvider
  {
    /// <summary>
    /// The profile data.
    /// </summary>
    ProfileDTO _profile;

    /// <summary>
    /// The service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor for <see cref="ProfileProvider"/> class.
    /// </summary>
    /// <param name="serviceProvider"> The dependency injection service provider.</param>
    public ProfileProvider(IServiceProvider serviceProvider)
    { 
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Load profile data.
    /// </summary>
    /// <param name="data"> The profile data.</param>
    public void LoadProfile(string data)
    {
      LoadProfileFromFileJson(data);
      if (_profile == null)
        LoadProfileFromFileXml(data);
      if (_profile == null)
        LoadProfileFromJson(data);
      if (_profile == null)
        LoadProfileFromXml(data);
    }

    /// <summary>
    /// Load profile data from json structure.
    /// </summary>
    /// <param name="profile"> The json structure of the json profile.</param>
    public void LoadProfileFromJson(string profile)
    {
    }

    /// <summary>
    /// Load profile data from xml structure.
    /// </summary>
    /// <param name="profile"> The xml structure of the xml profile.</param>
    public void LoadProfileFromXml(string profile)
    {
    }

    /// <summary>
    /// Load profile data from json file.
    /// </summary>
    /// <param name="profilePath"> The path of the json profile file.</param>
    public void LoadProfileFromFileJson(string profilePath)
    {
    }

    /// <summary>
    /// Load profile data from xml file.
    /// </summary>
    /// <param name="profilePath"> The path of the xml profile file.</param>
    public void LoadProfileFromFileXml(string profilePath)
    {
    }

    /// <summary>
    /// Save profile data to json file.
    /// </summary>
    /// <param name="profilePath"> The path of the json profile file.</param>
    public void SaveProfileToFileJson(string profilePath)
    {
      _profile = new ProfileDTO();

      string profileDto = JsonSerializer.Serialize(_profile);
    }

    /// <summary>
    /// Save profile data to xml file.
    /// </summary>
    /// <param name="profilePath"> The path of the xml profile file.</param>
    public void SaveProfileToFileXml(string profilePath)
    {
      _profile = new ProfileDTO();

      XmlSerializer xmlSerializer = new XmlSerializer(_profile.GetType());

      using (StringWriter textWriter = new StringWriter())
      {
        xmlSerializer.Serialize(textWriter, _profile);
        string profileDto = textWriter.ToString();
      }
    }

    /// <summary>
    /// The connection string of the source database.
    /// </summary>
    public string ConnectionString => _profile.Database.ConnectionString;

    /// <summary>
    /// The database name of the source database.
    /// </summary>
    public string DatabaseName => _profile.Database.DatabaseName;
  }
}
