namespace EntityGenerator.Profile
{
  /// <summary>
  /// Class <c>ProfileGeneratorCommon</c> models generator settings for the common project.
  /// </summary>
  public class ProfileGeneratorCommon
  {
    /// <summary>
    /// Flag for generating backend constants.
    /// </summary>
    public bool ConstantsBackend { get; set; }

    /// <summary>
    /// Flag for generating data transfer objects.
    /// </summary>
    public bool DataTransferObjects { get; set; }
  }
}
