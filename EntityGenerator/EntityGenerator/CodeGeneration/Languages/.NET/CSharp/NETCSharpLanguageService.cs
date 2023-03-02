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

    public override void GenerateBusinessLogic(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      // TODO Implement
      IBusinessLogicGenerator generator = _language as IBusinessLogicGenerator;
      foreach (Schema schema in db.Schemas)
      {
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
          StringBuilder sb = new();
          generator.BuildViewInterfaceHeader(sb, profile, schema, view);
          foreach (MethodType methodType in Enum.GetValues(typeof(MethodType)))
          {
            generator.BuildViewInterfaceMethod(sb, profile, schema, view, methodType);
          }
          _formatterService.CloseFile(sb);
        }

        foreach (Function function in schema.Functions.Where(x => x.FunctionType == "InlineFunction")) // TODO: Check string
        {
          StringBuilder sb = new();
          generator.BuildFunctionInterfaceHeader(sb, profile, schema, function);
          foreach (MethodType methodType in Enum.GetValues(typeof(MethodType)))
          {
            generator.BuildViewInterfaceMethod(sb, profile, schema, function, methodType);
          }
          _formatterService.CloseFile(sb);
        }

        foreach (TableValueFunction tableValueFunction in schema.Functions.Where(x => x.FunctionType == "TableValueFunction")) // TODO: Check string, Function?
        {
          StringBuilder sb = new();
          generator.BuildTableValueFunctionInterfaceHeader(sb, profile, schema, tableValueFunction);
          foreach (MethodType methodType in Enum.GetValues(typeof(MethodType)))
          {
            generator.BuildTableValueFunctionInterfaceMethod(sb, profile, schema, tableValueFunction, methodType);
          }
          _formatterService.CloseFile(sb);
        }
      }
    }

    public override void GenerateCommon(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateCommonPresentation(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateDataAccess(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateDataAccessFacade(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateFrontend(Database db, ProfileGeneratorDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }
  }
}
