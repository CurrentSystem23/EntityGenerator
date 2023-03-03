using EntityGenerator.Core.Models.ModelObjects;
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
    void BuildWhereParameterClass(StringBuilder sb, ProfileDto profile);

    void BuildADOInterface(StringBuilder sb, ProfileDto profile, Database db);

    void BuildDataAccessTableExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildDataAccessFunctionExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function);
    void BuildDataAccessTableValuedFunctionExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction);
    void BuildDataAccessViewExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view);

    void BuildDataAccessTableExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFunctionExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessTableValuedFunctionExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType);
    void BuildDataAccessViewExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType);

    void BuildDataAccessTableInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildDataAccessFunctionInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function);
    void BuildDataAccessTableValuedFunctionInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction);
    void BuildDataAccessViewInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view);

    void BuildDataAccessTableInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFunctionInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessTableValuefunctionInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType);
    void BuildDataAccessViewInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType);
  }
}
