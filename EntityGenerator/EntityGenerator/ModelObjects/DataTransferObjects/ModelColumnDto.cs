using EntityGenerator.ModelObjects.DataTransferObjects.Enums;
using System;

namespace EntityGenerator.ModelObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="ModelColumnDto"/> models the column.
  /// </summary>
  [Serializable]
  public class ModelColumnDto
  {
    /// <summary>
    /// Standardconstructor of <see cref="ModelColumnDto"/>
    /// </summary>
    public ModelColumnDto()
    {
      //Tables = new List<ModelTableDto>();
    }

    /// <summary>
    /// Constructor with column name of <see cref="ModelColumnDto"/>
    /// </summary>
    public ModelColumnDto(string name)
      : this()
    {
      Name = name;
    }

    /// <summary>
    /// The column name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The default value of the column.
    /// </summary>
    public string DefaultValue { get; set; }

    /// <summary>
    /// The nullable flag of the column.
    /// </summary>
    public bool IsNullable { get; set; }

    /// <summary>
    /// The identiy flag of the column.
    /// </summary>
    public bool IsIdentity { get; set; }

    /// <summary>
    /// The data type of the column.
    /// </summary>
    public ModelFieldTypes DataType { get; set; }

    /// <summary>
    /// The length of the column (-1 = max, 0 = not required).
    /// 
    /// required for [Binary]
    /// required for [Char]
    /// required for [NChar]
    /// required for [NVarChar]
    /// required for [VarBinary]
    /// required for [VarChar]
    ///
    /// max option is only aviable for [NVarChar]
    /// max option is only aviable for [VarBinary]
    /// max option is only aviable for [VarChar]
    /// </summary>
    public int Length { get; set; }

  }
}
