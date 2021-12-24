using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using System;
using System.Collections.Generic;

namespace EntityGenerator.DatabaseObjects.DataAccessObjects
{
  /// <summary>
  /// Abstract class <see cref="DataAccessObject"/> models the abstract technology independent data access for the source database.
  /// </summary>
  public abstract class DataAccessObject : IDataAccessObject
  {
    /// <summary>
    /// The profile data.
    /// </summary>
    IServiceProvider _serviceProvider;

    /// <summary>
    /// The <see cref="DataAccessObject"/> Constructor
    /// </summary>
    /// <param name="serviceProvider"> The <see cref="IServiceProvider"/> dependency injection service provider.</param>
    /// <param name="profileProvider"> The <see cref="ProfileProvider"/> profile provider</param>
    public DataAccessObject(IServiceProvider serviceProvider, ProfileProvider profileProvider)
    {
      _serviceProvider = serviceProvider;
      ProfileProvider = profileProvider;
    }

    /// <inheritdoc />
    public ProfileProvider ProfileProvider { get; }

    /// <inheritdoc />
    public abstract int DatabaseObjectCount();

    /// <inheritdoc />
    public abstract List<SchemaDto> DatabaseSchemas();

    /// <inheritdoc />
    public abstract List<TableValueObjectDto> DatabaseTableValueObjects();

    /// <inheritdoc />
    public abstract List<ColumnDto> DatabaseColumns();

    /// <inheritdoc />
    public abstract List<FunctionDto> DatabaseFunctions();

    /// <inheritdoc />
    public abstract void DatabaseFunctionReturnColumns(List<FunctionDto> databaseFunctions);

    /// <inheritdoc />
    public abstract List<ForeignKeyDto> DatabaseForeignKeys();

    /// <inheritdoc />
    public abstract List<CheckConstraintDto> DatabaseCheckConstraints();

    /// <inheritdoc />
    public abstract List<IndexDto> DatabaseIndices();

    /// <inheritdoc />
    public abstract List<TriggerDto> DatabaseTriggers();
  }
}
