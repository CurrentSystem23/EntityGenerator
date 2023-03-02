using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  internal interface IFrontendGenerator
  {
    void BuildTableServiceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildFunctionServiceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildTableValueFunctionServiceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildViewServiceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);

    void BuildTableServiceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionServiceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValueFunctionServiceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    void BuildViewServiceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

  }
}
