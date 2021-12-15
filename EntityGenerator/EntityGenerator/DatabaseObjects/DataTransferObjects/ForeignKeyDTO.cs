namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="ForeignKeyDTO"/> models the data transfer object for foreign keys.
  /// </summary>
  public class ForeignKeyDTO
  {
    /// <summary>
    /// The database name of the schema in the source database.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// The foreign key name of the foreign key in the source database.
    /// </summary>
    public string ForeignKeyName { get; set; }

    /// <summary>
    /// The schema name of the table in the source database.
    /// </summary>
    public string SchemaName { get; set; }

    /// <summary>
    /// The name of the table in the source database.
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// The name of the field in the source database.
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// The schema name of the referenced table in the source database.
    /// </summary>
    public string ReferencedSchemaName { get; set; }

    /// <summary>
    /// The name of the referenced table in the source database.
    /// </summary>
    public string ReferencedTableName { get; set; }

    /// <summary>
    /// The name of the referenced field in the source database.
    /// </summary>
    public string ReferencedFieldName { get; set; }

    /// <summary>
    /// The delete statement for the foreign key.
    /// </summary>
    public string DeleteStatement { get; set; }

    /// <summary>
    /// The create statement for the foreign key.
    /// </summary>
    public string CreateStatement { get; set; }
  }
}
