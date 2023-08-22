using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects
{
  [Serializable]
  public class ProfileGeneratorMichaTestDto : ProfileCodeGenerationBase
  {
    public ProfileGeneratorMichaTestDto() : base(CodeGenerationModules.MichaTestGenerator)
    {
    }

    public bool IsMichaTest { get; set; }
  }
}
