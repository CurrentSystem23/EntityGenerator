namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class TriggerDto : BaseDto
  {
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

    public byte ParentClass { get; set; }
    public string ParentClassDescription { get; set; }
    public Enums.DatabaseObjects TriggerType { get; set; }
    public string TriggerTypeDescription { get; set; }
    public bool IsMsShipped { get; set; }
    public bool IsDisabled { get; set; }
    public bool IsNotForReplication { get; set; }
    public bool IsInsteadOfTrigger { get; set; }
    public string TriggerDefinition { get; set; }
  }
}
