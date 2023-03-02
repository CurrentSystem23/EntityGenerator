using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Profile.DataTransferObjects.Enums
{
  public enum Languages
  {
    [EnumMember(Value = "NET5CSharp")]
    DOTNET_6_CSHARP,

    [EnumMember(Value = "Angular15TypeScript")]
    ANGULAR_15_TYPESCRIPT,
  }
}
