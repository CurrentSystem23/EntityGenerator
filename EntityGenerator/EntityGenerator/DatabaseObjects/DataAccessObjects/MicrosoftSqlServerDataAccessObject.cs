using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace EntityGenerator.DatabaseObjects.DataAccessObjects
{
  /// <summary>
  /// Class <see cref="MicrosoftSqlServerDataAccessObject"/> models the MS Sql-Server data access for the source database.
  /// </summary>
  public class MicrosoftSqlServerDataAccessObject : DataAccessObject
  {

    /// <summary>
    /// The <see cref="MicrosoftSqlServerDataAccessObject"/> Constructor
    /// </summary>
    /// <param name="profileProvider"> The <see cref="ProfileProvider"/>profile provider</param>
    public MicrosoftSqlServerDataAccessObject(ProfileProvider profileProvider)
      : base(profileProvider)
    {}

    /// <inheritdoc />
    public override int DatabaseObjectCount()
    {
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          cmd.Parameters.Clear();
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = GetSqlForDatabaseObjectCount(ProfileProvider.DatabaseName);

          con.Open();
          int ret = (int)(cmd.ExecuteScalar());
          con.Close();
          return ret;
        }
      }
    }

    /// <inheritdoc />
    public override List<SchemaDataTransferObject> DatabaseSchemas()
    {
      List<SchemaDataTransferObject> schemas = new List<SchemaDataTransferObject>();
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          cmd.Parameters.Clear();
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = GetSqlForDatabaseSchemas(ProfileProvider.DatabaseName);

          con.Open();
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              SchemaDataTransferObject dto = new SchemaDataTransferObject();
              dto.DatabaseName = reader.GetString(0);
              dto.SchemaName = reader.GetString(1);
              dto.SchemaId = reader.GetInt32(2);
              schemas.Add(dto);
            }
            reader.Close();
          }
          con.Close();
          return schemas;
        }
      }
    }

    /// <inheritdoc />
    public override List<TableValueObjectDataTransferObject> DatabaseTableValueObjects()
    {
      List<TableValueObjectDataTransferObject> tableValueObjects = new List<TableValueObjectDataTransferObject>();
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          cmd.Parameters.Clear();
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = GetSqlForTableValueObjects(ProfileProvider.DatabaseName);

          con.Open();
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              TableValueObjectDataTransferObject dto = new TableValueObjectDataTransferObject();
              dto.TableId = reader.GetInt32(0);
              dto.DatabaseName = reader.GetString(1);
              dto.SchemaName = reader.GetString(2);
              dto.TableValueObjectName = reader.GetString(3);
              dto.TypeName = reader.GetString(4);
              dto.XType = reader.GetString(5);
              tableValueObjects.Add(dto);
            }
            reader.Close();
          }
          con.Close();
          return tableValueObjects;
        }
      }
    }

    /// <inheritdoc />
    public override List<ColumnDataTransferObject> DatabaseColumns()
    {
      List<ColumnDataTransferObject> columns = new List<ColumnDataTransferObject>();
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          cmd.Parameters.Clear();
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = GetSqlForTableValueObjects(ProfileProvider.DatabaseName);

          con.Open();
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              ColumnDataTransferObject dto = new ColumnDataTransferObject();
              dto.DatabaseName = reader.GetString(0);
              dto.SchemaName = reader.GetString(1);
              dto.DatabaseObjectName = reader.GetString(2);
              dto.ColumnName = reader.GetString(3);
              dto.ColumnDefault = reader.IsDBNull(4) ? "NULL" : reader.GetString(4);
              dto.IsNullable = reader.GetString(5);
              dto.DataType = reader.GetString(6);
              dto.CharacterMaximumLength = reader.IsDBNull(7) ? null : reader.GetInt32(7);
              dto.CharacterOctetLength = reader.IsDBNull(8) ? null : reader.GetInt32(8);
              dto.NumericPrecision = reader.IsDBNull(9) ? null : reader.GetInt32(9);
              dto.NumericPrecisionRadix = reader.IsDBNull(10) ? null : reader.GetInt32(10);
              dto.NumericScale = reader.IsDBNull(11) ? null : reader.GetInt32(11);
              dto.DatetimePrecision = reader.IsDBNull(12) ? null : reader.GetInt32(12);
              columns.Add(dto);
            }
            reader.Close();
          }
          con.Close();
          return columns;
        }
      }
    }

    /// <summary>
    /// Gets the SQL statement to determine the count of all database objects for the generator
    /// </summary>
    /// <param name="databaseName"> Name of the source database</param>
    /// <returns>The SQL statement <see cref="string"/></returns>
    private string GetSqlForDatabaseObjectCount(string databaseName)
    {
      string ret = @$"
SELECT SUM(Summe) AS Summe
FROM 
(
  SELECT COUNT(0) AS Summe
    FROM [{databaseName}].[sys].[schemas] AS s
   INNER JOIN [{databaseName}].[sys].[database_principals] AS pr  ON pr.[principal_id] = s.[principal_id]
   WHERE pr.[type] = 'U'

   UNION ALL

  SELECT COUNT(0) AS Summe
    FROM [{databaseName}].[sys].[sysobjects] AS o
   INNER JOIN [{databaseName}].[sys].[schemas] AS s ON o.[UID] = s.[SCHEMA_ID]
   WHERE o.[xtype] = 'U'

   UNION ALL

  SELECT COUNT(0) AS Summe
    FROM [{databaseName}].[sys].[sysobjects] o
   INNER JOIN [{databaseName}].[sys].[schemas] s ON o.[UID] = s.[SCHEMA_ID]
   WHERE o.[xtype] = 'V'

   UNION ALL

  SELECT COUNT(0) AS Summe
    FROM [{databaseName}].[sys].[sysobjects] o
   INNER JOIN [{databaseName}].[sys].[schemas] s ON o.[UID] = s.[SCHEMA_ID]
   WHERE o.[xtype] = 'IF'

   UNION ALL

  SELECT COUNT(0) AS Summe
    FROM [{databaseName}].[information_schema].[columns]

  SELECT COUNT(0)
    FROM [{databaseName}].[sys].[objects] obj
   INNER JOIN [{databaseName}].[sys].[sql_modules] mo ON mo.[object_id] = obj.[object_id]
   CROSS APPLY (SELECT p.[name] + ' ' + TYPE_NAME(p.user_type_id) + ', ' 
                  FROM [{databaseName}].[sys].[parameters] p
                 WHERE p.[object_id] = obj.[object_id] 
                   AND p.[parameter_id] != 0 
                   FOR XML PATH ('') ) AS par (parameters)
    LEFT JOIN [{databaseName}].[sys].[parameters] ret ON obj.[object_id] = ret.[object_id] AND ret.[parameter_id] = 0
   WHERE obj.[type] IN ('FN', 'TF', 'IF')

) AS [CounterQuery]";
      return ret;
    }

    /// <summary>
    /// Gets the SQL statement to get all schemas in the database for the generator
    /// </summary>
    /// <param name="databaseName"> Name of the source database</param>
    /// <returns>The SQL statement <see cref="string"/></returns>
    private string GetSqlForDatabaseSchemas(string databaseName)
    {
      string ret = $@"
  SELECT '{databaseName}' AS [database_name],
         s.[name] COLLATE DATABASE_DEFAULT AS [schema_name],
         s.[schema_id] AS [schema_id]
    FROM [{databaseName}].[sys].[schemas] AS s
   INNER JOIN [{databaseName}].[sys].[database_principals] AS pr  ON pr.[principal_id] = s.[principal_id]
   WHERE pr.[type] = 'U'
   ORDER BY [database_name], s.[name];
";
      return ret;
    }

    /// <summary>
    /// Gets the SQL statement to get all table value objects in the database for the generator
    /// </summary>
    /// <param name="databaseName"> Name of the source database</param>
    /// <returns>The SQL statement <see cref="string"/></returns>
    private string GetSqlForTableValueObjects(string databaseName)
    {
      string ret = $@"
  SELECT o.[Id] AS Id,
         '{databaseName}' AS [database_name],
         s.[name] AS [schema_name], 
         o.[name] AS [object_name],
         CASE o.[xtype]
         WHEN 'U' THEN CAST('Table' AS VARCHAR(100))
         WHEN 'V' THEN CAST('View' AS VARCHAR(100))
         WHEN 'IF' THEN CAST('In-lined Table Function' AS VARCHAR(100))
         END AS [type],
         o.[xtype] as [xtype]
    FROM [{databaseName}].[sys].[sysobjects] o
   INNER JOIN [{databaseName}].[sys].[schemas] s ON o.[UID] = s.[SCHEMA_ID]
   WHERE o.[xtype] IN ('U', 'V', 'IF')
   ORDER BY s.[name], o.[name]
";
      return ret;
    }

    /// <summary>
    /// Gets the SQL statement to get all columns in the database for the generator
    /// </summary>
    /// <param name="databaseName"> Name of the source database</param>
    /// <returns>The SQL statement <see cref="string"/></returns>
    private string GetSqlForColumns(string databaseName)
    {
      string ret = $@"
SELECT [table_catalog],
       [table_schema],
       [table_name],
       [column_name],
       [column_default],
       [is_nullable],
       [data_type],
       [character_maximum_length],
       [character_octet_length],
       [numeric_precision],
       [numeric_precision_radix],
       [numeric_scale],
       [datetime_precision]
FROM [Lenny].[information_schema].[columns]
ORDER BY [table_catalog], [table_schema], [table_name], [ordinal_position];
";
      return ret;
    }
  }
}
