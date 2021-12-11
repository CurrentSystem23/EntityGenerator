using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EntityGenerator.Profile.DataTransferObject.Enums
{
  /// <summary>
  /// Types <see cref="Languages"/> of supported programming languages
  /// </summary>
  [DataContract]
  public enum Languages
  {
    /// <summary>
    /// CSharp
    /// </summary>
    [EnumMember(Value = "CSharp")]
    CSharp,

    /// <summary>
    /// TypeScript
    /// </summary>
    [EnumMember(Value = "TypeScript")]
    TypeScript,
  }
}
