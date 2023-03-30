using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObjects;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EntityGenerator.CodeGeneration.Services
{
  public class CodeGeneratorWorker : ICodeGeneratorWorker
  {
    ProfileDto _profile;

    /// <summary>
    /// The writer service.
    /// </summary>
    private readonly IFileWriterService _writerService;

    /// <summary>
    /// The profile provider.
    /// </summary>
    private readonly IProfileProvider _profileProvider;

    /// <summary>
    /// The code language provider.
    /// </summary>
    private readonly ILanguageProvider _languageProvider;

    /// <summary>
    /// The code generator.
    /// </summary>
    private readonly ICodeGenerator _codeGenerator;

    public CodeGeneratorWorker(IProfileProvider profileProvider, ILanguageProvider languageProvider, IFileWriterService writerService, ICodeGenerator codeGenerator)
    {
      _profileProvider = profileProvider;
      _languageProvider = languageProvider;
      _writerService = writerService;
      _codeGenerator = codeGenerator;
    }

    private List<ProfileCodeGenerationBase> GetActiveModulesList(ProfileGeneratorDto generatorProfile)
    {
      // Iterate over all generator profile property objects and filter by null value.
      return generatorProfile.GetType().GetProperties()
        .Where((prop) => (prop.PropertyType.BaseType == typeof(ProfileCodeGenerationBase)) && prop.GetValue(generatorProfile) != null)
        .ToList().ConvertAll(prop => (ProfileCodeGenerationBase)prop.GetValue(generatorProfile));
    }

    private dynamic ConvertToGeneratorProfile(ProfileCodeGenerationBase generator)
    {
      return Convert.ChangeType(generator, Type.GetType(generator.GetType().Name));
    }

    private MethodInfo GetGeneratorFunctionHandle(ProfileCodeGenerationBase generator)
    {
      string modName = generator.ModuleName.ToString();
      return typeof(ICodeGenerator).GetMethod($"Execute{generator.ModuleName.ToString()}");
    }

    public void Generate(Database db)
    {
      List<ProfileCodeGenerationBase> activeGenerators = GetActiveModulesList(_profile.Generator);

      foreach (ProfileCodeGenerationBase generator in activeGenerators)
      {
        GetGeneratorFunctionHandle(generator).Invoke(_codeGenerator, new object[] { db, _profile, _writerService });
      }
    }

    public void LoadProfile(ProfileDto profile)
    {
      // Check language support of requested features
      foreach (ProfileCodeGenerationBase codeGenerationProfile in GetActiveModulesList(profile.Generator))
      {
        Profile.DataTransferObjects.Enums.Languages language = codeGenerationProfile.Language;
        Profile.DataTransferObjects.Enums.CodeGenerationModules moduleName = codeGenerationProfile.ModuleName;

        if (!_languageProvider.CheckSupportedModule(language, moduleName))
          throw new NotSupportedException($"Selected language {language} does not support module {moduleName}. Aborting.");
      }

      _profile = profile;
    }
  }
}
