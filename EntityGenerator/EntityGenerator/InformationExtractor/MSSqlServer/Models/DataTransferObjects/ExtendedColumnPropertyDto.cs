namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class ExtendedColumnPropertyDto : BaseDto
  {
    public string ColumnName { get; set; }
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

    public string ExtendedPropertyValue { get; set; }
    public int ObjectMinorId { get; set; }

  }
}
