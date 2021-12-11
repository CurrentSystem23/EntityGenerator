namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="TableValueFunctionReturnColumnDTO"/> models the data transfer object for columns.
  /// </summary>
  public class TableValueFunctionReturnColumnDTO : DataTransferObject
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
    /// The max character length of the column.
    /// </summary>
    public int MaximumLength { get; set; }

    /// <summary>
    /// The order of the column.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// The database escaped column name of the column.
    /// </summary>
    public string EscapedColumnName => $"[{ColumnName}]";
  }
}
