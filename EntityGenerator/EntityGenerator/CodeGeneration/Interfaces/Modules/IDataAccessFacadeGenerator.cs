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

    void BuildDataAccessFacadeTableExternalInterfaceHeader(ProfileDto profile, Schema schema, Table table);
    void BuildDataAccessFacadeFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function function);
    void BuildDataAccessFacadeTableValuedFunctionExternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction);
    void BuildDataAccessFacadeViewExternalInterfaceHeader(ProfileDto profile, Schema schema, View view);

    void BuildDataAccessFacadeTableExternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFacadeFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildDataAccessFacadeViewExternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);
    
    void BuildDataAccessFacadeTableInternalInterfaceHeader(ProfileDto profile, Schema schema, Table table);
    void BuildDataAccessFacadeFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function function);
    void BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(ProfileDto profile, Schema schema, Function tableValuedFunction);
    void BuildDataAccessFacadeViewInternalInterfaceHeader(ProfileDto profile, Schema schema, View view);

    void BuildDataAccessFacadeTableInternalInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFacadeFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildDataAccessFacadeViewInternalInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);

    void BuildDataAccessFacadeTableClassHeader(ProfileDto profile, Schema schema, Table table);
    void BuildDataAccessFacadeFunctionClassHeader(ProfileDto profile, Schema schema, Function function);
    void BuildDataAccessFacadeTableValuedFunctionClassHeader(ProfileDto profile, Schema schema, Function tableValuedFunction);
    void BuildDataAccessFacadeViewClassHeader(ProfileDto profile, Schema schema, View view);

    void BuildDataAccessFacadeTableClassMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildDataAccessFacadeFunctionClassMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildDataAccessFacadeTableValuedFunctionClassMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildDataAccessFacadeViewClassMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);

  }
}
