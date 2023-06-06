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
    void IDataAccessGenerator.BuildBaseFile(ProfileDto profile)
    { 
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildFunctionDAOHeader(ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildFunctionDAOMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOHeader(ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOHeader(ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOHeader(ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOMethod(ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
