using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface ICommonGenerator
  {
    void BuildBaseDTO();
    void BuildTenantBase();
    void BuildDatabaseDTOs();
    void BuildSchemaDTO();
  }
}
