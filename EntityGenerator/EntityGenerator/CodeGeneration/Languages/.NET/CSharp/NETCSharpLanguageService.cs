using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp.NET_6;
using EntityGenerator.CodeGeneration.Services;
using EntityGenerator.Core.Models;
using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile.DataTransferObject;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public class NETCSharpLanguageService : LanguageService
  {
    private NETCSharp _language;
    private NETCSharpFormatterService _formatterService;

    public NETCSharpLanguageService(NETCSharp language, NETCSharpFormatterService formatterService)
    {
      _language = language;
      _formatterService = formatterService;
    }

    public NETCSharpLanguageService()
    {
      _language = new NET6CSharp();
      _formatterService = new NETCSharpFormatterService();
    }

    public NETCSharpLanguageService(NETCSharp language)
    {
      _language = language;
      _formatterService = new NETCSharpFormatterService();
    }

    public void GenerateBusinessLogicForSchema(Schema schema, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      // TODO Implement
      IBusinessLogicGenerator generator = _language as IBusinessLogicGenerator;

      foreach (Table table in schema.Tables)
      {
        StringBuilder sb = new();
        generator.BuildTableInterfaceHeader(sb, profile, schema, table);
        foreach (MethodType methodType in Enum.GetValues(typeof(MethodType))) // TODO: limit to types in use!
        {
          generator.BuildTableInterfaceMethod(sb, profile, schema, table, methodType);
          
        }
        _formatterService.CloseFile(sb);
        
      }

      foreach (View view in schema.Views)
      {

      }

      foreach (Function function in schema.Functions)
      {

      }
    }
  }
}
