namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class ConstraintDto : BaseDto
  {
    public string DatabaseName { get; set; }

    public string SchemaName { get; set; }
    public string TableName { get; set; }
    public string TableType { get; set; }

    public string ConstraintType { get; set; }
    public string Columns { get; set; }
    public string TargetSchema { get; set; }
    public string TargetTable { get; set; }
    public string ConstraintDefinition { get; set; }
  }
}
