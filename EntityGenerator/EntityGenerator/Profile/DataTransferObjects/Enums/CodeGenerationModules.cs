using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects.Enums
{
  public enum CodeGenerationModules
  {
    [EnumMember(Value = "IBusinessLogicGenerator")]
    BUSINESS_LOGIC,

    [EnumMember(Value = "ICommonGenerator")]
    COMMON,

    [EnumMember(Value = "ICommonPresentationGenerator")]
    COMMON_PRESENTATION,

    [EnumMember(Value = "IDataAccessGenerator")]
    DATA_ACCESS,

    [EnumMember(Value = "IDataAccessFacadeGenerator")]
    DATA_ACCESS_FACADE,

    [EnumMember(Value = "IFrontendGenerator")]
    FRONTEND,

  }
}
