namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <c>ProfileGeneratorDatabase</c> models generator settings for the database project.
  /// </summary>
  public class ProfileGeneratorDatabase
  {
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
