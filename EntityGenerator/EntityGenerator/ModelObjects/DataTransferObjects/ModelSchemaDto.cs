using System;
using System.Collections.Generic;

namespace EntityGenerator.ModelObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="ModelSchemaDto"/> models the schema.
  /// </summary>
  [Serializable]
  public class ModelSchemaDto
  {
    /// <summary>
    /// Standardconstructor of <see cref="ModelSchemaDto"/>
    /// </summary>
    public ModelSchemaDto()
    {
      Tables = new List<ModelTableDto>();
    }

    /// <summary>
    /// Constructor with schema name of <see cref="ModelSchemaDto"/>
    /// </summary>
    public ModelSchemaDto(string name)
      : this()
    {
      Name = name;
    }

    /// <summary>
    /// The schema name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The table list.
    /// </summary>
    public List<ModelTableDto> Tables { get; set; }
  }
}
