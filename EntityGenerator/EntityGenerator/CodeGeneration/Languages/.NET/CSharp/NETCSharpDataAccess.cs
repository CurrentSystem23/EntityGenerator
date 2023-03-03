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
  public abstract partial class NETCSharp : IDataAccessGenerator
  {
    void IDataAccessGenerator.BuildBaseFile(StringBuilder sb, ProfileDto profile)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildFunctionDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildFunctionDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
