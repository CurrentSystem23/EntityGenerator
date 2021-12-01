using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using System.Collections.Generic;

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

    ///<summary>Determine the schemas of a database for the generator</summary>
    ///<returns>the <see cref="List<SchemaDataTransferObject>"/> of all schemas in the database for the generator</returns>
    public abstract List<SchemaDataTransferObject> DatabaseSchemas();

    ///<summary>Determine the table value objects of a database for the generator</summary>
    ///<returns>the <see cref="List<TableValueObjectDataTransferObject>"/> of all schemas in the database for the generator</returns>
    public abstract List<TableValueObjectDataTransferObject> DatabaseTableValueObjects();

    ///<summary>Determine the columns of a database for the generator</summary>
    ///<returns>the <see cref="List<ColumnDataTransferObject>"/> of all schemas in the database for the generator</returns>
    public abstract List<ColumnDataTransferObject> DatabaseColumns();
  }
}
