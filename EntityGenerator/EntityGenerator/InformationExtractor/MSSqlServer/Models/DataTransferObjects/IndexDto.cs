namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="IndexDto"/> is the transfer object class for index information extraction from MS SqlServer.
/// </summary>
public class IndexDto : BaseDto
{
  /// <summary>
  /// Get or set the database table name.
  /// </summary>
  public string TableName { get; set; }

  /// <summary>
  /// Get or set the database schema name.
  /// </summary>
  public string SchemaName { get; set; }

  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }

  /// <summary>
  /// Get or set the index columns.
  /// </summary>
  public string IndexColumns { get; set; }

  /// <summary>
  /// Get or set the included columns.
  /// </summary>
  public string IncludedColumns { get; set; }

  /// <summary>
  /// Get or set the type id of the index.
  /// </summary>
  public byte IndexTypeId { get; set; }

  /// <summary>
  /// Get or set the type of the index.
  /// </summary>
  public string IndexType { get; set; }

  /// <summary>
  /// Get or set the unique flag.
  /// </summary>
  public bool IsUnique { get; set; }

  /// <summary>
  /// Get or set the unique value.
  /// </summary>
  public string Unique { get; set; }

  /// <summary>
  /// Get or set the type of the table.
  /// </summary>
  public string TableType { get; set; }

  /// <summary>
  /// Get or set the type of the object.
  /// </summary>
  public string ObjectType { get; set; }
}
