namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="TriggerDto"/> models the data transfer object for trigger.
  /// </summary>
  public class TriggerDto : DataTransferObject
  {
    /// <summary>
    /// The name of the ttrigger in the source database.
    /// </summary>
    public string TriggerName { get; set; }

    /// <summary>
    /// The name of the trigger parent in the source database.
    /// </summary>
    public string ParentObjectName { get; set; }

    /// <summary>
    /// The xtype of the trigger parent in the source database.
    /// </summary>
    public string ParentObjectXtype { get; set; }

    /// <summary>
    /// The definition of the trigger in the source database.
    /// </summary>
    public string Definition { get; set; }

    /// <summary>
    /// The disabled flag for the trigger in the source database.
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// The instead of trigger flag for the trigger in the source database.
    /// </summary>
    public bool IsInsteadOfTrigger { get; set; }

    /// <summary>
    /// The is not for replication flag for the trigger in the source database.
    /// </summary>
    public bool IsNotForReplication { get; set; }

    /// <summary>
    /// The is ms shipped flag for the trigger in the source database.
    /// </summary>
    public bool IsMsShipped { get; set; }
  }
}
