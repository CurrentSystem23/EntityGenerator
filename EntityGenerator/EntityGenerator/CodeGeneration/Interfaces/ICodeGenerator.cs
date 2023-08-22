using EntityGenerator.Core.Models.ModelObjects;
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
    void ExecuteBusinessLogicGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
    void ExecuteCommonGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
    void ExecuteCommonPresentationGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
    void ExecuteDataAccessGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
    void ExecuteDataAccessFacadeGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
    void ExecuteFrontendGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
    void ExecuteCS23DomainTypeValuesGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
    void ExecuteMichaTestGenerator(Database db, ProfileDto profile, IFileWriterService writerService);
  }
}
