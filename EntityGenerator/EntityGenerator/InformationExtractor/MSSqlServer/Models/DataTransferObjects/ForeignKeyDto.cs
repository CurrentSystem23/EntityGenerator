namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class ForeignKeyDto : BaseDto
  {
    public string DatabaseName { get; set; }

    public string FromSchemaName { get; set; }
    public string FromTableName { get; set; }
    public string FromColumnName { get; set; }

    public string ReferencedSchemaName { get; set; }
    public string ReferencedTableName { get; set; }
    public string ReferencedColumnName { get; set; }
  }
}
