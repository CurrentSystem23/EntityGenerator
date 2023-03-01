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
  #region Get Extended Table Properties
  #region async

  /// <inheritdoc/>
  public async Task<long> ExtendedTablePropertyCountGetAsync(SqlConnection con, string databaseName)
  {
    try
    {
      await using SqlCommand cmd = con.CreateCommand();
      GetExtendedTablePropertyCountPrepareCommand(cmd, databaseName);
      return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedTablePropertyCountGetAsync)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public async Task<ICollection<ExtendedTablePropertyDto>> ExtendedTablePropertyGetsAsync(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<ExtendedTablePropertyDto> dtos = new List<ExtendedTablePropertyDto>();
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetExtendedTablePropertyPrepareCommand(cmd, databaseName);
        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
        {
          while (await reader.ReadAsync().ConfigureAwait(false))
          {
            IncreasePosition();
            dtos.Add(GetExtendedTablePropertyRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedTablePropertyGetsAsync)}");
      throw;
    }
  }

  #endregion

  #region sync

  /// <inheritdoc/>
  public long ExtendedTablePropertyCountGet(SqlConnection con, string databaseName)
  {
    try
    {
      using SqlCommand cmd = con.CreateCommand();
      GetExtendedTablePropertyCountPrepareCommand(cmd, databaseName);
      return (long)(cmd.ExecuteScalar());
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedTablePropertyCountGet)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public ICollection<ExtendedTablePropertyDto> ExtendedTablePropertyGets(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<ExtendedTablePropertyDto> dtos = new List<ExtendedTablePropertyDto>();
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetExtendedTablePropertyPrepareCommand(cmd, databaseName);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            IncreasePosition();
            dtos.Add(GetExtendedTablePropertyRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedTablePropertyGets)}");
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
  private static void GetExtendedTablePropertyCountPrepareCommand(SqlCommand cmd, string databaseName)
  {
    ExtendedTablePropertyPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetExtendedTablePropertyCountSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetExtendedTablePropertyPrepareCommand(SqlCommand cmd, string databaseName)
  {
    ExtendedTablePropertyPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetExtendedTablePropertySqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void ExtendedTablePropertyPrepareCommand(SqlCommand cmd, string databaseName)
  {
    cmd.Parameters.Clear();
    cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
  }

  /// <summary>
  /// Get the sql statement for count.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetExtendedTablePropertyCountSqlStatement()
  {
    string sql = $@"
SELECT COUNT_BIG(0)
";
    sql += GetExtendedTablePropertyFromSqlStatement();
    sql += GetExtendedTablePropertyWhereSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetExtendedTablePropertySqlStatement()
  {
    string sql = @"
SELECT p.[name]                                              AS [object_name],
       p.[major_id]                                          AS [object_id],
       'TableExtendedProperty'                               AS [object_type],
       tbl.[name]                                            AS TableName, 
       ss.[name]                                             AS [schema_name],
       iss.[catalog_name]                                    AS [database_name],
       CAST(p.[value] AS sql_variant)                        AS ExtendedPropertyValue
      ";
    sql += GetExtendedTablePropertyFromSqlStatement();
    sql += GetExtendedTablePropertyWhereSqlStatement();
    sql += GetExtendedTablePropertyOrderSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the from part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetExtendedTablePropertyFromSqlStatement()
  {
    string sql = @"
  FROM sys.tables AS tbl
 INNER JOIN sys.extended_properties AS p ON p.[major_id] = tbl.[object_id] AND p.[class] = 1 AND p.[minor_id] = 0
 INNER JOIN sys.schemas AS ss ON tbl.[schema_id] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
";
    return sql;
  }

  /// <summary>
  /// Get the where part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetExtendedTablePropertyWhereSqlStatement()
  {
    string sql = @"
 WHERE iss.[catalog_name] = @databaseName
";
    return sql;
  }

  /// <summary>
  /// Get the order by part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetExtendedTablePropertyOrderSqlStatement()
  {
    string sql = @"
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          tbl.[name],
          p.[name];
";
    return sql;
  }

  /// <summary>
  /// Read the extended table property data.
  /// </summary>
  /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
  /// <returns>A <see cref="ExtendedTablePropertyDto"/> with extended table property data.</returns>
  private static ExtendedTablePropertyDto GetExtendedTablePropertyRead(SqlDataReader reader)
  {
    ExtendedTablePropertyDto dto = new()
    {
      ObjectName = reader.GetString(0),
      ObjectId = reader.GetInt32(1),
      DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
      TableName = reader.GetString(3),
      SchemaName = reader.GetString(4),
      DatabaseName = reader.GetString(5),
      ExtendedPropertyValue = reader.GetString(6)
    };

    return dto;
  }

}

