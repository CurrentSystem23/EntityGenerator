using EntityGenerator.Profile;

namespace EntityGenerator.DatabaseObjects.DataAccessObjects
{
  /// <summary>
  /// Abstract class <see cref="DataAccessObject"/> models the abstract technology independent data access for the source database.
  /// </summary>
  public abstract class DataAccessObject
  {

    /// <summary>
    /// The <see cref="DataAccessObject"/> Constructor
    /// </summary>
    /// <param name="profileProvider"> The <see cref="ProfileProvider"/>profile provider</param>
    public DataAccessObject(ProfileProvider profileProvider)
    {
      ProfileProvider = profileProvider;
    }

    /// <summary>
    /// The <see cref="ProfileProvider"/> for the current profile
    /// </summary>
    public ProfileProvider ProfileProvider { get; }

    ///<summary>Determine the count of all database objects for the generator</summary>
    ///<returns>the count of all database objects for the generator</returns>
    public abstract int DatabaseObjectCount();

  }
}
