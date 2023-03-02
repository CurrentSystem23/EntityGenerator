using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models;
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
    private ILanguageProvider _languageProvider;

    public CodeGenerator(ILanguageProvider languageProvider)
    {
      _languageProvider = languageProvider;
    }

    public void GenerateBusinessLogic(Database db, ProfileGeneratorBusinessLogicDto businessLogicProfile, IFileWriterService writerService)
    {
      foreach (Schema schema in db.Schemas)
      {
        LanguageService languageService = _languageProvider.GetLanguageService(businessLogicProfile.Language);
        _languageProvider
      }
    }

    public void GenerateCommon(ProfileGeneratorCommonDto commonProfile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateCommonPresentation(ProfileGeneratorCommonPresentationDto commonPresentationProfile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateDataAccess(ProfileGeneratorDataAccessDto dataAccessProfile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateDataAccessFacade(ProfileGeneratorDataAccessFacadeDto dataAccessFacadeProfile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateFrontend(ProfileGeneratorFrontendDto frontendProfile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }
  }
}
