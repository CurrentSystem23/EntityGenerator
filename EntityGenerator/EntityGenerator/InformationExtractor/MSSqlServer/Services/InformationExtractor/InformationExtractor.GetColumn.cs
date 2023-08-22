using EntityGenerator.InformationExtractor.Interfaces;
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
  #region Get Columns
  #region async

  /// <inheritdoc/>
  async Task<long> IInformationExtractor.ColumnCountGetAsync(SqlConnection con, string databaseName)
  {
    try
    {
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetColumnCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.ColumnCountGetAsync)}");
      throw;
    }
  }

  /// <inheritdoc/>
  async Task<ICollection<ColumnDto>> IInformationExtractor.ColumnGetsAsync(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<ColumnDto> dtos = new List<ColumnDto>();
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetColumnPrepareCommand(cmd, databaseName);
        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
        {
          while (await reader.ReadAsync().ConfigureAwait(false))
          {
            IncreasePosition();
            dtos.Add(GetColumnRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.ColumnGetsAsync)}");
      throw;
    }
  }

  #endregion

  #region sync
  
  /// <inheritdoc/>
  long IInformationExtractor.ColumnCountGet(SqlConnection con, string databaseName)
  {
    try
    {
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetColumnCountPrepareCommand(cmd, databaseName);
        return (long)cmd.ExecuteScalar();
      }
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.ColumnCountGet)}");
      throw;
    }
  }

  /// <inheritdoc/>
  ICollection<ColumnDto> IInformationExtractor.ColumnGets(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<ColumnDto> dtos = new List<ColumnDto>();
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetColumnPrepareCommand(cmd, databaseName);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            IncreasePosition();
            dtos.Add(GetColumnRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IInformationExtractor.ColumnGets)}");
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
  private static void GetColumnCountPrepareCommand(SqlCommand cmd, string databaseName)
  {
    ColumnPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetColumnCountSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetColumnPrepareCommand(SqlCommand cmd, string databaseName)
  {
    ColumnPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetColumnSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void ColumnPrepareCommand(SqlCommand cmd, string databaseName)
  {
    cmd.Parameters.Clear();
    cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
  }

  /// <summary>
  /// Get the sql statement for count.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetColumnCountSqlStatement()
  {
    string sql = $@"
SELECT COUNT_BIG(0)
";
    sql += GetColumnFromSqlStatement();
    sql += GetColumnWhereSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetColumnSqlStatement()
  {
    string sql = $@"
SELECT c.[name]                      AS [object_name],
       c.[column_id]                 AS [object_id],
       'Column'                      AS [object_type],
       o.[name]                      AS [table_name],
       ss.[name]                     AS [schema_name],
       iss.[catalog_name]            AS [database_name],
       t.[name]                      AS [column_type],
       c.[is_identity]               AS [column_is_identity],
       c.[is_nullable]               AS [column_is_nullable],
       d.[definition]                AS [column_default_definition],
       c.[is_computed]               AS [column_is_computed],
       c.[max_length]                AS [column_max_length],
       isc.[character_octet_length]  AS [column_character_octet_length],
       isc.[numeric_precision]       AS [column_numeric_precision],
       isc.[numeric_precision_radix] AS [column_numeric_precision_radix],
       isc.[numeric_scale]           AS [column_numeric_scale],
       isc.[datetime_precision]      AS [column_datetime_precision]
      ";
    sql += GetColumnFromSqlStatement();
    sql += GetColumnWhereSqlStatement();
    sql += GetColumnOrderSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the from part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetColumnFromSqlStatement()
  {
    string sql = $@"
  FROM sys.columns AS c 
 INNER JOIN sys.types AS t ON c.[user_type_id] = t.[user_type_id]
 INNER JOIN sys.sysobjects AS o ON c.[object_id] = o.[id]
 INNER JOIN sys.schemas ss ON o.uid = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 INNER JOIN information_schema.columns AS isc ON  iss.[catalog_name] = isc.[table_catalog] AND ss.[name] = isc.[table_schema] AND o.[name] = isc.[table_name] AND c.[name] = isc.[column_name]
  LEFT JOIN sys.default_constraints as d ON d.[object_id] = c.[default_object_id]
";
    return sql;
  }

  /// <summary>
  /// Get the where part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetColumnWhereSqlStatement()
  {
    string sql = $@"
 WHERE iss.[catalog_name] = @databaseName
   AND ss.[name] <> 'sys'
   AND o.[xtype] = 'U'
";
    return sql;
  }

  /// <summary>
  /// Get the order by part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetColumnOrderSqlStatement()
  {
    string sql = $@"
 ORDER BY iss.[catalog_name], ss.[name], o.[name], c.[column_id]
";
    return sql;
  }

  /// <summary>
  /// Converts raw database defintion into usable string value.
  /// </summary>
  /// <param name="defaultDefinition"></param>
  /// <returns></returns>
  private static string GetDefaultDefinition(string defaultDefinition)
  {
    if (defaultDefinition == "('')")
    {
      return string.Empty;
    }
    else
      return defaultDefinition;
  }

  /// <summary>
  /// Read the specific column data.
  /// </summary>
  /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
  /// <returns>A <see cref="ColumnDto"/> with specific column data.</returns>
  private static ColumnDto GetColumnRead(SqlDataReader reader)
  {
    return new ColumnDto
    {
      ObjectName = reader.GetString(0),
      ObjectId = reader.GetInt32(1),
      DatabaseObject = reader.GetEnum(2, Models.Enums.DatabaseObjects.Unknown),
      TableName = reader.GetString(3),
      SchemaName = reader.GetString(4),
      DatabaseName = reader.GetString(5),
      ColumnType = reader.GetString(6),
      ColumnTypeDataType = reader.GetEnum(6, Models.Enums.DataTypes.Unknown),
      ColumnIsIdentity = reader.GetBoolean(7),
      ColumnIsNullable = reader.GetBoolean(8),
      ColumnDefaultDefinition = GetDefaultDefinition(reader.GetStringNullableFromNullableDbValue(9)),
      ColumnIsComputed = reader.GetBoolean(10),
      ColumnMaxLength = reader.GetInt16(11),
      ColumnCharacterOctetLength = reader.GetInt32NullableFromNullableDbValue(12),
      ColumnNumericPrecision = reader.GetByteNullableFromNullableDbValue(13),
      ColumnNumericPrecisionRadix = reader.GetInt16NullableFromNullableDbValue(14),
      ColumnNumericScale = reader.GetInt32NullableFromNullableDbValue(15),
      ColumnDatetimePrecision = reader.GetInt16NullableFromNullableDbValue(16)
    };
  }

}