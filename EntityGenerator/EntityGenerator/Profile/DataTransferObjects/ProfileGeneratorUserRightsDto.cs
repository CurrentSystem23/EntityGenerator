using EntityGenerator.Profile.DataTransferObjects;
using EntityGenerator.Profile.DataTransferObjects.Enums;

namespace EntityGenerator.Profile.DataTransferObject
{
  public class ProfileGeneratorUserRightsDto : ProfileCodeGenerationBase
  {
    public ProfileGeneratorUserRightsDto() : base(CodeGenerationModules.UserRightsGenerator)
    {
    }
  }
}