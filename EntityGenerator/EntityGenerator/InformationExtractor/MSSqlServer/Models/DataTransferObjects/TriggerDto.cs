namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;

/// <summary>
/// Class <see cref="TriggerDto"/> is the transfer object class for trigger information extraction from MS SqlServer.
/// </summary>
public class TriggerDto : BaseDto
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
  /// Get or set the parent class.
  /// </summary>
  public byte ParentClass { get; set; }

  /// <summary>
  /// Get or set the parent class description.
  /// </summary>
  public string ParentClassDescription { get; set; }

  /// <summary>
  /// Get or set the type of the trigger.
  /// </summary>
  public Enums.DatabaseObjects TriggerType { get; set; }

  /// <summary>
  /// Get or set the trigger type description.
  /// </summary>
  public string TriggerTypeDescription { get; set; }

  /// <summary>
  /// Get or set the Microsoft shipped flag.
  /// </summary>
  public bool IsMsShipped { get; set; }

  /// <summary>
  /// Get or set the disabled flag.
  /// </summary>
  public bool IsDisabled { get; set; }

  /// <summary>
  /// Get or set the not for replication flag.
  /// </summary>
  public bool IsNotForReplication { get; set; }

  /// <summary>
  /// Get or set the instead of trigger flag.
  /// </summary>
  public bool IsInsteadOfTrigger { get; set; }

  /// <summary>
  /// Get or set the trigger definition.
  /// </summary>
  public string TriggerDefinition { get; set; }
}
