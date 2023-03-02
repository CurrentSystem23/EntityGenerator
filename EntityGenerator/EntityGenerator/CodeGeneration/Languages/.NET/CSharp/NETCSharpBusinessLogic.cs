using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IBusinessLogicGenerator
  {
    StringBuilder IBusinessLogicGenerator.BuildFunctionClassHeader(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildFunctionClassMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildFunctionInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildFunctionInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableClassHeader( StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableClassMethod( StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableInterfaceHeader( StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IBusinessLogicGenerator.BuildTableInterfaceMethod( StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildTableValueFunctionClassHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildTableValueFunctionClassMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildTableValueFunctionInterfaceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildTableValueFunctionInterfaceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildViewClassHeader(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildViewClassMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildViewInterfaceHeader(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    StringBuilder IBusinessLogicGenerator.BuildViewInterfaceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
