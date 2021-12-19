using System;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorDatabaseDto"/> models generator settings for the database project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorDatabaseDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorDatabaseDto"/> class.
    /// </summary>
    public ProfileGeneratorDatabaseDto()
    {
    }

    /// <summary>
    /// Flag for generating history tables.
    /// </summary>
    public bool HistoryTables { get; set; }

    /// <summary>
    /// Flag for generating history triggers.
    /// </summary>
    public bool HistoryTriggers { get; set; }

    /// <summary>
    /// Flag for generating merge scripts.
    /// </summary>
    public bool MergeScripts { get; set; }

    /// <summary>
    /// Flag for generating check constraint scripts.
    /// </summary>
    public bool CheckConstraintScripts { get; set; }
  }
}
