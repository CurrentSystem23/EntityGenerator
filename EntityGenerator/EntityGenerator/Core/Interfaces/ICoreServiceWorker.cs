using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;

namespace EntityGenerator.Core.Interfaces
{
  public interface ICoreServiceWorker
  {
    Database ExtractData(ProfileDto profile);
    void GenerateCode(Database db);
  }
}
