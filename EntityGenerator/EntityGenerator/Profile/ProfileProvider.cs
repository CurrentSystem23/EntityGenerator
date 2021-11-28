namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <see cref="ProfileProvider"/> models profile provider.
  /// </summary>
  public class ProfileProvider
  {
    Profile _profile;

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
