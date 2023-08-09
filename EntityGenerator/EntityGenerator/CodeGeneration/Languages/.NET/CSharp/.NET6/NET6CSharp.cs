using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp.NET_6
{
  public partial class NET6CSharp : NETCSharp
  {
    public NET6CSharp(StringBuilder sb, ProfileDto profile, List<DatabaseLanguageBase> databaseLanguages = null) : base(sb, profile, databaseLanguages) { }
  }
}
