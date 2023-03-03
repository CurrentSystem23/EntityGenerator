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
  public abstract partial class NETCSharp : ICommonGenerator
  {
    void ICommonGenerator.BuildBaseDTO(StringBuilder sb, ProfileDto profile)
    {
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildConstants(StringBuilder sb, Database db, ProfileDto profile)
    {
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildDatabaseFunctionDTO(StringBuilder sb, ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildDatabaseTableDTO(StringBuilder sb, ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildDatabaseTableValuedFunctionDTO(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildDatabaseViewDTO(StringBuilder sb, ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildSchemaDTO(StringBuilder sb, ProfileDto profile)
    {
      throw new NotImplementedException();
    }

    void ICommonGenerator.BuildTenantBase(StringBuilder sb, ProfileDto profile)
    {
      throw new NotImplementedException();
    }
  }
}
