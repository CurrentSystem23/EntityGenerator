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
    void BuildConstants(Database db);

    void BuildBaseDTO();
    void BuildTenantBase();
    void BuildSchemaDTO(Database db);

    void BuildTableDTO(Schema schema, Table table);
    void BuildTableValuedFunctionDTO(Schema schema, Function tableValuedFunction);
    void BuildViewDTO(Schema schema, View view);

  }
}
