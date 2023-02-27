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
  #region Get Indexes
  #region async

  /// <inheritdoc/>
  public async Task<long> IndexCountGetAsync(SqlConnection con, string databaseName)
  {
    try
    {
      await using SqlCommand cmd = con.CreateCommand();
      GetIndexCountPrepareCommand(cmd, databaseName);
      return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IndexCountGetAsync)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public async Task<ICollection<IndexDto>> IndexGetsAsync(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<IndexDto> dtos = new List<IndexDto>();
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetIndexPrepareCommand(cmd, databaseName);
        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
        {
          while (await reader.ReadAsync().ConfigureAwait(false))
          {
            IncreasePosition();
            dtos.Add(GetIndexRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IndexGetsAsync)}");
      throw;
    }
  }

  #endregion

  #region sync

  /// <inheritdoc/>
  public long IndexCountGet(SqlConnection con, string databaseName)
  {
    try
    {
      using SqlCommand cmd = con.CreateCommand();
      GetIndexCountPrepareCommand(cmd, databaseName);
      return (long)(cmd.ExecuteScalar());
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IndexCountGet)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public ICollection<IndexDto> IndexGets(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<IndexDto> dtos = new List<IndexDto>();
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetIndexPrepareCommand(cmd, databaseName);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            IncreasePosition();
            dtos.Add(GetIndexRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(IndexGets)}");
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
  private static void GetIndexCountPrepareCommand(SqlCommand cmd, string databaseName)
  {
    IndexPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetIndexCountSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetIndexPrepareCommand(SqlCommand cmd, string databaseName)
  {
    IndexPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetIndexSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void IndexPrepareCommand(SqlCommand cmd, string databaseName)
  {
    cmd.Parameters.Clear();
    cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
  }

  /// <summary>
  /// Get the sql statement for count.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetIndexCountSqlStatement()
  {
    string sql = $@"
SELECT COUNT_BIG(0)
";
    sql += GetIndexFromSqlStatement();
    sql += GetIndexWhereSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetIndexSqlStatement()
  {
    string sql = @"
SELECT i.[name]                                                          AS [object_name],
       i.[object_id]                                                     AS [object_id],
       'Index'                                                           AS [object_type],
       t.[name]                                                          AS [table_name],
       SCHEMA_NAME(t.schema_id)                                          AS [schema_name],
       iss.[catalog_name]                                                AS [database_name],
       SUBSTRING(column_names, 1, LEN(column_names)-1)                   AS [index_columns],
       SUBSTRING(included_column_names, 1, LEN(included_column_names)-1) AS [included_columns],
       i.[type]                                                          AS [index_type_id],
       CASE 
         WHEN i.[type] = 1 THEN 'Clustered index'
         WHEN i.[type] = 2 THEN 'Nonclustered unique index'
         WHEN i.[type] = 3 THEN 'XML index'
         WHEN i.[type] = 4 THEN 'Spatial index'
         WHEN i.[type] = 5 THEN 'Clustered columnstore index'
         WHEN i.[type] = 6 THEN 'Nonclustered columnstore index'
         WHEN i.[type] = 7 THEN 'Nonclustered hash index'
       END                                                               AS [index_type],
       i.[is_unique]                                                     AS [is_unique],
       CASE 
         WHEN i.[is_unique] = 1 THEN 'Unique'
         ELSE 'Not unique' 
       END                                                               AS [unique],
       t.[type]                                                          AS [table_type],
       CASE 
         WHEN t.[type] = 'U' THEN 'Table'
         WHEN t.[type] = 'V' THEN 'View'
       END                                                               AS [object_type]
";
    sql += GetIndexFromSqlStatement();
    sql += GetIndexWhereSqlStatement();
    sql += GetIndexOrderSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the from part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetIndexFromSqlStatement()
  {
    string sql = @"
  FROM sys.objects t
 INNER JOIN sys.indexes i ON t.[object_id] = i.[object_id]
 CROSS APPLY (SELECT col.[name] + ', '
                FROM sys.index_columns ic
               INNER JOIN sys.columns col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
               WHERE ic.[object_id] = t.[object_id]
                 AND ic.[index_id] = i.[index_id]
                 AND ic.[is_included_column] = 0
               ORDER BY key_ordinal
                 FOR XML PATH ('') ) AS D (column_names)
 CROSS APPLY (SELECT col.[name] + ', '
                FROM sys.index_columns ic
               INNER JOIN sys.columns col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
               WHERE ic.[object_id] = t.[object_id]
                 AND ic.[index_id] = i.[index_id]
                 AND ic.[is_included_column] = 1
               ORDER BY key_ordinal
                 FOR XML PATH ('') ) AS Dincluded (included_column_names)
 INNER JOIN information_schema.schemata AS iss ON SCHEMA_NAME(t.[schema_id]) = iss.[schema_name]
";
    return sql;
  }

  /// <summary>
  /// Get the where part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetIndexWhereSqlStatement()
  {
    string sql = @"
 WHERE iss.[catalog_name] = @databaseName
   AND t.[is_ms_shipped] <> 1
   AND [index_id] > 0
";
    return sql;
  }

  /// <summary>
  /// Get the order by part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetIndexOrderSqlStatement()
  {
    string sql = @"
 ORDER BY iss.[catalog_name], SCHEMA_NAME(t.schema_id), t.[name], i.[name]
";
    return sql;
  }
  /// <summary>
  /// Read the index data.
  /// </summary>
  /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
  /// <returns>A <see cref="IndexDto"/> with index data.</returns>
  private static IndexDto GetIndexRead(SqlDataReader reader)
  {
    IndexDto dto = new()
    {
      ObjectName = reader.GetString(0),
      ObjectId = reader.GetInt32(1),
      DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
      TableName = reader.GetString(3),
      SchemaName = reader.GetString(4),
      DatabaseName = reader.GetString(5),
      IndexColumns = reader.GetString(6),
      IncludedColumns = reader.GetStringFromNullableDbValue(7),
      IndexTypeId = reader.GetByte(8),
      IndexType = reader.GetString(9),
      IsUnique = reader.GetBoolean(10),
      Unique = reader.GetString(11),
      TableType = reader.GetString(12),
      ObjectType = reader.GetString(13)
    };

    return dto;
  }

}

