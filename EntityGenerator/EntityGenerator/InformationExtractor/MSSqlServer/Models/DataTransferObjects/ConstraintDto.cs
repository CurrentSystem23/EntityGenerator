using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="ConstraintDto"/> is the transfer object class for constraint information extraction from MS SqlServer.
/// </summary>
public class ConstraintDto : BaseDto
{
  /// <summary>
  /// Get or set the database name.
  /// </summary>
  public string DatabaseName { get; set; }

  /// <summary>
  /// Get or set the database schema name.
  /// </summary>
  public string SchemaName { get; set; }

  /// <summary>
  /// Get or set the database table name.
  /// </summary>
  public string TableName { get; set; }

  /// <summary>
  /// Get or set the type of the table.
  /// </summary>
  public string TableType { get; set; }

  /// <summary>
  /// Get or set the type of the constraint as string.
  /// </summary>
  public string ConstraintType { get; set; }

  /// <summary>
  /// Get or set the type of the constraint.
  /// </summary>
  public ConstraintTypes ConstraintTypeType { get; set; }

  /// <summary>
  /// Get or set the columns.
  /// </summary>
  public string Columns { get; set; }

  /// <summary>
  /// Get or set the target schema.
  /// </summary>
  public string TargetSchema { get; set; }

  /// <summary>
  /// Get or set the target table.
  /// </summary>
  public string TargetTable { get; set; }

  /// <summary>
  /// Get or set the constraint definition.
  /// </summary>
  public string ConstraintDefinition { get; set; }
}
