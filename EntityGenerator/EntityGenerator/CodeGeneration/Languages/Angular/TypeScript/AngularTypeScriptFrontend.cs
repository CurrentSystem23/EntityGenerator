using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Angular.TypeScript
{
  public abstract partial class AngularTypeScript : IFrontendGenerator
  {
    public StringBuilder BuildFunctionServiceHeader(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    public StringBuilder BuildFunctionServiceMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    public StringBuilder BuildTableServiceHeader(ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    public StringBuilder BuildTableServiceMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    public StringBuilder BuildTableValueFunctionServiceHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    public StringBuilder BuildTableValueFunctionServiceMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    public StringBuilder BuildViewServiceHeader(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    public StringBuilder BuildViewServiceMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
