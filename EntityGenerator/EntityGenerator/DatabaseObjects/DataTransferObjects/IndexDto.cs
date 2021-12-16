namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="IndexDto"/> models the data transfer object for check constraints.
  /// </summary>
  public class IndexDto
  {
    /// <summary>
    /// The database name of the schema in the source database.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// The foreign key name of the foreign key in the source database.
    /// </summary>
    public string IndexName { get; set; }

    /// <summary>
    /// The schema name of the table in the source database.
    /// </summary>
    public string SchemaName { get; set; }

    /// <summary>
    /// The name of the table in the source database.
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// The names of the index fields in the source database.
    /// </summary>
    public string IndexColumns { get; set; }

    /// <summary>
    /// The names of the included fields in the source database.
    /// </summary>
    public string IncludedColumns { get; set; }

    /// <summary>
    /// The index type in the source database.
    /// </summary>
    public string IndexType { get; set; }

    /// <summary>
    /// Is it a unique index in the source database.
    /// </summary>
    public string Unique { get; set; }

    /// <summary>
    /// The object type (table - view).
    /// </summary>
    public string ObjectType { get; set; }

    /// <summary>
    /// The delete statement for the index.
    /// </summary>
    public string DeleteStatement { get; set; }

    /// <summary>
    /// The create statement for the index.
    /// </summary>
    public string CreateStatement { get; set; }
  }
}
