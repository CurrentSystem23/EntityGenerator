using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Services
{
  public abstract class LanguageService
  {
    public abstract void GenerateBusinessLogic(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    public abstract void GenerateCommon(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    public abstract void GenerateCommonPresentation(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    public abstract void GenerateDataAccess(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    public abstract void GenerateDataAccessFacade(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    public abstract void GenerateFrontend(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
  }
}
