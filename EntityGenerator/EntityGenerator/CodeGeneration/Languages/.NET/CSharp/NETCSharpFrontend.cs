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
  public abstract partial class NETCSharp : IFrontendGenerator
  {
    StringBuilder IFrontendGenerator.BuildFunctionServiceHeader(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    StringBuilder IFrontendGenerator.BuildFunctionServiceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IFrontendGenerator.BuildTableServiceHeader(ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    StringBuilder IFrontendGenerator.BuildTableServiceMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IFrontendGenerator.BuildTableValueFunctionServiceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    StringBuilder IFrontendGenerator.BuildTableValueFunctionServiceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    StringBuilder IFrontendGenerator.BuildViewServiceHeader(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    StringBuilder IFrontendGenerator.BuildViewServiceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
