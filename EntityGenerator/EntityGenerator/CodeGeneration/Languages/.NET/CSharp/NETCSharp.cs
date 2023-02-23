using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : NETLanguageBase
  {
    protected override StringBuilder BuildClass(string className, string baseClass, bool isStatic, bool isPartial, bool isAbstract, AccessType accessModifier)
    {
      throw new NotImplementedException();
    }

    protected override StringBuilder BuildTraceLogCall(string message, string paramsStr, bool async)
    {
      throw new NotImplementedException();
    }
  }
}
