using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IDataAccessFacadeGenerator
  {
    StringBuilder BuildWhereParameterClass(ProfileGeneratorDto profile);

    StringBuilder BuildADOInterface(ProfileGeneratorDto profile, Database db);

    StringBuilder BuildDataAccessTableExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Table table);
    StringBuilder BuildDataAccessFunctionExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Function function);
    StringBuilder BuildDataAccessTableValueFunctionExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    StringBuilder BuildDataAccessViewExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, View view);

    StringBuilder BuildDataAccessTableExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    StringBuilder BuildDataAccessFunctionExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    StringBuilder BuildDataAccessTableValueFunctionExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    StringBuilder BuildDataAccessViewExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

    StringBuilder BuildDataAccessTableInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Table table);
    StringBuilder BuildDataAccessFunctionInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Function function);
    StringBuilder BuildDataAccessTableValueFunctionInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    StringBuilder BuildDataAccessViewInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, View view);

    StringBuilder BuildDataAccessTableInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    StringBuilder BuildDataAccessFunctionInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    StringBuilder BuildDataAccessTableValuefunctionInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    StringBuilder BuildDataAccessViewInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);
  }
}
