using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects
{
  public class ProfileGeneratorCS23DomainTypeValueDTO : ProfileCodeGenerationBase
  {
    public ProfileGeneratorCS23DomainTypeValueDTO() : base(CodeGenerationModules.CS23DomainTypeValueGenerator)
    {
    }

    public bool DomainTypeValues { get; set; }

    public string DomainTypeTableName { get; set; } = "DomainTypes";

    public string DomainValueTableName { get; set; } = "DomainValues";
  }
}
