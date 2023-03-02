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
  public abstract partial class NETCSharp : ICommonGenerator
  {
    StringBuilder ICommonGenerator.BuildBaseDTO(ProfileGeneratorDto profile)
    {
      throw new NotImplementedException();
    }

    StringBuilder ICommonGenerator.BuildConstants(ProfileGeneratorDto profile)
    {
      throw new NotImplementedException();
    }

    StringBuilder ICommonGenerator.BuildDatabaseFunctionDTO(ProfileGeneratorDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    StringBuilder ICommonGenerator.BuildDatabaseTableDTO(ProfileGeneratorDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    StringBuilder ICommonGenerator.BuildDatabaseTableValueFunctionDTO(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction)
    {
      throw new NotImplementedException();
    }

    StringBuilder ICommonGenerator.BuildDatabaseViewDTO(ProfileGeneratorDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    StringBuilder ICommonGenerator.BuildSchemaDTO(ProfileGeneratorDto profile)
    {
      throw new NotImplementedException();
    }

    StringBuilder ICommonGenerator.BuildTenantBase(ProfileGeneratorDto profile)
    {
      throw new NotImplementedException();
    }
  }
}
