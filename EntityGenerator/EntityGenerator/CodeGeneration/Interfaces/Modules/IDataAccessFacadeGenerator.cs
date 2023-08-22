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
    void BuildDependencyInjectionBaseFile();

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
