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
    #region Get Extended Column Properties
    #region async

    /// <inheritdoc/>
    public async Task<long> ExtendedColumnPropertyCountGetAsync( SqlConnection con, string databaseName )
    {
      try
      {
        await using SqlCommand cmd = con.CreateCommand();
        GetExtendedColumnPropertyCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedColumnPropertyCountGetAsync)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public async Task<ICollection<ExtendedColumnPropertyDto>> ExtendedColumnPropertyGetsAsync(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<ExtendedColumnPropertyDto> dtos = new List<ExtendedColumnPropertyDto>();
        await using (SqlCommand cmd = con.CreateCommand())
        {
          GetExtendedColumnPropertyPrepareCommand(cmd, databaseName);
          await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
          {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
              IncreasePosition();
              dtos.Add(GetExtendedColumnPropertyRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedColumnPropertyGetsAsync)}");
        throw;
      }
    }

    #endregion

    #region sync

    /// <inheritdoc/>
    public long ExtendedColumnPropertyCountGet( SqlConnection con, string databaseName )
    {
      try
      {
        using SqlCommand cmd = con.CreateCommand();
        GetExtendedColumnPropertyCountPrepareCommand(cmd, databaseName);
        return (long)(cmd.ExecuteScalar());
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedColumnPropertyCountGet)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public ICollection<ExtendedColumnPropertyDto> ExtendedColumnPropertyGets(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<ExtendedColumnPropertyDto> dtos = new List<ExtendedColumnPropertyDto>();
        using (SqlCommand cmd = con.CreateCommand())
        {
          GetExtendedColumnPropertyPrepareCommand(cmd, databaseName);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              IncreasePosition();
              dtos.Add(GetExtendedColumnPropertyRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(ExtendedColumnPropertyGets)}");
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
    private static void GetExtendedColumnPropertyCountPrepareCommand( SqlCommand cmd, string databaseName )
    {
      ExtendedColumnPropertyPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetExtendedColumnPropertyCountSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter and the SqlCommand Text.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void GetExtendedColumnPropertyPrepareCommand( SqlCommand cmd, string databaseName )
    {
      ExtendedColumnPropertyPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetExtendedColumnPropertySqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void ExtendedColumnPropertyPrepareCommand( SqlCommand cmd, string databaseName )
    {
      cmd.Parameters.Clear();
      cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
    }

    /// <summary>
    /// Get the sql statement for count.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetExtendedColumnPropertyCountSqlStatement()
    {
      string sql = $@"
SELECT COUNT_BIG(0)
";
      sql += GetExtendedColumnPropertyFromSqlStatement();
      sql += GetExtendedColumnPropertyWhereSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetExtendedColumnPropertySqlStatement()
    {
      string sql = @"
SELECT p.[name]                                              AS [object_name],
       p.[major_id]                                          AS [object_id],
       'ColumnExtendedProperty'                              AS [object_type],
       clmns.[name]                                          AS ColumnName,
       tbl.[name]                                            AS TableName, 
       ss.[name]                                             AS [schema_name],
       iss.[catalog_name]                                    AS [database_name],
       CAST(p.[value] AS sql_variant)                        AS ExtendedPropertyValue,
       p.[minor_id]                                          AS [object_minor_id]
      ";
      sql += GetExtendedColumnPropertyFromSqlStatement();
      sql += GetExtendedColumnPropertyWhereSqlStatement();
      sql += GetExtendedColumnPropertyOrderSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the from part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetExtendedColumnPropertyFromSqlStatement()
    {
      string sql = @"
  FROM sys.tables AS tbl
 INNER JOIN sys.all_columns AS clmns ON clmns.[object_id] = tbl.[object_id]
 INNER JOIN sys.extended_properties AS p ON p.[major_id] = tbl.[object_id] AND p.[minor_id] = clmns.[column_id] AND p.[class] = 1
 INNER JOIN sys.schemas AS ss ON tbl.[schema_id] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
";
      return sql;
    }

    /// <summary>
    /// Get the where part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetExtendedColumnPropertyWhereSqlStatement()
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
    private static string GetExtendedColumnPropertyOrderSqlStatement()
    {
      string sql = @"
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          tbl.[name],
          clmns.[name],
          p.[name];
";
      return sql;
    }

    /// <summary>
    /// Read the extended column property data.
    /// </summary>
    /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
    /// <returns>A <see cref="ExtendedColumnPropertyDto"/> with extended column property data.</returns>
    private static ExtendedColumnPropertyDto GetExtendedColumnPropertyRead(SqlDataReader reader)
    {
      ExtendedColumnPropertyDto dto = new ()
      {
        ObjectName = reader.GetString(0),
        ObjectId = reader.GetInt32(1),
        DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
        ColumnName = reader.GetString(3),
        TableName = reader.GetString(4),
        SchemaName = reader.GetString(5),
        DatabaseName = reader.GetString(6),
        ExtendedPropertyValue = reader.GetString(7),
        ObjectMinorId = reader.GetInt32(8)
      };

      return dto;
    }

  }
}
