using EntityGenerator.CodeGeneration.Languages;
using EntityGenerator.CodeGeneration.Languages.NET;
using EntityGenerator.CodeGeneration.Services;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  public interface ILanguageProvider
  {
    LanguageService GetLanguageService(Profile.DataTransferObjects.Enums.Languages language, ProfileDto profile);
    List<CodeGenerationModules> GetSupportedModules(Profile.DataTransferObjects.Enums.Languages language);
    bool CheckSupportedModule(Profile.DataTransferObjects.Enums.Languages language, CodeGenerationModules module);
  }
}
