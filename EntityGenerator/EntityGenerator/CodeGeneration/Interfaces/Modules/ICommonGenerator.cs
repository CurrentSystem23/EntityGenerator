using EntityGenerator.Core.Models;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface ICommonGenerator
  {
    StringBuilder BuildConstants(ProfileGeneratorDto profile);

    StringBuilder BuildBaseDTO(ProfileGeneratorDto profile);
    StringBuilder BuildTenantBase(ProfileGeneratorDto profile);
    StringBuilder BuildSchemaDTO(ProfileGeneratorDto profile);

    StringBuilder BuildDatabaseTableDTO(ProfileGeneratorDto profile, Schema schema, Table table);
    StringBuilder BuildDatabaseFunctionDTO(ProfileGeneratorDto profile, Schema schema, Function function);
    StringBuilder BuildDatabaseTableValueFunctionDTO(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    StringBuilder BuildDatabaseViewDTO(ProfileGeneratorDto profile, Schema schema, View view);

  }
}
