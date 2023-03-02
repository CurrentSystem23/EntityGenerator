using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  public interface ICodeGenerator
  {
    void GenerateBusinessLogic(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    void GenerateCommon(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    void GenerateCommonPresentation(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    void GenerateDataAccess(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    void GenerateDataAccessFacade(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
    void GenerateFrontend(Database db, ProfileGeneratorDto profile, IFileWriterService writerService);
  }
}
