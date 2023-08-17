using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Profile.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace EntityGenerator.Profile.DataTransferObject
{
  /// <summary>
  /// Class <see cref="ProfileGeneratorDto"/> models generator settings for a project.
  /// </summary>
  [Serializable]
  public class ProfileGeneratorDto
  {
    /// <summary>
    /// Constructor for <see cref="ProfileGeneratorDto"/> class.
    /// </summary>
    public ProfileGeneratorDto()
    {
      GeneratorFrontend = new ProfileGeneratorFrontendDto();
      GeneratorBusinessLogic = new ProfileGeneratorBusinessLogicDto();
      GeneratorCommon = new ProfileGeneratorCommonDto();
      GeneratorDatabase = new ProfileGeneratorDatabaseDto();
      GeneratorAPI = new ProfileGeneratorAPIDto();
      GeneratorCS23DomainTypeValues = new();
      GeneratorDataAccess = new();
      GeneratorDataAccessFacade = new();
      GeneratorUserRights = new();
      GeneratorTest = new();
      GeneratorDBScripts = new();
    }

    /// <summary>
    /// The list of method types for which functions should be generated. Default is all.
    /// </summary>
    public List<MethodType> MethodTypes { get; set; } = new List<MethodType>((IEnumerable<MethodType>)Enum.GetValues(typeof(MethodType)));

    /// <summary>
    /// The generator settings for the frontend project.
    /// </summary>
    public ProfileGeneratorFrontendDto GeneratorFrontend { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorBusinessLogicDto GeneratorBusinessLogic { get; set; }

    /// <summary>
    /// The generator settings for the common project.
    /// </summary>
    public ProfileGeneratorCommonDto GeneratorCommon { get; set; }

    /// <summary>
    /// The generator settings for the business logic project.
    /// </summary>
    public ProfileGeneratorDatabaseDto GeneratorDatabase { get; set; }

    /// <summary>
    /// The generator settings for the data access project.
    /// </summary>
    public ProfileGeneratorDataAccessDto GeneratorDataAccess { get; set; }

    /// <summary>
    /// The generator settings for the data access facade project.
    /// </summary>
    public ProfileGeneratorDataAccessFacadeDto GeneratorDataAccessFacade { get; set; }
    
    public ProfileGeneratorTestDto GeneratorTest { get; set; }
    public ProfileGeneratorAPIDto GeneratorAPI { get; set; }
    public ProfileGeneratorUserRightsDto GeneratorUserRights { get; set; }
    public ProfileGeneratorDBScriptsDto GeneratorDBScripts { get; set; }
    public ProfileGeneratorCS23DomainTypeValuesDTO GeneratorCS23DomainTypeValues { get; set; }
  }
}