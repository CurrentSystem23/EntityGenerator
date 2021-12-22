using System;
using System.Collections.Generic;

namespace EntityGenerator.ModelObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="ModelTableDto"/> models the table.
  /// </summary>
  [Serializable]
  public class ModelTableDto
  {
    /// <summary>
    /// Standardconstructor of <see cref="ModelTableDto"/>
    /// </summary>
    public ModelTableDto()
    {
      Columns = new List<ModelColumnDto>();
    }

    /// <summary>
    /// Constructor with table name of <see cref="ModelTableDto"/>
    /// </summary>
    public ModelTableDto(string name)
      : this()
    {
      Name = name;
    }

    /// <summary>
    /// The table name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The column list.
    /// </summary>
    public List<ModelColumnDto> Columns { get; set; }

  }
}
