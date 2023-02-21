using EntityGenerator.InformationExtractor.Interfaces;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services;

public partial class InformationExtractor
{
  #region Get Triggers
  #region async

  /// <inheritdoc/>
  async Task<long> IInformationExtractor.TriggerCountGetAsync(SqlConnection con, string databaseName)
  {
    try
    {
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetTriggerCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.TriggerCountGetAsync)}");
      throw;
    }
  }

  /// <inheritdoc/>
  async Task<ICollection<TriggerDto>> IInformationExtractor.TriggerGetsAsync(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<TriggerDto> dtos = new List<TriggerDto>();
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetTriggerPrepareCommand(cmd, databaseName);
        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
        {
          while (await reader.ReadAsync().ConfigureAwait(false))
          {
            IncreasePosition();
            dtos.Add(GetTriggerRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.TriggerGetsAsync)}");
      throw;
    }
  }

  #endregion

  #region sync
  
  /// <inheritdoc/>
  long IInformationExtractor.TriggerCountGet(SqlConnection con, string databaseName)
  {
    try
    {
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetTriggerCountPrepareCommand(cmd, databaseName);
        return (long)cmd.ExecuteScalar();
      }
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.TriggerCountGet)}");
      throw;
    }
  }

  /// <inheritdoc/>
  ICollection<TriggerDto> IInformationExtractor.TriggerGets(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<TriggerDto> dtos = new List<TriggerDto>();
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetTriggerPrepareCommand(cmd, databaseName);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            IncreasePosition();
            dtos.Add(GetTriggerRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.TriggerGets)}");
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
  private static void GetTriggerCountPrepareCommand(SqlCommand cmd, string databaseName)
  {
    TriggerPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetTriggerCountSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetTriggerPrepareCommand(SqlCommand cmd, string databaseName)
  {
    TriggerPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetTriggerSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void TriggerPrepareCommand(SqlCommand cmd, string databaseName)
  {
    cmd.Parameters.Clear();
    cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
  }

  /// <summary>
  /// Get the sql statement for count.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetTriggerCountSqlStatement()
  {
    string sql = $@"
SELECT COUNT_BIG(0)
";
    sql += GetTriggerFromSqlStatement();
    sql += GetTriggerWhereSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetTriggerSqlStatement()
  {
    string sql = $@"
SELECT tr.[name]                   AS [object_name],
       tr.[object_id]              AS [object_id],
       'Trigger'                   AS [object_type],
       o.[name]                    AS [table_name],
       ss.[name]                   AS [schema_name],
       iss.[catalog_name]          AS [database_name],
       tr.[parent_class]           AS [parent_class],
       tr.[parent_class_desc]      AS [parent_class_desc],
       tr.[type]                   AS [trigger_type],
       tr.[type_desc]              AS [trigger_type_desc],
       tr.[is_ms_shipped]          AS [is_ms_shipped],
       tr.[is_disabled]            AS [is_disabled],
       tr.[is_not_for_replication] AS [is_not_for_replication],
       tr.[is_instead_of_trigger]  AS [is_instead_of_trigger],
       mod.[definition]            AS [definition]
";
    sql += GetTriggerFromSqlStatement();
    sql += GetTriggerWhereSqlStatement();
    sql += GetTriggerOrderSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the from part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetTriggerFromSqlStatement()
  {
    string sql = $@"
  FROM sys.Triggers AS tr
 INNER JOIN sys.sql_modules AS mod ON mod.[object_id] = tr.[object_id]
  LEFT JOIN sys.sysobjects AS o ON tr.[parent_id] = o.[id]
  LEFT JOIN sys.schemas AS ss ON o.[uid] = ss.[schema_id]
  LEFT JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
";
    return sql;
  }

  /// <summary>
  /// Get the where part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetTriggerWhereSqlStatement()
  {
    string sql = $@"
  WHERE iss.[catalog_name] IS NULL OR iss.[catalog_name] = @databaseName
    AND tr.[type] IN ('TR', 'TA')
";
    return sql;
  }

  /// <summary>
  /// Get the order by part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetTriggerOrderSqlStatement()
  {
    string sql = $@"
 ORDER BY iss.[catalog_name], ss.[name], tr.[name]
";
    return sql;
  }

  /// <summary>
  /// Read the specific column data.
  /// </summary>
  /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
  /// <returns>A <see cref="ColumnDto"/> with specific column data.</returns>
  private static TriggerDto GetTriggerRead(SqlDataReader reader)
  {
    return new TriggerDto
    {
      ObjectName = reader.GetString(0),
      ObjectId = reader.GetInt32(1),
      DatabaseObject = reader.GetEnum(2, Models.Enums.DatabaseObjects.Unknown),
      TableName = reader.GetStringFromNullableDbValue(3),
      SchemaName = reader.GetStringFromNullableDbValue(4),
      DatabaseName = reader.GetStringFromNullableDbValue(5),
      ParentClass = reader.GetByte(6),
      ParentClassDescription = reader.GetString(7),
      TriggerType = reader.GetEnum(8, Models.Enums.DatabaseObjects.Unknown),
      TriggerTypeDescription = reader.GetString(9),
      IsMsShipped = reader.GetBoolean(10),
      IsDisabled = reader.GetBoolean(11),
      IsNotForReplication = reader.GetBoolean(12),
      IsInsteadOfTrigger = reader.GetBoolean(13),
      TriggerDefinition = reader.GetString(14)
    };
  }
}