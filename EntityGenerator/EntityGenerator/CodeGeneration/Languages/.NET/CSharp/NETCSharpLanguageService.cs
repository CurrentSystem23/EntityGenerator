using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
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
      _formatterService = new NETCSharpFormatterService(_sb, _language);
    }

    public override void GenerateBusinessLogic(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      IBusinessLogicGenerator generator = _language as IBusinessLogicGenerator;

      // Tables
      foreach (Schema schema in db.Schemas)
      {
        string schemaPath = $"{profile.Path.BusinessLogicDir}/{schema.Name}";

        foreach (Table table in schema.Tables)
        {
          // Interfaces
          generator.BuildInterfaceHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildTableInterfaceMethod(profile, schema, table, methodType);
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            generator.BuildTableClassMethod(profile, schema, table, methodType);
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Views
        foreach (View view in schema.Views)
        {
          // Interfaces
          generator.BuildInterfaceHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildViewInterfaceMethod(profile, schema, view, methodType);
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            generator.BuildViewClassMethod(profile, schema, view, methodType);
          } 
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }
        
        // Scalar Functions
        foreach (Function scalarFunction in schema.FunctionsScalar)
        {
          // Interfaces
          generator.BuildInterfaceHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildScalarFunctionInterfaceMethod(profile, schema, scalarFunction, methodType);
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            generator.BuildScalarFunctionClassMethod(profile, schema, scalarFunction, methodType);
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Table-valued Functions
        foreach (Function tableValuedFunction in schema.FunctionsTableValued)
        {
          // Interfaces
          generator.BuildInterfaceHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildScalarFunctionInterfaceMethod(profile, schema, tableValuedFunction, methodType);
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{tableValuedFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(profile, schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            generator.BuildScalarFunctionClassMethod(profile, schema, tableValuedFunction, methodType);
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{tableValuedFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }
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
      IDataAccessGenerator generator = _language as IDataAccessGenerator;
      // TODO

      // Base File
      generator.BuildBaseFile(profile);
      writerService.WriteToFile($@"{profile.Path.DataAccessDir}.SQL\_{profile.Global.GeneratedSuffix}\BaseClasses\", "Dao.cs", _formatterService.CloseFile());

      // Dependency Injections
      generator.BuildDependencyInjections(profile, db);
      writerService.WriteToFile($@"{profile.Path.DataAccessDir}.{db.Name}\_{profile.Global.GeneratedSuffix}\Helper\", $@"DataAccessAdoInitializer.{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
    }

    public override void GenerateDataAccessFacade(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      IDataAccessFacadeGenerator generator = _language as IDataAccessFacadeGenerator;

      // Tables
      foreach (Schema schema in db.Schemas)
      {
        string schemaPath = $"{profile.Path.DataAccessFacadeDir}/{schema.Name}";
        foreach (Table table in schema.Tables)
        {
          // External Interface
          generator.BuildDataAccessFacadeTableExternalInterfaceHeader(profile, schema, table);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeTableExternalInterfaceMethod(profile, schema, table, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeTableInternalInterfaceHeader(profile, schema, table);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeTableInternalInterfaceMethod(profile, schema, table, methodType);
          }

          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Views
        foreach (View view in schema.Views)
        {
          // External Interface
          generator.BuildDataAccessFacadeViewExternalInterfaceHeader(profile, schema, view);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeViewExternalInterfaceMethod(profile, schema, view, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeViewInternalInterfaceHeader(profile, schema, view);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeViewInternalInterfaceMethod(profile, schema, view, methodType);
          }

          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Scalar Functions
        foreach (Function scalarFunction in schema.FunctionsScalar)
        {
          // External Interface
          generator.BuildDataAccessFacadeFunctionExternalInterfaceHeader(profile, schema, scalarFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeFunctionExternalInterfaceMethod(profile, schema, scalarFunction, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeFunctionInternalInterfaceHeader(profile, schema, scalarFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeFunctionInternalInterfaceMethod(profile, schema, scalarFunction, methodType);
          }

          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Table-valued Functions
        foreach (Function tableValuedFunction in schema.FunctionsTableValued)
        {
          // External Interface
          generator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceHeader(profile, schema, tableValuedFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(profile, schema, tableValuedFunction, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{tableValuedFunction.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(profile, schema, tableValuedFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(profile, schema, tableValuedFunction, methodType);
          }

          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{tableValuedFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }
        // Universal ADO Interface
        generator.BuildADOInterface(profile, db);
        writerService.WriteToFile(profile.Path.DataAccessFacadeDir, $"IDataAccess.Ado.{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

        // Where-Parameter Class
        generator.BuildWhereParameterClass(profile);
        writerService.WriteToFile(profile.Path.DataAccessFacadeDir, "WhereParameter.cs", _formatterService.CloseFile());
      }
    }

    public override void GenerateFrontend(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
    }
  }
}
