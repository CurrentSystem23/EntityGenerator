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
    #region Get User Defined Table Types
    #region async

    /// <inheritdoc/>
    public async Task<long> UserDefinedTableTypeCountGetAsync(SqlConnection con, string databaseName)
    {
      try
      {
        await using SqlCommand cmd = con.CreateCommand();
        GetUserDefinedTableTypeCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UserDefinedTableTypeCountGetAsync)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public async Task<ICollection<UserDefinedTableTypeColumnDto>> UserDefinedTableTypeGetsAsync(SqlConnection con, string databaseName)
    {
      ICollection<UserDefinedTableTypeColumnDto> dtos = new List<UserDefinedTableTypeColumnDto>();
      try
      {
        await using (SqlCommand cmd = con.CreateCommand())
        {
          GetUserDefinedTableTypePrepareCommand(cmd, databaseName);
          await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
          {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
              IncreasePosition();
              dtos.Add(GetUserDefinedTableTypeRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UserDefinedTableTypeGetsAsync)}");
        throw;
      }
    }

    #endregion

    #region sync

    /// <inheritdoc/>
    public long UserDefinedTableTypeCountGet(SqlConnection con, string databaseName)
    {
      try
      {
        using SqlCommand cmd = con.CreateCommand();
        GetUserDefinedTableTypeCountPrepareCommand(cmd, databaseName);
        return (long)(cmd.ExecuteScalar());
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UserDefinedTableTypeCountGet)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public ICollection<UserDefinedTableTypeColumnDto> UserDefinedTableTypeGets(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<UserDefinedTableTypeColumnDto> dtos = new List<UserDefinedTableTypeColumnDto>();
        using (SqlCommand cmd = con.CreateCommand())
        {
          GetUserDefinedTableTypePrepareCommand(cmd, databaseName);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              IncreasePosition();
              dtos.Add(GetUserDefinedTableTypeRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(UserDefinedTableTypeGets)}");
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
    private static void GetUserDefinedTableTypeCountPrepareCommand(SqlCommand cmd, string databaseName)
    {
      UserDefinedTableTypePrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetUserDefinedTableTypeCountSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter and the SqlCommand Text.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void GetUserDefinedTableTypePrepareCommand(SqlCommand cmd, string databaseName)
    {
      UserDefinedTableTypePrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetUserDefinedTableTypeSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void UserDefinedTableTypePrepareCommand(SqlCommand cmd, string databaseName)
    {
      cmd.Parameters.Clear();
      cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
    }

    /// <summary>
    /// Get the sql statement for count.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetUserDefinedTableTypeCountSqlStatement()
    {
      string sql = $@"
SELECT COUNT_BIG(0)
";
      sql += GetUserDefinedTableTypeFromSqlStatement();
      sql += GetUserDefinedTableTypeWhereSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetUserDefinedTableTypeSqlStatement()
    {
      string sql = @"
SELECT tt.[name]                     AS [TableType],
       tt.[type_table_object_id]     AS [object_id],
       'UserDefinedTableTypeColumn'  AS [object_type],
       o.[name]                      AS [table_name],
       ss.[name]                     AS [schema_name],
       iss.[catalog_name]            AS [database_name],
       c.[name]                      AS [Field],
       t.[name]                      AS [Type],
       c.[is_identity]               AS [column_is_identity],
       c.[is_nullable]               AS [column_is_nullable],
       d.[definition]                AS [column_default_definition],
       c.[is_computed]               AS [column_is_computed],
       c.[max_length]                AS [column_max_length]
";
      sql += GetUserDefinedTableTypeFromSqlStatement();
      sql += GetUserDefinedTableTypeWhereSqlStatement();
      sql += GetUserDefinedTableTypeOrderSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the from part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetUserDefinedTableTypeFromSqlStatement()
    {
      string sql = @"
  FROM sys.table_types AS tt
 INNER JOIN sys.sysobjects AS o ON tt.[type_table_object_id] = o.[id]
 INNER JOIN sys.schemas ss ON tt.[schema_id] = ss.[schema_id]
 INNER JOIN sys.columns AS c on c.[object_id] = tt.[type_table_object_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 INNER JOIN sys.types AS t ON c.[system_type_id] = t.[system_type_id]
  LEFT JOIN sys.default_constraints as d ON d.[object_id] = c.[default_object_id]
";
      return sql;
    }

    /// <summary>
    /// Get the where part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetUserDefinedTableTypeWhereSqlStatement()
    {
      string sql = @"
 WHERE iss.[catalog_name] = @databaseName
   AND t.[name] != 'sysname'
";
      return sql;
    }

    /// <summary>
    /// Get the order by part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetUserDefinedTableTypeOrderSqlStatement()
    {
      string sql = @"
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          tt.[name],
          c.[column_id]
";
      return sql;
    }

    /// <summary>
    /// Read the user defined table type column data.
    /// </summary>
    /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
    /// <returns>A <see cref="UserDefinedTableTypeColumnDto"/> with user defined table type column data.</returns>
    private static UserDefinedTableTypeColumnDto GetUserDefinedTableTypeRead(SqlDataReader reader)
    {
      UserDefinedTableTypeColumnDto dto = new()
      {
        ObjectName = reader.GetString(0),
        ObjectId = reader.GetInt32(1),
        DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
        TableName = reader.GetString(3),
        SchemaName = reader.GetString(4),
        DatabaseName = reader.GetString(5),
        Field = reader.GetString(6),
        ColumnType = reader.GetString(7),
        ColumnTypeDataType = reader.GetEnum<Models.Enums.DataTypes>(7, Models.Enums.DataTypes.Unknown),
        ColumnIsIdentity = reader.GetBoolean(8),
        ColumnIsNullable = reader.GetBoolean(9),
        ColumnDefaultDefinition = reader.IsDBNull(10) ? null : reader.GetString(10),
        ColumnIsComputed = reader.GetBoolean(11),
        ColumnMaxLength = reader.GetInt16(12)
      };

      return dto;
    }

  }
}
