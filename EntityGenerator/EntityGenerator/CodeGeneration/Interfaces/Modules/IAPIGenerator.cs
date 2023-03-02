using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IAPIGenerator
  {
    void BuildTableControllerHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildTableControllerHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildTableControllerHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildTableControllerHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);

    void BuildTableControllerMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionControllerMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValueFunctionControllerMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    void BuildViewControllerMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);
  }
}
