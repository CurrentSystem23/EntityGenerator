namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="TableValueObjectDataTransferObject"/> models the data transfer object for table value objects.
  /// </summary>
  public class TableValueObjectDataTransferObject : DataTransferObject
  {
    /// <summary>
    /// The table id of the table value object in the source database.
    /// </summary>
    public int TableId { get; set; }

    /// <summary>
    /// The name of the table value object in the source database.
    /// </summary>
    public string TableValueObjectName { get; set; }

    /// <summary>
    /// The type name of the table value object in the source database.
    /// </summary>
    public string TypeName { get; set; }

    /// <summary>
    /// The xtype of the table value object in the source database.
    /// </summary>
    public string XType { get; set; }

  }
}
