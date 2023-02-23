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
    void GenerateBusinessLogic(ProfileGeneratorBusinessLogicDto businessLogicProfile, IWriterService writerService);
    void GenerateCommon(ProfileGeneratorCommonDto commonProfile, IWriterService writerService);
    void GenerateCommonPresentation(ProfileGeneratorCommonPresentationDto commonPresentationProfile, IWriterService writerService);
    void GenerateDataAccess(ProfileGeneratorDataAccessDto dataAccessProfile, IWriterService writerService);
    void GenerateDataAccessFacade(ProfileGeneratorDataAccessFacadeDto dataAccessFacadeProfile, IWriterService writerService);
    void GenerateFrontend(ProfileGeneratorFrontendDto frontendProfile, IWriterService writerService);
  }
}
