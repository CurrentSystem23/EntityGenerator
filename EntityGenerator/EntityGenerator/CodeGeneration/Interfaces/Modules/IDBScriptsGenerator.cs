using EntityGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IDBScriptsGenerator
  {
    void BuildConstaintsCheckToggleScript(StringBuilder sb, Database db);
    void BuildMergeInsertScript(StringBuilder sb, Schema schema, Table table);
    void BuildHistTableScript(StringBuilder sb, Schema schema, Table table);
    void BuildHistTriggerScript(StringBuilder sb, Schema schema, Table table);

    void BuildUserRightsRolesScript(StringBuilder sb, Database db);
    void BuildUserRightsScript(StringBuilder sb, Database db);
    void BuildUserRightToUserRightsRoleScript(StringBuilder sb, Database db);
  }
}
