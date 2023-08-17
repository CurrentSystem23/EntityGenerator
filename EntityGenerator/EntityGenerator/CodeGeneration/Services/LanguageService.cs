using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;

namespace EntityGenerator.CodeGeneration.Services
{
  public abstract class LanguageService
  {
    public abstract void GenerateBusinessLogic(Database db, ProfileDto profile, IFileWriterService writerService);
    public abstract void GenerateCommon(Database db, ProfileDto profile, IFileWriterService writerService);
    public abstract void GenerateCommonPresentation(Database db, ProfileDto profile, IFileWriterService writerService);
    public abstract void GenerateDataAccess(Database db, ProfileDto profile, IFileWriterService writerService);
    public abstract void GenerateDataAccessFacade(Database db, ProfileDto profile, IFileWriterService writerService);
    public abstract void GenerateFrontend(Database db, ProfileDto profile, IFileWriterService writerService);
    public abstract void GenerateCS23DomainTypeValues(Database db, ProfileDto profile, IFileWriterService writerService);
  }
}
