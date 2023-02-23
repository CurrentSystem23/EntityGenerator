using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces
{
  internal interface IFileWriterService : IWriterService
  {
    public void WriteToFile(string data);
  }
}
