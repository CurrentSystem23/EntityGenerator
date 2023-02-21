using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityGenerator.InformationExtractor.Interfaces
{
  public interface IInformationExtractor
  {
    /// <summary>
    /// Returns the count of all matching columns from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ColumnDto}"/> of all <see cref="ColumnDto"/> in matching database.</returns>
    Task<long> ColumnCountGetAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns all matching columns from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ColumnDto}"/> of all <see cref="ColumnDto"/> in matching database.</returns>
    Task<ICollection<ColumnDto>> ColumnGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching columns from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ColumnDto}"/> of all <see cref="ColumnDto"/> in matching database.</returns>
    long ColumnCountGet(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns all matching columns from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ColumnDto}"/> of all <see cref="ColumnDto"/> in matching database.</returns>
    ICollection<ColumnDto> ColumnGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching constraints from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ConstraintDto}"/> of all <see cref="ConstraintDto"/> in matching database.</returns>
    Task<long> ConstraintCountGetAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns all matching constraints from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ConstraintDto}"/> of all <see cref="ConstraintDto"/> in matching database.</returns>
    Task<ICollection<ConstraintDto>> ConstraintGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching constraints from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ConstraintDto}"/> of all <see cref="ConstraintDto"/> in matching database.</returns>
    long ConstraintCountGet(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns all matching constraints from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ConstraintDto}"/> of all <see cref="ConstraintDto"/> in matching database.</returns>
    ICollection<ConstraintDto> ConstraintGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching databases from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseDto}"/> of all <see cref="DatabaseDto"/> of all matching databases.</returns>
    Task<long> DatabaseCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching databases from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseDto}"/> of all <see cref="DatabaseDto"/> of all matching databases.</returns>
    Task<ICollection<DatabaseDto>> DatabaseGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching databases from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseDto}"/> of all <see cref="DatabaseDto"/> of all matching databases.</returns>
    long DatabaseCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching databases from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseDto}"/> of all <see cref="DatabaseDto"/> of all matching databases.</returns>
    ICollection<DatabaseDto> DatabaseGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching database objects from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseObjectDto}"/> of all <see cref="DatabaseObjectDto"/> in matching database.</returns>
    Task<long> DatabaseObjectCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching database objects from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseObjectDto}"/> of all <see cref="DatabaseObjectDto"/> in matching database.</returns>
    Task<ICollection<DatabaseObjectDto>> DatabaseObjectGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching database objects from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseObjectDto}"/> of all <see cref="DatabaseObjectDto"/> in matching database.</returns>
    long DatabaseObjectCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching database objects from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{DatabaseObjectDto}"/> of all <see cref="DatabaseObjectDto"/> in matching database.</returns>
    ICollection<DatabaseObjectDto> DatabaseObjectGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching extended column property from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedColumnPropertyDto}"/> of all <see cref="ExtendedColumnPropertyDto"/> in matching database.</returns>
    Task<long> ExtendedColumnPropertyCountGetAsync( SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching extended column property from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedColumnPropertyDto}"/> of all <see cref="ExtendedColumnPropertyDto"/> in matching database.</returns>
    Task<ICollection<ExtendedColumnPropertyDto>> ExtendedColumnPropertyGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching extended column property from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedColumnPropertyDto}"/> of all <see cref="ExtendedColumnPropertyDto"/> in matching database.</returns>
    long ExtendedColumnPropertyCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching extended column property from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedColumnPropertyDto}"/> of all <see cref="ExtendedColumnPropertyDto"/> in matching database.</returns>
    ICollection<ExtendedColumnPropertyDto> ExtendedColumnPropertyGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching extended table properties from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedTablePropertyDto}"/> of all <see cref="ExtendedTablePropertyDto"/> in matching database.</returns>
    Task<long> ExtendedTablePropertyCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching extended table properties from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedTablePropertyDto}"/> of all <see cref="ExtendedTablePropertyDto"/> in matching database.</returns>
    Task<ICollection<ExtendedTablePropertyDto>> ExtendedTablePropertyGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching extended table properties from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedTablePropertyDto}"/> of all <see cref="ExtendedTablePropertyDto"/> in matching database.</returns>
    long ExtendedTablePropertyCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching extended table properties from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ExtendedTablePropertyDto}"/> of all <see cref="ExtendedTablePropertyDto"/> in matching database.</returns>
    ICollection<ExtendedTablePropertyDto> ExtendedTablePropertyGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching foreign keys from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ForeignKeyDto}"/> of all <see cref="ForeignKeyDto"/> in matching database.</returns>
    Task<long> ForeignKeyCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching foreign keys from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ForeignKeyDto}"/> of all <see cref="ForeignKeyDto"/> in matching database.</returns>
    Task<ICollection<ForeignKeyDto>> ForeignKeyGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching foreign keys from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ForeignKeyDto}"/> of all <see cref="ForeignKeyDto"/> in matching database.</returns>
    long ForeignKeyCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching foreign keys from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{ForeignKeyDto}"/> of all <see cref="ForeignKeyDto"/> in matching database.</returns>
    ICollection<ForeignKeyDto> ForeignKeyGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching functions from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{FunctionDto}"/> of all <see cref="FunctionDto"/> in matching database.</returns>
    Task<long> FunctionCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching functions from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{FunctionDto}"/> of all <see cref="FunctionDto"/> in matching database.</returns>
    Task<ICollection<FunctionDto>> FunctionGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching functions from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{FunctionDto}"/> of all <see cref="FunctionDto"/> in matching database.</returns>
    long FunctionCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching functions from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{FunctionDto}"/> of all <see cref="FunctionDto"/> in matching database.</returns>
    ICollection<FunctionDto> FunctionGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching indexes from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{IndexDto}"/> of all <see cref="IndexDto"/> in matching database.</returns>
    Task<long> IndexCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching indexes from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{IndexDto}"/> of all <see cref="IndexDto"/> in matching database.</returns>
    Task<ICollection<IndexDto>> IndexGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching indexes from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{IndexDto}"/> of all <see cref="IndexDto"/> in matching database.</returns>
    long IndexCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching indexes from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{IndexDto}"/> of all <see cref="IndexDto"/> in matching database.</returns>
    ICollection<IndexDto> IndexGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching schemas from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{SchemaDto}"/> of all <see cref="SchemaDto"/> in matching database.</returns>
    Task<long> SchemaCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching schemas from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{SchemaDto}"/> of all <see cref="SchemaDto"/> in matching database.</returns>
    Task<ICollection<SchemaDto>> SchemaGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching schemas from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{SchemaDto}"/> of all <see cref="SchemaDto"/> in matching database.</returns>
    long SchemaCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching schemas from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{SchemaDto}"/> of all <see cref="SchemaDto"/> in matching database.</returns>
    ICollection<SchemaDto> SchemaGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching table value function return values from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    Task<long> TableValueFunctionsReturnValueCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching table value function return values from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    Task<ICollection<TableValueFunctionsReturnValueDto>> TableValueFunctionsReturnValueGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching table value function return values from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    long TableValueFunctionsReturnValueCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching table value function return values from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    ICollection<TableValueFunctionsReturnValueDto> TableValueFunctionsReturnValueGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching triggers from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    Task<long> TriggerCountGetAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns all matching triggers from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    Task<ICollection<TriggerDto>> TriggerGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching triggers from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    long TriggerCountGet(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns all matching triggers from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TableValueFunctionsReturnValueDto}"/> of all <see cref="TableValueFunctionsReturnValueDto"/> in matching database.</returns>
    ICollection<TriggerDto> TriggerGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching used types from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TypeDto}"/> of all <see cref="TypeDto"/> in matching database.</returns>
    Task<long> UsedTypeCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching used types from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TypeDto}"/> of all <see cref="TypeDto"/> in matching database.</returns>
    Task<ICollection<TypeDto>> UsedTypeGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching used types from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TypeDto}"/> of all <see cref="TypeDto"/> in matching database.</returns>
    long UsedTypeCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching used types from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{TypeDto}"/> of all <see cref="TypeDto"/> in matching database.</returns>
    ICollection<TypeDto> UsedTypeGets(SqlConnection con, string databaseName);

    /// <summary>
    /// Returns the count of all matching user defined table types from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{UserDefinedTableTypeColumnDto}"/> of all <see cref="UserDefinedTableTypeColumnDto"/> in matching database.</returns>
    Task<long> UserDefinedTableTypeCountGetAsync(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching user defined table types from server by given database name asynchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{UserDefinedTableTypeColumnDto}"/> of all <see cref="UserDefinedTableTypeColumnDto"/> in matching database.</returns>
    Task<ICollection<UserDefinedTableTypeColumnDto>> UserDefinedTableTypeGetsAsync(SqlConnection con, string databaseName);
    /// <summary>
    /// Returns the count of all matching user defined table types from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{UserDefinedTableTypeColumnDto}"/> of all <see cref="UserDefinedTableTypeColumnDto"/> in matching database.</returns>
    long UserDefinedTableTypeCountGet(SqlConnection con, string databaseName );
    /// <summary>
    /// Returns all matching user defined table types from server by given database name synchronous.
    /// </summary>
    /// <param name="con">The <see cref="SqlConnection"/></param>
    /// <param name="databaseName">The given database name</param>
    /// <returns>An <see cref="ICollection{UserDefinedTableTypeColumnDto}"/> of all <see cref="UserDefinedTableTypeColumnDto"/> in matching database.</returns>
    ICollection<UserDefinedTableTypeColumnDto> UserDefinedTableTypeGets(SqlConnection con, string databaseName);

  }
}
