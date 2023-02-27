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
  #region Get TableValueFunctionsReturnValues
  #region async

  /// <inheritdoc/>
  public async Task<long> TableValueFunctionsReturnValueCountGetAsync(SqlConnection con, string databaseName)
  {
    try
    {
      await using SqlCommand cmd = con.CreateCommand();
      GetTableValueFunctionsReturnValueCountPrepareCommand(cmd, databaseName);
      return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(TableValueFunctionsReturnValueCountGetAsync)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public async Task<ICollection<TableValueFunctionsReturnValueDto>> TableValueFunctionsReturnValueGetsAsync(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<TableValueFunctionsReturnValueDto> dtos = new List<TableValueFunctionsReturnValueDto>();
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetTableValueFunctionsReturnValuePrepareCommand(cmd, databaseName);
        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
        {
          while (await reader.ReadAsync().ConfigureAwait(false))
          {
            IncreasePosition();
            dtos.Add(GetTableValueFunctionsReturnValueRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(TableValueFunctionsReturnValueGetsAsync)}");
      throw;
    }
  }

  #endregion

  #region sync

  /// <inheritdoc/>
  public long TableValueFunctionsReturnValueCountGet(SqlConnection con, string databaseName)
  {
    try
    {
      using SqlCommand cmd = con.CreateCommand();
      GetTableValueFunctionsReturnValueCountPrepareCommand(cmd, databaseName);
      return (long)(cmd.ExecuteScalar());
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(TableValueFunctionsReturnValueCountGet)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public ICollection<TableValueFunctionsReturnValueDto> TableValueFunctionsReturnValueGets(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<TableValueFunctionsReturnValueDto> dtos = new List<TableValueFunctionsReturnValueDto>();
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetTableValueFunctionsReturnValuePrepareCommand(cmd, databaseName);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            IncreasePosition();
            dtos.Add(GetTableValueFunctionsReturnValueRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(TableValueFunctionsReturnValueGets)}");
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
  private static void GetTableValueFunctionsReturnValueCountPrepareCommand(SqlCommand cmd, string databaseName)
  {
    TableValueFunctionsReturnValuePrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetTableValueFunctionsReturnValueCountSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetTableValueFunctionsReturnValuePrepareCommand(SqlCommand cmd, string databaseName)
  {
    TableValueFunctionsReturnValuePrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetTableValueFunctionsReturnValueSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void TableValueFunctionsReturnValuePrepareCommand(SqlCommand cmd, string databaseName)
  {
    cmd.Parameters.Clear();
    cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
  }

  /// <summary>
  /// Get the sql statement for count.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetTableValueFunctionsReturnValueCountSqlStatement()
  {
    string sql = $@"
SELECT COUNT_BIG(0)
";
    sql += GetTableValueFunctionsReturnValueFromSqlStatement();
    sql += GetTableValueFunctionsReturnValueWhereSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetTableValueFunctionsReturnValueSqlStatement()
  {
    string sql = @"
SELECT c.[name]                      AS [object_name],
       c.[column_id]                 AS [object_id],
       'Column'                      AS [object_type],
       o.[name]                      AS [function_name],
       s.[name]                      AS [schema_name],
       iss.[catalog_name]            AS [database],
       t.[name]                      AS [column_type],
       c.[is_identity]               AS [column_is_identity],
       c.[is_nullable]               AS [column_is_nullable],
       d.[definition]                AS [column_default_definition],
       c.[is_computed]               AS [column_is_computed],
       c.[max_length]                AS [column_max_length],
       t.[precision]                 AS [column_numeric_precision],
       t.[scale]                     AS [column_numeric_scale]
";
    sql += GetTableValueFunctionsReturnValueFromSqlStatement();
    sql += GetTableValueFunctionsReturnValueWhereSqlStatement();
    sql += GetTableValueFunctionsReturnValueOrderSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the from part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetTableValueFunctionsReturnValueFromSqlStatement()
  {
    string sql = @"
  FROM sys.columns AS c
 INNER JOIN sys.types AS t ON t.[system_type_id] = c.[system_type_id]
 INNER JOIN sys.sysobjects o ON o.[id] = c.[object_id]
 INNER JOIN sys.schemas s ON o.[uid] = s.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON s.[name] = iss.[schema_name]
  LEFT JOIN sys.default_constraints as d ON d.[object_id] = c.[default_object_id]
";
    return sql;
  }

  /// <summary>
  /// Get the where part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetTableValueFunctionsReturnValueWhereSqlStatement()
  {
    string sql = @"
 WHERE iss.[catalog_name] = @databaseName
   AND o.[xtype] IN ('TF','IF','FT')
";
    return sql;
  }

  /// <summary>
  /// Get the order by part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetTableValueFunctionsReturnValueOrderSqlStatement()
  {
    string sql = @"
 ORDER BY iss.[catalog_name],
          s.[name],
          o.[name], 
          c.[column_id];
";
    return sql;
  }

  /// <summary>
  /// Read the table value function return value data.
  /// </summary>
  /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
  /// <returns>A <see cref="TableValueFunctionsReturnValueDto"/> with table value function return value data.</returns>
  private static TableValueFunctionsReturnValueDto GetTableValueFunctionsReturnValueRead(SqlDataReader reader)
  {
    TableValueFunctionsReturnValueDto dto = new()
    {
      ObjectName = reader.GetString(0),
      ObjectId = reader.GetInt32(1),
      DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
      FunctionName = reader.GetString(3),
      SchemaName = reader.GetString(4),
      DatabaseName = reader.GetString(5),
      ColumnType = reader.GetString(6),
      ColumnTypeDataType = reader.GetEnum<Models.Enums.DataTypes>(NormalizeTypeName(reader.GetString(6)), Models.Enums.DataTypes.Unknown),
      ColumnIsIdentity = reader.GetBoolean(7),
      ColumnIsNullable = reader.GetBoolean(8),
      ColumnDefaultDefinition = reader.IsDBNull(9) ? null : reader.GetString(9),
      ColumnIsComputed = reader.GetBoolean(10),
      ColumnMaxLength = reader.GetInt16(11),
      ColumnNumericPrecision = reader.IsDBNull(12) ? null : reader.GetByte(12),
      ColumnNumericScale = reader.IsDBNull(13) ? null : reader.GetByte(13)
    };

    return dto;
  }

}

