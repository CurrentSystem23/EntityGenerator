using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using System;
using System.Collections.Generic;

namespace EntityGenerator.DatabaseObjects.DataAccessObjects
{
  /// <summary>
  /// Abstract class <see cref="DataAccessObject"/> models the abstract technology independent data access for the source database.
  /// </summary>
  public interface IDataAccessObject
  {

    /// <summary>
    /// The <see cref="ProfileProvider"/> for the current profile
    /// </summary>
    ProfileProvider ProfileProvider { get; }

    ///<summary>Determine the count of all database objects for the generator</summary>
    ///<returns>the count of all database objects for the generator</returns>
    int DatabaseObjectCount();

    ///<summary>Determine the schemas of a database for the generator</summary>
    ///<returns>the <see cref="List<SchemaDataTransferObject>"/> of all schemas in the database for the generator</returns>
    List<SchemaDto> DatabaseSchemas();

    ///<summary>Determine the table value objects of a database for the generator</summary>
    ///<returns>the <see cref="List<TableValueObjectDataTransferObject>"/> of all table value objects in the database for the generator</returns>
    List<TableValueObjectDto> DatabaseTableValueObjects();

    ///<summary>Determine the columns of a database for the generator</summary>
    ///<returns>the <see cref="List<ColumnDataTransferObject>"/> of all columns in the database for the generator</returns>
    List<ColumnDto> DatabaseColumns();

    ///<summary>Determine the user defined functions of a database for the generator</summary>
    ///<returns>the <see cref="List<FunctionDataTransferObject>"/> of all user defined functions in the database for the generator</returns>
    List<FunctionDto> DatabaseFunctions();

    ///<summary>Determine the return column information for user defined functions of a database for the generator</summary>
    void DatabaseFunctionReturnColumns(List<FunctionDto> databaseFunctions);

    ///<summary>Determine the foreign keys of a database for the generator</summary>
    ///<returns>the <see cref="List<ForeignKeyDto>"/> of all foreign keys in the database for the generator</returns>
    List<ForeignKeyDto> DatabaseForeignKeys();

    ///<summary>Determine the check constraints of a database for the generator</summary>
    ///<returns>the <see cref="List<CheckConstraintDto>"/> of all check constraints in the database for the generator</returns>
    List<CheckConstraintDto> DatabaseCheckConstraints();

    ///<summary>Determine the indices of a database for the generator</summary>
    ///<returns>the <see cref="List<IndexDto>"/> of all indices in the database for the generator</returns>
    List<IndexDto> DatabaseIndices();

    ///<summary>Determine the triggers of a database for the generator</summary>
    ///<returns>the <see cref="List<TriggerDto>"/> of all triggers in the database for the generator</returns>
    List<TriggerDto> DatabaseTriggers();
  }
}
