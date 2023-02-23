using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Services;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public class NETCSharpLanguageService : LanguageService
  {
    private NETCSharp _language;
    private NETCSharpLinterService _linterService;

    NETCSharpLanguageService(NETCSharp language, NETCSharpLinterService linterService)
    {
      _language = language;
      _linterService = linterService;
    }

    public void GenerateBusinessLogic(IWriterService writerService)
    {
      // TODO Implement
      _language.BuildLogicInterface();
      _language.BuildLogicClasses();
    }
  }
}
