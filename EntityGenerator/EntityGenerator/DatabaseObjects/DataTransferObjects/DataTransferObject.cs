using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  public abstract class DataTransferObject
  {
    /// <summary>
    /// The database name of the schema in the source database.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// The schema name of the schema in the source database.
    /// </summary>
    public string SchemaName { get; set; }

    /// <summary>
    /// The name of the table value object in the source database.
    /// </summary>
    public string DatabaseObjectName { get; set; }

    /// <summary>
    /// The full qualified name of the object in the source database.
    /// </summary>
    public string FullName => $"{DatabaseName}.{SchemaName}{(string.IsNullOrWhiteSpace(DatabaseObjectName) ? string.Empty : $".{DatabaseObjectName}")}";

    /// <summary>
    /// The database escaped full qualified name of the table value object in the source database.
    /// </summary>
    public string FullExcapedName => $"[{DatabaseName}].[{SchemaName}{(string.IsNullOrWhiteSpace(DatabaseObjectName) ? string.Empty : $".[{DatabaseObjectName}]")}";
  }
}
