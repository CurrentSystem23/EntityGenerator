using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;

namespace EntityGenerator.InformationExtractor.Interfaces
{
  public interface IInformationExtractorWorker
  {
    Database ExtractData(ProfileDto profile);
    long GetDataCount(ProfileDto profile);
  }
}
