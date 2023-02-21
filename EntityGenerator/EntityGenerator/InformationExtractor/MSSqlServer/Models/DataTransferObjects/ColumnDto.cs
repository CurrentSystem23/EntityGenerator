using EntityGenerator.InformationExtractor.MSSqlServer.Models.Enums;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects
{
  public class ColumnDto : BaseDto
  {
    public string TableName { get; set; }
    public string SchemaName { get; set; }
    public string DatabaseName { get; set; }

    public string ColumnType { get; set; }
    public DataTypes ColumnTypeDataType { get; set; }
    public bool ColumnIsIdentity { get; set; }
    public bool ColumnIsNullable { get; set; }
    public string ColumnDefaultDefinition { get; set; }
    public bool ColumnIsComputed { get; set; }
    public short ColumnMaxLength { get; set; }
    public int? ColumnCharacterOctetLength { get; set; }
    public byte? ColumnNumericPrecision { get; set; }
    public short? ColumnNumericPrecisionRadix { get; set; }
    public int? ColumnNumericScale { get; set; }
    public short? ColumnDatetimePrecision { get; set; }

  }
}
