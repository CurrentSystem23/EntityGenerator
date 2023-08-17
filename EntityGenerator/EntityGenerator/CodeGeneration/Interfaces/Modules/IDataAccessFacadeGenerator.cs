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
    void BuildWhereParameterClass();

    void BuildDependencyInjectionBaseFile();
    void BuildADOInterface(Database db);

    void BuildDataAccessFacadeTableExternalInterfaceHeader(Schema schema, Table table);
    void BuildDataAccessFacadeFunctionExternalInterfaceHeader(Schema schema, Function function);
    void BuildDataAccessFacadeTableValuedFunctionExternalInterfaceHeader(Schema schema, Function tableValuedFunction);
    void BuildDataAccessFacadeViewExternalInterfaceHeader(Schema schema, View view);

    void BuildDataAccessFacadeTableExternalInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync);
    void BuildDataAccessFacadeFunctionExternalInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync);
    void BuildDataAccessFacadeTableValuedFunctionExternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync);
    void BuildDataAccessFacadeViewExternalInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync);
    
    void BuildDataAccessFacadeTableInternalInterfaceHeader(Schema schema, Table table);
    void BuildDataAccessFacadeFunctionInternalInterfaceHeader(Schema schema, Function function);
    void BuildDataAccessFacadeTableValuedFunctionInternalInterfaceHeader(Schema schema, Function tableValuedFunction);
    void BuildDataAccessFacadeViewInternalInterfaceHeader(Schema schema, View view);

    void BuildDataAccessFacadeTableInternalInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync, int databaseId);
    void BuildDataAccessFacadeFunctionInternalInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId);
    void BuildDataAccessFacadeTableValuedFunctionInternalInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId);
    void BuildDataAccessFacadeViewInternalInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId);

    void BuildDataAccessFacadeTableClassHeader(Schema schema, Table table);
    void BuildDataAccessFacadeFunctionClassHeader(Schema schema, Function function);
    void BuildDataAccessFacadeTableValuedFunctionClassHeader(Schema schema, Function tableValuedFunction);
    void BuildDataAccessFacadeViewClassHeader(Schema schema, View view);

    void BuildDataAccessFacadeTableClassMethod(Schema schema, Table table, MethodType methodType, bool isAsync, int databaseId);
    void BuildDataAccessFacadeFunctionClassMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId);
    void BuildDataAccessFacadeTableValuedFunctionClassMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId);
    void BuildDataAccessFacadeViewClassMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId);

  }
}
