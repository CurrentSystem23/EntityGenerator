using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Services
{
  public class CodeGenerator : ICodeGenerator
  {
    /// <summary>
    /// The code language provider.
    /// </summary>
    private readonly ILanguageProvider _languageProvider;

    /// <summary>
    /// The logging provider.
    /// </summary>
    private readonly ILogger<CodeGenerator> _logger;

    /// <summary>
    /// The output provider.
    /// </summary>
    private readonly IOutputProvider _outputProvider;

    public CodeGenerator(ILanguageProvider languageProvider, ILogger<CodeGenerator> logger = null, IOutputProvider outputProvider = null)
    {
      _languageProvider = languageProvider;
      _logger = logger;
      _outputProvider = outputProvider;
    }

    public void ExecuteBusinessLogicGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      LanguageService languageService = _languageProvider.GetLanguageService(profile.Generator.GeneratorBusinessLogic.Language, profile);
      languageService.GenerateBusinessLogic(db, profile, writerService);
    }

    public void ExecuteCommonGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      LanguageService languageService = _languageProvider.GetLanguageService(profile.Generator.GeneratorCommon.Language, profile);
      languageService.GenerateCommon(db, profile, writerService);
    }

    public void ExecuteCommonPresentationGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      _languageProvider.GetLanguageService(profile.Generator.GeneratorCommon.Language, profile).GenerateCommonPresentation(db, profile, writerService);
    }

    public void ExecuteDataAccessFacadeGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      _languageProvider.GetLanguageService(profile.Generator.GeneratorDataAccessFacade.Language, profile).GenerateDataAccessFacade(db, profile, writerService);
    }

    public void ExecuteDataAccessGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      _languageProvider.GetLanguageService(profile.Generator.GeneratorDataAccess.Language, profile).GenerateDataAccess(db, profile, writerService);
    }

    public void ExecuteFrontendGenerator(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      LanguageService languageService = _languageProvider.GetLanguageService(profile.Generator.GeneratorFrontend.Language, profile);
      languageService.GenerateFrontend(db, profile, writerService);
    }

    /// <summary>
    /// Increase the output step by one.
    /// </summary>
    private void IncreasePosition()
    {
      _outputProvider?.IncreasePosition();
    }
  }
}
