using EntityGenerator.CodeGeneration.Interfaces.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public partial class NETCSharp : IMichaTestGenerator
  {
    public void TestOutput()
    {
      Console.WriteLine("Micha Test is true");
    }
  }
}
