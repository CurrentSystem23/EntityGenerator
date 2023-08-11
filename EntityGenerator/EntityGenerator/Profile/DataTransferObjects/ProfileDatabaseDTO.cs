using EntityGenerator.Profile.SerializingHelper;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileDatabaseDto"/> models database settings for a project.
  /// </summary>
  [Serializable] 
  public class ProfileDatabaseDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileDatabaseDto"/> class.
    /// </summary>
    public ProfileDatabaseDto()
    {
    }

    /// <summary>
    /// The connection string of source database.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// The database name of source database.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// The type of source database from which it is generated.
    /// </summary>
    [JsonConverter(typeof(StringNullableEnumConverter<Enums.DatabaseTypes>))]
    public Enums.DatabaseTypes SourceDatabaseType { get; set; } = Enums.DatabaseTypes.MicrosoftSqlServer;

    /// <summary>
    /// The type of database for which it is being generated.
    /// </summary>
    [JsonConverter(typeof(ListStringEnumConverter<List<Enums.DatabaseTypes>>))]
    public List<Enums.DatabaseTypes> TargetDatabaseTypes { get; set; } = new List<Enums.DatabaseTypes>() { Enums.DatabaseTypes.MicrosoftSqlServer };

    public bool GuidIndexing { get; set; }
  }
}
