namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class BaseDto
    {
      public string ObjectName { get; set; }
      public int ObjectId { get; set; }
      public Enums.DatabaseObjects DatabaseObject { get; set; }
    }
}
