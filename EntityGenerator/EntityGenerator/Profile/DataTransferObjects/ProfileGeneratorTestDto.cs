using EntityGenerator.Profile.DataTransferObjects;
using EntityGenerator.Profile.DataTransferObjects.Enums;

namespace EntityGenerator.Profile.DataTransferObject
{
  public class ProfileGeneratorTestDto : ProfileCodeGenerationBase
  {
    public ProfileGeneratorTestDto() : base(CodeGenerationModules.TestGenerator)
    {
    }
  }
}