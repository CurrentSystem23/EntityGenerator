using EntityGenerator.Profile.DataTransferObjects;
using EntityGenerator.Profile.DataTransferObjects.Enums;

namespace EntityGenerator.Profile.DataTransferObject
{
  public class ProfileGeneratorAPIDto : ProfileCodeGenerationBase
  {
    public ProfileGeneratorAPIDto() : base(CodeGenerationModules.APIGenerator)
    {
    }
  }
}