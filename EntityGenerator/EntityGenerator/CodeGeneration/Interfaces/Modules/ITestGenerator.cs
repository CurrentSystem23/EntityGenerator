using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  internal interface ITestGenerator
  {
    void BuildTableMock(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildFunctionMock(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildTableValueFunctionMock(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildViewMock(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);
  }
}
