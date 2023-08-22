using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface ICommonGenerator
  {
    void BuildConstants(Database db);

    void BuildBaseDTO();
    void BuildTenantBase();
    void BuildSchemaDTO(Database db);
    void BuildADOInterface(Database db);
    void BuildWhereParameterClass();

    void BuildTableDTO(Schema schema, Table table);
    void BuildTableValuedFunctionDTO(Schema schema, Function tableValuedFunction);
    void BuildViewDTO(Schema schema, View view);

    void BuildInterfaceHeader(Schema schema);

    void BuildTableInterfaceMethod(Schema schema, Table table, MethodType methodType, bool isAsync);
    void BuildScalarFunctionInterfaceMethod(Schema schema, Function function, MethodType methodType, bool isAsync);
    void BuildTableValuedFunctionInterfaceMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync);
    void BuildViewInterfaceMethod(Schema schema, View view, MethodType methodType, bool isAsync);

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

  }
}
