using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Services
{
  public class CodeGenerator : ICodeGenerator
  {
    private readonly ILanguageProvider _languageProvider;

    public CodeGenerator(ILanguageProvider languageProvider)
    {
      _languageProvider = languageProvider;
    }

    public void ExecuteBusinessLogicGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      LanguageService languageService = _languageProvider.GetLanguageService(profile.Generator.GeneratorBusinessLogic.Language);
      languageService.GenerateBusinessLogic(db, profile, writerService);
    }

    public void ExecuteCommonGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      LanguageService languageService = _languageProvider.GetLanguageService(profile.Generator.GeneratorCommon.Language);
      languageService.GenerateCommon(db, profile, writerService);
    }

    public void ExecuteCommonPresentationGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void ExecuteDataAccessFacadeGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void ExecuteDataAccessGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void ExecuteFrontendGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      LanguageService languageService = _languageProvider.GetLanguageService(profile.Generator.GeneratorFrontend.Language);
      languageService.GenerateFrontend(db, profile, writerService);
    }
  }
}
