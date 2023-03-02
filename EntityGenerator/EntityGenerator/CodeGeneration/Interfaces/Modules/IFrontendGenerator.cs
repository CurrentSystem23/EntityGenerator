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
    StringBuilder BuildTableServiceHeader(ProfileGeneratorDto profile, Schema schema, Table table);
    StringBuilder BuildFunctionServiceHeader(ProfileGeneratorDto profile, Schema schema, Function function);
    StringBuilder BuildTableValueFunctionServiceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    StringBuilder BuildViewServiceHeader(ProfileGeneratorDto profile, Schema schema, View view);

    StringBuilder BuildTableServiceMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    StringBuilder BuildFunctionServiceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    StringBuilder BuildTableValueFunctionServiceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    StringBuilder BuildViewServiceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

  }
}
