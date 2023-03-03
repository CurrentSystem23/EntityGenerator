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
    void BuildConstants(StringBuilder sb, Database db, ProfileGeneratorDto profile);

    void BuildBaseDTO(StringBuilder sb, ProfileGeneratorDto profile);
    void BuildTenantBase(StringBuilder sb, ProfileGeneratorDto profile);
    void BuildSchemaDTO(StringBuilder sb, ProfileGeneratorDto profile);

    void BuildDatabaseTableDTO(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildDatabaseFunctionDTO(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildDatabaseTableValueFunctionDTO(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildDatabaseViewDTO(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);

  }
}
