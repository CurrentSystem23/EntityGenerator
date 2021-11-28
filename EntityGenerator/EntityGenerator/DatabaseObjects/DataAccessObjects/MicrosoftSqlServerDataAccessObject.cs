using EntityGenerator.Profile;
using Microsoft.Data.SqlClient;
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
  }
}
