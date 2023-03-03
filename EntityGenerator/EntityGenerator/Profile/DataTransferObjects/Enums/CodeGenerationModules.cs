using EntityGenerator.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects.Enums
{
  [DataContract]
  public enum CodeGenerationModules
  {
    [EnumMember(Value = "BusinessLogic")]
    [StringValue("IBusinessLogicGenerator")]
    BusinessLogicGenerator,

    [EnumMember(Value = "Common")]
    [StringValue("ICommonGenerator")]
    CommonGenerator,

    [EnumMember(Value = "CommonPresentation")]
    [StringValue("ICommonPresentationGenerator")]
    CommonPresentationGenerator,

    [EnumMember(Value = "DataAccess")]
    [StringValue("IDataAccessGenerator")]
    DataAccessGenerator,

    [EnumMember(Value = "DataAccessFacade")]
    [StringValue("IDataAccessFacadeGenerator")]
    DataAccessFacadeGenerator,

    [EnumMember(Value = "Frontend")]
    [StringValue("IFrontendGenerator")]
    FrontendGenerator,

    [EnumMember(Value = "API")]
    [StringValue("IAPIGenerator")]
    APIGenerator,
    
    [EnumMember(Value = "Test")]
    [StringValue("ITestGenerator")]
    TestGenerator,

    [EnumMember(Value = "UserRights")]
    [StringValue("IUserRightsGenerator")]
    UserRightsGenerator,

    [EnumMember(Value = "DBScripts")]
    [StringValue("IDBScriptsGenerator")]
    DBScriptsGenerator,
  }
}
