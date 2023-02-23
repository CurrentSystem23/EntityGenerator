using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Services
{
  internal class CodeGenerator : ICodeGenerator
  {
    public void GenerateBusinessLogic(ProfileGeneratorBusinessLogicDto businessLogicProfile, IWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateCommon(ProfileGeneratorCommonDto commonProfile, IWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateCommonPresentation(ProfileGeneratorCommonPresentationDto commonPresentationProfile, IWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateDataAccess(ProfileGeneratorDataAccessDto dataAccessProfile, IWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateDataAccessFacade(ProfileGeneratorDataAccessFacadeDto dataAccessFacadeProfile, IWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateFrontend(ProfileGeneratorFrontendDto frontendProfile, IWriterService writerService)
    {
      throw new NotImplementedException();
    }
  }
}
