using EntityGenerator;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects
{
  [Serializable]
  public abstract class ProfileCodeGenerationBase
  {
    protected ProfileCodeGenerationBase(CodeGenerationModules moduleName)
    {
      _moduleName = moduleName;
    }
    /// <summary>
    /// Toggle for core functionality of code generation module.
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// The corresponding code generator module of this profile.
    /// </summary>
    [JsonIgnore]
    public CodeGenerationModules ModuleName { get => _moduleName; }

    [NonSerialized]
    protected CodeGenerationModules _moduleName;

    /// <summary>
    /// The language this component will be written in.
    /// </summary>
    public Languages Language { get; set; }
    public bool Cleanup { get; set; } = true;
  }
}
