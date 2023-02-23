using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Angular.TypeScript.Angular15;
using EntityGenerator.CodeGeneration.Languages.NET;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp.NET_5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Profile.DataTransferObjects.Enums;

namespace EntityGenerator.CodeGeneration.Languages
{
  internal abstract class LanguageProvider : ILanguageProvider
  {
    public LanguageBase GetLanguageObject(Profile.DataTransferObjects.Enums.Languages language)
    {
      switch (language)
      {
        case Profile.DataTransferObjects.Enums.Languages.DOTNET_5_CSHARP:
          return new NET5CSharp();
        case Profile.DataTransferObjects.Enums.Languages.ANGULAR_15_TYPESCRIPT:
          return new Angular15TypeScript();
        default:
          throw new NotSupportedException($"Language {language} is not supported.");
      }
    }

    private Type GetLanguageType(Profile.DataTransferObjects.Enums.Languages language)
    {
      return Type.GetType(EnumExtensions.GetStringValue(language));
    }

    public List<CodeGenerationModules> GetSupportedModules(Profile.DataTransferObjects.Enums.Languages language)
    {
      List<CodeGenerationModules> moduleList = new List<CodeGenerationModules>();
      foreach (Type moduleType in GetLanguageType(language).GetInterfaces())
      {
        if (Enum.TryParse<CodeGenerationModules>(moduleType.Name, false, out CodeGenerationModules module))
          moduleList.Add(module);
      }
      return moduleList;
    }

    public bool CheckSupportedModule(Profile.DataTransferObjects.Enums.Languages language, CodeGenerationModules module)
    {
      return GetSupportedModules(language).Contains(module);
    }

    public LanguageProvider()
    {
    }
  }
}
