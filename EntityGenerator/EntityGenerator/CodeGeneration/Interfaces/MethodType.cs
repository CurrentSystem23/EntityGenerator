using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  [DataContract(Name = "MethodType")]
  public enum MethodType
  {
    [EnumMember]
    GET,
    [EnumMember]
    SAVE,
    [EnumMember]
    DELETE,
    [EnumMember]
    MERGE,
    [EnumMember]
    COUNT,
    [EnumMember]
    HAS_CHANGED,
    [EnumMember]
    BUlK_INSERT,
    [EnumMember]
    BULK_MERGE,
    [EnumMember]
    BULK_UPDATE,
    [EnumMember]
    HIST_GET,
    [EnumMember]
    EXECUTE
  }
}
