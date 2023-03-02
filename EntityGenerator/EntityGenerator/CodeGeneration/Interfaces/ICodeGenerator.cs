using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  public interface ICodeGenerator
  {
    void GenerateBusinessLogic(Database db, ProfileGeneratorBusinessLogicDto businessLogicProfile, IFileWriterService writerService);
    void GenerateCommon(ProfileGeneratorCommonDto commonProfile, IFileWriterService writerService);
    void GenerateCommonPresentation(ProfileGeneratorCommonPresentationDto commonPresentationProfile, IFileWriterService writerService);
    void GenerateDataAccess(ProfileGeneratorDataAccessDto dataAccessProfile, IFileWriterService writerService);
    void GenerateDataAccessFacade(ProfileGeneratorDataAccessFacadeDto dataAccessFacadeProfile, IFileWriterService writerService);
    void GenerateFrontend(ProfileGeneratorFrontendDto frontendProfile, IFileWriterService writerService);
  }
}
