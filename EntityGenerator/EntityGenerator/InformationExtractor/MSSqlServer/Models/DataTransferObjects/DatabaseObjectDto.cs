namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class DatabaseObjectDto : BaseDto
  {
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

  }
}
