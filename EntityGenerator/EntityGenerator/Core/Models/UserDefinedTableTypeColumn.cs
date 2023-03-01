using EntityGenerator.Core.Models.Enums;

namespace EntityGenerator.Core.Models
{
  public class UserDefinedTableTypeColumn
  {
    public string Name { get; set; }
    public long Id { get; set; }

    public string Field { get; set; }
    public string ColumnType { get; set; }
    public DataTypes ColumnTypeDataType { get; set; }
    public bool ColumnIsIdentity { get; set; }
    public bool ColumnIsNullable { get; set; }
    public string ColumnDefaultDefinition { get; set; }
    public bool ColumnIsComputed { get; set; }
    public short ColumnMaxLength { get; set; }

  }
}
