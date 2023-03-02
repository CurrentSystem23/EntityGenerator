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
  public abstract partial class NETCSharp : IDataAccessGenerator
  {
    StringBuilder IDataAccessGenerator.BuildBaseFile(ProfileGeneratorDto profile)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildFunctionDAOHeader(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildFunctionDAOMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildTableDAOHeader(ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildTableDAOMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildTableValueFunctionDAOHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildTableValueFunctionDAOMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildViewDAOHeader(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    StringBuilder IDataAccessGenerator.BuildViewDAOMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
