using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace EntityGenerator.DatabaseObjects.DataAccessObjects
{
  /// <summary>
  /// Abstract class <see cref="DataAccessObject"/> models the abstract technology independent data access for the source database.
  /// </summary>
  public class DataAccessObject : IDataAccessObject
  {
    /// <summary>
    /// The profile data.
    /// </summary>
    IDataAccessObject _dataAccessObject;

    /// <summary>
    /// The <see cref="DataAccessObject"/> Constructor
    /// </summary>
    /// <param name="serviceProvider"> The <see cref="IServiceProvider"/> dependency injection service provider.</param>
    /// <param name="profileProvider"> The <see cref="ProfileProvider"/> profile provider</param>
    public DataAccessObject(IServiceProvider serviceProvider, ProfileProvider profileProvider)
    {
      ProfileProvider = profileProvider;
      switch (ProfileProvider.Profile.Database.SourceDatabaseType)
      {
        case Profile.DataTransferObject.Enums.DatabaseTypes.MicrosoftSqlServer:
          _dataAccessObject = serviceProvider.GetRequiredService<MicrosoftSqlServerDao>();
          break;
        default:
          throw new NotImplementedException($"Unknown {nameof(ProfileProvider.Profile.Database.SourceDatabaseType)}");
      }
    }

    /// <inheritdoc />
    public ProfileProvider ProfileProvider { get; }

    /// <inheritdoc />
    public int DatabaseObjectCount()
    {
      return _dataAccessObject.DatabaseObjectCount();
    }

    /// <inheritdoc />
    public List<SchemaDto> DatabaseSchemas()
    {
      return _dataAccessObject.DatabaseSchemas();
    }

    /// <inheritdoc />
    public List<TableValueObjectDto> DatabaseTableValueObjects()
    {
      return _dataAccessObject.DatabaseTableValueObjects();
    }

    /// <inheritdoc />
    public List<ColumnDto> DatabaseColumns()
    {
      return _dataAccessObject.DatabaseColumns();
    }

    /// <inheritdoc />
    public List<FunctionDto> DatabaseFunctions()
    {
      return _dataAccessObject.DatabaseFunctions();
    }

    /// <inheritdoc />
    public void DatabaseFunctionReturnColumns(List<FunctionDto> databaseFunctions)
    {
      _dataAccessObject.DatabaseFunctionReturnColumns(databaseFunctions);
    }

    /// <inheritdoc />
    public List<ForeignKeyDto> DatabaseForeignKeys()
    {
      return _dataAccessObject.DatabaseForeignKeys();
    }

    /// <inheritdoc />
    public List<CheckConstraintDto> DatabaseCheckConstraints()
    {
      return _dataAccessObject.DatabaseCheckConstraints();
    }

    /// <inheritdoc />
    public List<IndexDto> DatabaseIndices()
    {
      return _dataAccessObject.DatabaseIndices();
    }

    /// <inheritdoc />
    public List<TriggerDto> DatabaseTriggers()
    {
      return _dataAccessObject.DatabaseTriggers();
    }
  }
}
