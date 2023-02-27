using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractor;

public partial class InformationExtractor
{
  #region Get DatabaseObjects
  #region async

  /// <inheritdoc/>
  public async Task<long> DatabaseObjectCountGetAsync(SqlConnection con, string databaseName)
  {
    try
    {
      await using SqlCommand cmd = con.CreateCommand();
      GetDatabaseObjectCountPrepareCommand(cmd, databaseName);
      return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseObjectCountGetAsync)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public async Task<ICollection<DatabaseObjectDto>> DatabaseObjectGetsAsync(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<DatabaseObjectDto> dtos = new List<DatabaseObjectDto>();
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetDatabaseObjectPrepareCommand(cmd, databaseName);
        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
        {

          while (await reader.ReadAsync().ConfigureAwait(false))
          {
            IncreasePosition();
            dtos.Add(GetDatabaseObjectRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseObjectGetsAsync)}");
      throw;
    }
  }

  #endregion

  #region sync

  /// <inheritdoc/>
  public long DatabaseObjectCountGet(SqlConnection con, string databaseName)
  {
    try
    {
      using SqlCommand cmd = con.CreateCommand();
      GetDatabaseObjectCountPrepareCommand(cmd, databaseName);
      return (long)(cmd.ExecuteScalar());
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseObjectCountGet)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public ICollection<DatabaseObjectDto> DatabaseObjectGets(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<DatabaseObjectDto> dtos = new List<DatabaseObjectDto>();
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetDatabaseObjectPrepareCommand(cmd, databaseName);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            IncreasePosition();
            dtos.Add(GetDatabaseObjectRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseObjectGets)}");
      throw;
    }
  }

  #endregion
  #endregion

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetDatabaseObjectCountPrepareCommand(SqlCommand cmd, string databaseName)
  {
    DatabaseObjectPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetDatabaseObjectCountSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetDatabaseObjectPrepareCommand(SqlCommand cmd, string databaseName)
  {
    DatabaseObjectPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetDatabaseObjectSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void DatabaseObjectPrepareCommand(SqlCommand cmd, string databaseName)
  {
    cmd.Parameters.Clear();
    cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
  }

  /// <summary>
  /// Get the sql statement for count.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetDatabaseObjectCountSqlStatement()
  {
    string sql = $@"
SELECT COUNT_BIG(0)
";
    sql += GetDatabaseObjectFromSqlStatement();
    sql += GetDatabaseObjectWhereSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetDatabaseObjectSqlStatement()
  {
    string sql = @"
SELECT o.[name]           AS [object_name],
       o.[id]             AS [object_id],
       o.[xtype]          AS [object_type],
       ss.[name]          AS [schema_name],
       iss.[catalog_name] AS [database_name]
      ";
    sql += GetDatabaseObjectFromSqlStatement();
    sql += GetDatabaseObjectWhereSqlStatement();
    sql += GetDatabaseObjectOrderSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the from part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetDatabaseObjectFromSqlStatement()
  {
    string sql = @"
  FROM sys.sysobjects AS o
 INNER JOIN sys.schemas ss ON o.uid = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
";
    return sql;
  }

  /// <summary>
  /// Get the where part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetDatabaseObjectWhereSqlStatement()
  {
    string sql = @"
 WHERE iss.catalog_name = @databaseName
   AND ss.[name] <> 'sys'
";
    return sql;
  }

  /// <summary>
  /// Get the order by part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetDatabaseObjectOrderSqlStatement()
  {
    string sql = @"
 ORDER BY iss.[catalog_name], ss.[name], o.[xtype], o.[name]
";
    return sql;
  }

  /// <summary>
  /// Read the specific database object data.
  /// </summary>
  /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
  /// <returns>A <see cref="DatabaseObjectDto"/> with specific database object data.</returns>
  private static DatabaseObjectDto GetDatabaseObjectRead(SqlDataReader reader)
  {
    DatabaseObjectDto dto = new()
    {
      ObjectName = reader.GetString(0),
      ObjectId = reader.GetInt32(1),
      DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
      SchemaName = reader.GetString(3),
      DatabaseName = reader.GetString(4)
    };

    return dto;
  }

}

