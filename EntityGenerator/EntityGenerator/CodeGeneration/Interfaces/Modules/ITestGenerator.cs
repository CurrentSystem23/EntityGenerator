using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface ITestGenerator
  {
    void BuildTableMock(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildFunctionMock(StringBuilder sb, ProfileDto profile, Schema schema, Function function);
    void BuildTableValuedFunctionMock(StringBuilder sb, ProfileDto profile, Schema schema, Function tableValuedFunction);
    void BuildViewMock(StringBuilder sb, ProfileDto profile, Schema schema, View view);
  }
}
