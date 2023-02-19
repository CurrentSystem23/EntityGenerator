using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
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
    #region Get UsedTypes
    #region async

    /// <inheritdoc/>
    public async Task<long> UsedTypeCountGetAsync( SqlConnection con, string databaseName )
    {
      try
      {
        await using SqlCommand cmd = con.CreateCommand();
        GetUsedTypeCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UsedTypeCountGetAsync)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public async Task<ICollection<TypeDto>> UsedTypeGetsAsync(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<TypeDto> dtos = new List<TypeDto>();
        await using (SqlCommand cmd = con.CreateCommand())
        {
          GetUsedTypePrepareCommand(cmd, databaseName);
          await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
          {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
              IncreasePosition();
              dtos.Add(GetUsedTypeRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UsedTypeGetsAsync)}");
        throw;
      }
    }

    #endregion

    #region sync

    /// <inheritdoc/>
    public long UsedTypeCountGet( SqlConnection con, string databaseName )
    {
      try
      {
        using SqlCommand cmd = con.CreateCommand();
        GetUsedTypeCountPrepareCommand(cmd, databaseName);
        return (long)(cmd.ExecuteScalar());
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UsedTypeCountGet)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public ICollection<TypeDto> UsedTypeGets(SqlConnection con, string databaseName)
    {

      try
      {
        ICollection<TypeDto> dtos = new List<TypeDto>();
        using (SqlCommand cmd = con.CreateCommand())
        {
          GetUsedTypePrepareCommand(cmd, databaseName);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              IncreasePosition();
              dtos.Add(GetUsedTypeRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UsedTypeGets)}");
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
    private static void GetUsedTypeCountPrepareCommand( SqlCommand cmd, string databaseName )
    {
      UsedTypePrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetUsedTypeCountSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter and the SqlCommand Text.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void GetUsedTypePrepareCommand( SqlCommand cmd, string databaseName )
    {
      UsedTypePrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetUsedTypeSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void UsedTypePrepareCommand( SqlCommand cmd, string databaseName )
    {
      cmd.Parameters.Clear();
      cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
    }

    /// <summary>
    /// Get the sql statement for count.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetUsedTypeCountSqlStatement()
    {
      string sql = $@"
SELECT COUNT_BIG(0)
";
      sql += GetUsedTypeFromSqlStatement();
      sql += GetUsedTypeWhereSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetUsedTypeSqlStatement()
    {
      string sql = @"
SELECT DISTINCT 
       o.[xtype]          AS [object_name],
       -1                 AS [object_id],
       'Type'             AS [object_type],
	     iss.[catalog_name] AS [database]
";
      sql += GetUsedTypeFromSqlStatement();
      sql += GetUsedTypeWhereSqlStatement();
      sql += GetUsedTypeOrderSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the from part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetUsedTypeFromSqlStatement()
    {
      string sql = @"
  FROM sys.sysobjects AS o
 INNER JOIN sys.schemas ss ON o.[uid] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
";
      return sql;
    }

    /// <summary>
    /// Get the where part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetUsedTypeWhereSqlStatement()
    {
      string sql = @"
 WHERE iss.catalog_name = @databaseName
";
      return sql;
    }

    /// <summary>
    /// Get the order by part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetUsedTypeOrderSqlStatement()
    {
      string sql = @"
 ORDER BY iss.[catalog_name], o.[xtype]
";
      return sql;
    }

    /// <summary>
    /// Read the type data.
    /// </summary>
    /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
    /// <returns>A <see cref="TypeDto"/> with type data.</returns>
    private static TypeDto GetUsedTypeRead(SqlDataReader reader)
    {
      TypeDto dto = new ()
      {
        ObjectName = reader.GetString(0),
        ObjectId = reader.GetInt32(1),
        DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
        DatabaseName = reader.GetString(3)
      };

      return dto;
    }

  }
}
