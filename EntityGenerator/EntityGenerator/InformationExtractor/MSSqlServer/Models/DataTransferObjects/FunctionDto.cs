namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class FunctionDto : BaseDto
  {
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

    public string ObjectTypeName { get; set; }
    public string FunctionParameters { get; set; }
    public string FunctionReturnType { get; set; }
    public string FunctionDefinition { get; set; }
  }
}
