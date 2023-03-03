using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Angular.TypeScript.Angular15;
using EntityGenerator.CodeGeneration.Languages.NET;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp.NET_6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using EntityGenerator.CodeGeneration.Services;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp;
using System.Text.Json.Serialization;
using EntityGenerator.CodeGeneration.Languages.Angular.TypeScript;

namespace EntityGenerator.CodeGeneration.Languages
{
  public class LanguageProvider : ILanguageProvider
  {
    public LanguageProvider()
    {
    }

    public LanguageService GetLanguageService(Profile.DataTransferObjects.Enums.Languages language)
    {
      switch (language)
      {
        case Profile.DataTransferObjects.Enums.Languages.DOTNET_6_CSHARP:
          return new NETCSharpLanguageService();
        case Profile.DataTransferObjects.Enums.Languages.ANGULAR_15_TYPESCRIPT:
          return new AngularTypeScriptLanguageService();
        default:
          throw new NotSupportedException($"Language {language} is not supported.");
      }
    }

    private Type GetLanguageType(Profile.DataTransferObjects.Enums.Languages language)
    {
      return Type.GetType($"EntityGenerator.CodeGeneration.Languages.{language.GetStringValue()}");
    }

    public List<CodeGenerationModules> GetSupportedModules(Profile.DataTransferObjects.Enums.Languages language)
    {
      List<CodeGenerationModules> moduleList = new List<CodeGenerationModules>();
      foreach (Type moduleType in GetLanguageType(language).GetInterfaces())
      {
        if (Enum.TryParse<CodeGenerationModules>(moduleType.Name.Substring(1), false, out CodeGenerationModules module))
          moduleList.Add(module);
      }
      return moduleList;
    }

    public bool CheckSupportedModule(Profile.DataTransferObjects.Enums.Languages language, CodeGenerationModules module)
    {
      return GetSupportedModules(language).Contains(module);
    }
  }
}
