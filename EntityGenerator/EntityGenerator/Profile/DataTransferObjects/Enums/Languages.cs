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
  public enum Languages
  {
    [EnumMember(Value = "NET6CSharp")]
    [StringValue("NET.CSharp.NET_6.NET6CSharp")]
    DOTNET_6_CSHARP,

    [EnumMember(Value = "Angular15TypeScript")]
    [StringValue("Angular.TypeScript.Angular15.Angular15TypeScript")]
    ANGULAR_15_TYPESCRIPT,
  }
}
