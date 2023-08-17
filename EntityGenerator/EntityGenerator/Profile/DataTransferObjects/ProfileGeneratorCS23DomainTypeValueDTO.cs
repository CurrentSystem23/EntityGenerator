using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects
{
  [Serializable]
  public class ProfileGeneratorCS23DomainTypeValuesDTO : ProfileCodeGenerationBase
  {
    public ProfileGeneratorCS23DomainTypeValuesDTO() : base(CodeGenerationModules.CS23DomainTypeValuesGenerator)
    {
    }

    public bool DomainTypeValues { get; set; }

    public string DomainTypeTableName { get; set; } = "DomainType";

    public string DomainValueTableName { get; set; } = "DomainValue";
  }
}
