using System;
using System.Collections.Generic;

namespace EntityGenerator.ModelObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="ModelDatabaseDto"/> models the database model.
  /// </summary>
  [Serializable]
  public class ModelDatabaseDto
  {
    /// <summary>
    /// Standardconstructor of <see cref="ModelDatabaseDto"/>
    /// </summary>
    public ModelDatabaseDto()
    {
      Schemas = new List<ModelSchemaDto>();
    }

    /// <summary>
    /// Constructor with database name of <see cref="ModelDatabaseDto"/>
    /// </summary>
    public ModelDatabaseDto(string name)
      : this()
    {
      Name = name;
    }

    /// <summary>
    /// The database name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The list of database schemas.
    /// </summary>
    public List<ModelSchemaDto> Schemas { get; }
  }
}
