using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessFacadeGenerator
  {
    StringBuilder IDataAccessFacadeGenerator.BuildADOInterface(ProfileGeneratorDto profile, Database db)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessFunctionExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessFunctionExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessFunctionInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessFunctionInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableValueFunctionExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableValueFunctionExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableValueFunctionInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessTableValuefunctionInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessViewExternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessViewExternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessViewInternalInterfaceHeader(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildDataAccessViewInternalInterfaceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessFacadeGenerator.BuildWhereParameterClass(ProfileGeneratorDto profile)
    {
      throw new NotImplementedException();
    }
  }
}
