namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class IndexDto : BaseDto
  {
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

    public string IndexColumns { get; set; }
    public string IncludedColumns { get; set; }
    public byte IndexTypeId { get; set; }
    public string IndexType { get; set; }
    public bool IsUnique { get; set; }

    public string Unique { get; set; }
    public string TableType { get; set; }
    public string ObjectType { get; set; }
  }
}
