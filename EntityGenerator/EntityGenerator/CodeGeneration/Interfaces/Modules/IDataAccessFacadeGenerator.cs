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
    void BuildWhereParameterClass(StringBuilder sb, ProfileGeneratorDto profile);

    void BuildADOInterface(StringBuilder sb, ProfileGeneratorDto profile, Database db);

    void BuildDataAccessTableExternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildDataAccessFunctionExternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildDataAccessTableValueFunctionExternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildDataAccessViewExternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);

    void BuildDataAccessTableExternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFunctionExternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessTableValueFunctionExternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    void BuildDataAccessViewExternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);

    void BuildDataAccessTableInternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildDataAccessFunctionInternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildDataAccessTableValueFunctionInternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildDataAccessViewInternalInterfaceHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);

    void BuildDataAccessTableInternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFunctionInternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessTableValuefunctionInternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    void BuildDataAccessViewInternalInterfaceMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);
  }
}
