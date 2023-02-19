namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class ExtendedTablePropertyDto : BaseDto
  {
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

    public string ExtendedPropertyValue { get; set; }
  }
}
