using EntityGenerator.Core.Models.ModelObjects;
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
    void BuildTableServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildFunctionServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function);
    void BuildTableValuedFunctionServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction);
    void BuildViewServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view);

    void BuildTableServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType);
    void BuildViewServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType);

  }
}
