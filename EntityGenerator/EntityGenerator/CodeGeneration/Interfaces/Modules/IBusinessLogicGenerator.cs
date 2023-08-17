﻿using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System.Text;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IBusinessLogicGenerator
  {
    void BuildClassHeader(Schema schema);
 
    void BuildTableClassMethod(Schema schema, Table table, MethodType methodType, bool isAsync);
    void BuildScalarFunctionClassMethod(Schema schema, Function function, MethodType methodType, bool isAsync);
    void BuildTableValuedFunctionClassMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync);
    void BuildViewClassMethod(Schema schema, View view, MethodType methodType, bool isAsync);
  }
}
