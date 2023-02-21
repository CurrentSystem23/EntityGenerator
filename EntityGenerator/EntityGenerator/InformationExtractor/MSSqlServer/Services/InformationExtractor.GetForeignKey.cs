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
    #region Get ForeignKeys
    #region async

    /// <inheritdoc/>
    public async Task<long> ForeignKeyCountGetAsync( SqlConnection con, string databaseName )
    {
      long count;
      try
      {
        await using SqlCommand cmd = con.CreateCommand();
        GetForeignKeyPrepareCommand(cmd, databaseName);
        count = (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ForeignKeyCountGetAsync)}");
        throw;
      }
      return count;
    }

    /// <inheritdoc/>
    public async Task<ICollection<ForeignKeyDto>> ForeignKeyGetsAsync(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<ForeignKeyDto> dtos = new List<ForeignKeyDto>();
        await using (SqlCommand cmd = con.CreateCommand())
        {
          GetForeignKeyPrepareCommand(cmd, databaseName);
          await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
          {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
              IncreasePosition();
              dtos.Add(GetForeignKeyRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ForeignKeyGetsAsync)}");
        throw;
      }
    }

    #endregion

    #region sync

    /// <inheritdoc/>
    public long ForeignKeyCountGet( SqlConnection con, string databaseName )
    {
      try
      {
        using SqlCommand cmd = con.CreateCommand();
        GetForeignKeyCountPrepareCommand(cmd, databaseName);
        return (long)(cmd.ExecuteScalar());
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ForeignKeyCountGet)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public ICollection<ForeignKeyDto> ForeignKeyGets(SqlConnection con, string databaseName)
    {

      try
      {
        ICollection<ForeignKeyDto> dtos = new List<ForeignKeyDto>();
        using (SqlCommand cmd = con.CreateCommand())
        {
          GetForeignKeyPrepareCommand(cmd, databaseName);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              IncreasePosition();
              dtos.Add(GetForeignKeyRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ForeignKeyGets)}");
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
    private static void GetForeignKeyCountPrepareCommand( SqlCommand cmd, string databaseName )
    {
      ForeignKeyPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetForeignKeyCountSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter and the SqlCommand Text.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void GetForeignKeyPrepareCommand( SqlCommand cmd, string databaseName )
    {
      ForeignKeyPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetForeignKeySqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void ForeignKeyPrepareCommand( SqlCommand cmd, string databaseName )
    {
      cmd.Parameters.Clear();
      cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
    }

    /// <summary>
    /// Get the sql statement for count.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetForeignKeyCountSqlStatement()
    {
      string sql = $@"
SELECT COUNT_BIG(0)
";
      sql += GetForeignKeyFromSqlStatement();
      sql += GetForeignKeyWhereSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetForeignKeySqlStatement()
    {
      string sql = @"
SELECT obj.[name]         AS [object_name],
       obj.[object_id]    AS [object_id],
       'ForeignKey'       AS [object_type],
       iss.[catalog_name] AS [database_name],
       sch1.[name]        AS [schema_name],
       tab1.[name]        AS [table],
       col1.[name]        AS [column],
       sch2.[name]        AS [referenced_schema_name],
       tab2.[name]        AS [referenced_table],
       col2.[name]        AS [referenced_column]
";
      sql += GetForeignKeyFromSqlStatement();
      sql += GetForeignKeyWhereSqlStatement();
      sql += GetForeignKeyOrderSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the from part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetForeignKeyFromSqlStatement()
    {
      string sql = @"
  FROM sys.foreign_key_columns fkc
 INNER JOIN sys.objects obj ON obj.object_id = fkc.constraint_object_id
 INNER JOIN sys.tables tab1 ON tab1.object_id = fkc.parent_object_id
 INNER JOIN sys.schemas sch1 ON tab1.schema_id = sch1.schema_id
 INNER JOIN sys.columns col1 ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id
 INNER JOIN sys.tables tab2 ON tab2.object_id = fkc.referenced_object_id
 INNER JOIN sys.columns col2 ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id
 INNER JOIN sys.schemas sch2 ON tab2.schema_id = sch2.schema_id
 INNER JOIN information_schema.schemata AS iss ON sch1.[name] = iss.[schema_name]
";
      return sql;
    }

    /// <summary>
    /// Get the where part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetForeignKeyWhereSqlStatement()
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
    private static string GetForeignKeyOrderSqlStatement()
    {
      string sql = @"
 ORDER BY iss.[catalog_name],
          sch1.[name],
          tab1.[name],
          col1.[name]
";
      return sql;
    }

    /// <summary>
    /// Read the foreign key data.
    /// </summary>
    /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
    /// <returns>A <see cref="ForeignKeyDto"/> with foreign key data.</returns>
    private static ForeignKeyDto GetForeignKeyRead(SqlDataReader reader)
    {
      ForeignKeyDto dto = new ()
      {
        ObjectName = reader.GetString(0),
        ObjectId = reader.GetInt32(1),
        DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
        DatabaseName = reader.GetString(3),
        FromSchemaName = reader.GetString(4),
        FromTableName = reader.GetString(5),
        FromColumnName = reader.GetString(6),
        ReferencedSchemaName = reader.GetString(7),
        ReferencedTableName = reader.GetString(8),
        ReferencedColumnName = reader.GetString(9)
      };

      return dto;
    }

  }
}
