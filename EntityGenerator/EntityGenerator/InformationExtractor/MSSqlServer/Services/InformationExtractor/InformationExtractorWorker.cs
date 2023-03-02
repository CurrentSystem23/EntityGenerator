using EntityGenerator.Core.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.Interfaces;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;
using EntityGenerator.Profile.DataTransferObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractor;

public class InformationExtractorWorker : IInformationExtractorWorker
{
  /// <summary>
  /// The service provider.
  /// </summary>
  private readonly IServiceProvider _serviceProvider;

  /// <summary>
  /// The profile provider.
  /// </summary>
  private readonly IInformationExtractor _informationExtractor;

  /// <summary>
  /// Constructor for <see cref="InformationExtractorWorker"/> class.
  /// </summary>
  /// <param name="serviceProvider"> The dependency injection service provider.</param>
  /// <param name="informationExtractor"> The information extractor service.</param>
  public InformationExtractorWorker(IServiceProvider serviceProvider, IInformationExtractor informationExtractor)
  {
    _serviceProvider = serviceProvider;
    _informationExtractor = informationExtractor;
    OutputProvider = null;
  }

  /// <summary>
  /// Get or set the output provider.
  /// </summary>
  public IOutputProvider OutputProvider { get; set; }

  /// <inheritdoc/>
  public Database ExtractData(ProfileDto profile)
  {
    InitializeOutputProvider(profile);
    using SqlConnection con = new(profile.Database.ConnectionString);
    con.Open();
    ICollection<ColumnDto> columns = _informationExtractor.ColumnGets(con, profile.Database.DatabaseName);
    ICollection<ConstraintDto> constraints = _informationExtractor.ConstraintGets(con, profile.Database.DatabaseName);
    ICollection<DatabaseDto> databases = _informationExtractor.DatabaseGets(con, profile.Database.DatabaseName);
    ICollection<DatabaseObjectDto> databaseObjects = _informationExtractor.DatabaseObjectGets(con, profile.Database.DatabaseName);
    ICollection<ExtendedColumnPropertyDto> extendedColumnProperties = _informationExtractor.ExtendedColumnPropertyGets(con, profile.Database.DatabaseName);
    ICollection<ExtendedTablePropertyDto> extendedTableProperties = _informationExtractor.ExtendedTablePropertyGets(con, profile.Database.DatabaseName);
    ICollection<ForeignKeyDto> foreignKeys = _informationExtractor.ForeignKeyGets(con, profile.Database.DatabaseName);
    ICollection<FunctionDto> functions = _informationExtractor.FunctionGets(con, profile.Database.DatabaseName);
    ICollection<IndexDto> indexes = _informationExtractor.IndexGets(con, profile.Database.DatabaseName);
    ICollection<SchemaDto> schemas = _informationExtractor.SchemaGets(con, profile.Database.DatabaseName);
    ICollection<TableValueFunctionsReturnValueDto> tableValueFunctionsReturnValues = _informationExtractor.TableValueFunctionsReturnValueGets(con, profile.Database.DatabaseName);
    ICollection<TriggerDto> triggers = _informationExtractor.TriggerGets(con, profile.Database.DatabaseName);
    ICollection<TypeDto> types = _informationExtractor.UsedTypeGets(con, profile.Database.DatabaseName);
    ICollection<UserDefinedTableTypeColumnDto> userDefinedTableTypeColumns = _informationExtractor.UserDefinedTableTypeGets(con, profile.Database.DatabaseName);
    con.Close();

    return CoreMapper.MapToCoreModel(
      databases,
      schemas,
      databaseObjects,
      functions,
      tableValueFunctionsReturnValues,
      columns,
      extendedTableProperties,
      extendedColumnProperties,
      types,
      userDefinedTableTypeColumns,
      constraints,
      foreignKeys,
      indexes,
      triggers
    );

  }

  /// <inheritdoc/>
  public long GetDataCount(ProfileDto profile)
  {
    using SqlConnection con = new(profile.Database.ConnectionString);
    con.Open();
    long columnsCount = _informationExtractor.ColumnCountGet(con, profile.Database.DatabaseName);
    long constraintsCount = _informationExtractor.ConstraintCountGet(con, profile.Database.DatabaseName);
    long databasesCount = _informationExtractor.DatabaseCountGet(con, profile.Database.DatabaseName);
    long databaseObjectsCount = _informationExtractor.DatabaseObjectCountGet(con, profile.Database.DatabaseName);
    long extendedColumnPropertiesCount = _informationExtractor.ExtendedColumnPropertyCountGet(con, profile.Database.DatabaseName);
    long extendedTablePropertiesCount = _informationExtractor.ExtendedTablePropertyCountGet(con, profile.Database.DatabaseName);
    long foreignKeysCount = _informationExtractor.ForeignKeyCountGet(con, profile.Database.DatabaseName);
    long functionsCount = _informationExtractor.FunctionCountGet(con, profile.Database.DatabaseName);
    long indexesCount = _informationExtractor.IndexCountGet(con, profile.Database.DatabaseName);
    long schemasCount = _informationExtractor.SchemaCountGet(con, profile.Database.DatabaseName);
    long tableValueFunctionsReturnValuesCount = _informationExtractor.TableValueFunctionsReturnValueCountGet(con, profile.Database.DatabaseName);
    long triggersCount = _informationExtractor.TriggerCountGet(con, profile.Database.DatabaseName);
    long typesCount = _informationExtractor.UsedTypeCountGet(con, profile.Database.DatabaseName);
    long userDefinedTableTypeColumnsCount = _informationExtractor.UserDefinedTableTypeCountGet(con, profile.Database.DatabaseName);
    con.Close();

    long sum = columnsCount + constraintsCount + databasesCount + databaseObjectsCount +
               extendedColumnPropertiesCount +
               extendedTablePropertiesCount + foreignKeysCount + functionsCount + indexesCount + schemasCount +
               tableValueFunctionsReturnValuesCount + triggersCount + typesCount + userDefinedTableTypeColumnsCount;

    return sum;
  }

  /// <summary>
  /// Initialize the output provider.
  /// </summary>
  private void InitializeOutputProvider(ProfileDto profile)
  {
    if (OutputProvider != null)
    {
      OutputProvider.Reset();
      OutputProvider.OutputTitle = "Lese Datenbankinformationen: ";
      OutputProvider.MaxCount = GetDataCount(profile);
    }
  }
}

