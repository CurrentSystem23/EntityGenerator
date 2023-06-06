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
    void BuildWhereParameterClass(ProfileDto profile);

    void BuildADOInterface(ProfileDto profile, Database db);

    void BuildDataAccessTableExternalInterfaceHeader(ProfileDto profile, Schema schema, Table table);
    void BuildDataAccessFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function function);
    void BuildDataAccessTableValuedFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction);
    void BuildDataAccessViewExternalInterfaceHeader(ProfileDto profile, Schema schema, View view);

    void BuildDataAccessTableExternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessTableValuedFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildDataAccessViewExternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);
    
    void BuildDataAccessTableInternalInterfaceHeader(ProfileDto profile, Schema schema, Table table);
    void BuildDataAccessFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function function);
    void BuildDataAccessTableValuedFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction);
    void BuildDataAccessViewInternalInterfaceHeader(ProfileDto profile, Schema schema, View view);

    void BuildDataAccessTableInternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessTableValuedFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildDataAccessViewInternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);
  }
}
