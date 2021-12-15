using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using Microsoft.Data.SqlClient;
using System;
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
    /// <param name="serviceProvider"> The <see cref="IServiceProvider"/> dependency injection service provider.</param>
    /// <param name="profileProvider"> The <see cref="ProfileProvider"/> profile provider</param>
    public MicrosoftSqlServerDataAccessObject(IServiceProvider serviceProvider, ProfileProvider profileProvider)
      : base(serviceProvider, profileProvider)
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
    public override List<SchemaDTO> DatabaseSchemas()
    {
      List<SchemaDTO> schemas = new List<SchemaDTO>();
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
              SchemaDTO dto = new SchemaDTO();
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
    public override List<TableValueObjectDTO> DatabaseTableValueObjects()
    {
      List<TableValueObjectDTO> tableValueObjects = new List<TableValueObjectDTO>();
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
              TableValueObjectDTO dto = new TableValueObjectDTO();
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
    public override List<FunctionDTO> DatabaseFunctions()
    {
      List<FunctionDTO> tableValueObjects = new List<FunctionDTO>();
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          cmd.Parameters.Clear();
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = GetSqlForFunctions(ProfileProvider.DatabaseName);

          con.Open();
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              FunctionDTO dto = new FunctionDTO();
              dto.FunctionId = reader.GetInt32(0);
              dto.DatabaseName = reader.GetString(1);
              dto.SchemaName = reader.GetString(2);
              dto.FunctionName = reader.GetString(3);
              dto.TypeName = reader.GetString(4);
              dto.XType = reader.GetString(5);
              dto.Parameters = reader.GetString(6);
              dto.ReturnType = reader.GetString(7);
              dto.FunctionBody = reader.GetString(8);
              tableValueObjects.Add(dto);
            }
            reader.Close();
          }
          con.Close();
        }
      }
      DatabaseFunctionReturnColumns(tableValueObjects);
      return tableValueObjects;
    }

    /// <inheritdoc />
    public override void DatabaseFunctionReturnColumns(List<FunctionDTO> databaseFunctions)
    {
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          foreach (FunctionDTO functionData in databaseFunctions)
          {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@schemaName", SqlDbType.VarChar).Value = functionData.SchemaName;
            cmd.Parameters.Add("@functionName", SqlDbType.VarChar).Value = functionData.DatabaseObjectName;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = GetSqlForReturnColumns(ProfileProvider.DatabaseName);
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
              while (reader.Read())
              {
                TableValueFunctionReturnColumnDTO dto = new TableValueFunctionReturnColumnDTO();
                dto.DatabaseName = reader.GetString(0);
                dto.SchemaName = reader.GetString(1);
                dto.DatabaseObjectName = reader.GetString(2);
                dto.ColumnName = reader.GetString(3);
                dto.ColumnDefault = reader.IsDBNull(4) ? "NULL" : reader.GetString(4);
                dto.IsNullable = reader.GetString(5);
                dto.DataType = reader.GetString(6);
                dto.MaximumLength = reader.GetInt32(7);
                dto.Order = reader.GetInt32(8);
                functionData.ReturnColumns.Add(dto);
              }
              reader.Close();
            }
            con.Close();
          }
        }
      }
    }

    /// <inheritdoc />
    public override List<ColumnDTO> DatabaseColumns()
    {
      List<ColumnDTO> columns = new List<ColumnDTO>();
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          cmd.Parameters.Clear();
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = GetSqlForColumns(ProfileProvider.DatabaseName);

          con.Open();
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              ColumnDTO dto = new ColumnDTO();
              dto.DatabaseName = reader.GetString(0);
              dto.SchemaName = reader.GetString(1);
              dto.DatabaseObjectName = reader.GetString(2);
              dto.ColumnName = reader.GetString(3);
              dto.ColumnDefault = reader.IsDBNull(4) ? "NULL" : reader.GetString(4);
              dto.IsNullable = reader.GetString(5);
              dto.DataType = reader.GetString(6);
              dto.CharacterMaximumLength = reader.IsDBNull(7) ? null : reader.GetInt32(7);
              dto.CharacterOctetLength = reader.IsDBNull(8) ? null : reader.GetInt32(8);
              dto.NumericPrecision = reader.IsDBNull(9) ? null : reader.GetByte(9);
              dto.NumericPrecisionRadix = reader.IsDBNull(10) ? null : reader.GetInt16(10);
              dto.NumericScale = reader.IsDBNull(11) ? null : reader.GetInt32(11);
              dto.DatetimePrecision = reader.IsDBNull(12) ? null : reader.GetInt16(12);
              columns.Add(dto);
            }
            reader.Close();
          }
          con.Close();
          return columns;
        }
      }
    }

    /// <inheritdoc />
    public override List<ForeignKeyDTO> DatabaseForeignKeys()
    {
      List<ForeignKeyDTO> columns = new List<ForeignKeyDTO>();
      using (SqlConnection con = new SqlConnection(ProfileProvider.ConnectionString))
      {
        using (SqlCommand cmd = con.CreateCommand())
        {
          cmd.Parameters.Clear();
          cmd.CommandType = CommandType.Text;
          cmd.CommandText = GetSqlForForeignKeys(ProfileProvider.DatabaseName);

          con.Open();
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              ForeignKeyDTO dto = new ForeignKeyDTO();
              dto.DatabaseName = reader.GetString(0);
              dto.ForeignKeyName = reader.GetString(1);
              dto.SchemaName = reader.GetString(2);
              dto.TableName = reader.GetString(3);
              dto.FieldName = reader.GetString(4);
              dto.ReferencedSchemaName = reader.GetString(5);
              dto.ReferencedTableName = reader.GetString(6);
              dto.ReferencedFieldName= reader.GetString(7);
              dto.DeleteStatement = reader.GetString(8);
              dto.CreateStatement = reader.GetString(9);
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

   UNION ALL

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
FROM [{databaseName}].[information_schema].[columns]
ORDER BY [table_catalog], [table_schema], [table_name], [ordinal_position];
";
      return ret;
    }

    /// <summary>
    /// Gets the SQL statement to get all user defined functions in the database for the generator
    /// </summary>
    /// <param name="databaseName"> Name of the source database</param>
    /// <returns>The SQL statement <see cref="string"/></returns>
    private string GetSqlForFunctions(string databaseName)
    {
      string ret = $@"
SELECT obj.[object_id] AS [object_id],
       schema_name(obj.[schema_id]) as [schema_name],
       obj.[name] as [function_name],
       CASE type
            WHEN 'FN' THEN 'SQL scalar function'
            WHEN 'TF' THEN 'SQL inline table-valued function'
            WHEN 'IF' THEN 'SQL table-valued-function'
       END AS [type],
       obj.[type] AS [xtype],
       SUBSTRING(par.[parameters], 0, LEN(par.[parameters])) as [parameters],
       TYPE_NAME(ret.[user_type_id]) as [return_type],
       mod.[definition]
  FROM [sys].[objects] obj
 INNER JOIN [sys].[sql_modules] mod ON mod.[object_id] = obj.[object_id]
 CROSS APPLY (SELECT p.[name] + ' ' + TYPE_NAME(p.[user_type_id]) + ', ' 
                FROM [sys].[parameters] p
               WHERE p.[object_id] = obj.[object_id]
                 AND p.[parameter_id] != 0 
                 FOR XML PATH ('') ) AS par ([parameters])
  LEFT JOIN [sys].[parameters] ret ON obj.[object_id] = ret.[object_id] AND ret.[parameter_id] = 0
 WHERE obj.[type] in ('FN', 'TF', 'IF')
 ORDER BY [schema_name], [function_name];
";
      return ret;
    }

    /// <summary>
    /// Gets the SQL statement to get all columns in the database for the generator
    /// </summary>
    /// <param name="databaseName"> Name of the source database</param>
    /// <returns>The SQL statement <see cref="string"/></returns>
    private string GetSqlForReturnColumns(string databaseName)
    {
      string ret = $@"
SELECT '{databaseName}' AS [table_catalog],
       s.[name] AS [schema_name],
       o.[name] AS [function_name],
       c.[name] AS [column_name],
       null AS [column_default],
       CASE 
          WHEN c.[is_nullable] = 1 THEN 'YES' 
          ELSE 'NO' 
       END AS [is_nullable],
       t.[name] AS [data_type],
       c.[max_length],
       [column_id] AS [order]
  FROM sys.columns AS c
 INNER JOIN sys.types AS t ON  t.system_type_id = c.system_type_id
 INNER JOIN sys.sysobjects o ON o.id = c.object_id
 INNER JOIN sys.schemas s ON o.UID = s.SCHEMA_ID
 WHERE s.name = @schemaName
   AND o.name = @functionName
   AND t.name <> 'sysname'
ORDER BY o.name, c.column_id
";
      return ret;
    }

    /// <summary>
    /// Gets the SQL statement to get all foreign keys in the database for the generator
    /// </summary>
    /// <param name="databaseName"> Name of the source database</param>
    /// <returns>The SQL statement <see cref="string"/></returns>
    private string GetSqlForForeignKeys(string databaseName)
    {
      string ret = $@"
SELECT '{databaseName}' AS table_catalog,
       f.name AS ForeignKeyName,
       s1.name AS TableSchemaName,
       o1.name AS TableName,
       oCol1.column_name AS FieldName,
       s2.name AS ReferencesTableSchemaName,
       o2.name AS ReferencesTableName,
       oCol2.column_name AS ReferencesFieldname,
       'ALTER TABLE [' + s1.name + '].[' + o1.name + ']  DROP CONSTRAINT [' + f.name + ']' AS DeleteForeignKey,
       'ALTER TABLE [' + s2.name + '].[' + o1.name + ']  WITH NOCHECK ADD CONSTRAINT [' + f.name + '] FOREIGN KEY([' + oCol2.column_name + ']) REFERENCES [' + s2.name + '].[' + o2.name + '] ([' + oCol2.column_name + '])' AS CreateForeignKey
  FROM [{databaseName}].sys.foreign_keys AS f
 INNER JOIN [{databaseName}].[sys].[sysobjects] AS o1 ON o1.id = f.parent_object_id
 INNER JOIN [{databaseName}].[sys].[schemas] s1 ON s1.[SCHEMA_ID] = o1.[UID]
 INNER JOIN [{databaseName}].sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id
 INNER JOIN [{databaseName}].sys.tables AS t2 ON t2.OBJECT_ID = fc.referenced_object_id
 INNER JOIN [{databaseName}].[sys].[sysobjects] AS o2 ON o2.id = t2.object_id
 INNER JOIN [{databaseName}].[sys].[schemas] s2 ON s2.[SCHEMA_ID] = o2.[UID]
 INNER JOIN [{databaseName}].[information_schema].[columns] AS oCol1 ON oCol1.TABLE_SCHEMA = s1.name AND oCol1.TABLE_NAME = o1.name AND oCol1.ORDINAL_POSITION = fc.parent_column_id
 INNER JOIN [{databaseName}].[information_schema].[columns] AS oCol2 ON oCol2.TABLE_SCHEMA = s2.name AND oCol2.TABLE_NAME = o2.name AND oCol2.ORDINAL_POSITION = fc.referenced_column_id
 ORDER BY TableName, FieldName;
";
      return ret;
    }

  }
}
