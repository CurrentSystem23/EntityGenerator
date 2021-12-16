namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="CheckConstraintDto"/> models the data transfer object for check constraints.
  /// </summary>
  public class CheckConstraintDto
  {
    /// <summary>
    /// The database name of the schema in the source database.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// The foreign key name of the foreign key in the source database.
    /// </summary>
    public string CheckConstraintName { get; set; }

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
    /// The definition of the check constraint in the source database.
    /// </summary>
    public string Definition { get; set; }

    /// <summary>
    /// The status of the check constraint in the source database.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// The disabled status of the check constraint in the source database.
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// The delete statement for the check constraint.
    /// </summary>
    public string DeleteStatement { get; set; }

    /// <summary>
    /// The create statement for the check constraint.
    /// </summary>
    public string CreateStatement { get; set; }
  }
}
