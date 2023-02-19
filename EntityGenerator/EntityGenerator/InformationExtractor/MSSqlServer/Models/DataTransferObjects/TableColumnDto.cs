namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class TableCoulumnDto : BaseDto
  {
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

  }
}
