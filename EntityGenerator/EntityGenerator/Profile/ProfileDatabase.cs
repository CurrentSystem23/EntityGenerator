using System.Collections.Generic;

namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <c>ProfileDatabase</c> models database settings for a project.
  /// </summary>
  public class ProfileDatabase
  {
    /// <summary>
    /// The connection string of source database.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// The type of source database from which it is generated.
    /// </summary>
    public Enums.DatabaseTypes SourceDatabaseType { get; set; } = Enums.DatabaseTypes.MicrosoftSqlServer;

    /// <summary>
    /// The type of database for which it is being generated.
    /// </summary>
    public List<Enums.DatabaseTypes> TargetDatabaseTypes { get; set; } = new List<Enums.DatabaseTypes>() { Enums.DatabaseTypes.MicrosoftSqlServer };

  }
}
