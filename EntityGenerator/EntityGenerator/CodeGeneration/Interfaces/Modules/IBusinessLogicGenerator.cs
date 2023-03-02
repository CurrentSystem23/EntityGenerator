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
    StringBuilder BuildFunctionInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Function function);
    StringBuilder BuildTableValueFunctionInterfaceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    StringBuilder BuildViewInterfaceHeader(ProfileGeneratorDto profile, Schema schema, View view);

    void BuildTableInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    StringBuilder BuildFunctionInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    StringBuilder BuildTableValueFunctionInterfaceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    StringBuilder BuildViewInterfaceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

    void BuildTableClassHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    StringBuilder BuildFunctionClassHeader(ProfileGeneratorDto profile, Schema schema, Function function);
    StringBuilder BuildTableValueFunctionClassHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    StringBuilder BuildViewClassHeader(ProfileGeneratorDto profile, Schema schema, View view);
 
    void BuildTableClassMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    StringBuilder BuildFunctionClassMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    StringBuilder BuildTableValueFunctionClassMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    StringBuilder BuildViewClassMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

  }
}
