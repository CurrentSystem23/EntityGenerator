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

    public void GenerateBusinessLogic(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      foreach (Schema schema in db.Schemas)
      {
        LanguageService languageService = _languageProvider.GetLanguageService(profile.GeneratorBusinessLogic.Language);
        languageService.GenerateBusinessLogicForSchema(schema, profile, writerService);
      }
    }

    public void GenerateCommon(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateCommonPresentation(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateDataAccess(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateDataAccessFacade(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public void GenerateFrontend(Database db, ProfileGeneratorDto frontendProfile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }
  }
}
