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
    #region Get Functions
    #region async

    /// <inheritdoc/>
    public async Task<long> FunctionCountGetAsync( SqlConnection con, string databaseName )
    {
      try
      {
        await using SqlCommand cmd = con.CreateCommand();
        GetFunctionCountPrepareCommand(cmd, databaseName);
        return (long)(await cmd.ExecuteScalarAsync().ConfigureAwait(false))!;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(FunctionCountGetAsync)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public async Task<ICollection<FunctionDto>> FunctionGetsAsync(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<FunctionDto> dtos = new List<FunctionDto>();
        await using (SqlCommand cmd = con.CreateCommand())
        {
          GetFunctionPrepareCommand(cmd, databaseName);
          await using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
          {
            while (await reader.ReadAsync().ConfigureAwait(false))
            {
              IncreasePosition();
              dtos.Add(GetFunctionRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(FunctionGetsAsync)}");
        throw;
      }
    }

    #endregion

    #region sync

    /// <inheritdoc/>
    public long FunctionCountGet( SqlConnection con, string databaseName )
    {
      try
      {
        using SqlCommand cmd = con.CreateCommand();
        GetFunctionCountPrepareCommand(cmd, databaseName);
        return (long)(cmd.ExecuteScalar());
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(FunctionCountGet)}");
        throw;
      }
    }

    /// <inheritdoc/>
    public ICollection<FunctionDto> FunctionGets(SqlConnection con, string databaseName)
    {
      try
      {
        ICollection<FunctionDto> dtos = new List<FunctionDto>();
        using (SqlCommand cmd = con.CreateCommand())
        {
          GetFunctionPrepareCommand(cmd, databaseName);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              IncreasePosition();
              dtos.Add(GetFunctionRead(reader));
            }

            reader.Close();
          }
        }

        return dtos;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, $@"{nameof(InformationExtractor)}.{nameof(FunctionGets)}");
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
    private static void GetFunctionCountPrepareCommand( SqlCommand cmd, string databaseName )
    {
      FunctionPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetFunctionCountSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter and the SqlCommand Text.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void GetFunctionPrepareCommand( SqlCommand cmd, string databaseName )
    {
      FunctionPrepareCommand(cmd, databaseName);
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = GetFunctionSqlStatement();
    }

    /// <summary>
    /// Sets the SqlCommand parameter.
    /// </summary>
    /// <param name="cmd"> The <see cref="SqlCommand"/>.</param>
    /// <param name="databaseName">The given database name</param>
    private static void FunctionPrepareCommand( SqlCommand cmd, string databaseName )
    {
      cmd.Parameters.Clear();
      cmd.Parameters.Add("@databaseName", SqlDbType.NVarChar).Value = databaseName;
    }

    /// <summary>
    /// Get the sql statement for count.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetFunctionCountSqlStatement()
    {
      string sql = $@"
SELECT COUNT_BIG(0)
";
      sql += GetFunctionFromSqlStatement();
      sql += GetFunctionWhereSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the sql statement.</returns>
    private static string GetFunctionSqlStatement()
    {
      string sql = @"
SELECT obj.[name]                                            AS [object_name],
       obj.[object_id]                                       AS [object_id],
       obj.[type]                                            AS [object_type],
	     ss.[name]                                             AS [schema_name],
	     iss.[catalog_name]                                    AS [database_name],
	     CASE obj.[type]
         WHEN 'AF' THEN 'Aggregate function'
         WHEN 'FN' THEN 'SQL scalar function'
         WHEN 'TF' THEN 'SQL inline table-valued function'
         WHEN 'IF' THEN 'SQL table-valued-function'
         WHEN 'FS' THEN 'Assembly (CLR) Scalar-Function'
         WHEN 'FT' THEN 'Assembly (CLR) Table-Valued Function'
         WHEN 'P'  THEN 'Stored Procedure'
       END                                                   AS [object_type_name],
       SUBSTRING(par.[parameters], 0, LEN(par.[parameters])) AS [function_parameters],
       TYPE_NAME(ret.[user_type_id])                         AS [function_return_type],
       mod.[definition]                                      AS [function_definition]
";
      sql += GetFunctionFromSqlStatement();
      sql += GetFunctionWhereSqlStatement();
      sql += GetFunctionOrderSqlStatement();
      return sql;
    }

    /// <summary>
    /// Get the from part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetFunctionFromSqlStatement()
    {
      string sql = @"
  FROM sys.objects obj
 INNER JOIN sys.sql_modules mod ON mod.[object_id] = obj.[object_id]
 INNER JOIN sys.schemas AS ss ON obj.[schema_id] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 CROSS APPLY (SELECT p.name + ' ' + TYPE_NAME(p.[user_type_id]) + ', '
                FROM sys.parameters p
               WHERE p.[object_id] = obj.[object_id]
                 AND p.[parameter_id] != 0
                 FOR XML PATH ('') ) AS par (parameters)
  LEFT JOIN sys.parameters ret ON obj.[object_id] = ret.[object_id] AND ret.[parameter_id] = 0
";
      return sql;
    }

    /// <summary>
    /// Get the where part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetFunctionWhereSqlStatement()
    {
      string sql = @"
 WHERE iss.[catalog_name] = @databaseName
   AND obj.[type] in ('AF', 'FN', 'TF', 'IF', 'FS', 'FT', 'P')
";
      return sql;
    }

    /// <summary>
    /// Get the order by part of the sql statement.
    /// </summary>
    /// <returns>A <see cref="string"/> with the part of sql statement.</returns>
    private static string GetFunctionOrderSqlStatement()
    {
      string sql = @"
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          obj.[name];
";
      return sql;
    }

    /// <summary>
    /// Read the function data.
    /// </summary>
    /// <param name="reader"> The <see cref="SqlDataReader"/>.</param>
    /// <returns>A <see cref="FunctionDto"/> with function data.</returns>
    private static FunctionDto GetFunctionRead(SqlDataReader reader)
    {
      FunctionDto dto = new ()
      {
        ObjectName = reader.GetString(0),
        ObjectId = reader.GetInt32(1),
        DatabaseObject = reader.GetEnum<Models.Enums.DatabaseObjects>(2, Models.Enums.DatabaseObjects.Unknown),
        SchemaName = reader.GetString(3),
        DatabaseName = reader.GetString(4),
        ObjectTypeName = reader.GetString(5),
        FunctionParameters = reader.IsDBNull(6) ? null : reader.GetString(6),
        FunctionReturnType = reader.IsDBNull(7) ? null : reader.GetString(7),
        FunctionDefinition = reader.IsDBNull(8) ? null : reader.GetString(8)
      };

      return dto;
    }

  }
}
