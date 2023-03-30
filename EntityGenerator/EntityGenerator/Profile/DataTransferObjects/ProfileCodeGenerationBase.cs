using EntityGenerator;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects
{
  public abstract class ProfileCodeGenerationBase
  {
    protected ProfileCodeGenerationBase(CodeGenerationModules moduleName)
    {
      ModuleName = moduleName;
    }
    /// <summary>
    /// The corresponding code generator module of this profile.
    /// </summary>
    public CodeGenerationModules ModuleName { get; }

    /// <summary>
    /// The language this component will be written in.
    /// </summary>
    public Languages Language { get; set; }
    public bool Cleanup { get; set; } = true;
  }
}
