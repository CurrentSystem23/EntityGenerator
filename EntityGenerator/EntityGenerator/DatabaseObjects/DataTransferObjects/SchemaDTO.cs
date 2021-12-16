namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="SchemaDto"/> models the data transfer object for schemas.
  /// </summary>
  public class SchemaDto : DataTransferObject
  {

    /// <summary>
    /// The schema id of the schema in the source database.
    /// </summary>
    public int SchemaId { get; set; }

  }
}
