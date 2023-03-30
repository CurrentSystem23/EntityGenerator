using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.Core.Models.ModelObjects;
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
    void IDataAccessFacadeGenerator.BuildADOInterface(StringBuilder sb, ProfileDto profile, Database db)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessFunctionInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuedFunctionExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuedFunctionExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuedFunctionInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessTableValuefunctionInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewExternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewExternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewInternalInterfaceHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildDataAccessViewInternalInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessFacadeGenerator.BuildWhereParameterClass(StringBuilder sb, ProfileDto profile)
    {
      throw new NotImplementedException();
    }
  }
}
