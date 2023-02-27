using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;

namespace EntityGenerator.InformationExtractor.Interfaces
{
  public interface IInformationExtractorWorker
  {
    /// <summary>
    /// Extract the information of the database.
    /// </summary>
    /// <param name="profile"> The profile with database connection information.</param>
    /// <returns>A <see cref="Database"/> with the mapped to core database structure.</returns>
    Database ExtractData(ProfileDto profile);

    /// <summary>
    /// Get the count of reading steps.
    /// </summary>
    /// <returns>A <see cref="long"/> with the count of reading steps.</returns>
    long GetDataCount(ProfileDto profile);
  }
}
