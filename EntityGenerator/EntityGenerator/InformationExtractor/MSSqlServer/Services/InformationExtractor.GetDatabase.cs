using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public partial class InformationExtractor
  {
    #region Get Database
    #region async

    /// <inheritdoc/>
    public async Task<long> DatabaseCountGetAsync( SqlConnection con, string databaseName )
    {
      try
      {
        await using SqlCommand cmd = con.CreateCommand();
        GetDatabaseCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseCountGetAsync)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public async Task<ICollection<DatabaseDto>> DatabaseGetsAsync(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<DatabaseDto> dtos = new List<DatabaseDto>();
        await using (SqlCommand cmd = con.CreateCommand())
        {
          GetDatabasePrepareCommand(cmd, databaseName);
          await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
          {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
              IncreasePosition();
              dtos.Add(GetDatabaseRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseGetsAsync)}");
        throw;
      }
    }

    #endregion

    #region sync

    /// <inheritdoc/>
    public long DatabaseCountGet( SqlConnection con, string databaseName )
    {
      try
      {
        using SqlCommand cmd = con.CreateCommand();
        GetDatabaseCountPrepareCommand(cmd, databaseName);
        return (long)(cmd.ExecuteScalar());
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseCountGet)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public ICollection<DatabaseDto> DatabaseGets(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<DatabaseDto> dtos = new List<DatabaseDto>();
        using (SqlCommand cmd = con.CreateCommand())
        {
          GetDatabasePrepareCommand(cmd, databaseName);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              IncreasePosition();
              dtos.Add(GetDatabaseRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(DatabaseGets)}");
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
    private static void GetDatabaseCountPrepareCommand( SqlCommand cmd, string databaseName )
    {
      DatabasePrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetDatabaseCountSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter and the SqlCommand Text.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void GetDatabasePrepareCommand( SqlCommand cmd, string databaseName )
    {
      DatabasePrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetDatabaseSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void DatabasePrepareCommand( SqlCommand cmd, string databaseName )
    {
      cmd.Parameters.Clear();
      cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
    }

    /// <summary>
    /// Get the sql statement for count.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetDatabaseCountSqlStatement()
    {
      string sql = $@"
SELECT COUNT_BIG(0)
";
      sql += GetDatabaseFromSqlStatement();
      sql += GetDatabaseWhereSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetDatabaseSqlStatement()
    {
      string sql = @"
SELECT d.[name]        AS [object_name], 
       d.[database_id] AS [object_id],
       'Database'      AS [object_type]
      ";
      sql += GetDatabaseFromSqlStatement();
      sql += GetDatabaseWhereSqlStatement();
      sql += GetDatabaseOrderSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the from part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetDatabaseFromSqlStatement()
    {
      string sql = @"
  FROM sys.databases AS d
";
      return sql;
    }

    /// <summary>
    /// Get the where part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetDatabaseWhereSqlStatement()
    {
      string sql = @"
 WHERE d.[name] = @databaseName
";
      return sql;
    }

    /// <summary>
    /// Get the order by part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetDatabaseOrderSqlStatement()
    {
      string sql = @"
 ORDER BY d.[name]
";
      return sql;
    }

    /// <summary>
    /// Read the database data.
    /// </summary>
    /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
    /// <returns>A <see cref="DatabaseDto"/> with database data.</returns>
    private static DatabaseDto GetDatabaseRead(SqlDataReader reader)
    {
      DatabaseDto dto = new ()
      {
        ObjectName = reader.GetString(0),
        ObjectId = reader.GetInt32(1),
        DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown)
      };

      return dto;
    }

  }
}
