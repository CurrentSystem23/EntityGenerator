namespace EntityGenerator.Core.Models
{
  public class Trigger
  {
    public string Name { get; set; }
    public long Id { get; set; }

    public bool IsMsShipped { get; set; }
    public bool IsDisabled { get; set; }
    public bool IsNotForReplication { get; set; }
    public bool IsInsteadOfTrigger { get; set; }
    public string TriggerDefinition { get; set; }

  }
}
