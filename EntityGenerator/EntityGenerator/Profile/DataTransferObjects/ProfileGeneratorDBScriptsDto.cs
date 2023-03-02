using EntityGenerator.Profile.DataTransferObjects;
using EntityGenerator.Profile.DataTransferObjects.Enums;

namespace EntityGenerator.Profile.DataTransferObject
{
  public class ProfileGeneratorDBScriptsDto : ProfileCodeGenerationBase
  {
    public ProfileGeneratorDBScriptsDto() : base(CodeGenerationModules.DB_SCRIPTS)
    {
    }
  }
}