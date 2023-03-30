using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IUserRightsGenerator
  {
    void BuildUserRightsConstants(StringBuilder sb, ProfileDto profile, Database db);
  }
}
