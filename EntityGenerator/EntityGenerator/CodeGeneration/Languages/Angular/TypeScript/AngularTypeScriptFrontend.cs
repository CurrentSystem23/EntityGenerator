using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.Core.Models.ModelObjects;
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
    void IFrontendGenerator.BuildFunctionServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IFrontendGenerator.BuildFunctionServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IFrontendGenerator.BuildTableServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IFrontendGenerator.BuildTableServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IFrontendGenerator.BuildTableValuedFunctionServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IFrontendGenerator.BuildTableValuedFunctionServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IFrontendGenerator.BuildViewServiceHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IFrontendGenerator.BuildViewServiceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
