using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IDBScriptsGenerator
  {
    void BuildConstaintsCheckToggleScript(StringBuilder sb, ProfileDto profile, Database db);
    void BuildMergeInsertScript(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildHistTableScript(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildHistTriggerScript(StringBuilder sb, ProfileDto profile, Schema schema, Table table);

    void BuildUserRightsRolesScript(StringBuilder sb, ProfileDto profile, Database db);
    void BuildUserRightsScript(StringBuilder sb, ProfileDto profile, Database db);
    void BuildUserRightToUserRightsRoleScript(StringBuilder sb, ProfileDto profile, Database db);
  }
}
