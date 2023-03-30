using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.NET.CSharp.NET_6;
using EntityGenerator.CodeGeneration.Services;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObjects.Enums;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public class NETCSharpLanguageService : LanguageService
  {
    private readonly NETCSharp _language;
    private readonly NETCSharpFormatterService _formatterService;
    private readonly StringBuilder _sb;

    public NETCSharpLanguageService(NETCSharp language, NETCSharpFormatterService formatterService)
    {
      _language = language;
      _formatterService = formatterService;
    }

    public NETCSharpLanguageService()
    {
      _sb = new();
      _language = new NET6CSharp(_sb);
      _formatterService = new NETCSharpFormatterService(_sb);
    }

    public override void GenerateBusinessLogic(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      // TODO Implement
      IBusinessLogicGenerator generator = _language as IBusinessLogicGenerator;
      foreach (Schema schema in db.Schemas)
      {
        foreach (Table table in schema.Tables)
        {
          generator.BuildInterfaceHeader(profile, schema);
          foreach (MethodType methodType in Enum.GetValues(typeof(MethodType))) // TODO: limit to types in use!
          {
            generator.BuildTableInterfaceMethod(profile, schema, table, methodType);

          }
          writerService.WriteToFile(profile.Path.BusinessLogicDir, $"{profile.Global.GeneratedPrefix}{table.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        foreach (View view in schema.Views)
        {
          StringBuilder sb = new();
          generator.BuildInterfaceHeader(profile, schema);
          foreach (MethodType methodType in Enum.GetValues(typeof(MethodType)))
          {
            generator.BuildViewInterfaceMethod(sb, profile, schema, view, methodType);
          }
          _formatterService.CloseFile();
        }

        foreach (Function function in schema.FunctionsSqlScalar) // TODO: Check string
        {
          StringBuilder sb = new();
          generator.BuildInterfaceHeader(profile, schema);
          foreach (MethodType methodType in Enum.GetValues(typeof(MethodType)))
          {
            generator.BuildFunctionInterfaceMethod(sb, profile, schema, function, methodType);
          }
          _formatterService.CloseFile();
        }
        /**
        foreach (TableValuedFunction tableValueFunction in schema.FunctionsSqlTableValued) // TODO: Check string, Function?
        {
          StringBuilder sb = new();
          generator.BuildInterfaceHeader(sb, profile, schema, tableValueFunction);
          foreach (MethodType methodType in Enum.GetValues(typeof(MethodType)))
          {
            generator.BuildTableValuedFunctionInterfaceMethod(sb, profile, schema, tableValueFunction, methodType);
          }
          _formatterService.CloseFile(sb);
        }*/
      }
    }

    public override void GenerateCommon(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateCommonPresentation(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateDataAccess(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateDataAccessFacade(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }

    public override void GenerateFrontend(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }
  }
}
