﻿using System;
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
using EntityGenerator.CodeGeneration.Languages.SQL.MSSQL.NETCSharp;
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

    public NETCSharpLanguageService(ProfileDto profile)
    {
      _sb = new();
      _language = new NET6CSharp(_sb, profile, new List<DatabaseLanguageBase> { new MSSQLCSharp(_sb, _language, profile) });
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
          generator.BuildInterfaceHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildTableInterfaceMethod(schema, table, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildTableInterfaceMethod(schema, table, methodType, false);
            }
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildTableClassMethod(schema, table, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildTableClassMethod(schema, table, methodType, false);
            }
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Views
        foreach (View view in schema.Views)
        {
          // Interfaces
          generator.BuildInterfaceHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildViewInterfaceMethod(schema, view, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildViewInterfaceMethod(schema, view, methodType, false);
            }
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildViewClassMethod(schema, view, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildViewClassMethod(schema, view, methodType, false);
            }
          } 
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }
        
        // Scalar Functions
        foreach (Function scalarFunction in schema.FunctionsScalar)
        {
          // Interfaces
          generator.BuildInterfaceHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildScalarFunctionInterfaceMethod(schema, scalarFunction, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildScalarFunctionInterfaceMethod(schema, scalarFunction, methodType, false);
            }
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildScalarFunctionClassMethod(schema, scalarFunction, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildScalarFunctionClassMethod(schema, scalarFunction, methodType, false);
            }
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Table-valued Functions
        foreach (Function tableValuedFunction in schema.FunctionsTableValued)
        {
          // Interfaces
          generator.BuildInterfaceHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildTableValuedFunctionInterfaceMethod(schema, tableValuedFunction, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildTableValuedFunctionInterfaceMethod(schema, tableValuedFunction, methodType, false);
            }
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{tableValuedFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());

          // Logic Classes
          generator.BuildClassHeader(schema);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes())
          {
            if (profile.Generator.GeneratorDataAccess.AsyncDaos)
            {
              generator.BuildTableValuedFunctionClassMethod(schema, tableValuedFunction, methodType, true);
            }
            if (profile.Generator.GeneratorDataAccess.SyncDaos)
            {
              generator.BuildTableValuedFunctionClassMethod(schema, tableValuedFunction, methodType, false);
            }
          }
          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{tableValuedFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }
      }
    }

    public override void GenerateCommon(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      // DTOs
      string basePath = $@"{profile.Path.CommonDir}\_{profile.Global.GeneratedSuffix}\DTOs\";
      //_fileWriter.WriteFile(basePath, $"DtoBase.{_config.GeneratedSuffix}.cs", codeBuilder.Finalize());

      // Tenant Dto
      //_fileWriter.WriteFile(basePath, $"DtoBaseTenant.{_config.GeneratedSuffix}.cs", codeBuilder.Finalize());

      // Schema DTO
      string path = $@"{profile.Path.CommonDir}\DbSchemaInformation\";
      string fileName = $@"{profile.Global.ProjectName}.{profile.Global.GeneratedSuffix}.cs";
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
      for (int databaseId = 0; databaseId < _language.DatabaseLanguages.Count; databaseId++)
      {
        generator.BuildBaseFile(databaseId);
        writerService.WriteToFile($@"{profile.Path.DataAccessDir}.SQL\_{profile.Global.GeneratedSuffix}\BaseClasses\", "Dao.cs", _formatterService.CloseFile());
      }



      // Dependency Injections
      // TODO Support multiple databases
      generator.BuildDependencyInjections(db);
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
          generator.BuildDataAccessFacadeTableExternalInterfaceHeader(schema, table);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeTableExternalInterfaceMethod(schema, table, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeTableInternalInterfaceHeader(schema, table);
          
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            for (int databaseId = 0; databaseId < _language.DatabaseLanguages.Count; databaseId++)
            {
              if (profile.Generator.GeneratorDataAccess.AsyncDaos)
              {
                generator.BuildDataAccessFacadeTableInternalInterfaceMethod(schema, table, methodType, true, databaseId);
              }
              if (profile.Generator.GeneratorDataAccess.SyncDaos)
              {
                generator.BuildDataAccessFacadeTableInternalInterfaceMethod(schema, table, methodType, false, databaseId);
              }
            }
          }

          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{table.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Views
        foreach (View view in schema.Views)
        {
          // External Interface
          generator.BuildDataAccessFacadeViewExternalInterfaceHeader(schema, view);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeViewExternalInterfaceMethod(schema, view, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeViewInternalInterfaceHeader(schema, view);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            for (int databaseId = 0; databaseId < _language.DatabaseLanguages.Count; databaseId++)
            {
              if (profile.Generator.GeneratorDataAccess.AsyncDaos)
              {
                generator.BuildDataAccessFacadeViewInternalInterfaceMethod(schema, view, methodType, true, databaseId);
              }
              if (profile.Generator.GeneratorDataAccess.SyncDaos)
              {
                generator.BuildDataAccessFacadeViewInternalInterfaceMethod(schema, view, methodType, false, databaseId);
              }
            }
          }

          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{view.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Scalar Functions
        foreach (Function scalarFunction in schema.FunctionsScalar)
        {
          // External Interface
          generator.BuildDataAccessFacadeFunctionExternalInterfaceHeader(schema, scalarFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeFunctionExternalInterfaceMethod(schema, scalarFunction, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeFunctionInternalInterfaceHeader(schema, scalarFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            for (int databaseId = 0; databaseId < _language.DatabaseLanguages.Count; databaseId++)
            {
              if (profile.Generator.GeneratorDataAccess.AsyncDaos)
              {
                generator.BuildDataAccessFacadeFunctionInternalInterfaceMethod(schema, scalarFunction, methodType, true, databaseId);
              }
              if (profile.Generator.GeneratorDataAccess.SyncDaos)
              {
                generator.BuildDataAccessFacadeFunctionInternalInterfaceMethod(schema, scalarFunction, methodType, false, databaseId);
              }
            }
          }

          writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{scalarFunction.Name}{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
        }

        // Table-valued Functions
        foreach (Function tableValuedFunction in schema.FunctionsTableValued)
        {
          // External Interface
          generator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceHeader(schema, tableValuedFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            generator.BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(schema, tableValuedFunction, methodType);
          }
          if (!profile.Generator.GeneratorDataAccessFacade.CombinedInterfaces)
          {
            writerService.WriteToFile(schemaPath, $"{profile.Global.GeneratedPrefix}{tableValuedFunction.Name}External{profile.Global.GeneratedSuffix}.cs", _formatterService.CloseFile());
          }

          // Internal Interface
          generator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(schema, tableValuedFunction);
          foreach (MethodType methodType in MethodHelper.GetMethodTypes()) // TODO: limit to types in use!
          {
            for (int databaseId = 0; databaseId < _language.DatabaseLanguages.Count; databaseId++)
            {
              if (profile.Generator.GeneratorDataAccess.AsyncDaos)
              {
                generator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(schema, tableValuedFunction, methodType, true, databaseId);
              }
              if (profile.Generator.GeneratorDataAccess.SyncDaos)
              {
                generator.BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(schema, tableValuedFunction, methodType, false, databaseId);
              }
            }
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
