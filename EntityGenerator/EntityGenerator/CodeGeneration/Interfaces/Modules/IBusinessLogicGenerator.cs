using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IBusinessLogicGenerator
  {
    void BuildTableInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildFunctionInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildTableValueFunctionInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildViewInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);

    void BuildTableInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValueFunctionInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    void BuildViewInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

    void BuildTableClassHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildFunctionClassHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildTableValueFunctionClassHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildViewClassHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);
 
    void BuildTableClassMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionClassMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValueFunctionClassMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    void BuildViewClassMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

  }
}
