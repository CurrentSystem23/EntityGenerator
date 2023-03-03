using EntityGenerator.Core.Models.ModelObjects;
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
    void BuildConstants(StringBuilder sb, Database db, ProfileDto profile);

    void BuildBaseDTO(StringBuilder sb, ProfileDto profile);
    void BuildTenantBase(StringBuilder sb, ProfileDto profile);
    void BuildSchemaDTO(StringBuilder sb, ProfileDto profile);

    void BuildDatabaseTableDTO(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildDatabaseFunctionDTO(StringBuilder sb, ProfileDto profile, Schema schema, Function function);
    void BuildDatabaseTableValuedFunctionDTO(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction);
    void BuildDatabaseViewDTO(StringBuilder sb, ProfileDto profile, Schema schema, View view);

  }
}
