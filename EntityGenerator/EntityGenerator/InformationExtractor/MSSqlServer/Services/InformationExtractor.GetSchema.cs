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
    #region Get Schemas
    #region async

    /// <inheritdoc/>
    public async Task<long> SchemaCountGetAsync( SqlConnection con, string databaseName )
    {
      try
      {
        await using SqlCommand cmd = con.CreateCommand();
        GetSchemaCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(SchemaCountGetAsync)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public async Task<ICollection<SchemaDto>> SchemaGetsAsync(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<SchemaDto> dtos = new List<SchemaDto>();
        await using (SqlCommand cmd = con.CreateCommand())
        {
          GetSchemaPrepareCommand(cmd, databaseName);
          await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
          {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
              IncreasePosition();
              dtos.Add(GetSchemaRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(SchemaGetsAsync)}");
        throw;
      }
    }

    #endregion

    #region sync

    /// <inheritdoc/>
    public long SchemaCountGet( SqlConnection con, string databaseName )
    {
      try
      {
        using SqlCommand cmd = con.CreateCommand();
        GetSchemaCountPrepareCommand(cmd, databaseName);
        return (long)(cmd.ExecuteScalar());
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(SchemaCountGet)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public ICollection<SchemaDto> SchemaGets(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<SchemaDto> dtos = new List<SchemaDto>();
        using (SqlCommand cmd = con.CreateCommand())
        {
          GetSchemaPrepareCommand(cmd, databaseName);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              IncreasePosition();
              dtos.Add(GetSchemaRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(SchemaGets)}");
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
    private static void GetSchemaCountPrepareCommand( SqlCommand cmd, string databaseName )
    {
      SchemaPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetSchemaCountSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter and the SqlCommand Text.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void GetSchemaPrepareCommand( SqlCommand cmd, string databaseName )
    {
      SchemaPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetSchemaSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void SchemaPrepareCommand( SqlCommand cmd, string databaseName )
    {
      cmd.Parameters.Clear();
      cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
    }

    /// <summary>
    /// Get the sql statement for count.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetSchemaCountSqlStatement()
    {
      string sql = $@"
SELECT COUNT_BIG(0)
";
      sql += GetSchemaFromSqlStatement();
      sql += GetSchemaWhereSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetSchemaSqlStatement()
    {
      string sql = @"
SELECT iss.[schema_name]  AS [object_name],
       ss.[schema_id]     AS [object_id],
       'Schema'           AS [object_type],
	     iss.[catalog_name] AS [database]
";
      sql += GetSchemaFromSqlStatement();
      sql += GetSchemaWhereSqlStatement();
      sql += GetSchemaOrderSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the from part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetSchemaFromSqlStatement()
    {
      string sql = @"
  FROM information_schema.schemata AS iss
 INNER JOIN sys.schemas AS ss ON ss.[name] = iss.[schema_name]
";
      return sql;
    }

    /// <summary>
    /// Get the where part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetSchemaWhereSqlStatement()
    {
      string sql = @"
 WHERE iss.catalog_name = @databaseName
   AND iss.schema_owner = 'dbo' -- ist dbo hier wirklich die richtige Einschränkung?
";
      return sql;
    }

    /// <summary>
    /// Get the order by part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetSchemaOrderSqlStatement()
    {
      string sql = @"
 ORDER BY iss.[catalog_name], iss.[schema_name]
";
      return sql;
    }

    /// <summary>
    /// Read the schema data.
    /// </summary>
    /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
    /// <returns>A <see cref="SchemaDto"/> with schema data.</returns>
    private static SchemaDto GetSchemaRead(SqlDataReader reader)
    {
      SchemaDto dto = new ()
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
