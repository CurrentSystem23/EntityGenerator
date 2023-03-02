using EntityGenerator.Profile.DataTransferObject;

namespace EntityGenerator.Core.Interfaces
{
  public interface ICoreServiceWorker
  {
    void ExtractData(ProfileDto profile);
    void GenerateCode();
  }
}
