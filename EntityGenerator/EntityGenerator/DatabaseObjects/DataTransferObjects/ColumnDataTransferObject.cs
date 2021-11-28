namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="ColumnDataTransferObject"/> models the data transfer object for columns.
  /// </summary>
  public class ColumnDataTransferObject : DataTransferObject
  {
    /// <summary>
    /// The column name of the column.
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// The default value of the column.
    /// </summary>
    public string ColumnDefault { get; set; }

    /// <summary>
    /// The nullable flag of the column (YES|NO).
    /// </summary>
    public string IsNullable { get; set; }

    /// <summary>
    /// The boolean nullable flag of the column.
    /// </summary>
    public bool IsNullableTyped => IsNullable.Equals("YES", System.StringComparison.InvariantCultureIgnoreCase) ? true : false;

    /// <summary>
    /// The data type of the column.
    /// </summary>
    public string DataType { get; set; }

    /// <summary>
    /// The max character length of the column (unicode).
    /// </summary>
    public int? CharacterMaximumLength { get; set; }

    /// <summary>
    /// The max character length of the column (non-unicode).
    /// </summary>
    public int? CharacterOctetLength { get; set; }

    /// <summary>
    /// The numeric precision value of the column.
    /// </summary>
    public int? NumericPrecision { get; set; }

    /// <summary>
    /// The numeric precision radix value of the column.
    /// </summary>
    public int? NumericPrecisionRadix { get; set; }

    /// <summary>
    /// The numeric scale value of the column.
    /// </summary>
    public int? NumericScale { get; set; }

    /// <summary>
    /// The date time precision value of the column.
    /// </summary>
    public int? DatetimePrecision { get; set; }

    /// <summary>
    /// The database escaped column name of the column.
    /// </summary>
    public string EscapedColumnName => $"[{ColumnName}]";
  }
}
