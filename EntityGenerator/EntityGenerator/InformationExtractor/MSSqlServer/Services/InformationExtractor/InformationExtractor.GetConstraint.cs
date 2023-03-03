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
  #region Get Constraints
  #region async

  /// <inheritdoc/>
  public async Task<long> ConstraintCountGetAsync(SqlConnection con, string databaseName)
  {
    try
    {
      await using SqlCommand cmd = con.CreateCommand();
      GetConstraintCountPrepareCommand(cmd, databaseName);
      return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ConstraintCountGetAsync)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public async Task<ICollection<ConstraintDto>> ConstraintGetsAsync(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<ConstraintDto> dtos = new List<ConstraintDto>();
      await using (SqlCommand cmd = con.CreateCommand())
      {
        GetConstraintPrepareCommand(cmd, databaseName);
        await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
        {
          while (await reader.ReadAsync().ConfigureAwait(false))
          {
            IncreasePosition();
            dtos.Add(GetConstraintRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ConstraintGetsAsync)}");
      throw;
    }
  }

  #endregion

  #region sync

  /// <inheritdoc/>
  public long ConstraintCountGet(SqlConnection con, string databaseName)
  {
    try
    {
      using SqlCommand cmd = con.CreateCommand();
      GetConstraintCountPrepareCommand(cmd, databaseName);
      return (long)(cmd.ExecuteScalar());
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ConstraintCountGet)}");
      throw;
    }
  }

  /// <inheritdoc/>
  public ICollection<ConstraintDto> ConstraintGets(SqlConnection con, string databaseName)
  {
    try
    {
      ICollection<ConstraintDto> dtos = new List<ConstraintDto>();
      using (SqlCommand cmd = con.CreateCommand())
      {
        GetConstraintPrepareCommand(cmd, databaseName);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            IncreasePosition();
            dtos.Add(GetConstraintRead(reader));
          }

          reader.Close();
        }
      }

      return dtos;
    }
    catch (Exception ex)
    {
      _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ConstraintGets)}");
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
  private static void GetConstraintCountPrepareCommand(SqlCommand cmd, string databaseName)
  {
    ConstraintPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetConstraintCountSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter and the SqlCommand Text.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void GetConstraintPrepareCommand(SqlCommand cmd, string databaseName)
  {
    ConstraintPrepareCommand(cmd, databaseName);
    cmd.CommandType = CommandType.Text;
    cmd.CommandText = GetConstraintSqlStatement();
  }

  /// <summary>
  /// Sets the SqlCommand parameter.
  /// </summary>
  /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
  /// <param name="databaseName">The given database name</param>
  private static void ConstraintPrepareCommand(SqlCommand cmd, string databaseName)
  {
    cmd.Parameters.Clear();
    cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
  }

  /// <summary>
  /// Get the sql statement for count.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetConstraintCountSqlStatement()
  {
    string sql = $@"
SELECT COUNT_BIG(0)
";
    sql += GetConstraintFromSqlStatement();
    sql += GetConstraintWhereSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the sql statement.</returns>
  private static string GetConstraintSqlStatement()
  {
    string sql = $@"
SELECT a.[constraint_name] AS [object_name],
       a.[object_id]       AS [object_id],
       'Constraint'        AS [object_type],
       iss.[catalog_name]  AS [database_name],
       a.[Schema]          AS [schema_name],
       a.[table_view]      AS [table_view_name],
       a.[object_type]     AS [source_type], 
       a.[constraint_type] AS [constraint_type],
       a.[details]         AS [Columns],
       a.[TargetSchema]    AS [TargetSchema],
       a.[TargetTable]     AS [TargetTable],
       a.[definition]      AS [definition]
      ";
    sql += GetConstraintFromSqlStatement();
    sql += GetConstraintWhereSqlStatement();
    sql += GetConstraintOrderSqlStatement();
    return sql;
  }

  /// <summary>
  /// Get the from part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetConstraintFromSqlStatement()
  {
    string sql = $@"
  FROM (
    --Primary key and Unique constraint
    SELECT SCHEMA_NAME(t.[schema_id]) AS [Schema],
           t.[name] AS table_view, 
           CASE WHEN t.[type] = 'U'  THEN 'Table'
                WHEN t.[type] = 'V'  THEN 'View'
                END AS [object_type],
           CASE WHEN c.[type] = 'PK' THEN 'PrimaryKeyConstraint'
                WHEN c.[type] = 'UQ' THEN 'UniqueConstraint'
                WHEN i.[type] = 1    THEN 'UniqueClusteredIndex'
                WHEN i.[type] = 2    THEN 'UniqueIndex'
               END AS constraint_type, 
           ISNULL(c.[name], i.[name]) AS constraint_name,
           SUBSTRING(column_names, 1, LEN(column_names)-1) AS [details],
           t.[object_id] AS [object_id],
           '' AS [definition],
           '' AS [TargetSchema],
           '' AS [TargetTable]
      FROM sys.objects t
      LEFT OUTER JOIN sys.indexes i ON t.[object_id] = i.[object_id]
      LEFT OUTER JOIN sys.key_constraints c ON i.[object_id] = c.[parent_object_id] AND i.[index_id] = c.[unique_index_id]
     CROSS APPLY (SELECT col.[name] + ', '
                    FROM sys.index_columns ic
                   INNER JOIN sys.columns col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
                   WHERE ic.[object_id] = t.[object_id]
                     AND ic.[index_id] = i.[index_id]
                   ORDER BY col.[column_id]
                    FOR XML PATH ('') 
                  ) AS D (column_names)
     WHERE [is_unique] = 1
       AND t.[is_ms_shipped] <> 1

     UNION ALL 

    --Check constraint
    SELECT SCHEMA_NAME(t.[schema_id]) AS [Schema],
           t.[name],
           'Table',
           'CheckConstraint',
           con.[name] AS constraint_name,
           '',
           t.[object_id] AS [object_id],
           con.[definition] AS [definition],
           '' AS [TargetSchema],
           '' AS [TargetTable]
      FROM sys.check_constraints con
      LEFT OUTER JOIN sys.objects t ON con.[parent_object_id] = t.[object_id]
      LEFT OUTER JOIN sys.all_columns col ON con.[parent_column_id] = col.[column_id] AND con.[parent_object_id] = col.[object_id]

     UNION ALL 

    --Default constraint
    SELECT SCHEMA_NAME(t.[schema_id]) AS [Schema],
           t.[name],
           'Table',
           'DefaultConstraint',
           con.[name],
           col.[name],
           t.[object_id] AS [object_id],
           col.[name] + ' = ' + con.[definition] AS [definition],
           SCHEMA_NAME(t.[schema_id]) AS [TargetSchema],
           t.[name] AS [TargetTable]
      FROM sys.default_constraints con
      LEFT OUTER JOIN sys.objects t ON con.[parent_object_id] = t.[object_id]
      LEFT OUTER JOIN sys.all_columns col ON con.[parent_column_id] = col.[column_id] AND con.[parent_object_id] = col.[object_id]

       ) AS a
 INNER JOIN information_schema.schemata AS iss ON a.[Schema] = iss.[schema_name]
";
    return sql;
  }

  /// <summary>
  /// Get the where part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetConstraintWhereSqlStatement()
  {
    string sql = $@"
 WHERE iss.[catalog_name] = @databaseName
";
    return sql;
  }

  /// <summary>
  /// Get the order by part of the sql statement.
  /// </summary>
  /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
  private static string GetConstraintOrderSqlStatement()
  {
    string sql = $@"
 ORDER BY iss.[catalog_name],
          a.[Schema],
          a.[table_view], 
          a.[constraint_type], 
          a.[constraint_name]
";
    return sql;
  }

  /// <summary>
  /// Read the constraint data.
  /// </summary>
  /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
  /// <returns>A <see cref="ConstraintDto"/> with constraint data.</returns>
  private static ConstraintDto GetConstraintRead(SqlDataReader reader)
  {
    ConstraintDto dto = new()
    {
      ObjectName = reader.GetString(0),
      ObjectId = reader.GetInt32(1),
      DatabaseObject = reader.GetEnum(2, Models.Enums.DatabaseObjects.Unknown),
      DatabaseName = reader.GetString(3),
      SchemaName = reader.GetString(4),
      TableName = reader.GetString(5),
      TableType = reader.GetString(6),
      ConstraintType = reader.GetString(7),
      ConstraintTypeType = reader.GetEnum(7, Models.Enums.ConstraintTypes.Unknown),
      Columns = reader.GetString(8),
      TargetSchema = reader.GetString(9),
      TargetTable = reader.GetString(10),
      ConstraintDefinition = reader.GetString(11)
    };

    return dto;
  }

}

