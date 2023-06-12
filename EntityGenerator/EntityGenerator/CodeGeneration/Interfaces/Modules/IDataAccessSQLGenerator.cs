using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IDataAccessSQLGenerator : IDataAccessGenerator
  {
    void BuildBaseFileExtension(ProfileDto profile);
    void BuildWhereParameterClass(ProfileDto profile);
  }
}
