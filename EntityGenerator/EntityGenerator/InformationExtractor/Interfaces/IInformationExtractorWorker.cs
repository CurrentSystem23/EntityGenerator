using EntityGenerator.Profile.DataTransferObject;

namespace EntityGenerator.InformationExtractor.Interfaces
{
  public interface IInformationExtractorWorker
  {
    void ExtractData(ProfileDto profile);
    long GetDataCount(ProfileDto profile);
  }
}
