using System.Runtime.Serialization;

namespace EntityGenerator.Profile.DataTransferObject.Enums
{
  /// <summary>
  /// Types <see cref="DatabaseTypes"/> of supported databases
  /// </summary>
  [DataContract]
  public enum DatabaseTypes
  {
    /// <summary>
    /// Microsoft SQL Server
    /// </summary>
    [EnumMember(Value = "MicrosoftSqlServer")]
    MicrosoftSqlServer,

    /// <summary>
    /// Oracle
    /// </summary>
    [EnumMember(Value = "Oracle")]
    Oracle,
  }
}
