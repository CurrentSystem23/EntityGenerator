using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Services;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Angular.TypeScript
{
  internal class AngularTypeScriptLanguageService : LanguageService
  {
    public override void GenerateBusinessLogic(Database db, ProfileDto profile, IFileWriterService writerService)
    {
      throw new NotImplementedException();
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
      Console.WriteLine("TypeScript Frontend not yet implemented");
    }
  }
}
