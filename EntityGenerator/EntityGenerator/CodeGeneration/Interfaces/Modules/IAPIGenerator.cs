using EntityGenerator.Core.Models.ModelObjects;
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
    void BuildTableControllerHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildFunctionControllerHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function);
    void BuildTableValuedFunctionControllerHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValueFunction);
    void BuildViewControllerHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view);

    void BuildTableControllerMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionControllerMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValueFunctionControllerMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValueFunction, MethodType methodType);
    void BuildViewControllerMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType);
  }
}
